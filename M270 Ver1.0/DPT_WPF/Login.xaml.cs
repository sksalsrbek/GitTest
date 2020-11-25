using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace DPT_WPF
{
    /// <summary>
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Window
    {
        String machineName = "";

        public Login()
        {
            InitializeComponent();

            this.Show();

            string tempPath = @"C:\Depert_Profile\machineProfile3.dpf";
            string _contentProfile = "";

            using (StreamReader reader = new StreamReader(tempPath))
            {
                _contentProfile = reader.ReadToEnd();
            }

            DefineValue dv = new DefineValue();

            #region FTP-Profile
            cd_Profile cp = new cd_Profile();

            var dic = cp.SplitProfile(_contentProfile);

            DefineValueSetter(dic);
            #endregion
        }

        private DefineValue DefineValueSetter(Dictionary<string, string> dic)
        {
            DefineValue dv = new DefineValue();

            dv.FtpName = dic["FTPName"];
            dv.HostName = dic["HostName"];
            dv.UserName = dic["ID"];
            dv.Password = dic["PW"];
            machineName = dic["Machine Name"];

            string readCount = dic["ReadCount"];
            string writeCount = dic["WrtieCount"];
            dv.ReadCount = Convert.ToInt32(readCount);


            dv.OPCItemNameTextBoxes = new string[dv.ReadCount];
            dv.OPCItemValueTextBoxes = new string[Convert.ToInt32(readCount)];
            dv.OPCItemQualityTextBoxes = new string[Convert.ToInt32(readCount)];

            dv.OPCItemWriteValueTextBoxes = new string[Convert.ToInt32(writeCount)];
            dv.OPCItemNameWriteTextBoxes = new string[Convert.ToInt32(writeCount)];

            //===========================================================================
            dv.OPCItemNameWriteTextBoxes[4] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[5] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[6] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[7] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[8] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[9] = dic["Motor0Reset"];
            dv.OPCItemNameWriteTextBoxes[10] = dic["Motor1Reset"];
            dv.OPCItemNameWriteTextBoxes[11] = dic["Motor2Reset"];
            dv.OPCItemNameWriteTextBoxes[12] = dic["Motor3Reset"];
            dv.OPCItemNameWriteTextBoxes[13] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[14] = dic["SupervisorRequest"];
            //===========================================================================
            dv.OPCItemNameTextBoxes[0] = dic["bLog"];
            dv.OPCItemNameTextBoxes[1] = dic["bLog"];
            dv.OPCItemNameTextBoxes[2] = dic["bLog"];
            dv.OPCItemNameTextBoxes[3] = dic["bLog"];
            dv.OPCItemNameTextBoxes[4] = dic["bLog"];
            dv.OPCItemNameTextBoxes[5] = dic["bLog"];
            dv.OPCItemNameTextBoxes[6] = dic["bLog"];
            dv.OPCItemNameTextBoxes[7] = dic["bLog"];
            dv.OPCItemNameTextBoxes[8] = dic["bLog"];
            dv.OPCItemNameTextBoxes[9] = dic["bLog"];

            dv.OPCItemNameTextBoxes[10] = dic["Motor0PostionFeedback"];
            dv.OPCItemNameTextBoxes[11] = dic["Motor1PostionFeedback"];
            dv.OPCItemNameTextBoxes[12] = dic["Motor2PostionFeedback"];
            dv.OPCItemNameTextBoxes[13] = dic["Motor3PostionFeedback"];
            dv.OPCItemNameTextBoxes[14] = dic["bLog"];
            dv.OPCItemNameTextBoxes[15] = dic["Motor0Alram"];
            dv.OPCItemNameTextBoxes[16] = dic["Motor1Alram"];
            dv.OPCItemNameTextBoxes[17] = dic["Motor2Alram"];
            dv.OPCItemNameTextBoxes[18] = dic["Motor3Alram"];
            dv.OPCItemNameTextBoxes[19] = dic["bLog"];

            dv.OPCItemNameTextBoxes[20] = dic["SupervisorRequest"];
            dv.OPCItemNameTextBoxes[21] = dic["CurrentLayer"];
            dv.OPCItemNameTextBoxes[22] = dic["TotalLayer"];
            dv.OPCItemNameTextBoxes[23] = dic["ScanStatus"];
            dv.OPCItemNameTextBoxes[24] = dic["JobFile"];
            dv.OPCItemNameTextBoxes[25] = dic["SupervisorFeedback"];

            dv.OPCItemNameTextBoxes[26] = dic["bLog"];
            dv.OPCItemNameTextBoxes[27] = dic["bLog"];
            dv.OPCItemNameTextBoxes[28] = dic["bLog"];
            dv.OPCItemNameTextBoxes[29] = dic["bLog"];
            dv.OPCItemNameTextBoxes[30] = dic["bLog"];
            dv.OPCItemNameTextBoxes[31] = dic["bLog"];
            dv.OPCItemNameTextBoxes[32] = dic["bLog"];
            dv.OPCItemNameTextBoxes[33] = dic["bLog"];
            dv.OPCItemNameTextBoxes[34] = dic["bLog"];
            dv.OPCItemNameTextBoxes[35] = dic["bLog"];
            dv.OPCItemNameTextBoxes[36] = dic["bLog"];
            dv.OPCItemNameTextBoxes[37] = dic["bLog"];
            dv.OPCItemNameTextBoxes[38] = dic["bLog"];//Motor0SpeedFeedback
            dv.OPCItemNameTextBoxes[39] = dic["bLog"];
            dv.OPCItemNameTextBoxes[40] = dic["bLog"];
            dv.OPCItemNameTextBoxes[41] = dic["bLog"];
            dv.OPCItemNameTextBoxes[42] = dic["bLog"];
            dv.OPCItemNameTextBoxes[43] = dic["bLog"];
            dv.OPCItemNameTextBoxes[44] = dic["bLog"];
            dv.OPCItemNameTextBoxes[45] = dic["bLog"];
            dv.OPCItemNameTextBoxes[46] = dic["bLog"];
            dv.OPCItemNameTextBoxes[47] = dic["bLog"];
            dv.OPCItemNameTextBoxes[48] = dic["bLog"];
            dv.OPCItemNameTextBoxes[49] = dic["bLog"];
            dv.OPCItemNameTextBoxes[50] = dic["strLOG"];
            dv.OPCItemNameTextBoxes[51] = dic["CameraShot"];
            dv.OPCItemNameTextBoxes[52] = dic["Motor1TotalDistance"];
            dv.OPCItemNameTextBoxes[53] = dic["Motor2TotalDistance"];
            dv.OPCItemNameTextBoxes[54] = dic["Motor3TotalDistance"];
            dv.OPCItemNameTextBoxes[55] = dic["pump"];
            dv.OPCItemNameTextBoxes[56] = dic["pump"];
            dv.OPCItemNameTextBoxes[57] = dic["MotorTwoway"];
            dv.OPCItemNameTextBoxes[58] = dic["AfterBladeMotor"];
            dv.OPCItemNameTextBoxes[59] = dic["Filter"];
            dv.OPCItemNameTextBoxes[60] = dic["MachineTime"];
            dv.OPCItemNameTextBoxes[61] = dic["PumpTime"];
            dv.OPCItemNameTextBoxes[62] = dic["ScannerTime"];
            dv.OPCItemNameTextBoxes[63] = dic["MonthMachine"];
            dv.OPCItemNameTextBoxes[64] = dic["PrintingTimeLine"];
            dv.OPCItemNameTextBoxes[65] = dic["bMonitoring"];
            dv.OPCItemNameTextBoxes[66] = dic["bLog"];
            dv.OPCItemNameTextBoxes[67] = dic["selN2"];
            dv.OPCItemNameTextBoxes[68] = dic["selAR"];
            dv.OPCItemNameTextBoxes[69] = dic["Gas1Pressure"];
            dv.OPCItemNameTextBoxes[70] = dic["Gas2Pressure"];
            dv.OPCItemNameTextBoxes[71] = dic["GasFlow2"];
            dv.OPCItemNameTextBoxes[72] = dic["GasPressure"];
            dv.OPCItemNameTextBoxes[73] = dic["WindSpeed"];
            dv.OPCItemNameTextBoxes[74] = dic["ChamberDew"];
            dv.OPCItemNameTextBoxes[75] = dic["AirPressure"];
            dv.OPCItemNameTextBoxes[76] = dic["AirFlow"];
            dv.OPCItemNameTextBoxes[77] = dic["powderQuantityTop"];
            dv.OPCItemNameTextBoxes[78] = dic["powderQuantityUnder"];
            dv.OPCItemNameTextBoxes[79] = dic["BackFilter1"];
            dv.OPCItemNameTextBoxes[80] = dic["BackFilter2"];
            dv.OPCItemNameTextBoxes[81] = dic["AirValve"];
            dv.OPCItemNameTextBoxes[82] = dic["ChamberDoorLock2"];
            dv.OPCItemNameTextBoxes[83] = dic["AutoSupply"];
            dv.OPCItemNameTextBoxes[84] = dic["PipeOxy"];
            dv.OPCItemNameTextBoxes[85] = dic["PipeTemper"];
            dv.OPCItemNameTextBoxes[86] = dic["PowderRatio"];
            dv.OPCItemNameTextBoxes[87] = dic["BuildTotalTime"];
            dv.OPCItemNameTextBoxes[88] = dic["mainpump"];
            dv.OPCItemNameTextBoxes[89] = dic["remainpump"];

            #region Disk 공통신호
            dv.OPCItemNameWriteTextBoxes[15] = dic["selN2"];
            dv.OPCItemNameWriteTextBoxes[16] = dic["MaxOxy"];
            dv.OPCItemNameWriteTextBoxes[17] = dic["MinOxy"];
            dv.OPCItemNameWriteTextBoxes[18] = dic["GasSupply"];
            dv.OPCItemNameWriteTextBoxes[19] = dic["pump"];
            dv.OPCItemNameWriteTextBoxes[20] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[21] = dic["Motor1PostionRequest"];
            dv.OPCItemNameWriteTextBoxes[22] = dic["Motor2PostionRequest"];
            dv.OPCItemNameWriteTextBoxes[23] = dic["Motor3PostionRequest"];
            dv.OPCItemNameWriteTextBoxes[24] = dic["Motor4PostionRequest"];
            dv.OPCItemNameWriteTextBoxes[25] = dic["Motor0SpeedRequest"];
            dv.OPCItemNameWriteTextBoxes[26] = dic["Motor1SpeedRequest"];
            dv.OPCItemNameWriteTextBoxes[27] = dic["Motor2SpeedRequest"];
            dv.OPCItemNameWriteTextBoxes[28] = dic["Motor3SpeedRequest"];
            dv.OPCItemNameWriteTextBoxes[29] = dic["Motor4SpeedRequest"];
            dv.OPCItemNameWriteTextBoxes[30] = dic["Motor0Finish"];
            dv.OPCItemNameWriteTextBoxes[31] = dic["Motor1Finish"];
            dv.OPCItemNameWriteTextBoxes[32] = dic["Motor2Finish"];
            dv.OPCItemNameWriteTextBoxes[33] = dic["Motor3Finish"];
            dv.OPCItemNameWriteTextBoxes[34] = dic["Motor4Finish"];
            dv.OPCItemNameWriteTextBoxes[35] = dic["Motor0Command"];
            dv.OPCItemNameWriteTextBoxes[36] = dic["Motor1Command"];
            dv.OPCItemNameWriteTextBoxes[37] = dic["Motor2Command"];
            dv.OPCItemNameWriteTextBoxes[38] = dic["Motor3Command"];
            dv.OPCItemNameWriteTextBoxes[39] = dic["Motor4Command"];
            dv.OPCItemNameWriteTextBoxes[40] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[41] = dic["pump"];
            dv.OPCItemNameWriteTextBoxes[42] = dic["Motor1TotalDistance"];
            dv.OPCItemNameWriteTextBoxes[43] = dic["Motor2TotalDistance"];
            dv.OPCItemNameWriteTextBoxes[44] = dic["Motor3TotalDistance"];
            dv.OPCItemNameWriteTextBoxes[45] = dic["pump"];
            dv.OPCItemNameWriteTextBoxes[46] = dic["pump"];
            dv.OPCItemNameWriteTextBoxes[47] = dic["MotorTwoway"];
            dv.OPCItemNameWriteTextBoxes[48] = dic["AfterBladeMotor"];
            dv.OPCItemNameWriteTextBoxes[49] = dic["Filter"];
            dv.OPCItemNameWriteTextBoxes[50] = dic["PumpTime"];
            dv.OPCItemNameWriteTextBoxes[51] = dic["ScannerTime"];
            dv.OPCItemNameWriteTextBoxes[52] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[53] = dic["bMonitoring"];
            dv.OPCItemNameWriteTextBoxes[54] = dic["selAR"];
            dv.OPCItemNameWriteTextBoxes[55] = dic["BackFilter1"];
            dv.OPCItemNameWriteTextBoxes[56] = dic["BackFilter2"];
            dv.OPCItemNameWriteTextBoxes[57] = dic["AirValve"];
            dv.OPCItemNameWriteTextBoxes[58] = dic["bLog"];
            dv.OPCItemNameWriteTextBoxes[59] = dic["AutoSupply"];
            dv.OPCItemNameWriteTextBoxes[60] = dic["PowderRatio"];
            dv.OPCItemNameWriteTextBoxes[61] = dic["mainpump"];
            dv.OPCItemNameWriteTextBoxes[62] = dic["remainpump"];
            #endregion
            
            try
            {
                HMIM270 hmiM270 = new HMIM270(dv);
                Boolean chConnect = hmiM270.fun_connect(dv.HostName, dv.UserName);

                if (chConnect == true)
                {
                    hmiM270.Show();
                    string profilePath = "C:\\Depert_Profile\\temp.dpf";
                    string strProfile = "[MACHINE]\n" + "machineName\n" + dv.HostName + "\n" + dv.FtpName + "\n" + dv.UserName + "\n" + dv.Password;

                    using (StreamWriter sw = new StreamWriter(profilePath))
                    {
                        sw.WriteLine(strProfile);
                    }
                    this.Hide();
                }
                else
                {
                    Debug.WriteLine("실패");
                    Console.WriteLine("실패");
                    MessageBox.Show("QNdldld");
                }
                return dv;
            }
            catch (Exception e)
            {
                Debug.WriteLine("e : " + e.Message);
                return dv;
            }
        }
    }
}

