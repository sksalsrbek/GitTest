using Kepware.ClientAce.OpcDaClient;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Window
    {

        string machineName = "";

        public Login()
        {
            
            InitializeComponent();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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

                #region LED
                dv.OPCItemNameWriteTextBoxes[0] = dic["LED1"];
                dv.OPCItemNameWriteTextBoxes[1] = dic["LED2"];
                dv.OPCItemNameWriteTextBoxes[2] = dic["LED3"];
                dv.OPCItemNameWriteTextBoxes[3] = dic["LED4"];
                dv.OPCItemNameTextBoxes[0] = dic["LED1"];
                dv.OPCItemNameTextBoxes[1] = dic["LED2"];
                dv.OPCItemNameTextBoxes[2] = dic["LED3"];
                dv.OPCItemNameTextBoxes[3] = dic["LED4"];
                #endregion

                #region GAS 공통신호
                dv.OPCItemNameTextBoxes[4] = dic["Chmberoxy"];





                #endregion

                #region PUMP 공통신호
                dv.OPCItemNameTextBoxes[5] = dic["pump"];//76vv
                dv.OPCItemNameWriteTextBoxes[4] = dic["pump"];//8
                #endregion

                #region Temperature 공통신호

                //나중에 생기면 저거쓸것.

                //dv.OPCItemNameTextBoxes[14] = dic["pump"];
                //dv.OPCItemNameTextBoxes[15] = dic["pump"];

                #endregion

                #region Laser 공통신호
                dv.OPCItemNameWriteTextBoxes[5] = dic["Laser"];//9v
                dv.OPCItemNameWriteTextBoxes[6] = dic["LaserPower"];//10v
                dv.OPCItemNameWriteTextBoxes[7] = dic["GuideBeam"];//11v
                dv.OPCItemNameWriteTextBoxes[8] = dic["LaserReady"];//12v

                dv.OPCItemNameTextBoxes[6] = dic["Laser"];//16vv
                dv.OPCItemNameTextBoxes[7] = dic["LaserPower"];//17v
                dv.OPCItemNameTextBoxes[8] = dic["GuideBeam"];//18v
                dv.OPCItemNameTextBoxes[9] = dic["LaserReady"];//19v
                #endregion

                #region Motor 공통신호

                dv.OPCItemNameTextBoxes[10] = dic["Motor0PostionFeedback"];//20v
                dv.OPCItemNameTextBoxes[11] = dic["Motor1PostionFeedback"];//21v
                dv.OPCItemNameTextBoxes[12] = dic["Motor2PostionFeedback"];//59v
                dv.OPCItemNameTextBoxes[13] = dic["Motor3PostionFeedback"];//60v
                dv.OPCItemNameTextBoxes[14] = dic["Motor4PostionFeedback"];//61v






                dv.OPCItemNameTextBoxes[15] = dic["Motor0Alram"];//26v
                dv.OPCItemNameTextBoxes[16] = dic["Motor1Alram"];//27v
                dv.OPCItemNameTextBoxes[17] = dic["Motor2Alram"];//68v
                dv.OPCItemNameTextBoxes[18] = dic["Motor3Alram"];//69v
                dv.OPCItemNameTextBoxes[19] = dic["Motor4Alram"];//70v

                dv.OPCItemNameWriteTextBoxes[9] = dic["Motor0Reset"];//17
                dv.OPCItemNameWriteTextBoxes[10] = dic["Motor1Reset"];//18
                dv.OPCItemNameWriteTextBoxes[11] = dic["Motor2Reset"];//30
                dv.OPCItemNameWriteTextBoxes[12] = dic["Motor3Reset"];//31
                dv.OPCItemNameWriteTextBoxes[13] = dic["Motor4Reset"];//32



                #endregion

                #region Door 공통신호

                #endregion

                #region BuildTotalTime
                //dv.OPCItemNameTextBoxes[20] = dic["BuildTotalTime"];//29
                //dv.OPCItemNameTextBoxes[21] = dic["bBuildCheck"];//30
                //dv.OPCItemNameTextBoxes[22] = dic["BuildEstTime"];//31

                #endregion

                #region Printing 공통신호
                dv.OPCItemNameTextBoxes[20] = dic["SupervisorRequest"];//32v
                dv.OPCItemNameWriteTextBoxes[14] = dic["SupervisorRequest"];//23v
                dv.OPCItemNameTextBoxes[21] = dic["CurrentLayer"];//33v
                dv.OPCItemNameTextBoxes[22] = dic["TotalLayer"];//34v
                dv.OPCItemNameTextBoxes[23] = dic["ScanStatus"];//35v
                dv.OPCItemNameTextBoxes[24] = dic["JobFile"];//36v
                dv.OPCItemNameTextBoxes[25] = dic["SupervisorFeedback"];//37v

                #endregion

                #region Camera
                dv.OPCItemNameTextBoxes[26] = dic["GasFlow2"];
                dv.OPCItemNameTextBoxes[27] = dic["MaxOxy"];
                dv.OPCItemNameTextBoxes[28] = dic["MinOxy"];
                dv.OPCItemNameTextBoxes[29] = dic["GasSupply"];
                dv.OPCItemNameTextBoxes[30] = dic["pump"];
                dv.OPCItemNameTextBoxes[31] = dic["ChamberTemperature"];
                dv.OPCItemNameTextBoxes[32] = dic["GloveTemperature"];
                dv.OPCItemNameTextBoxes[33] = dic["pump"];
                dv.OPCItemNameTextBoxes[34] = dic["pump"];
                dv.OPCItemNameTextBoxes[35] = dic["pump"];
                dv.OPCItemNameTextBoxes[36] = dic["pump"];
                dv.OPCItemNameTextBoxes[37] = dic["pump"];
                dv.OPCItemNameTextBoxes[38] = dic["Motor0SpeedFeedback"];
                dv.OPCItemNameTextBoxes[39] = dic["Motor1SpeedFeedback"];
                dv.OPCItemNameTextBoxes[40] = dic["Motor2SpeedFeedback"];
                dv.OPCItemNameTextBoxes[41] = dic["Motor3SpeedFeedback"];
                dv.OPCItemNameTextBoxes[42] = dic["Motor4SpeedFeedback"];
                dv.OPCItemNameTextBoxes[43] = dic["ChamberDoorLock"];//추가한것//-
                dv.OPCItemNameTextBoxes[44] = dic["ChamberDoorCheck"];
                dv.OPCItemNameTextBoxes[45] = dic["pump"];//powderSupply
                dv.OPCItemNameTextBoxes[46] = dic["MachineStatus"];
                dv.OPCItemNameTextBoxes[47] = dic["pump"];//layerChange
                dv.OPCItemNameTextBoxes[48] = dic["CPUDiskUse"];
                dv.OPCItemNameTextBoxes[49] = dic["ALLCPU"];
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
                //dv.OPCItemNameTextBoxes[69] = dic["ChamberHumity"];
                //dv.OPCItemNameTextBoxes[70] = dic["ChamberDewpoint"];
                //dv.OPCItemNameTextBoxes[71] = dic["ChamberPressure"];
                //dv.OPCItemNameTextBoxes[72] = dic["BuildTotalTime"];
                //dv.OPCItemNameTextBoxes[73] = dic["BuildEstTime"];


            #endregion

            #region Disk 공통신호
            dv.OPCItemNameWriteTextBoxes[15] = dic["selN2"];
            dv.OPCItemNameWriteTextBoxes[16] = dic["MaxOxy"];
            dv.OPCItemNameWriteTextBoxes[17] = dic["MinOxy"];
            dv.OPCItemNameWriteTextBoxes[18] = dic["GasSupply"];
            dv.OPCItemNameWriteTextBoxes[19] = dic["pump"];
            dv.OPCItemNameWriteTextBoxes[20] = dic["Motor0PostionRequest"];
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
            dv.OPCItemNameWriteTextBoxes[40] = dic["ChamberDoorLock"];//추가한것//-
            dv.OPCItemNameWriteTextBoxes[41] = dic["pump"];//powderSupply
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
            dv.OPCItemNameWriteTextBoxes[58] = dic["ChamberDoorLock2"];
            dv.OPCItemNameWriteTextBoxes[59] = dic["AutoSupply"];
            dv.OPCItemNameWriteTextBoxes[60] = dic["PowderRatio"];
            dv.OPCItemNameWriteTextBoxes[61] = dic["mainpump"];
            dv.OPCItemNameWriteTextBoxes[62] = dic["remainpump"];

            #endregion

            #region buildTime
            #endregion

            #region Monitoring

            //dv.OPCItemNameTextBoxes[58] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[59] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[60] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[61] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[62] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[63] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[64] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[65] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[66] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[67] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[68] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[69] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[70] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[60] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[60] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[40] = dic["CPUDiskUse"];
            //dv.OPCItemNameTextBoxes[40] = dic["CPUDiskUse"];
            #endregion

            #region Power Supply
            #endregion
            try
            {
                if (machineName.Contains("M270 001"))
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

                    }
                    else
                    {
                        Debug.WriteLine("실패");
                        Console.WriteLine("실패");
                    }

                    //HMIMG hmiMG = new HMIMG(dv);
                }
                else if (machineName.Contains("metal M135"))
                {

                }
                else if (machineName.Contains("metal M200"))
                {

                }
                else if (machineName.Contains("metal M270"))
                {
                    //HMIM270 hmiM270 = new HMIM270(dv);
                    //Boolean chConnect = hmiM270.fun_connect(dv.HostName, dv.UserName);

                    //if(chConnect)
                    //{
                    //    hmiM270.Show();
                    //}
                    //else
                    //{
                    //    Console.WriteLine("실패");
                    //}
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

