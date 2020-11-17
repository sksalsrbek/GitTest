using System.Windows;
using System.Windows.Input;

namespace DPT_WPF
{
    /// <summary>
    /// closeWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class closeWindow : Window
    {
        HMIM270 _hmimetal270;
        //string whatmachine = "metal135";

        public closeWindow(HMIM270 hmimetal270)
        {
            InitializeComponent();
            //whatmachine = "metal270";
            _hmimetal270 = hmimetal270;
        }

        private void btnNo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (whatmachine == "metal270")
            //{
            //    _hmimetal270.darkbackground.Visibility = Visibility.Collapsed;
            //    this.Close();
            //}
            //else
            //{

            //}

            //this.Close();

            _hmimetal270.darkbackground.Visibility = Visibility.Collapsed;
            this.Close();
        }

        private void btnYes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (whatmachine == "metal270")
            //{
            //    _hmimetal270.darkbackground.Visibility = Visibility.Collapsed;
            //    this.Close();
            //    _hmimetal270.Close();
            //}
            //else
            //{

            //}

            _hmimetal270.darkbackground.Visibility = Visibility.Collapsed;
            this.Close();
            _hmimetal270.Close();
        }

        private void btnCom_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (whatmachine == "metal270")
            //{
            //    System.Diagnostics.Process.Start("shutdown.exe", "-s -f -t 00");
            //}
            System.Diagnostics.Process.Start("shutdown.exe", "-s -f -t 00");
        }
    }
}
