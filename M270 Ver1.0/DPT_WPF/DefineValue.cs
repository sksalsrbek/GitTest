﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using Kepware.ClientAce.OpcDaClient;
using LiveCharts;

namespace DPT_WPF
{    public partial class DefineValue : PropertyChangedBase
    {
       

        public string[] PrintWeekend { get; set; }
        public Func<double, string> DayofWeek { get; set; }
        public double AxisStep { get; set; }

        

        private PointCollection points = new PointCollection();
        public PointCollection Points
        {
            get { return points; }
            set
            {
                points = value;
                NotifyOfPropertyChange(() => this.Points);
            }
        }


        private double _axisMax;
        private double _axisMin;
        public double AxisUnit { get; set; }
        public DefineValue()
        {
            
        }

        private string strTimeStart;
        public string StrTimeStart
        {
            get { return strTimeStart; }
            set
            {
                strTimeStart = value;
                NotifyOfPropertyChange(() => this.StrTimeStart);
            }
        }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                NotifyOfPropertyChange(() => this.AxisMax);
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                NotifyOfPropertyChange(() => this.AxisMin);
            }
        }

        public Func<double, string> Formatter { get; set; }
        public Func<double, string> PrintingFormatter { get; set; }

        public SeriesCollection PrintingSeries { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public SeriesCollection ChamberOxySeries { get; set; }
        public SeriesCollection GloveOxySeries { get; set; }
        public SeriesCollection LaserSeries { get; set; }

        public SeriesCollection SubChamberOxySeries { get; set; }
        public SeriesCollection SubGloveOxySeries { get; set; }
        public SeriesCollection SubLaserSeries { get; set; }

        public Func<ChartPoint, string> PointLabel { get; set; }
        public Func<ChartPoint, string> PointLabel1 { get; set; }

        #region Monitoring



        private double _xPointer;
        private double _yPointer;
        private double _from;
        private double _to;

        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                NotifyOfPropertyChange(() => this.From);
            }
        }

        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                NotifyOfPropertyChange(() => this.To);
            }
        }

        public double XPointer
        {
            get { return _xPointer; }
            set
            {
                _xPointer = value;
                NotifyOfPropertyChange(() => this.XPointer);
            }
        }

        public double YPointer
        {
            get { return _yPointer; }
            set
            {
                _yPointer = value;
                NotifyOfPropertyChange(() => this.YPointer);
            }
        }

        //public Func<double, string> Formatter { get; set; }

       

        public SeriesCollection MonthlyMachineCollection { get; set; }
        private double dblMotorStepper0PostionFeedback;
        private double dblMotorStepper1PostionFeedback;
        private double dblMotorStepper2PostionFeedback;
        
        private string strMachineTime = "0";
        private string strFilterTime = "0";
        private string strPumpTime = "0";
        private string strScannerTime = "0";

        private string strCameraShot = "0";
        private string strTwowayCount = "0";
        private string strAfterBlade = "0";

        private string strOxyCheck = "정상";

        public string StrOxyCheck
        {
            get { return strOxyCheck; }
            set
            {
                if (strOxyCheck != value)
                {
                    strOxyCheck = value;
                    NotifyOfPropertyChange(() => this.StrOxyCheck);
                }
            }
        }
        public string StrCameraShot
        {
            get { return strCameraShot; }
            set
            {
                if (strCameraShot != value)
                {
                    strCameraShot = value;
                    NotifyOfPropertyChange(() => this.StrCameraShot);
                }
            }
        }
        public string StrTwowayCount
        {
            get { return strTwowayCount; }
            set
            {
                if (strTwowayCount != value)
                {
                    strTwowayCount = value;
                    NotifyOfPropertyChange(() => this.StrTwowayCount);
                }
            }
        }
        public string StrAfterBlade
        {
            get { return strAfterBlade; }
            set
            {
                if (strAfterBlade != value)
                {
                    strAfterBlade = value;
                    NotifyOfPropertyChange(() => this.StrAfterBlade);
                }
            }
        }
        public string StrScannerTime
        {
            get { return strScannerTime; }
            set
            {
                if (strScannerTime != value)
                {
                    strScannerTime = value;
                    NotifyOfPropertyChange(() => this.StrScannerTime);
                }
            }
        }
        public string StrMachineTime
        {
            get { return strMachineTime; }
            set
            {
                if (strMachineTime != value)
                {
                    strMachineTime = value;
                    NotifyOfPropertyChange(() => this.StrMachineTime);
                }
            }
        }
        public string StrFilterTime
        {
            get { return strFilterTime; }
            set
            {
                if (strFilterTime != value)
                {
                    strFilterTime = value;
                    NotifyOfPropertyChange(() => this.StrFilterTime);
                }
            }
        }
        public string StrPumpTime
        {
            get { return strPumpTime; }
            set
            {
                if (strPumpTime != value)
                {
                    strPumpTime = value;
                    NotifyOfPropertyChange(() => this.StrPumpTime);
                }
            }
        }

        public double DblMotorStepper0PostionFeedback
        {
            get { return dblMotorStepper0PostionFeedback; }
            set
            {
                if (dblMotorStepper0PostionFeedback != value)
                {
                    dblMotorStepper0PostionFeedback = value;
                    NotifyOfPropertyChange(() => this.DblMotorStepper0PostionFeedback);
                }
            }
        }
        public double DblMotorStepper1PostionFeedback
        {
            get { return dblMotorStepper1PostionFeedback; }
            set
            {
                if (dblMotorStepper1PostionFeedback != value)
                {
                    dblMotorStepper1PostionFeedback = value;
                    NotifyOfPropertyChange(() => this.DblMotorStepper1PostionFeedback);
                }
            }
        }
        public double DblMotorStepper2PostionFeedback
        {
            get { return dblMotorStepper2PostionFeedback; }
            set
            {
                if (dblMotorStepper2PostionFeedback != value)
                {
                    dblMotorStepper2PostionFeedback = value;
                    NotifyOfPropertyChange(() => this.DblMotorStepper2PostionFeedback);
                }
            }
        }
        #endregion


        #region  //로그인 부분
        private string _ftpName = "";
        private string _hostNmae; //opc.tcp://192.168.255.221
        private string _userName; //daeguntech
        private string _password; //daeguntech 
        public string FtpName
        {
            get { return _ftpName; }
            set
            {
                if (_ftpName != value)
                {
                    _ftpName = value;
                    NotifyOfPropertyChange(() => this.FtpName);
                }
            }
        }
        public string HostName
        {
            get { return _hostNmae; }
            set
            {
                if (_hostNmae != value)
                {
                    _hostNmae = value;
                    NotifyOfPropertyChange(() => this.HostName);
                }
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    NotifyOfPropertyChange(() => this.UserName);
                }
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    NotifyOfPropertyChange(() => this.Password);
                }
            }
        }

        #endregion

        #region//READ WRITE 갯수
        public string[] OPCItemNameTextBoxes;
        public string[] OPCItemValueTextBoxes;
        public string[] OPCItemQualityTextBoxes;
        public string[] OPCItemWriteValueTextBoxes;
        public string[] OPCItemNameWriteTextBoxes;
        private int _readCount;
        public int ReadCount
        {
            get { return _readCount; }
            set
            {
                if (_readCount != value)
                {
                    _readCount = value;
                    NotifyOfPropertyChange(() => this.ReadCount);
                }
            }
        }

        #endregion

        #region REAL TIME 실시간 값
        private string puPowderLeft = "100%";
        public string PUPowderLeft
        {
            get { return puPowderLeft; }
            set
            {
                puPowderLeft = value;
                NotifyOfPropertyChange(() => this.PUPowderLeft);
            }
        }


        private string puLeftHeight = "0mm";
        public string PULeftHeight
        {
            get { return puLeftHeight; }
            set
            {
                puLeftHeight = value;
                NotifyOfPropertyChange(() => this.PULeftHeight);
            }
        }

        private string puCPUuse = "0%";
        public string PUCPUuse
        {
            get { return puCPUuse; }
            set
            {
                puCPUuse = value;
                NotifyOfPropertyChange(() => this.PUCPUuse);
            }
        }
        private string puChamberOxy = "21";
        public string PUChamberOxy
        {
            get { return puChamberOxy; }
            set
            {
                puChamberOxy = value;
                NotifyOfPropertyChange(() => this.PUChamberOxy);
            }
        }

        private string puChamberLaser = "21";
        public string PuChamberLaser
        {
            get { return puChamberLaser; }
            set
            {
                puChamberLaser = value;
                NotifyOfPropertyChange(() => this.PuChamberLaser);
            }
        }
        private string puGloveOxy = "21";
        public string PUGloveOxy
        {
            get { return puGloveOxy; }
            set
            {
                puGloveOxy = value;
                NotifyOfPropertyChange(() => this.PUGloveOxy);
            }
        }

        private string puChamberSubOxy = "21";
        public string PUChamberSubOxy
        {
            get { return puChamberSubOxy; }
            set
            {
                puChamberSubOxy = value;
                NotifyOfPropertyChange(() => this.PUChamberSubOxy);
            }
        }
        #endregion

        #region laser
        private string strLaser = "0";
        public string StrLaser
        {
            get { return strLaser; }
            set
            {
                if (strLaser != value)
                {
                    strLaser = value;
                    NotifyOfPropertyChange(() => this.StrLaser);
                }
            }
        }

        #endregion

        #region Temperature / Humity
        private double puHumity = 0;
        public double PuHumity
        {
            get { return puHumity; }
            set
            {
                if (puHumity != value)
                {
                    puHumity = value;
                    NotifyOfPropertyChange(() => this.PuHumity);
                }
            }
        }
        #endregion

        #region Printing
        private string strPrintStaus = "출력전";
        private string strPrintPercetage = "0%";
        private string strPrintEstTime = "";

        private int intPirntPercetage = 0;
        public int IntPirntPercetage
        {
            get { return intPirntPercetage; }
            set
            {
                if (intPirntPercetage != value)
                {
                    intPirntPercetage = value;
                    NotifyOfPropertyChange(() => this.IntPirntPercetage);
                }
            }
        }
        public string StrPrintStaus
        {
            get { return strPrintStaus; }
            set
            {
                if (strPrintStaus != value)
                {
                    strPrintStaus = value;
                    NotifyOfPropertyChange(() => this.StrPrintStaus);
                }
            }
        }
        public string StrPrintPercetage
        {
            get { return strPrintPercetage; }
            set
            {
                if (strPrintPercetage != value)
                {
                    strPrintPercetage = value;
                    NotifyOfPropertyChange(() => this.StrPrintPercetage);
                }
            }
        }

        public string StrPrintEstTime
        {
            get { return strPrintEstTime; }
            set
            {
                if (strPrintEstTime != value)
                {
                    strPrintEstTime = value;
                    NotifyOfPropertyChange(() => this.StrPrintEstTime);
                }
            }
        }
        #endregion

        private int ledCount = 0;
        private int ledRightCount = 0;

        private double dblGasFlow = 0;
        private double dblAirFlow = 0;
        private double dblLaserPower = 0;
        private string strMaxOxy = "0.1";
        private string strMinOxy = "0.05";

        public double DblGasFlow
        {
            get { return dblGasFlow; }
            set
            {
                if (dblGasFlow != value)
                {
                    dblGasFlow = value;
                    NotifyOfPropertyChange(() => this.DblGasFlow);
                }
            }
        }
        public double DblAirFlow
        {
            get { return dblAirFlow; }
            set
            {
                if (dblAirFlow != value)
                {
                    dblAirFlow = value;
                    NotifyOfPropertyChange(() => this.DblAirFlow);
                }
            }
        }
        public double DblLaserPower
        {
            get { return dblLaserPower; }
            set
            {
                if (dblLaserPower != value)
                {
                    dblLaserPower = value;
                    NotifyOfPropertyChange(() => this.DblLaserPower);
                }
            }
        }
        public string StrMaxOxy
        {
            get { return strMaxOxy; }
            set
            {
                if (strMaxOxy != value)
                {
                    strMaxOxy = value;
                    NotifyOfPropertyChange(() => this.StrMaxOxy);
                }
            }
        }
        public string StrMinOxy
        {
            get { return strMinOxy; }
            set
            {
                if (strMinOxy != value)
                {
                    strMinOxy = value;
                    NotifyOfPropertyChange(() => this.StrMinOxy);
                }
            }
        }


        private string strPump = "0";
        public string StrPump
        {
            get { return strPump; }
            set
            {
                if (strPump != value)
                {
                    strPump = value;
                    NotifyOfPropertyChange(() => this.StrPump);
                }
            }
        }

        private string pumpnum1 = "0";

        public string Pumpnum1
        {
            get { return pumpnum1; }
            set
            {
                if (pumpnum1 != value)
                {
                    pumpnum1 = value;
                    NotifyOfPropertyChange(() => this.Pumpnum1);
                }
            }
        }

        private string pumpnum2 = "0";

        public string Pumpnum2
        {
            get { return pumpnum2; }
            set
            {
                if (pumpnum2 != value)
                {
                    pumpnum2 = value;
                    NotifyOfPropertyChange(() => this.Pumpnum2);
                }
            }
        }



        private string strTemperature = "20℃";
        private string strTemperatureGlove = "20℃";
        private int intTemperature = 20;
        private int intTemperatureGlove = 20;
        

        private string strHumity = "0%";
        
        private string strHumityGlove = "0%";
        private int intHumity = 0;
        private int intHumityGlove = 0;
        private string strPressure = "0";
        private string strDewPoint = "0";
        private int intDewPoint = 0;
        private int intPressure = 0;

        private string gasMaxValue = "0.10";
        private string gasMinValue = "0.05";

        private string dblMotor1Distance = "005.000";
        private string dblMotor2Distance = "005.000";
        private string dblMotor3Distance = "005.000";
        private string dblMotor4Distance = "005.000";
        private string dblMotor5Distance = "005.000";

        private string dblMotor1AllDistance = "0";
        private string dblMotor2AllDistance = "0";
        private string dblMotor3AllDistance = "0";
        private string dblMotor4AllDistance = "0";
        private string dblMotor5AllDistance = "0";


        private double dblMotor1Position = 0;
        private double dblMotor2Position = 0;
        private double dblMotor3Position = 0;
        private double dblMotor4Position = 0;
        private double dblMotor5Position = 0;

        private string dblMotor1Speed = "10";
        private string dblMotor2Speed = "10";
        private string dblMotor3Speed = "10";
        private string dblMotor4Speed = "10";
        private string dblMotor5Speed = "10";

        private string strRoomPosition = "글로브 룸";

        public int LedCount
        {
            get { return ledCount; }
            set
            {
                if (ledCount != value)
                {
                    ledCount = value;
                    NotifyOfPropertyChange(() => this.LedCount);
                }
            }
        }
        public int LedLeftCount
        {
            get { return ledCount; }
            set
            {
                if (ledCount != value)
                {
                    ledCount = value;
                    NotifyOfPropertyChange(() => this.LedLeftCount);
                }
            }
        }
        public int LedRightCount
        {
            get { return ledRightCount; }
            set
            {
                if (ledRightCount != value)
                {
                    ledRightCount = value;
                    NotifyOfPropertyChange(() => this.LedRightCount);
                }
            }
        }
        public string GasMaxValue
        {
            get { return gasMaxValue; }
            set
            {
                if (gasMaxValue != value)
                {
                    gasMaxValue = value;
                    NotifyOfPropertyChange(() => this.GasMaxValue);
                }
            }
        }
        public string GasMinValue
        {
            get { return gasMinValue; }
            set
            {
                if (gasMinValue != value)
                {
                    gasMinValue = value;
                    NotifyOfPropertyChange(() => this.GasMinValue);
                }
            }
        }
        public string StrTemperature
        {
            get { return strTemperature; }
            set
            {
                if (strTemperature != value)
                {
                    strTemperature = value;
                    NotifyOfPropertyChange(() => this.StrTemperature);
                }
            }
        }
        public string StrTemperatureGlove
        {
            get { return strTemperatureGlove; }
            set
            {
                if (strTemperatureGlove != value)
                {
                    strTemperatureGlove = value;
                    NotifyOfPropertyChange(() => this.StrTemperatureGlove);
                }
            }
        }
        public int IntTemperature
        {
            get { return intTemperature; }
            set
            {
                if (intTemperature != value)
                {
                    intTemperature = value;
                    NotifyOfPropertyChange(() => this.IntTemperature);
                }
            }
        }
        public int IntTemperatureGlove
        {
            get { return intTemperatureGlove; }
            set
            {
                if (intTemperatureGlove != value)
                {
                    intTemperatureGlove = value;
                    NotifyOfPropertyChange(() => this.IntTemperatureGlove);
                }
            }
        }
        public string StrHumity
        {
            get { return strHumity; }
            set
            {
                if (strHumity != value)
                {
                    strHumity = value;
                    NotifyOfPropertyChange(() => this.StrHumity);
                }
            }
        }
       

        public string StrDewPoint
        {
            get { return strHumity; }
            set
            {
                if (strDewPoint != value)
                {
                    strDewPoint = value;
                    NotifyOfPropertyChange(() => this.StrDewPoint);
                }
            }
        }
        public string StrPressure
        {
            get { return strHumity; }
            set
            {
                if (strPressure != value)
                {
                    strPressure = value;
                    NotifyOfPropertyChange(() => this.StrPressure);
                }
            }
        }
        public int IntDewPoint
        {
            get { return intHumity; }
            set
            {
                if (intDewPoint != value)
                {
                    intDewPoint = value;
                    NotifyOfPropertyChange(() => this.IntDewPoint);
                }
            }
        }
        public int IntPressure
        {
            get { return intHumity; }
            set
            {
                if (intPressure != value)
                {
                    intPressure = value;
                    NotifyOfPropertyChange(() => this.IntPressure);
                }
            }
        }
        public string StrHumityGlove
        {
            get { return strHumityGlove; }
            set
            {
                if (strHumityGlove != value)
                {
                    strHumityGlove = value;
                    NotifyOfPropertyChange(() => this.StrHumityGlove);
                }
            }
        }
        public int IntHumity
        {
            get { return intHumity; }
            set
            {
                if (intHumity != value)
                {
                    intHumity = value;
                    NotifyOfPropertyChange(() => this.IntHumity);
                }
            }
        }
        public int IntHumityGlvoe
        {
            get { return intHumityGlove; }
            set
            {
                if (intHumityGlove != value)
                {
                    intHumityGlove = value;
                    NotifyOfPropertyChange(() => this.IntHumityGlvoe);
                }
            }
        }


        public string DblMotor1AllDistance
        {
            get { return dblMotor1AllDistance; }
            set
            {
                if (dblMotor1AllDistance != value)
                {
                    dblMotor1AllDistance = value;
                    NotifyOfPropertyChange(() => this.DblMotor1AllDistance);
                }
            }
        }
        public string DblMotor2AllDistance
        {
            get { return dblMotor2AllDistance; }
            set
            {
                if (dblMotor2AllDistance != value)
                {
                    dblMotor2AllDistance = value;
                    NotifyOfPropertyChange(() => this.DblMotor2AllDistance);
                }
            }
        }
        public string DblMotor3AllDistance
        {
            get { return dblMotor3AllDistance; }
            set
            {
                if (dblMotor3AllDistance != value)
                {
                    dblMotor3AllDistance = value;
                    NotifyOfPropertyChange(() => this.DblMotor3AllDistance);
                }
            }
        }

        public string DblMotor4AllDistance
        {
            get { return dblMotor4AllDistance; }
            set
            {
                if (dblMotor4AllDistance != value)
                {
                    dblMotor4AllDistance = value;
                    NotifyOfPropertyChange(() => this.DblMotor4AllDistance);
                }
            }
        }

        public string DblMotor5AllDistance
        {
            get { return dblMotor5AllDistance; }
            set
            {
                if (dblMotor5AllDistance != value)
                {
                    dblMotor5AllDistance = value;
                    NotifyOfPropertyChange(() => this.DblMotor5AllDistance);
                }
            }
        }
        public string DblMotor1Distance
        {
            get { return dblMotor1Distance; }
            set
            {
                if (dblMotor1Distance != value)
                {
                    dblMotor1Distance = value;
                    NotifyOfPropertyChange(() => this.DblMotor1Distance);
                }
            }
        }
        public string DblMotor2Distance
        {
            get { return dblMotor2Distance; }
            set
            {
                if (dblMotor2Distance != value)
                {
                    dblMotor2Distance = value;
                    NotifyOfPropertyChange(() => this.DblMotor2Distance);
                }
            }
        }
        public string DblMotor3Distance
        {
            get { return dblMotor3Distance; }
            set
            {
                if (dblMotor3Distance != value)
                {
                    dblMotor3Distance = value;
                    NotifyOfPropertyChange(() => this.DblMotor3Distance);
                }
            }
        }
        public string DblMotor4Distance
        {
            get { return dblMotor4Distance; }
            set
            {
                if (dblMotor4Distance != value)
                {
                    dblMotor4Distance = value;
                    NotifyOfPropertyChange(() => this.DblMotor4Distance);
                }
            }
        }
        public string DblMotor5Distance
        {
            get { return dblMotor5Distance; }
            set
            {
                if (dblMotor5Distance != value)
                {
                    dblMotor5Distance = value;
                    NotifyOfPropertyChange(() => this.DblMotor5Distance);
                }
            }
        }
        public double DblMotor1Position
        {
            get { return dblMotor1Position; }
            set
            {
                if (dblMotor1Position != value)
                {
                    dblMotor1Position = value;
                    NotifyOfPropertyChange(() => this.DblMotor1Position);
                }
            }
        }
        public double DblMotor2Position
        {
            get { return dblMotor2Position; }
            set
            {
                if (dblMotor2Position != value)
                {
                    dblMotor2Position = value;
                    NotifyOfPropertyChange(() => this.DblMotor2Position);
                }
            }
        }
        public double DblMotor3Position
        {
            get { return dblMotor3Position; }
            set
            {
                if (dblMotor3Position != value)
                {
                    dblMotor3Position = value;
                    NotifyOfPropertyChange(() => this.DblMotor3Position);
                }
            }
        }
        public double DblMotor4Position
        {
            get { return dblMotor4Position; }
            set
            {
                if (dblMotor4Position != value)
                {
                    dblMotor4Position = value;
                    NotifyOfPropertyChange(() => this.DblMotor4Position);
                }
            }
        }
        public double DblMotor5Position
        {
            get { return dblMotor5Position; }
            set
            {
                if (dblMotor5Position != value)
                {
                    dblMotor5Position = value;
                    NotifyOfPropertyChange(() => this.DblMotor5Position);
                }
            }
        }
        public string DblMotor1Speed
        {
            get { return dblMotor1Speed; }
            set
            {
                if (dblMotor1Speed != value)
                {
                    dblMotor1Speed = value;
                    NotifyOfPropertyChange(() => this.DblMotor1Speed);
                }
            }
        }
        public string DblMotor2Speed
        {
            get { return dblMotor2Speed; }
            set
            {
                if (dblMotor2Speed != value)
                {
                    dblMotor2Speed = value;
                    NotifyOfPropertyChange(() => this.DblMotor2Speed);
                }
            }
        }
        public string DblMotor3Speed
        {
            get { return dblMotor3Speed; }
            set
            {
                if (dblMotor3Speed != value)
                {
                    dblMotor3Speed = value;
                    NotifyOfPropertyChange(() => this.DblMotor3Speed);
                }
            }
        }
        public string DblMotor4Speed
        {
            get { return dblMotor4Speed; }
            set
            {
                if (dblMotor4Speed != value)
                {
                    dblMotor4Speed = value;
                    NotifyOfPropertyChange(() => this.DblMotor4Speed);
                }
            }
        }
        public string DblMotor5Speed
        {
            get { return dblMotor5Speed; }
            set
            {
                if (dblMotor5Speed != value)
                {
                    dblMotor5Speed = value;
                    NotifyOfPropertyChange(() => this.DblMotor5Speed);
                }
            }
        }

        public string StrRoomPosition
        {
            get { return strRoomPosition; }
            set
            {
                if (strRoomPosition != value)
                {
                    strRoomPosition = value;
                    NotifyOfPropertyChange(() => this.StrRoomPosition);
                }
            }
        }
        private int picFileCount = 0;
        public int PicFileCount
        {
            get { return picFileCount; }
            set
            {
                if (picFileCount != value)
                {
                    picFileCount = value;
                    NotifyOfPropertyChange(() => this.PicFileCount);
                }

            }
        }

        private IChartValues values;
        public IChartValues Values
        {
            get { return values; }
            set
            {
                if (values != value)
                {
                    values = value;
                    NotifyOfPropertyChange(() => this.Values);
                }

            }
        }

        private IChartValues minusvalues;
        public IChartValues Minusvalues
        {
            get { return minusvalues; }
            set
            {
                if (minusvalues != value)
                {
                    minusvalues = value;
                    NotifyOfPropertyChange(() => this.Minusvalues);
                }

            }
        }


        private string monitorMotor1TotalDistance = "0";
        private string monitorMotor2TotalDistance = "0";
        private string monitorMotor3TotalDistance = "0";
        private string monitorMotor4TotalDistance = "0";
        private string monitorMotor5TotalDistance = "0";

        public string MonitorMotor1TotalDistance
        {
            get { return monitorMotor1TotalDistance; }
            set
            {
                if (monitorMotor1TotalDistance != value)
                {
                    monitorMotor1TotalDistance = value;
                    NotifyOfPropertyChange(() => this.MonitorMotor1TotalDistance);
                }
            }
        }
        public string MonitorMotor2TotalDistance
        {
            get { return monitorMotor2TotalDistance; }
            set
            {
                if (monitorMotor2TotalDistance != value)
                {
                    monitorMotor2TotalDistance = value;
                    NotifyOfPropertyChange(() => this.MonitorMotor2TotalDistance);
                }
            }
        }
        public string MonitorMotor3TotalDistance
        {
            get { return monitorMotor3TotalDistance; }
            set
            {
                if (monitorMotor3TotalDistance != value)
                {
                    monitorMotor3TotalDistance = value;
                    NotifyOfPropertyChange(() => this.MonitorMotor3TotalDistance);
                }
            }
        }
        public string MonitorMotor4TotalDistance
        {
            get { return monitorMotor4TotalDistance; }
            set
            {
                if (monitorMotor4TotalDistance != value)
                {
                    monitorMotor4TotalDistance = value;
                    NotifyOfPropertyChange(() => this.MonitorMotor4TotalDistance);
                }
            }
        }
        public string MonitorMotor5TotalDistance
        {
            get { return monitorMotor5TotalDistance; }
            set
            {
                if (monitorMotor5TotalDistance != value)
                {
                    monitorMotor5TotalDistance = value;
                    NotifyOfPropertyChange(() => this.MonitorMotor5TotalDistance);
                }
            }
        }

        #region Heater set
        private int currentHeaterTemperature = 0;
        private string targetHeaterTemperature = "0";
        private int ctargetHeaterTemperature = 0;

        public int CurrentHeaterTemperature
        {
            get { return currentHeaterTemperature; }
            set
            {
                if (currentHeaterTemperature != value)
                {
                    currentHeaterTemperature = value;
                    NotifyOfPropertyChange(() => this.CurrentHeaterTemperature);
                }
            }
        }

        public string TargetHeaterTemperature
        {
            get { return targetHeaterTemperature; }
            set
            {
                if (targetHeaterTemperature != value)
                {
                    targetHeaterTemperature = value;
                    NotifyOfPropertyChange(() => this.TargetHeaterTemperature);
                }
            }
        }

        public int CtargetHeaterTemperature
        {
            get { return ctargetHeaterTemperature; }
            set
            {
                if (ctargetHeaterTemperature != value)
                {
                    ctargetHeaterTemperature = value;
                    NotifyOfPropertyChange(() => this.CtargetHeaterTemperature);
                }
            }
        }
        #endregion

        #region Heater 온도 세팅

        private int heaterSet1 = 0;
        private int heaterSet2 = 0;
        private int heaterSet3 = 1;


        public int HeaterSet1
        {
            get { return heaterSet1; }
            set
            {
                if (heaterSet1 != value)
                {
                    heaterSet1 = value;
                    NotifyOfPropertyChange(() => this.HeaterSet1);
                }
            }
        }
        public int HeaterSet2
        {
            get { return heaterSet2; }
            set
            {
                if (heaterSet2 != value)
                {
                    heaterSet2 = value;
                    NotifyOfPropertyChange(() => this.HeaterSet2);
                }
            }
        }
        public int HeaterSet3
        {
            get { return heaterSet3; }
            set
            {
                if (heaterSet3 != value)
                {
                    heaterSet3 = value;
                    NotifyOfPropertyChange(() => this.HeaterSet3);
                }
            }
        }

        #endregion


        #region speed 및 거리 number powder number

        private int snumber1 = 0;
        private int snumber2 = 0;
        private int snumber3 = 0;

        private int pnumber1 = 2;
        private int pnumber2 = 0;
        private int pnumber3 = 0;

        private int omaxnumber1 = 0;
        private int omaxnumber2 = 0;
        private int omaxnumber3 = 5;

        private int ominnumber1 = 0;
        private int ominnumber2 = 0;
        private int ominnumber3 = 1;

        private int number1 = 0;
        private int number2 = 0;
        private int number3 = 0;
        private int number4 = 0;
        private int number5 = 0;
        private int number6 = 0;

        private int gNumber1 = 0;
        private int gNumber2 = 0;
        private int gNumber3 = 0;

        

        public int Omaxnumber1
        {
            get { return omaxnumber1; }
            set
            {
                if (omaxnumber1 != value)
                {
                    omaxnumber1 = value;
                    NotifyOfPropertyChange(() => this.Omaxnumber1);
                }

            }
        }
        public int Omaxnumber2
        {
            get { return omaxnumber2; }
            set
            {
                if (omaxnumber2 != value)
                {
                    omaxnumber2 = value;
                    NotifyOfPropertyChange(() => this.Omaxnumber2);
                }

            }
        }
        public int Omaxnumber3
        {
            get { return omaxnumber3; }
            set
            {
                if (omaxnumber3 != value)
                {
                    omaxnumber3 = value;
                    NotifyOfPropertyChange(() => this.Omaxnumber3);
                }

            }
        }

        public int Ominnumber1
        {
            get { return ominnumber1; }
            set
            {
                if (ominnumber1 != value)
                {
                    ominnumber1 = value;
                    NotifyOfPropertyChange(() => this.Ominnumber1);
                }

            }
        }
        public int Ominnumber2
        {
            get { return ominnumber2; }
            set
            {
                if (ominnumber2 != value)
                {
                    ominnumber2 = value;
                    NotifyOfPropertyChange(() => this.Ominnumber2);
                }

            }
        }
        public int Ominnumber3
        {
            get { return ominnumber3; }
            set
            {
                if (ominnumber3 != value)
                {
                    ominnumber3 = value;
                    NotifyOfPropertyChange(() => this.Ominnumber3);
                }

            }
        }

        public int Pnumber1
        {
            get { return pnumber1; }
            set
            {
                if (pnumber1 != value)
                {
                    pnumber1 = value;
                    NotifyOfPropertyChange(() => this.Pnumber1);
                }

            }
        }
        public int Pnumber2
        {
            get { return pnumber2; }
            set
            {
                if (pnumber2 != value)
                {
                    pnumber2 = value;
                    NotifyOfPropertyChange(() => this.Pnumber2);
                }

            }
        }
        public int Pnumber3
        {
            get { return pnumber3; }
            set
            {
                if (pnumber3 != value)
                {
                    pnumber3 = value;
                    NotifyOfPropertyChange(() => this.Pnumber3);
                }

            }
        }

        public int SNumber1
        {
            get { return snumber1; }
            set
            {
                if (snumber1 != value)
                {
                    snumber1 = value;
                    NotifyOfPropertyChange(() => this.SNumber1);
                }

            }
        }
        public int SNumber2
        {
            get { return snumber2; }
            set
            {
                if (snumber2 != value)
                {
                    snumber2 = value;
                    NotifyOfPropertyChange(() => this.SNumber2);
                }

            }
        }
        public int SNumber3
        {
            get { return snumber3; }
            set
            {
                if (snumber3 != value)
                {
                    snumber3 = value;
                    NotifyOfPropertyChange(() => this.SNumber3);
                }

            }
        }

        public int Number1
        {
            get { return number1; }
            set
            {
                if (number1 != value)
                {
                    number1 = value;
                    NotifyOfPropertyChange(() => this.Number1);
                }

            }
        }
        public int Number2
        {
            get { return number2; }
            set
            {
                if (number2 != value)
                {
                    number2 = value;
                    NotifyOfPropertyChange(() => this.Number2);
                }

            }
        }
        public int Number3
        {
            get { return number3; }
            set
            {
                if (number3 != value)
                {
                    number3 = value;
                    NotifyOfPropertyChange(() => this.Number3);
                }

            }
        }
        public int Number4
        {
            get { return number4; }
            set
            {
                if (number4 != value)
                {
                    number4 = value;
                    NotifyOfPropertyChange(() => this.Number4);
                }

            }
        }
        public int Number5
        {
            get { return number5; }
            set
            {
                if (number5 != value)
                {
                    number5 = value;
                    NotifyOfPropertyChange(() => this.Number5);
                }

            }
        }
        public int Number6
        {
            get { return number6; }
            set
            {
                if (number6 != value)
                {
                    number6 = value;
                    NotifyOfPropertyChange(() => this.Number6);
                }

            }
        }
        public int GNumber1
        {
            get { return gNumber1; }
            set
            {
                if (gNumber1 != value)
                {
                    gNumber1 = value;
                    NotifyOfPropertyChange(() => this.GNumber1);
                }

            }
        }
        public int GNumber2
        {
            get { return gNumber2; }
            set
            {
                if (gNumber2 != value)
                {
                    gNumber2 = value;
                    NotifyOfPropertyChange(() => this.GNumber2);
                }

            }
        }
        public int GNumber3
        {
            get { return gNumber3; }
            set
            {
                if (gNumber3 != value)
                {
                    gNumber3 = value;
                    NotifyOfPropertyChange(() => this.GNumber3);
                }

            }
        }
        #endregion

        private string dblPowderSupply1 = "0";
        public string DblPowderSupply1
        {
            get { return dblPowderSupply1; }
            set
            {
                if (dblPowderSupply1 != value)
                {
                    dblPowderSupply1 = value;
                    NotifyOfPropertyChange(() => this.DblPowderSupply1);
                }
            }
        }
        private string dblPowderSupply2 = "0";
        public string DblPowderSupply2
        {
            get { return dblPowderSupply2; }
            set
            {
                if (dblPowderSupply2 != value)
                {
                    dblPowderSupply2 = value;
                    NotifyOfPropertyChange(() => this.DblPowderSupply2);
                }
            }
        }
        private string dblPowderSupply3 = "0";
        public string DblPowderSupply3
        {
            get { return dblPowderSupply3; }
            set
            {
                if (dblPowderSupply3 != value)
                {
                    dblPowderSupply3 = value;
                    NotifyOfPropertyChange(() => this.DblPowderSupply3);
                }
            }
        }
        private string dblPowderSupply4 = "0";
        public string DblPowderSupply4
        {
            get { return dblPowderSupply4; }
            set
            {
                if (dblPowderSupply4 != value)
                {
                    dblPowderSupply4 = value;
                    NotifyOfPropertyChange(() => this.DblPowderSupply4);
                }
            }
        }
        public ICommand UpFilter { get; set; }
        public ICommand DownFilter { get; set; }

        public ICommand Setting3Command { get; set; }
        public ICommand WindowCloseCommand { get; set; }
        public ICommand LEDGloveCommand { get; set; }
        public ICommand LEDChamberCommand { get; set; }

        public ICommand LEDL1Command { get; set; }
        public ICommand LEDL2Command { get; set; }
        public ICommand LEDL3Command { get; set; }
        public ICommand LEDR1Command { get; set; }
        public ICommand LEDR2Command { get; set; }
        public ICommand LEDR3Command { get; set; }
        public ICommand SelArCommand { get; set; }
        public ICommand SelAr1Command { get; set; }
        public ICommand SelN2Command { get; set; }
        public ICommand PumpUpCommand { get; set; }
        public ICommand PumpDownCommand { get; set; }
        public ICommand GasSupplyCommand { get; set; }
        public ICommand LaserControlCommand { get; set; }
        public ICommand GuideBeamCommand { get; set; }

        public ICommand Motor1ResetCommand { get; set; }
        public ICommand Motor2ResetCommand { get; set; }
        public ICommand Motor3ResetCommand { get; set; }
        public ICommand Motor4ResetCommand { get; set; }
        public ICommand Motor5ResetCommand { get; set; }

        public ICommand MGMotor1ResetCommand { get; set; }
        public ICommand MGMotor2ResetCommand { get; set; }
        public ICommand MGMotor3ResetCommand { get; set; }
        public ICommand MGMotor1HomeCommand { get; set; }
        public ICommand MGMotor2HomeCommand { get; set; }
        public ICommand MGMotor3HomeCommand { get; set; }

        public ICommand Motor1MoveCommand { get; set; }
        public ICommand Motor2MoveCommand { get; set; }
        public ICommand Motor3MoveCommand { get; set; }
        public ICommand Motor4MoveCommand { get; set; }
        public ICommand Motor5MoveCommand { get; set; }

        public ICommand Motor1SpeedCommand { get; set; }
        public ICommand Motor2SpeedCommand { get; set; }
        public ICommand Motor3SpeedCommand { get; set; }
        public ICommand Motor4SpeedCommand { get; set; }
        public ICommand Motor5SpeedCommand { get; set; }

        public ICommand Motor1HomeCommand { get; set; }
        public ICommand Motor2HomeCommand { get; set; }
        public ICommand Motor3HomeCommand { get; set; }
        public ICommand Motor4HomeCommand { get; set; }
        public ICommand Motor5HomeCommand { get; set; }

        public ICommand MotorUpCommand { get; set; }
        public ICommand MotorDownCommand { get; set; }
        public ICommand MotorLeftCommand { get; set; }
        public ICommand MotorRightCommand { get; set; }
        public ICommand MotorTwowayCommand { get; set; }
        public ICommand DoorChamberLockCommand { get; set; }
        public ICommand DoorChamberUnLockCommand { get; set; }
        public ICommand DoorGloveLockCommand { get; set; }
        public ImageSource Thumbnail { get; set; }

        #region ICommand
        public ICommand Up1Command { get; set; }
        public ICommand Down1Command { get; set; }
        public ICommand Up2Command { get; set; }
        public ICommand Down2Command { get; set; }
        public ICommand Up3Command { get; set; }
        public ICommand Down3Command { get; set; }
        public ICommand Up4Command { get; set; }
        public ICommand Down4Command { get; set; }
        public ICommand Up5Command { get; set; }
        public ICommand Down5Command { get; set; }
        public ICommand Up6Command { get; set; }
        public ICommand Down6Command { get; set; }

        public ICommand GasUpCommand { get; set; }
        public ICommand GasDownCommand { get; set; }
        public ICommand GasConfirmCommand { get; set; }
        public ICommand GWMaxCommand { get; set; }
        public ICommand GWMinCommand { get; set; }

        public ICommand Down1HeatersetCommand { get; set; }
        public ICommand Down2HeatersetCommand { get; set; }
        public ICommand Down3HeatersetCommand { get; set; }

        public ICommand Up1HeatersetCommand { get; set; }
        public ICommand Up2HeatersetCommand { get; set; }
        public ICommand Up3HeatersetCommand { get; set; }

        public ICommand Up1SpeedCommand { get; set; }
        public ICommand Down1SpeedCommand { get; set; }
        public ICommand Up2SpeedCommand { get; set; }
        public ICommand Down2SpeedCommand { get; set; }
        public ICommand Up3SpeedCommand { get; set; }
        public ICommand Down3SpeedCommand { get; set; }

        public ICommand Up1PowderCommand { get; set; }
        public ICommand Down1PowderCommand { get; set; }
        public ICommand Up2PowderCommand { get; set; }
        public ICommand Down2PowderCommand { get; set; }
        public ICommand Up3PowderCommand { get; set; }
        public ICommand Down3PowderCommand { get; set; }
        public ICommand AirPressureCommand { get; set; }
        public ICommand AutoPumpCommand { get; set; }
        public ICommand SupplyRatioCommand { get; set; }
        public ICommand Up1OxyMaxCommand { get; set; }
        public ICommand Down1OxyMaxCommand { get; set; }
        public ICommand Up2OxyMaxCommand { get; set; }
        public ICommand Down2OxyMaxCommand { get; set; }
        public ICommand Up3OxyMaxCommand { get; set; }
        public ICommand Down3OxyMaxCommand { get; set; }

        public ICommand Up1OxyMinCommand { get; set; }
        public ICommand Down1OxyMinCommand { get; set; }
        public ICommand Up2OxyMinCommand { get; set; }
        public ICommand Down2OxyMinCommand { get; set; }
        public ICommand Up3OxyMinCommand { get; set; }
        public ICommand Down3OxyMinCommand { get; set; }

        //자동공급부 펌프
        public ICommand AutoSupplyCommand { get; set; }
        public ICommand Up1PumpCommand { get; set; }
        public ICommand Up2PumpCommand { get; set; }
        public ICommand Down1PumpCommand { get; set; }
        public ICommand Down2PumpCommand { get; set; }

        #endregion

        public ICommand MonitorFilterCommand { get; set; }
        public ICommand MonitorPumpCommand { get; set; }
        public ICommand MonitorScannerCommand { get; set; }
        public ICommand MonitorCameraShotCommand { get; set; }
        public ICommand MonitorTotalMotor1Command { get; set; }
        public ICommand MonitorTotalMotor2Command { get; set; }
        public ICommand MonitorTotalMotor3Command { get; set; }
        public ICommand MonitorMotorTwowayCommand { get; set; }
        public ICommand MonitorAfterBladeCommand { get; set; }

        public ICommand CameraResfreshCommand { get; set; }
        public ICommand CameraFolderSaveCommand { get; set; }
        public ICommand CameraFolderDeleteCommand { get; set; }

        public ICommand LEDOffCommand { get; set; }
        public ICommand LED1Command { get; set; }
        public ICommand LED2Command { get; set; }
        public ICommand LED3Command { get; set; }

        public ICommand LEDLeftCommand { get; set; }
        public ICommand LEDRightCommand { get; set; }


        public ICommand PrintStartCommand { get; set; }
        public ICommand PrintPauseCommand { get; set; }
        public ICommand PrintResumeCommand { get; set; }
        public ICommand PrintStopCommand { get; set; }

        public ICommand RoomMoveCommand { get; set; }
        public ICommand RoomMoveOtherCommand { get; set; }
        public ICommand RoomMoveYesCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public ICommand PrintDeleteCommand { get; set; }
        public ICommand PrintResetCommand { get; set; }
        public ICommand PrintFinishCommand { get; set; }
        public ICommand PrintFileArrayCommand { get; set; }
        public ICommand PrintFileDeleteCommand { get; set; }

        public ICommand TabClickCommand { get; set; }
        public ICommand OPCWriteCommand { get; set; }

        public ICommand TabRightCommand { get; set; }
        public ICommand TabLeftCommand { get; set; }
        public ICommand PowderSupplyWindowCommand1 { get; set; }
        public ICommand PowderSupplyWindowCommand2 { get; set; }
        public ICommand PowderSupplyWindowCommand3 { get; set; }
        public ICommand PowderApplyCommand { get; set; }
        public ICommand PowderSupplyCommand { get; set; }
        public ICommand OxygenMaxWindowCommand { get; set; }
        public ICommand OxygenMinWindowCommand { get; set; }
        public ICommand OxygenMaxApplyCommand { get; set; }
        public ICommand OxygenMinApplyCommand { get; set; }
        public ICommand HeaterApplyCommand { get; set; }
        public ICommand HeaterSetApplyCommand { get; set; }
        public ICommand HeaterMoveCommand { get; set; }

        public ICommand PumpPlusCommand { get; set; }
        public ICommand PumpPlusCommand2 { get; set; }
        public ICommand PumpMinusCommand { get; set; }
        public ICommand PumpMinusCommand2 { get; set; }


        public ICommand BuildRoom_Focus { get; set; }
        public ICommand Recotor_Focus { get; set; }
        public ICommand Front_Focus { get; set; }
        public ICommand Rear_Focus { get; set; }
        public ICommand Supply_Focus { get; set; }

        public ICommand GasPopupCloseCommand { get; set; }

        public void opcWrite(string btName, DaServerMgt dsm)
        {
            int itemIndex;

            // initialize the return code varaible
            ReturnCode returnCode;


            try
            {
                switch (btName)
                {
                    case "OPCItemSyncWrite0":
                        itemIndex = 0;
                        break;

                    case "OPCItemSyncWrite1":
                        itemIndex = 1;
                        break;

                    case "OPCItemSyncWrite2":
                        itemIndex = 2;
                        break;

                    case "OPCItemSyncWrite3":
                        itemIndex = 3;
                        break;

                    case "OPCItemSyncWrite4":
                        itemIndex = 4;
                        break;

                    case "OPCItemSyncWrite5":
                        itemIndex = 5;
                        break;

                    case "OPCItemSyncWrite6":
                        itemIndex = 6;
                        break;

                    case "OPCItemSyncWrite7":
                        itemIndex = 7;
                        break;

                    case "OPCItemSyncWrite8":
                        itemIndex = 8;
                        break;

                    case "OPCItemSyncWrite9":
                        itemIndex = 9;
                        break;

                    case "OPCItemSyncWrite10":
                        itemIndex = 10;
                        break;

                    case "OPCItemSyncWrite11":
                        itemIndex = 11;
                        break;

                    case "OPCItemSyncWrite12":
                        itemIndex = 12;
                        break;

                    case "OPCItemSyncWrite13":
                        itemIndex = 13;
                        break;

                    case "OPCItemSyncWrite14":
                        itemIndex = 14;
                        break;

                    case "OPCItemSyncWrite15":
                        itemIndex = 15;
                        break;

                    case "OPCItemSyncWrite16":
                        itemIndex = 16;
                        break;

                    case "OPCItemSyncWrite17":
                        itemIndex = 17;
                        break;

                    case "OPCItemSyncWrite18":
                        itemIndex = 18;
                        break;

                    case "OPCItemSyncWrite19":
                        itemIndex = 19;
                        break;

                    case "OPCItemSyncWrite20":
                        itemIndex = 20;
                        break;

                    case "OPCItemSyncWrite21":
                        itemIndex = 21;
                        break;
                    case "OPCItemSyncWrite22":
                        itemIndex = 22;
                        break;
                    case "OPCItemSyncWrite23":
                        itemIndex = 23;
                        break;
                    case "OPCItemSyncWrite24":
                        itemIndex = 24;
                        break;
                    case "OPCItemSyncWrite25":
                        itemIndex = 25;
                        break;
                    case "OPCItemSyncWrite26":
                        itemIndex = 26;
                        break;
                    case "OPCItemSyncWrite27":
                        itemIndex = 27;
                        break;
                    case "OPCItemSyncWrite28":
                        itemIndex = 28;
                        break;
                    case "OPCItemSyncWrite29":
                        itemIndex = 29;
                        break;
                    case "OPCItemSyncWrite30":
                        itemIndex = 30;
                        break;
                    case "OPCItemSyncWrite31":
                        itemIndex = 31;
                        break;
                    case "OPCItemSyncWrite32":
                        itemIndex = 32;
                        break;
                    case "OPCItemSyncWrite33":
                        itemIndex = 33;
                        break;
                    case "OPCItemSyncWrite34":
                        itemIndex = 34;
                        break;
                    case "OPCItemSyncWrite35":
                        itemIndex = 35;
                        break;
                    case "OPCItemSyncWrite36":
                        itemIndex = 36;
                        break;
                    case "OPCItemSyncWrite37":
                        itemIndex = 37;
                        break;
                    case "OPCItemSyncWrite38":
                        itemIndex = 38;
                        break;
                    case "OPCItemSyncWrite39":
                        itemIndex = 39;
                        break;
                    case "OPCItemSyncWrite40":
                        itemIndex = 40;
                        break;
                    case "OPCItemSyncWrite41":
                        itemIndex = 41;
                        break;
                    case "OPCItemSyncWrite42":
                        itemIndex = 42;
                        break;
                    case "OPCItemSyncWrite43":
                        itemIndex = 43;
                        break;
                    case "OPCItemSyncWrite44":
                        itemIndex = 44;
                        break;
                    case "OPCItemSyncWrite45":
                        itemIndex = 45;
                        break;
                    case "OPCItemSyncWrite46":
                        itemIndex = 46;
                        break;
                    case "OPCItemSyncWrite47":
                        itemIndex = 47;
                        break;
                    case "OPCItemSyncWrite48":
                        itemIndex = 48;
                        break;
                    case "OPCItemSyncWrite49":
                        itemIndex = 49;
                        break;
                    case "OPCItemSyncWrite50":
                        itemIndex = 50;
                        break;
                    case "OPCItemSyncWrite51":
                        itemIndex = 51;
                        break;
                    case "OPCItemSyncWrite52":
                        itemIndex = 52;
                        break;
                    case "OPCItemSyncWrite53":
                        itemIndex = 53;
                        break;
                    case "OPCItemSyncWrite54":
                        itemIndex = 54;
                        break;
                    case "OPCItemSyncWrite55":
                        itemIndex = 55;
                        break;
                    case "OPCItemSyncWrite56":
                        itemIndex = 56;
                        break;
                    case "OPCItemSyncWrite57":
                        itemIndex = 57;
                        break;
                    case "OPCItemSyncWrite58":
                        itemIndex = 58;
                        break;
                    case "OPCItemSyncWrite59":
                        itemIndex = 59;
                        break;
                    case "OPCItemSyncWrite60":
                        itemIndex = 60;
                        break;
                    case "OPCItemSyncWrite61":
                        itemIndex = 61;
                        break;
                    case "OPCItemSyncWrite62":
                        itemIndex = 62;
                        break;

                    default:
                        // Failsafe in case someone adds another row of controls
                        // and forgets to add a select case here.
                        return;
                }

                // Define parameters for Write method:

                // The item identifiers array describe the items we wish to write to.
                // We may write to more than one item at a time. However, the GUI in
                // this example is set up so that only one item can be written to at
                // a time.
                ItemIdentifier[] itemIdentifiers = new ItemIdentifier[1];
                itemIdentifiers[0] = new ItemIdentifier();
                itemIdentifiers[0].ItemName = OPCItemNameWriteTextBoxes[itemIndex];
                itemIdentifiers[0].ClientHandle = itemIndex;

                // The itemValues array contains the values we wish to write to the
                // items.
                ItemValue[] itemValues = new ItemValue[1];
                itemValues[0] = new ItemValue();

                //if (itemIndex == 10 || itemIndex == 23 || itemIndex == 24 || itemIndex == 25 || itemIndex == 8 || itemIndex == 4 || itemIndex == 5 || itemIndex == 12 || itemIndex == 30 || itemIndex == 31 || itemIndex == 32 || itemIndex == 20 || itemIndex == 21 || itemIndex == 22 || itemIndex == 13 || itemIndex == 33 || itemIndex == 27 || itemIndex == 28 || itemIndex == 29)
                //{
                //    itemValues[0].Value = System.Convert.ToString(OPCItemWriteValueTextBoxes[itemIndex]);
                //}
                if ( itemIndex == 24 || itemIndex == 25 || itemIndex == 33 || itemIndex == 34 || itemIndex == 41 || itemIndex == 42 || itemIndex == 43 || itemIndex == 60)
                {
                    itemValues[0].Value = System.Convert.ToDouble(OPCItemWriteValueTextBoxes[itemIndex]);
                }
                else if (itemIndex == 52 || itemIndex == 54 || itemIndex == 18 || itemIndex == 0 || itemIndex == 1 || itemIndex == 2 || itemIndex == 3 || itemIndex == 11 || itemIndex == 4 || itemIndex == 7 || itemIndex == 9 || itemIndex == 6 || itemIndex == 20 || itemIndex == 5 || itemIndex == 39 || itemIndex == 12 || itemIndex == 13 || itemIndex == 14 || itemIndex == 15 || itemIndex == 22
                  || itemIndex == 23 || itemIndex == 26 || itemIndex == 27 || itemIndex == 28 || itemIndex == 29 || itemIndex == 30 || itemIndex == 40 || itemIndex == 31 || itemIndex == 55 || itemIndex == 56 || itemIndex == 57 || itemIndex == 58 || itemIndex == 59 || itemIndex == 61 || itemIndex == 62) 
                {
                    itemValues[0].Value = System.Convert.ToString(OPCItemWriteValueTextBoxes[itemIndex]);
                }
                else if (itemIndex == 8)
                {
                    itemValues[0].Value = System.Convert.ToSingle(OPCItemWriteValueTextBoxes[itemIndex]);
                }
                else
                {
                    itemValues[0].Value = System.Convert.ToInt32(OPCItemWriteValueTextBoxes[itemIndex]);
                }


                int TransID = RandomNumber(65535, 1);

                // Call the Write API method:
                returnCode = dsm.WriteAsync(TransID, ref itemIdentifiers, itemValues);

                // Handle result:
                if (returnCode != ReturnCode.SUCCEEDED)
                {
                    MessageBox.Show("Async Write failed with a result of " + System.Convert.ToString(itemIdentifiers[0].ResultID.Code) + "\r\n" + "Description: " + itemIdentifiers[0].ResultID.Description);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Async Write exception. Reason: " + ex.Message);
            }
        }

        private int RandomNumber(int MaxNumber, int MinNumber)
        {
            Random r = new Random(System.DateTime.Now.Millisecond);

            if (MinNumber > MaxNumber)
            {
                int t = MinNumber;
                MinNumber = MaxNumber;
                MaxNumber = t;
            }

            return r.Next(MinNumber, MaxNumber);
        }

    }
}
