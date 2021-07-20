using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Eksmaru;
using Puru.Wpf;

namespace UncategorizedPlugin {
    public partial class Scratchpad : UserControl, IViewablePlugin {
        public String ComponentName => "Scratchpad";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        public Scratchpad() {
            InitializeComponent();
            CommonView.ConfigButtonAccesssor.Visibility = Visibility.Collapsed;
            CommonView.ProcessButtonAccesssor.Visibility = Visibility.Collapsed;

            InitializeInternalComponent();
        }

        String GetAssemblyDirectory() {
            String[] paths = Directory.GetFiles(
                AppDomain.CurrentDomain.BaseDirectory,
                $"{GetType().Module}", SearchOption.AllDirectories);

            if (paths.Length <= 0)
                throw new InvalidOperationException("Assembly directory not valid.");

            return Path.GetDirectoryName(paths[0]);
        }

        void ProcessItem(XmlNode item) {

        }

        void LoadFormSchema() {

            String filePath = Path.Combine(GetAssemblyDirectory(), "form.config");
            if (File.Exists(filePath)) {
                XmlDocument config = XmlExt.LoadFromPath(filePath);
                var form = config
                    .SelectNodes("configuration/app")
                    .Cast<XmlNode>()
                    .Select(ProcessItem)
                    .ToList();
            }
        }

        void InitializeInternalComponent() {

        }
    }

    public class TwelveColsGrid : Grid {
        public static readonly DependencyProperty ColProperty =
                DependencyProperty.RegisterAttached("Col", typeof(Int32), typeof(TwelveColsGrid),
                    new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure));

        Int32 GetColProperty(UIElement element) {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return (Int32) element.GetValue(ColProperty);
        }

        void SetColProperty(UIElement element, Int32 value) {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            element.SetValue(ColProperty, value);
        }

        public TwelveColsGrid() {
            for (Int32 idx = 0; idx < 12; idx++)
                ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
    }

    //public class TwelveColsGrid : Panel {
    //    public static readonly DependencyProperty ColProperty =
    //        DependencyProperty.RegisterAttached("Col", typeof(Int32), typeof(TwelveColsGrid),
    //            new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure));
    //    //public Int32 Columns {
    //    //    get => (Int32) GetValue(ColumnsProperty);
    //    //    set => SetValue(ColumnsProperty, value);
    //    //}

    //    Int32 GetColProperty(UIElement element) {
    //        if (element == null)
    //            throw new ArgumentNullException(nameof(element));

    //        return (Int32) element.GetValue(ColProperty);
    //    }

    //    void SetColProperty(UIElement element, Int32 value) {
    //        if (element == null)
    //            throw new ArgumentNullException(nameof(element));

    //        element.SetValue(ColProperty, value);
    //    }

    //    public static readonly DependencyProperty ColumnSpacingProperty =
    //        DependencyProperty.Register("ColumnSpacing", typeof(Double), typeof(TwelveColsGrid),
    //            new FrameworkPropertyMetadata(15.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
    //    public Double ColumnSpacing {
    //        get => (Double) GetValue(ColumnSpacingProperty);
    //        set => SetValue(ColumnSpacingProperty, value);
    //    }

    //    public static readonly DependencyProperty RowSpacingProperty =
    //        DependencyProperty.Register("RowSpacing", typeof(Double), typeof(TwelveColsGrid),
    //            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
    //    public Double RowSpacing {
    //        get => (Double) GetValue(RowSpacingProperty);
    //        set => SetValue(RowSpacingProperty, value);
    //    }

    //    //public static readonly DependencyProperty LabelControlSpacingProperty =
    //    //    DependencyProperty.Register("LabelControlSpacing", typeof(Double), typeof(FormPanel),
    //    //        new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
    //    //public Double LabelControlSpacing {
    //    //    get => (Double) GetValue(LabelControlSpacingProperty);
    //    //    set => SetValue(LabelControlSpacingProperty, value);
    //    //}

    //    //public static readonly DependencyProperty LabelSizeProperty =
    //    //    DependencyProperty.Register("LabelSize", typeof(Size), typeof(FormPanel));
    //    //public Size LabelSize {
    //    //    get => (Size) GetValue(LabelSizeProperty);
    //    //    set => SetValue(LabelSizeProperty, value);
    //    //}

