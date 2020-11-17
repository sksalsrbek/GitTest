using Kepware.ClientAce.OpcDaClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DPT_WPF
{
    /// <summary>
    /// speedWindows.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class speedWindows : Window
    {
        DaServerMgt dsm;
        DefineValue d;

        public speedWindows()
        {
            InitializeComponent();
        }

        public void initalSetting(DefineValue dv, DaServerMgt daservermgt, int cn)
        {
            InitializeComponent();
            dsm = daservermgt;

            if (cn == 1)
            {
                tbTile.Text = "BUILD SPEED";
            }
            else if (cn == 2)
            {
                tbTile.Text = "RECOTOR";
            }
            else if (cn == 3)
            {
                tbTile.Text = "FRONT";
            }
            else if (cn == 4)
            {
                tbTile.Text = "REAR";
            }
            else if (cn == 5)
            {
                tbTile.Text = "SUPPLY";
            }

            else
            {

            }

            this.DataContext = dv;
            d = dv;
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
