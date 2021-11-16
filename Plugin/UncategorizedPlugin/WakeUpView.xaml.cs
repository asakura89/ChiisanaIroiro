using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace UncategorizedPlugin {
    public partial class WakeUpView : UserControl, IViewablePlugin {
        public String ComponentName => "Wakeup your machine";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public WakeUpView() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ClipboardButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Content = "▷";
            CommonView.ProcessButtonAccesssor.Click += ProcessButton_Click;
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(Byte bVk, Byte bScan, UInt32 dwFlags, UInt32 dwExtraInfo);

        CancellationTokenSource ctSource;

        Boolean active = false;

        async void ProcessButton_Click(Object sender, RoutedEventArgs e) {
            active = !active;
            if (active) {
                CommonView.ProcessButtonAccesssor.Content = "❚❚";

                ctSource = new CancellationTokenSource();
                var taskSch = TaskScheduler.FromCurrentSynchronizationContext();
                await Task
                    .Factory
                    .StartNew(async () => {
                        ctSource.Token.ThrowIfCancellationRequested();
                        while (!ctSource.Token.IsCancellationRequested) {
                            await Task.Factory.StartNew(() => CommonView.Output += $"Pressing keeey at {DateTime.Now.ToString("hh:mm")}", CancellationToken.None, TaskCreationOptions.None, taskSch);

                            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=netframework-4.6.1
                            // https://stackoverflow.com/questions/1645815/how-can-i-programmatically-generate-keypress-events-in-c
                            // https://docs.microsoft.com/id-id/windows/win32/inputdev/virtual-key-codes
                            Int32 VK_F13 = 0x7C;
                            UInt32 KEYEVENTF_EXTENDEDKEY = 0x0001;
                            keybd_event((Byte) VK_F13, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);

                            await Task.Factory.StartNew(() => CommonView.Output += "\r\nPressed", CancellationToken.None, TaskCreationOptions.None, taskSch);
                            await Task.Factory.StartNew(() => CommonView.Output += "\r\nOk cooldown a bit\r\n\r\n", CancellationToken.None, TaskCreationOptions.None, taskSch);

                            // should implement timer that can be cancelled by cancellationtoken ut I'm to lazy
                            Thread.Sleep(1000 * 60 * 5);
                        }

                        await Task.Factory.StartNew(() => {
                            CommonView.Output += "\r\nStopped\r\n\r\n";
                            CommonView.ProcessButtonAccesssor.Content = "▷";
                        }, CancellationToken.None, TaskCreationOptions.None, taskSch);
                    }, ctSource.Token);
            }
            else
                ctSource.Cancel();
        }
    }
}
