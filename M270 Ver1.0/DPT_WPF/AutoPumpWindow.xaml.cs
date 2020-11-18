using Kepware.ClientAce.OpcDaClient;
using System.Windows;
using System.Windows.Input;

namespace DPT_WPF
{
    /// <summary>
    /// speedWindows.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AutoPumpWindow : Window
    {
        DaServerMgt dsm;
        DefineValue d;

        

        public AutoPumpWindow()
        {
            InitializeComponent();
        }

        public void initalSetting(DefineValue dv, DaServerMgt daservermgt)
        {
            InitializeComponent();
            dsm = daservermgt;

            this.DataContext = dv;
            d = dv;

            string Pump1_main = "0";
            string Pump1_remain = "0";

            if(Pump1_main != d.OPCItemValueTextBoxes[88])
            {
                d.Pumpnum1 = d.OPCItemValueTextBoxes[88];
            }

            if (Pump1_remain != d.OPCItemValueTextBoxes[89])
            {
                d.Pumpnum2 = d.OPCItemValueTextBoxes[89];
            }
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
