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
    /// ucCommonGAS.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonGAS : UserControl
    {
        DefineValue d;
        DaServerMgt dsm;

        public ucCommonGAS()
        {
            InitializeComponent();
        }
        
        public ucCommonGAS(DefineValue dv, DaServerMgt daservermgt)
        {
            dsm = daservermgt;
            dv.SelArCommand = new DelegateCommand(this.SelAr);
            dv.SelN2Command = new DelegateCommand(this.SelN2);
            dv.GasSupplyCommand = new DelegateCommand(this.GasSupply);
            d = dv;
        }
        
        private void GasSupply()
        {
            if (d.OPCItemValueTextBoxes[29] == "False")
            {
                d.OPCItemWriteValueTextBoxes[18] = "True";
                d.opcWrite("OPCItemSyncWrite18", dsm);
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[18] = "False";
                d.opcWrite("OPCItemSyncWrite18", dsm);
            }
            
        }
        private void SelAr()
        {
            SelN2_OFF();
            d.OPCItemWriteValueTextBoxes[54] = "True";
            d.opcWrite("OPCItemSyncWrite54", dsm);//아르곤가스 주입
        }
        private void SelAr_OFF()
        {
            d.OPCItemWriteValueTextBoxes[54] = "False";
            d.opcWrite("OPCItemSyncWrite54", dsm);//아르곤가스 주입 최소
        }
        private void SelN2()
        {
            SelAr_OFF();
            d.OPCItemWriteValueTextBoxes[15] = "True";
            d.opcWrite("OPCItemSyncWrite15", dsm);//질소 주입
        }
        private void SelN2_OFF()
        {
            d.OPCItemWriteValueTextBoxes[15] = "False";
            d.opcWrite("OPCItemSyncWrite15", dsm);// 질소 주집입
        }
        
    }
}
