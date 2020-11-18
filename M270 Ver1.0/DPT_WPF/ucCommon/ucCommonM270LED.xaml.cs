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
    /// ucCommonMgLED.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonM270LED : UserControl
    {
        public ucCommonM270LED()
        {
            InitializeComponent();
        }

        DaServerMgt dsm;
        DefineValue d;

        public ucCommonM270LED(DefineValue dv, DaServerMgt daservermgt)
        {
            dsm = daservermgt;
            dv.LEDGloveCommand = new DelegateCommand(this.LEDGlove);
            dv.LEDChamberCommand = new DelegateCommand(this.LEDChamber);
            
            dv.LEDL1Command = new DelegateCommand(this.LEDLeft1);
            dv.LEDL2Command = new DelegateCommand(this.LEDLeft2);
            dv.LEDL3Command = new DelegateCommand(this.LEDLeft3);
            dv.LEDR1Command = new DelegateCommand(this.LEDRight1);
            dv.LEDR2Command = new DelegateCommand(this.LEDRight2);
            dv.LEDR3Command = new DelegateCommand(this.LEDRight3);
            d = dv;
        }

        private void fLEDControl(string led1, string led2, string led3)
        {
            d.OPCItemWriteValueTextBoxes[0] = led1;
            d.OPCItemWriteValueTextBoxes[1] = led2;
            d.OPCItemWriteValueTextBoxes[2] = led3;
            d.opcWrite("OPCItemSyncWrite0", dsm);
            d.opcWrite("OPCItemSyncWrite1", dsm);
            d.opcWrite("OPCItemSyncWrite2", dsm);
        }

        private void LEDChamber()
        {
            if (d.LedLeftCount == 0)
            {
                fLEDControl("1", "0", "0");
            }
            else if (d.LedLeftCount == 1)
            {
                fLEDControl("1", "1", "0");
            }
            else if (d.LedLeftCount == 2)
            {
                fLEDControl("1", "1", "1");
            }
            else if (d.LedLeftCount == 3)
            {
                fLEDControl("0", "0", "0");
            }
            else
            { 
                
            }
        }
        
        private void LEDGlove()
        {
            if (d.OPCItemValueTextBoxes[0] == "True")
            {
                d.OPCItemWriteValueTextBoxes[0] = "0";
                d.opcWrite("OPCItemSyncWrite0", dsm);
            }
            else if (d.OPCItemValueTextBoxes[0] == "False")
            {
                d.OPCItemWriteValueTextBoxes[0] = "1";
                d.opcWrite("OPCItemSyncWrite0", dsm);
            }
        }


        private void LEDLeft1()
        {
            if (d.OPCItemValueTextBoxes[0] == "True")
            {
                d.OPCItemWriteValueTextBoxes[0] = "0";
                d.opcWrite("OPCItemSyncWrite0", dsm);
            }
            else if (d.OPCItemValueTextBoxes[0] == "False")
            {
                d.OPCItemWriteValueTextBoxes[0] = "1";
                d.opcWrite("OPCItemSyncWrite0", dsm);
            }
        }
        private void LEDLeft2()
        {
            if (d.OPCItemValueTextBoxes[1] == "True")
            {
                d.OPCItemWriteValueTextBoxes[1] = "0";
                d.opcWrite("OPCItemSyncWrite1", dsm);
            }
            else if (d.OPCItemValueTextBoxes[1] == "False")
            {
                d.OPCItemWriteValueTextBoxes[1] = "1";
                d.opcWrite("OPCItemSyncWrite1", dsm);
            }
        }
        private void LEDLeft3()
        {
            if(d.OPCItemValueTextBoxes[0] == "True" && d.OPCItemValueTextBoxes[1] == "True")
            {
                d.OPCItemWriteValueTextBoxes[0] = "0";
                d.OPCItemWriteValueTextBoxes[1] = "0";
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[0] = "1";
                d.OPCItemWriteValueTextBoxes[1] = "1";
            }
            d.opcWrite("OPCItemSyncWrite0", dsm);
            d.opcWrite("OPCItemSyncWrite1", dsm);
        }
        private void LEDRight1()
        {
            if (d.OPCItemValueTextBoxes[2] == "True")
            {
                d.OPCItemWriteValueTextBoxes[2] = "0";
                d.opcWrite("OPCItemSyncWrite2", dsm);
            }
            else if (d.OPCItemValueTextBoxes[2] == "False")
            {
                d.OPCItemWriteValueTextBoxes[2] = "1";
                d.opcWrite("OPCItemSyncWrite2", dsm);
            }
        }
        private void LEDRight2()
        {
            if (d.OPCItemValueTextBoxes[3] == "True")
            {
                d.OPCItemWriteValueTextBoxes[3] = "0";
                d.opcWrite("OPCItemSyncWrite3", dsm);
            }
            else if (d.OPCItemValueTextBoxes[3] == "False")
            {
                d.OPCItemWriteValueTextBoxes[3] = "1";
                d.opcWrite("OPCItemSyncWrite3", dsm);
            }
        }
        private void LEDRight3()
        {
            if (d.OPCItemValueTextBoxes[2] == "True" && d.OPCItemValueTextBoxes[3] == "True")
            {
                d.OPCItemWriteValueTextBoxes[2] = "0";
                d.OPCItemWriteValueTextBoxes[3] = "0";
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[2] = "1";
                d.OPCItemWriteValueTextBoxes[3] = "1";
            }
            d.opcWrite("OPCItemSyncWrite2", dsm);
            d.opcWrite("OPCItemSyncWrite3", dsm);
        }
    }
}