    //    //public static readonly DependencyProperty ControlSizeProperty =
    //    //    DependencyProperty.Register("ControlSize", typeof(Size), typeof(FormPanel));
    //    //public Size ControlSize {
    //    //    get => (Size) GetValue(ControlSizeProperty);
    //    //    set => SetValue(ControlSizeProperty, value);
    //    //}

    //    //public IFormPanelCoordinator Coordinator { get; set; }

    //    Size 

    //    protected override Size MeasureOverride(Size availableSize) {
    //        var parent = Parent as UIElement;

    //        Double desiredWidth = parent == null ? 0 : parent.DesiredSize.Width - 10;
    //        Double desiredHeight = parent == null ? 0 : parent.DesiredSize.Height - 10;

    //        return new Size(desiredWidth, desiredHeight);

    //        //Double labelMaxWidth = 0;
    //        //Double labelMaxHeight = 0;
    //        //Double controlMaxWidth = 0;
    //        //Double controlMaxHeight = 0;
    //        //for (Int32 i = 0; i < Children.Count - 1; i += 2) {
    //        //    Children[i].Measure(availableSize);
    //        //    Children[i + 1].Measure(availableSize);
    //        //    labelMaxWidth = Math.Max(labelMaxWidth, Children[i].DesiredSize.Width);
    //        //    labelMaxHeight = Math.Max(labelMaxHeight, Children[i].DesiredSize.Height);
    //        //    controlMaxWidth = Math.Max(controlMaxWidth, Children[i + 1].DesiredSize.Width);
    //        //    controlMaxHeight = Math.Max(controlMaxHeight, Children[i + 1].DesiredSize.Height);
    //        //}

    //        //var oldLabelSize = LabelSize;
    //        //var oldControlSize = ControlSize;
    //        //var newLabelSize = new Size(labelMaxWidth, labelMaxHeight);
    //        //var newControlSize = new Size(controlMaxWidth, controlMaxHeight);
    //        //LabelSize = newLabelSize;
    //        //ControlSize = newControlSize;

    //        //if (Coordinator != null &&
    //        //    (newLabelSize != oldLabelSize || newControlSize != oldControlSize)) {
    //        //    Coordinator.ControlOrLabelSizeChanged(this);
    //        //}

    //        //return new Size(
    //        //    Columns * (LabelSize.Width + ControlSize.Width + LabelControlSpacing) + (Columns - 1) * ColumnSpacing,
    //        //    ((Children.Count / 2) / Columns) * Math.Max(LabelSize.Height, ControlSize.Height) + (((Children.Count / 2) / Columns) - 1) * RowSpacing);
    //    }

    //    protected override Size ArrangeOverride(Size finalSize) {
    //        for (Int32 idx = 0; idx < Children.Count - 1; idx++) {
                
    //        }

    //        Double controlWidth = (finalSize.Width - (Columns - 1) * ColumnSpacing - Columns * (LabelSize.Width + LabelControlSpacing)) / Columns;
    //        Double rowHeight = Math.Max(LabelSize.Height, ControlSize.Height) + RowSpacing;
    //        Double columnWidth = LabelSize.Width + LabelControlSpacing + controlWidth + ColumnSpacing;
    //        for (Int32 i = 0; i < Children.Count - 1; i += 2) {
    //            var labelRect = new Rect(
    //                columnWidth * ((i / 2) % Columns), rowHeight * ((i / 2) / Columns),
    //                LabelSize.Width, rowHeight - RowSpacing);
    //            Children[i].Arrange(
    //                new Rect(
    //                    labelRect.Left,
    //                    labelRect.Top + (labelRect.Height - Children[i].DesiredSize.Height) / 2,
    //                    Children[i].DesiredSize.Width, Children[i].DesiredSize.Height));
    //            Children[i + 1].Arrange(new Rect(
    //                (columnWidth * ((i / 2) % Columns)) + LabelSize.Width + LabelControlSpacing, rowHeight * ((i / 2) / Columns),
    //                controlWidth, rowHeight - RowSpacing));
    //        }
    //        return new Size(finalSize.Width, rowHeight * ((Children.Count / 2) / Columns + 1));
    //    }
    //}

    //public interface IFormPanelCoordinator {
    //    void ControlOrLabelSizeChanged(FormPanel sender);
    //}
}
