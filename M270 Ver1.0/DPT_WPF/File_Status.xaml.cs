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
            //dFileLog.Content = fileLog;
            //ColumnDefinition gridCol1 = new ColumnDefinition();
            //ColumnDefinition gridCol2 = new ColumnDefinition();
            //gridCol1.Width = new GridLength(150);
            //gridCol2.Width = new GridLength(450);

            //dynamicGrid.ColumnDefinitions.Add(gridCol1);
            //dynamicGrid.ColumnDefinitions.Add(gridCol2);

            //string[] stringArry = fileLog.Split(new string[] { "\n" }, StringSplitOptions.None);

            //for (int i = 0; i < stringArry.Length - 1; i++)
            //{
            //    RowDefinition gridRow = new RowDefinition();
            //    gridRow.Height = new GridLength(35);
            //    dynamicGrid.RowDefinitions.Add(gridRow);
            //    string[] strLine = stringArry[i].Split(new string[] { "] " }, StringSplitOptions.None);

            //    Label LeftLabel = new Label();
            //    Label RightLabel = new Label();
            //    LeftLabel.Content = strLine[0] + "]";
            //    RightLabel.Content = strLine[1];
            //    LeftLabel.FontSize = 14;
            //    LeftLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));
            //    RightLabel.FontSize = 14;
            //    Grid.SetRow(LeftLabel, i);
            //    Grid.SetColumn(LeftLabel, 0);
            //    Grid.SetRow(RightLabel, i);
            //    Grid.SetColumn(RightLabel, 1);
            //    dynamicGrid.Children.Add(LeftLabel);
            //    dynamicGrid.Children.Add(RightLabel);
            //}

            /*
            InitializeComponent();
            dFileName.Content = fileName;
            if (fileLog != "")
            {
                fileLog = fileLog.Substring(0, fileLog.Length - 1);
                txtLog.Text = fileLog;
            }
            
            dFileFinsh.Content = strFinsh;
            
            this.SizeToContent = SizeToContent.Height;
            */
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
