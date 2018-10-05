using System;
using System.Linq;
using System.Windows.Forms;
using Ayumi.Core;
using Ayumi.Data;
using Ayumi.Desktop;
using ChiisanaIroiro.Presenter;
using ChiisanaIroiro.Service;
using ChiisanaIroiro.Utility;
using ChiisanaIroiro.ViewModel;

namespace ChiisanaIroiro {
    public partial class MainForm : Form, IMainFormViewModel {
        const String ErrorSessName = "mainformview.session.errormessage";

        readonly IMainFormPresenter presenter;

        public MainForm() {
            InitializeComponent();

            IMainFormService service = ObjectRegistry.GetRegisteredObject<IMainFormService>();
            presenter = ObjectRegistry.GetRegisteredObject<IMainFormPresenter>(this, service);
            presenter.Initialize();
        }

        public ICommonList ViewActions => new DesktopDropdownList(cmbAvailableFeatures);
        public String ViewName => "Main Form";
        public String ViewDesc => "";

        public UserControl ActiveView {
            get { return pnlMain.Controls.Cast<UserControl>().SingleOrDefault(); }
            set {
                UserControl control = value;
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(control);
            }
        }

        public String SearchedFeature {
            get { return cmbAvailableFeatures.Text; }
            set { cmbAvailableFeatures.Text = value; }
        }

        public String ErrorMessage {
            get { return Convert.ToString(SessionStore.Get(ErrorSessName)); }
            set {
                SessionStore.Add(ErrorSessName, value);
                MessageBox.Show(this, value);
            }
        }

        void cmbFeature_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                presenter.FeatureSelectedAction();
                presenter.CaptureAction(ViewName, "Feature selected.");
            }
            catch (Exception ex) {
                presenter.CaptureException(ex);
            }
        }

        void cmbFeature_KeyPress(object sender, KeyPressEventArgs e) {
            try {
                presenter.FeatureSearchedAction();
                presenter.CaptureAction(ViewName, "Feature searched.");
            }
            catch (Exception ex) {
                presenter.CaptureException(ex);
            }
        }
    }
}