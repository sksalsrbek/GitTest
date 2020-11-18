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
    /// GasWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GasWindow : Window
    {

        
        private int checkNumber = 1;
        DaServerMgt dsm;

        DefineValue d;
        public GasWindow()
        {
            
        }
        
        private void gBtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        #region temp
        
        public void initalSetting(DefineValue dv, DaServerMgt daservermgt, int cn)
        {
            InitializeComponent();
            checkNumber = cn;
            dsm = daservermgt;

            string tempPostion = "";


            //if (checkNumber == 1)
            //{
            //    btnUp.Visibility = Visibility.Visible;
            //    btnDown.Visibility = Visibility.Visible;

            //    tempPostion = dv.DblMotor1Distance;
            //    tbTile.Text = "BUILD ROOM";
            //}
            //else if (checkNumber == 2)
            //{
            //    btnUp.Visibility = Visibility.Visible;
            //    btnDown.Visibility = Visibility.Visible;

            //    tempPostion = dv.DblMotor2Distance;
            //    tbTile.Text = "POWDER ROOM";
            //}
            //else if (checkNumber == 3)
            //{
            //    btnUp.Visibility = Visibility.Collapsed;
            //    btnDown.Visibility = Visibility.Collapsed;

            //    tempPostion = dv.DblMotor3Distance;
            //    tbTile.Text = "RECOTOR";
            //}
            //else if (checkNumber == 4)
            //{
            //    this.Close();
            //}
            //else
            //{

            //}

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
        
        #endregion
    }
}
