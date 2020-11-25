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
    /// ucCommonMotor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCommonM270Motor : UserControl
    {
        DefineValue d;
        DaServerMgt dsm;

        public ucCommonM270Motor()
        {
            InitializeComponent();
        }

        public ucCommonM270Motor(DefineValue dv,DaServerMgt daservermgt)
        {
            dsm = daservermgt;
            
            dv.Motor1ResetCommand = new DelegateCommand(this.Motor1Reset);
            dv.Motor2ResetCommand = new DelegateCommand(this.Motor2Reset);
            dv.Motor3ResetCommand = new DelegateCommand(this.Motor3Reset);
            dv.Motor4ResetCommand = new DelegateCommand(this.Motor4Reset);
            
            d = dv;
            //this.DataContext = dv;
        }



        #region METAL-MOTOR MOTOR RESET
        private void Motor5Reset()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Reset\",\"parameters\": {\"id\":\"Modbus Motion 5\"}}";//"{\"message\":\"Script.MotorSupplyReset\"}";
            d.opcWrite("OPCItemSyncWrite14", dsm);
        }
        private void Motor4Reset()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Reset\",\"parameters\": {\"id\":\"Modbus Motion 4\"}}";//"{\"message\":\"Script.MotorRearReset\"}";
            d.opcWrite("OPCItemSyncWrite14", dsm);
        }
        private void Motor3Reset()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Reset\",\"parameters\": {\"id\":\"Modbus Motion 3\"}}";//"{\"message\":\"Script.MotorFrontReset\"}";
            d.opcWrite("OPCItemSyncWrite14", dsm);
        }

        private void Motor2Reset()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Reset\",\"parameters\": {\"id\":\"Modbus Motion 2\"}}"; //"{\"message\":\"Script.MotorRecoaterReset\"}";
            d.opcWrite("OPCItemSyncWrite14", dsm);
        }

        private void Motor1Reset()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Reset\",\"parameters\": {\"id\":\"Modbus Motion 1\"}}"; //"{\"message\":\"Script.MotorBuildReset\"}";
            d.opcWrite("OPCItemSyncWrite14", dsm);
        }
        #endregion
    }
}
