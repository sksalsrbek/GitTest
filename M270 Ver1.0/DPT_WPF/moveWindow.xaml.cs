using Kepware.ClientAce.OpcDaClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DPT_WPF
{
    /// <summary>
    /// moveWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class moveWindow : Window
    {


        private int checkNumber = 1;
        DaServerMgt dsm;

        DefineValue d;
        public moveWindow()
        {
            
        }
        public void initalSetting(DefineValue dv, DaServerMgt daservermgt, int cn)
        {
            InitializeComponent();
            checkNumber = cn;
            dsm = daservermgt;

            string tempPostion = "";


            if (checkNumber == 1)
            {
                btnUp.Visibility = Visibility.Visible;
                btnDown.Visibility = Visibility.Visible;
                btnUpRCT.Visibility = Visibility.Collapsed;
                btnDownRCT.Visibility = Visibility.Collapsed;

                tempPostion = dv.DblMotor1Distance;
                tbTile.Text = "BUILD ROOM";
            }
            else if (checkNumber == 2)
            {
                btnUp.Visibility = Visibility.Collapsed;
                btnDown.Visibility = Visibility.Collapsed;
                btnUpRCT.Visibility = Visibility.Visible;
                btnDownRCT.Visibility = Visibility.Visible;

                tempPostion = dv.DblMotor2Distance;
                tbTile.Text = "RECOATER";
            }
            else if (checkNumber == 3)
            {
                btnUp.Visibility = Visibility.Visible;
                btnDown.Visibility = Visibility.Visible;
                btnUpRCT.Visibility = Visibility.Collapsed;
                btnDownRCT.Visibility = Visibility.Collapsed;

                tempPostion = dv.DblMotor3Distance;
                tbTile.Text = "FRONT";
            }
            else if (checkNumber == 4)
            {
                btnUp.Visibility = Visibility.Visible;
                btnDown.Visibility = Visibility.Visible;
                btnUpRCT.Visibility = Visibility.Collapsed;
                btnDownRCT.Visibility = Visibility.Collapsed;

                tempPostion = dv.DblMotor4Distance;
                tbTile.Text = "REAR";
            }
            else if (checkNumber == 5)
            {
                btnUp.Visibility = Visibility.Visible;
                btnDown.Visibility = Visibility.Visible;
                btnUpRCT.Visibility = Visibility.Collapsed;
                btnDownRCT.Visibility = Visibility.Collapsed;

                tempPostion = dv.DblMotor5Distance;
                tbTile.Text = "SUPPLY";
            }
            else
            {

            }

            this.DataContext = dv;
            d = dv;
        }



        private void da_Completed(object sender, EventArgs e)
        {
            DoubleAnimation da1 = new DoubleAnimation();
            da1.From = 100;
            da1.To = 370;
            da1.AccelerationRatio = 0.5;
            double tempTime1 = (25 / 10) + 2.5;
            da1.Duration = new Duration(TimeSpan.FromSeconds(tempTime1));
            //Rightbed3.BeginAnimation(Canvas.LeftProperty, da1);
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnReset_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            d.Number1 = 0;
            d.Number2 = 0;
            d.Number3 = 5;
            d.Number4 = 0;
            d.Number5 = 0;
            d.Number6 = 0;
            
            if (checkNumber == 1)
            {
                d.DblMotor1Distance = "005.000";    
            }
            if (checkNumber == 2)
            {
                d.DblMotor2Distance = "005.000";
            }
            if (checkNumber == 3)
            {
                d.DblMotor3Distance = "005.000";
            }
            if (checkNumber == 4)
            {
                d.DblMotor4Distance = "005.000";
            }
            if (checkNumber == 5)
            {
                d.DblMotor5Distance = "005.000";
            }
            
        }
    }
}
