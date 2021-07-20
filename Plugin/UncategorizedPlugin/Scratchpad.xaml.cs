using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Puru.Wpf;

namespace UncategorizedPlugin {
    public abstract class TabViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected String title;
        public String Title {
            get => title;
            set {
                title = value;
                OnPropertyChanged();
            }
        }

        protected UserControl content = null;
        public UserControl Content {
            get => content;
            set {
                content = value;
                OnPropertyChanged();
            }
        }
    }

    public class TabDataContext : TabViewModel {
        Boolean isNewTabButton = false;
        public Boolean IsNewTabButton {
            get => isNewTabButton;
            set {
                isNewTabButton = value;
                OnPropertyChanged();
            }
        }
    }

    public partial class Scratchpad : UserControl, IViewablePlugin {
        public String ComponentName => "Scratchpad";

        public String ComponentDesc => String.Empty;

        public UserControl View => this;

        public Object Process(Object processArgs) => throw new InvalidOperationException("Not used.");

        Int32 TabIndex = 1;
        readonly ObservableCollection<TabDataContext> TabDataContextList = new ObservableCollection<TabDataContext>();

        public Scratchpad() {
            InitializeComponent();
            InitializeInternalComponent();
        }

        void InitializeInternalComponent() {
            var initialTab = new TabDataContext {
                Title = $"Tab {TabIndex}",
                Content = new DummyUserControl()
            };
            TabDataContextList.Add(initialTab);
            GenerateNewTabButton();

            MainTabControl.ItemsSource = TabDataContextList;
            MainTabControl.SelectionChanged += MainTabControlOnSelectionChanged;
        }

        void MainTabControlOnSelectionChanged(Object sender, SelectionChangedEventArgs e) {
            if (e.Source is TabControl) {
                Int32 currentIdx = MainTabControl.SelectedIndex;
                Int32 lastIdx = TabDataContextList.Count - 1;
                if (currentIdx != 0 && currentIdx == lastIdx) {
                    UpdateCurrentTab(TabDataContextList.Last());
                    GenerateNewTabButton();
                }
            }
        }

        void UpdateCurrentTab(TabDataContext tab) {
            TabIndex++;
            tab.Title = $"Tab {TabIndex}";
            tab.IsNewTabButton = false;
            tab.Content = new DummyUserControl();
        }

        void GenerateNewTabButton() =>
            TabDataContextList.Add(new TabDataContext {
                Title = "➕",
                IsNewTabButton = true
            });

        void CloseTabButtonOnClick(Object sender, RoutedEventArgs e) {
            var closeTabButton = sender as Button;
            if (closeTabButton != null) {
                var dataCtx = closeTabButton.DataContext as TabDataContext;
                if (TabDataContextList.Count > 2) {
                    Int32 currentIdx = TabDataContextList.IndexOf(dataCtx);
                    Int32 lastNonNewTabIdx = TabDataContextList.Count -2;
                    if (currentIdx == lastNonNewTabIdx)
                        MainTabControl.SelectedIndex--;

                    TabDataContextList.RemoveAt(currentIdx);
                }
            }
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
