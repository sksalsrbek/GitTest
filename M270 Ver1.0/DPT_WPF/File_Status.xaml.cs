using System.Windows;
using System.Windows.Input;

namespace DPT_WPF
{
    /// <summary>
    /// File_Status.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class File_Status : Window
    {
        public File_Status()
        {
            //InitializeComponent();
        }
       
        public File_Status(string fileName, string fileLog, string strFinsh)
        {
            InitializeComponent();
            dFileName.Content = fileName;
            if (fileLog != "")
            {
                fileLog = fileLog.Substring(0, fileLog.Length - 1);
                txtLog.Text = fileLog;
            }
            
            dFileFinsh.Content = strFinsh;
            
            this.SizeToContent = SizeToContent.Height;
        }
        public void initalSetting(string fileName, string fileLog, string strFinsh)
        {
            InitializeComponent();
            dFileName.Content = fileName;
            if (fileLog != "")
            {
                fileLog = fileLog.Substring(0, fileLog.Length - 1);
                txtLog.Text = fileLog;
            }
            
            dFileFinsh.Content = strFinsh;

            this.SizeToContent = SizeToContent.Height;
        }
        
        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
