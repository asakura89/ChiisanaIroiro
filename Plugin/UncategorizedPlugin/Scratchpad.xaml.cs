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
}
