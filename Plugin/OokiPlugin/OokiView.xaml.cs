using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using Eksmaru;
using Plugin.Common;
using Plugin.Dialog;
using Puru.Wpf;

namespace OokiPlugin {
    public partial class OokiView : UserControl, IViewablePlugin {
        public String ComponentName => "Window Re-sizer";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        IList<Profile> profiles = new List<Profile> {
            new Profile { Name = "Default Window", Size = new Size(1265, 695) },
            new Profile { Name = "Default Mini-window", Size = new Size(850, 340) },
            new Profile { Name = "Default Bar", Size = new Size(235, 26) }
        };

        public OokiView() {
            //Sizer.SetAppDPIAware();
            InitializeComponent();

            LoadProfiles();
            WindowSizeDropdownList.SelectedItem = profiles.First();
            IsVisibleChanged += OnIsVisibleChanged;
        }

        void OnIsVisibleChanged(Object sender, DependencyPropertyChangedEventArgs e) =>
            WindowBorder.BorderBrush = new SolidColorBrush(ColorGenerator.GetColor());

        void AddButton_Click(Object sender, RoutedEventArgs e) {

        }

        String GetAssemblyDirectory() {
            String[] paths = Directory.GetFiles(
                AppDomain.CurrentDomain.BaseDirectory,
                $"{GetType().Module}", SearchOption.AllDirectories);

            if (paths.Length <= 0)
                throw new InvalidOperationException("Assembly directory not valid.");

            return Path.GetDirectoryName(paths[0]);
        }

        void LoadProfiles() {
            String filePath =  Path.Combine(GetAssemblyDirectory(), "ooki.config");
            if (File.Exists(filePath)) {
                XmlDocument config = XmlExt.LoadFromPath(filePath);
                profiles = config
                    .SelectNodes("Configuration/Item")
                    .Cast<XmlNode>()
                    .Select(item => {
                        String[] splittedSize = item.GetAttributeValue("Size").Split(',');
                        return new Profile {
                            Name = item.GetAttributeValue("Name"),
                            Size = new Size(Convert.ToInt32(splittedSize[0].Trim()), Convert.ToInt32(splittedSize[1].Trim()))
                        };
                    })
                    .ToList();
            }

            WindowSizeDropdownList.ItemsSource = profiles;
        }

        void ResizeButton_Click(Object sender, RoutedEventArgs e) {
            try {
                IList<ActiveWindow> allWindows = ActiveWindowCollector.GetActiveWindows();
                IList<ActiveWindow> selectedWindowList = allWindows
                    .Where(wnd => !new[] { "start", "chiisanairoiro" }.Contains(wnd.Name.ToLowerInvariant()))
                    .ToList();

                foreach (ActiveWindow item in selectedWindowList) {
                    var profile = (Profile) WindowSizeDropdownList.SelectedItem;
                    Sizer.Set(item.Id, (Int32) profile.Size.Width, (Int32) profile.Size.Height);
                    // ^ this function will have DPI scaling awareness when called from Win Forms but not from WPF :(
                }
            }
            catch (Exception ex) {
                ex.ShowInTaskDialog();
            }
        }

        public class Profile {
            public String Name { get; set; }
            public Size Size { get; set; }

            public override String ToString() => $"{Name} [{Size.Width}×{Size.Height}]";
        }

        public class ActiveWindow {
            public IntPtr Id { get; set; }
            public String Name { get; set; }
        }

        public static class Sizer {
            public static void Set(IntPtr hWnd, Int32 w, Int32 h) {
                SetSizeAsScaled(ref w, ref h);
                var rect = new Rect();
                if (GetWindowRect(hWnd, ref rect))
                    MoveWindow(hWnd, rect.X, rect.Y, w, h, true);
            }

            static void SetSizeAsScaled(ref Int32 w, ref Int32 h) {
                UInt32 dpix, dpiy;
                GetDpi(0, 0, MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out dpix, out dpiy);
                Single scale = GetScalingFactor((Int32) dpix);
                w = (Int32) Math.Round(w * scale);
                h = (Int32) Math.Round(h * scale);
            }

