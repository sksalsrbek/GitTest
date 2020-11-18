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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DPT_WPF.ucCommon
{
    /// <summary>
    /// ucCommonPump.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonM270Pump : UserControl
    {
        DefineValue d;
        DaServerMgt dsm;
        HMIM270 hMIM270;

        public ucCommonM270Pump()
        {
            InitializeComponent();
        }

        public ucCommonM270Pump(DefineValue dv, DaServerMgt daservermgt)
        {
            InitializeComponent();
            dsm = daservermgt;
            this.DataContext = dv;
            
            d = dv;

            
        }
        
        
       
        private static Action EmptyDelegate = delegate () { };
        public static void Refresh(UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Normal, EmptyDelegate);
        }

        


    }
}
