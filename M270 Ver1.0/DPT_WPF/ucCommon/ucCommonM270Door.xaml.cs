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

namespace DPT_WPF.ucCommon
{
    /// <summary>
    /// ucCommonMgDoor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonM270Door : UserControl
    {
        DefineValue d;
        DaServerMgt daservermgt;

        public ucCommonM270Door()
        {
            InitializeComponent();
        }
        
        public ucCommonM270Door(DefineValue dv, DaServerMgt dsm)
        {
            InitializeComponent();

            daservermgt = dsm;
            //dv.DoorChamberLockCommand = new DelegateCommand(this.DoorChamberLock);
            //dv.DoorChamberUnLockCommand = new DelegateCommand(this.DoorUnLock);
            d = dv;
            
        }

    }
}