            static Single GetScalingFactor(Int32 dpi) {
                const Int32 Default = 96;
                return (Single) dpi / Default;
            }

            static void GetDpi(Int32 x, Int32 y, MONITOR_DPI_TYPE dpiType, out UInt32 dpiX, out UInt32 dpiY) {
                var point = new System.Drawing.Point(x + 1, y + 1);
                IntPtr mon = MonitorFromPoint(point, MONITOR_POINT.MONITOR_DEFAULTTONEAREST);
                GetDpiForMonitor(mon, dpiType, out dpiX, out dpiY);
            }

            //public static void SetAppDPIAware() {
            //    //if (Environment.OSVersion.Version.Major >= 6)
            //    //    SetProcessDPIAware();
            //    if (Environment.OSVersion.Version.Major >= 6)
            //        SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
            //}

            [StructLayout(LayoutKind.Sequential)]
            public struct Rect {
                public Int32 X;
                public Int32 Y;
                public Int32 Width;
                public Int32 Height;
            }

            [DllImport("user32.dll", SetLastError = true)]
            static extern Boolean GetWindowRect(IntPtr hWnd, ref Rect rect);

            [DllImport("user32.dll", SetLastError = true)]
            static extern Boolean MoveWindow(IntPtr hWnd, Int32 x, Int32 y, Int32 width, Int32 height, Boolean repaint);

            //[DllImport("user32.dll", SetLastError = true)]
            //static extern Boolean SetProcessDPIAware();

            enum MONITOR_POINT {
                MONITOR_DEFAULTTONULL = 0,
                MONITOR_DEFAULTTOPRIMARY = 1,
                MONITOR_DEFAULTTONEAREST = 2
            }

            enum PROCESS_DPI_AWARENESS {
                PROCESS_DPI_UNAWARE = 0,
                PROCESS_SYSTEM_DPI_AWARE = 1,
                PROCESS_PER_MONITOR_DPI_AWARE = 2
            }

            //[DllImport("SHCore.dll", SetLastError = true)]
            //static extern Boolean SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

            enum MONITOR_DPI_TYPE {
                MDT_EFFECTIVE_DPI = 0,
                MDT_ANGULAR_DPI = 1,
                MDT_RAW_DPI = 2,
                MDT_DEFAULT = 3
            }

            [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr MonitorFromPoint([In]System.Drawing.Point pt, [In]MONITOR_POINT dwFlags);


            [DllImport("SHCore.dll", SetLastError = true)]
            static extern IntPtr GetDpiForMonitor([In]IntPtr hmonitor, [In]MONITOR_DPI_TYPE dpiType, [Out]out UInt32 dpiX, [Out]out UInt32 dpiY);
        }

        public static class ActiveWindowCollector {
            public static IList<ActiveWindow> GetActiveWindows() {
                IntPtr lShellWindow = GetShellWindow();
                var lWindows = new List<ActiveWindow>();

                EnumWindows(delegate (IntPtr hWnd, Int32 lParam) {
                    if (hWnd == lShellWindow)
                        return true;
                    if (!IsWindowVisible(hWnd))
                        return true;

                    Int32 lLength = GetWindowTextLength(hWnd);
                    if (lLength == 0)
                        return true;

                    var lBuilder = new StringBuilder(lLength);
                    GetWindowText(hWnd, lBuilder, lLength + 1);

                    lWindows.Add(new ActiveWindow { Id = hWnd, Name = lBuilder.ToString() });
                    return true;

                }, 0);

                return lWindows;
            }

            delegate Boolean EnumWindowsProc(IntPtr hWnd, Int32 lParam);

            [DllImport("user32.dll", SetLastError = true)]
            static extern Boolean EnumWindows(EnumWindowsProc enumFunc, Int32 lParam);

            [DllImport("user32.dll", SetLastError = true)]
            static extern Int32 GetWindowText(IntPtr hWnd, StringBuilder lpString, Int32 nMaxCount);

            [DllImport("user32.dll", SetLastError = true)]
            static extern Int32 GetWindowTextLength(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            static extern Boolean IsWindowVisible(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr GetShellWindow();
        }
    }
}
