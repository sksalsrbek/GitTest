using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
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

using Kepware.ClientAce.OpcDaClient;
using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using LiveCharts.Helpers;
using Microsoft.Win32;
using System.ComponentModel;
using DPT_WPF.ucCommon;
using System.Windows.Media.Animation;
using System.Diagnostics;
using DPT_WPF.ucM270;
using System.Timers;

namespace DPT_WPF
{
    /// <summary>
    /// HMIM270.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HMIM270 : Window
    {
        #region pre-Definition
        DefineValue d;
        DaServerMgt daServerMgt = new DaServerMgt();
        powderSupplyWindow psupply = new powderSupplyWindow();
        private int ledcount = 0;
        private double tempPowderSupply = 0;
        private string tempMaxOxy = "0";
        private string tempMinOxy = "0";
        private string ledGlovecount = "False";
        private string tempMotor1Alram = "0";
        private string tempMotor2Alram = "0";
        private string tempMotor3Alram = "0";
        private string tempMotor4Alram = "0";
        private string tempMotor5Alram = "0";
        private double tempLeft = 0;
        private double bteSeconds = 0;
        private double bteClock = 0;
        private string bteClockTest = "00h:00m:00s";

        private string tempGlovePosition = "1";

        private string tempLaser = "False";
        private string tempGuideBeam = "False";

        private string tempPump = "0";

        private string tempPump1 = "0";
        private string tempPump2 = "0";

        private string chamberCheck = "0";
        private string strLeftDoor = "False";
        private string strRightDoor = "False";
        private string strGloveBottomDoor = "False";
        private string strChamberBottomDoor = "True";
        private string strGloveDoor = "False";
        private string strChamberDoor = "False";
        private double totalFileSize = 0;

        private string fristCheck = "0";
        private string tempMotorPostion = null;
        private int tempFileCount = 0;

        private Boolean bTimer = false;
        private bool _timerRunning = true;
        private bool _timerScannerRunning = true;
        private bool _timerPrintingRunning = false;

        double[] str12Time = new double[12];
        DateTime MachineStartTime;
        DateTime FilterStartTime;
        DateTime PumpStartTime;
        DateTime ScannerStartTime;
        private DispatcherTimer Pumptimer;
        private DispatcherTimer Scannertimer;
        private DispatcherTimer Filtertimer;
        public string tempPrintingProfile = "";
        private bool bPrintingFinsh = false;
        private List<string> printingLog = new List<string>();

        //7일간의 최근 이력 저장
        List<string> strlastWeek = new List<string>();
        string tempLastWeek = "";
        List<string> _strFileName = new List<string>();
        private bool blastweekone = false;

        //Windows
        moveWindow mw = new moveWindow();
        GasWindow gw = new GasWindow();
        speedWindows sw = new speedWindows();
        AutoPumpWindow apw = new AutoPumpWindow();

        //흘러가는 시간
        DateTime ps_StartTime;
        DispatcherTimer psDispatcherTimer = new DispatcherTimer();
        Stopwatch psStopWatch = new Stopwatch();
        string psCurrentTime = string.Empty;
        DateTime psStartTime;
        

        //출력예상 시간 
        DispatcherTimer estDispatcherTimer = new DispatcherTimer();
        Stopwatch estStopWatch = new Stopwatch();
        string estCurrentTime = string.Empty;
        DateTime estStartTime;
        DateTime firstStartTime;
        double fixTime = 0;
        System.Timers.Timer bteTimer = new System.Timers.Timer();
        

        private string[] allCameraFolder;
        private double timeChnage = 0;
        private double timeestChnage = 0;

        private readonly ChartValues<GanttPoint> _values;
        ObservableValue[] observableCount = new ObservableValue[30];
        RowSeries rowseries = new RowSeries();
        List<RowSeries> _listRowSeries = new List<RowSeries>();
        List<string> labels = new List<string>();
        List<string> printingProcess = new List<string>();
        List<string> printingStartTime = new List<string>();
        List<string> printingEndTime = new List<string>();
        List<string> printFinsh = new List<string>();
        private Boolean bnonePrinting = false;
        private BackgroundWorker linethread = new BackgroundWorker();
        

        #endregion

        public HMIM270()
        {
            InitializeComponent();
            #region 로그 및 일주일간 출력기록 임의데이터
            //List<WeekFile> data = new List<WeekFile>();
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //data.Add(new WeekFile() { WeekImage = "1", WeekStartTime = "1", WeekEndTime = "1", WeekWorkTime = "1", WeekFileName = "1", WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
            //lvWeekFile.ItemsSource = data;
            //lvWeekFile.Items.Refresh();

            //List<LogFile> _log1 = new List<LogFile>();
            //ListView log;
            //log = lvLogs;

            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //_log1.Add(new LogFile() { Time = "123", Machine = "456", Work = "789", Image = @"/imgTab/5_log/light-blue.png" });
            //log.ItemsSource = _log1;
            //log.Items.Refresh();
            #endregion
        }

        public HMIM270(DefineValue dv)
        {
            InitializeComponent();
            _timerPrintingRunning = false;


            d = dv;
            this.DataContext = dv;

            cd_FileLoading cdFileLoading = new cd_FileLoading();

            string[] strfile = cdFileLoading.GetFileList(d.FtpName);
            lvFile.ItemsSource = strfile;

            // 이벤트 선언부
            dv.Motor1MoveCommand = new DelegateCommand(this.Motor1Move);
            dv.Motor2MoveCommand = new DelegateCommand(this.Motor2Move);
            dv.Motor3MoveCommand = new DelegateCommand(this.Motor3Move);
            dv.Motor4MoveCommand = new DelegateCommand(this.Motor4Move);
            dv.Motor5MoveCommand = new DelegateCommand(this.Motor5Move);

            dv.Motor1SpeedCommand = new DelegateCommand(this.Motor1Speed);
            dv.Motor2SpeedCommand = new DelegateCommand(this.Motor2Speed);
            dv.Motor3SpeedCommand = new DelegateCommand(this.Motor3Speed);
            dv.Motor4SpeedCommand = new DelegateCommand(this.Motor4Speed);
            dv.Motor5SpeedCommand = new DelegateCommand(this.Motor5Speed);

            dv.PumpPlusCommand = new DelegateCommand(this.PumpPlus);
            dv.PumpPlusCommand2 = new DelegateCommand(this.PumpPlus2);
            dv.PumpMinusCommand = new DelegateCommand(this.PumpMinus);
            dv.PumpMinusCommand2 = new DelegateCommand(this.PumpMinus2);
            dv.BuildRoom_Focus = new DelegateCommand(this.BuildRoomFocus);
            dv.Recotor_Focus = new DelegateCommand(this.RecotorFocus);
            dv.Front_Focus = new DelegateCommand(this.FrontFocus);
            dv.Rear_Focus = new DelegateCommand(this.RearFocus);
            dv.Supply_Focus = new DelegateCommand(this.SupplyFocus);

            dv.GasUpCommand = new DelegateCommand(this.GasUp);
            dv.GasDownCommand = new DelegateCommand(this.GasDown);
            dv.GasConfirmCommand = new DelegateCommand(this.GasConfirm);
            dv.GWMaxCommand = new DelegateCommand(this.GWPopupMax);
            dv.GWMinCommand = new DelegateCommand(this.GWPopupMin);
            dv.GasPopupCloseCommand = new DelegateCommand(this.GPopupClose);

            dv.Up1SpeedCommand = new DelegateCommand(this.SUp1);
            dv.Up2SpeedCommand = new DelegateCommand(this.SUp2);
            dv.Up3SpeedCommand = new DelegateCommand(this.SUp3);
            dv.Down1SpeedCommand = new DelegateCommand(this.SDown1);
            dv.Down2SpeedCommand = new DelegateCommand(this.SDown2);
            dv.Down3SpeedCommand = new DelegateCommand(this.SDown3);

            dv.Up1PowderCommand = new DelegateCommand(this.PUp1);
            dv.Up2PowderCommand = new DelegateCommand(this.PUp2);
            dv.Up3PowderCommand = new DelegateCommand(this.PUp3);
            dv.Down1PowderCommand = new DelegateCommand(this.PDown1);
            dv.Down2PowderCommand = new DelegateCommand(this.PDown2);
            dv.Down3PowderCommand = new DelegateCommand(this.PDown3);
            dv.AirPressureCommand = new DelegateCommand(this.AirPressureSwitch);
            dv.AutoSupplyCommand = new DelegateCommand(this.AutoSupplySwitch);
            dv.SupplyRatioCommand = new DelegateCommand(this.PowderSupplyWindow4);
            ucLogin uclogin = new ucLogin(dv);
            ucCommonM270LED uccommonmgled = new ucCommonM270LED(dv, daServerMgt);
            ucCommonGAS uccommongas = new ucCommonGAS(dv, daServerMgt);

            ucCommonM270TemperHumity ucccommonTemper = new ucCommonM270TemperHumity(dv, daServerMgt);
            ucCommonM270Motor uccommonmotor = new ucCommonM270Motor(dv, daServerMgt);
            uc270Door ucmgdoor = new uc270Door(dv, daServerMgt);

            //ucM270Motor ucmotor = new ucMGMotor(dv, daServerMgt);

            dv.PointLabel = chartPoint =>
               string.Format("67%");

            dv.PointLabel1 = chartPoint =>
            string.Format("100");
            dv.UpFilter = new DelegateCommand(this.Upfilter);
            dv.DownFilter = new DelegateCommand(this.Downfilter);

            dv.Up1Command = new DelegateCommand(this.Up1);
            dv.Up2Command = new DelegateCommand(this.Up2);
            dv.Up3Command = new DelegateCommand(this.Up3);
            dv.Up4Command = new DelegateCommand(this.Up4);
            dv.Up5Command = new DelegateCommand(this.Up5);
            dv.Up6Command = new DelegateCommand(this.Up6);

            dv.Down1Command = new DelegateCommand(this.Down1);
            dv.Down2Command = new DelegateCommand(this.Down2);
            dv.Down3Command = new DelegateCommand(this.Down3);
            dv.Down4Command = new DelegateCommand(this.Down4);
            dv.Down5Command = new DelegateCommand(this.Down5);
            dv.Down6Command = new DelegateCommand(this.Down6);

            dv.MotorUpCommand = new DelegateCommand(this.MotorUp);
            dv.MotorDownCommand = new DelegateCommand(this.MotorDown);
            dv.MotorLeftCommand = new DelegateCommand(this.MotorLeft);
            dv.MotorRightCommand = new DelegateCommand(this.MotorRight);

            dv.Motor1HomeCommand = new DelegateCommand(this.M270Motor1Home);
            dv.Motor2HomeCommand = new DelegateCommand(this.M270Motor2Home);
            dv.Motor3HomeCommand = new DelegateCommand(this.M270Motor3Home);
            dv.Motor4HomeCommand = new DelegateCommand(this.M270Motor4Home);
            dv.Motor5HomeCommand = new DelegateCommand(this.M270Motor5Home);

            dv.DoorChamberLockCommand = new DelegateCommand(this.DoorChamberLock);
            dv.DoorChamberUnLockCommand = new DelegateCommand(this.DoorChamberLock);
            dv.DoorGloveLockCommand = new DelegateCommand(this.DoorGloveLock);

            dv.MotorTwowayCommand = new DelegateCommand(this.MotorTwoway);

            dv.PowderSupplyWindowCommand1 = new DelegateCommand(this.PowderSupplyWindow1);
            dv.PowderSupplyWindowCommand2 = new DelegateCommand(this.PowderSupplyWindow2);
            dv.PowderSupplyWindowCommand3 = new DelegateCommand(this.PowderSupplyWindow3);

            dv.PrintStartCommand = new DelegateCommand(this.PrintStart);
            dv.PrintPauseCommand = new DelegateCommand(this.PrintPause);
            dv.PrintResumeCommand = new DelegateCommand(this.PrintResume);
            dv.PrintStopCommand = new DelegateCommand(this.PrintStop);
            dv.PrintResetCommand = new DelegateCommand(this.PrintReset);
            dv.PrintFinishCommand = new DelegateCommand(this.PrintFinish);
            dv.PrintFileDeleteCommand = new DelegateCommand(this.PrintFileDelete);
            dv.PrintFileArrayCommand = new DelegateCommand(this.PrintFileArray);

            dv.MonitorFilterCommand = new DelegateCommand(this.MonitorFilter);
            dv.MonitorPumpCommand = new DelegateCommand(this.MonitorPump);
            dv.MonitorScannerCommand = new DelegateCommand(this.MonitorScanner);
            dv.MonitorCameraShotCommand = new DelegateCommand(this.MonitorCameraShot);
            dv.MonitorTotalMotor1Command = new DelegateCommand(this.MonitorTotalMotor1);
            dv.MonitorTotalMotor2Command = new DelegateCommand(this.MonitorTotalMotor2);
            dv.MonitorTotalMotor3Command = new DelegateCommand(this.MonitorTotalMotor3);
            dv.MonitorMotorTwowayCommand = new DelegateCommand(this.MonitorMotorTwoway);
            dv.MonitorAfterBladeCommand = new DelegateCommand(this.MonitorAfterBlade);
            dv.CameraResfreshCommand = new DelegateCommand(this.CameraResfresh);
            dv.GuideBeamCommand = new DelegateCommand(this.GuideBeam);
            dv.LaserControlCommand = new DelegateCommand(this.LaserControl);

            //ucCommonRealTime uccommonrealtime = new ucCommonRealTime(dv);

            dv.WindowCloseCommand = new DelegateCommand(this.WindowClose);
            dv.PowderApplyCommand = new DelegateCommand(this.PowderApply);
            dv.PowderSupplyCommand = new DelegateCommand(this.PowderSupplyRatio);

            dv.AutoPumpCommand = new DelegateCommand(this.AutoPumpCommand);
            dv.Up1PumpCommand = new DelegateCommand(this.PumpUp1);
            dv.Down1PumpCommand = new DelegateCommand(this.PumpDown1);
            dv.Up2PumpCommand = new DelegateCommand(this.PumpUp2);
            dv.Down2PumpCommand = new DelegateCommand(this.PumpDown2);

            var now = DateTime.Now;

            _values = new ChartValues<GanttPoint>
            {
                 new GanttPoint(now.Ticks, now.Ticks),
                 //new GanttPoint(now.Ticks, now.Ticks),
            };


            dv.PrintingSeries = new SeriesCollection
            {
               new RowSeries
                {
                    Values = _values,
                    DataLabels = true, Fill = new SolidColorBrush(Color.FromArgb(200, 45, 137, 239)),
                    Stroke = new SolidColorBrush(Color.FromArgb(255, 45, 137, 239)),
                }
            };



            DateTime datesXStatr = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

            //axisPrinting.MaxValue = datesXStatr.Ticks;
            //axisPrinting.MinValue = datesXStatr.Ticks - TimeSpan.FromDays(8).Ticks;
            dv.PrintWeekend = labels.ToArray();
            //axisPrinting.Unit = TimeSpan.TicksPerDay;
            dv.AxisStep = TimeSpan.FromDays(1).Ticks;
            dv.DayofWeek = values => new DateTime((long)values).ToString("MMM월dd일HH시");

            dv.Formatter = value => new DateTime((long)value).ToString("dd MMM");
            dv.PrintingFormatter = x => x.ToString("N2") + "일";
            dv.XPointer = 0;
            dv.YPointer = 0;

            // ChamberOxy 차트
            dv.ChamberOxySeries = new SeriesCollection
                {
                    new LineSeries
                    {
                        AreaLimit = -10,
                        Values = new ChartValues<ObservableValue>
                        {
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4]))
                     }
                    }
                };
            // Laser 차트
            dv.LaserSeries = new SeriesCollection
                {
                    new LineSeries
                    {
                        AreaLimit = -10,
                        Values = new ChartValues<ObservableValue>
                        {
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[7]))
                     }
                    }
                };

            dv.SeriesCollection = new SeriesCollection
            {
                
                //0 --> 챔버 산소농도
                //1 --> 챔버 온도
                //2 --> CPU 사용량
                //3 --> 파우더 잔량
                //4 --> 글로브 산소
                //5 --> 글로브 온도
                //6 --> 레이저 파워
                //7 --> 펌프
                //8 --> 챔버 습도
                //9 --> 글로브 습도
                
             #region 0 --> 챔버 산소농도
                new LineSeries
                {

                     Title = "챔버산소농도(%)",
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[0])),
                     },Fill = new SolidColorBrush(Colors.Transparent), ScalesYAt = 0,
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[0]) }
                },
                #endregion
                
             #region 1 --> 챔버 온도
            new LineSeries
                {
                     Title = "온도(℃)",
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[8]))
                     },Fill = new SolidColorBrush(Colors.Transparent), ScalesYAt = 1,
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) }
                },
                 #endregion

             #region 2 --> 레이저
                new LineSeries
                {
                     Title = "레이저 파워(W)",
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[10]) * 10),
                     },Fill = new SolidColorBrush(Colors.Transparent), ScalesYAt = 2,
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) }
                },
                 #endregion

            };

            dv.SeriesCollection = new SeriesCollection
            {

                //0 --> 챔버 산소농도
                //1 --> 챔버 온도
                //2 --> CPU 사용량
                //3 --> 파우더 잔량
                //4 --> 글로브 산소
                //5 --> 글로브 온도
                //6 --> 레이저 파워
                //7 --> 펌프
                //8 --> 챔버 습도
                //9 --> 글로브 습도

              #region 0 --> 챔버 산소농도
                new LineSeries
                {

                     Title = "챔버산소농도",
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[4])),
                     },Fill = new SolidColorBrush(Colors.Transparent), ScalesYAt = 0
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[0]) }
                },
                #endregion

              #region 1 --> 챔버 온도
                new LineSeries
                {
                     Title = "압력",
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[72]))
                     },Fill = new SolidColorBrush(Colors.Transparent), ScalesYAt = 2
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) }
                },
                 #endregion

              #region 2 --> 챔버습도
                new LineSeries
                {
                     Title = "이슬점",
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[74])),
                     },Fill = new SolidColorBrush(Colors.Transparent), ScalesYAt = 1
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) }
                },
                 #endregion

              #region 3 --> 글로브 습도
                new LineSeries
                {
                     Title = "공압",
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[75])),
                     },Fill = new SolidColorBrush(Colors.Transparent), ScalesYAt = 2
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) }
                },
                 #endregion

              #region 4 --> 글로브 산소농도
                new LineSeries
                {

                     Title = "Gas1압력",
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[69]))
                     },Fill = new SolidColorBrush(Colors.Transparent), ScalesYAt = 2
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]), Convert.ToDouble(dv.OPCItemValueTextBoxes[0]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[0]) }
                },
                #endregion

              #region 5 --> 글로브 온도
                new LineSeries
                {
                     Title = "Gas2압력", Fill = new SolidColorBrush(Colors.Transparent),
                     Values = new ChartValues<ObservableValue>
                        {
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),

                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70])),
                            new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[70]))                     }, ScalesYAt = 2
                    //Values = new ChartValues<double> { Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]), Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) ,Convert.ToDouble(dv.OPCItemValueTextBoxes[8]) }
                }
                 #endregion


            };

            dv.SeriesCollection[0].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[0])));
            dv.SeriesCollection[0].Values.RemoveAt(0);

            #region 글러브산소농도 실시간 변경, 현재 사용하지않음
            /*
            dv.GloveOxySeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44]))
                     }
                 }
            };
            dv.SubGloveOxySeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44])),
                        new ObservableValue(Convert.ToDouble(dv.OPCItemValueTextBoxes[44]))
                     }
                 }
            };
            */
            #endregion

            #region CPU사용량, 현재 사용하지않음

            dv.PointLabel = chartPoint =>
              string.Format("67%");

            dv.PointLabel1 = chartPoint =>
            string.Format("100");
            #endregion

            
            SubscribeToOPCDAServerEvents();
            
            linethread.WorkerReportsProgress = true;

            // 작업 취소 여부
            linethread.WorkerSupportsCancellation = true;

            // 작업 쓰레드 
            linethread.DoWork += new DoWorkEventHandler(linethread_DoWork);

            // 진행률 변경
            linethread.ProgressChanged += new ProgressChangedEventHandler(linethread_ProgressChanged);

            // 작업 완료
            linethread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(linethread_RunWorkerCompleted);

            psDispatcherTimer.Tick += new EventHandler(ps_Tick);
            psDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            estDispatcherTimer.Tick += new EventHandler(est_Tick);
            estDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            bteTimer.Interval = 1000;
            bteTimer.Elapsed += new ElapsedEventHandler(bteTimer_Event);

            // 모터속도 세팅
            d.DblMotor1Speed = "10";
            d.DblMotor2Speed = "100";
            d.DblMotor3Speed = "2";
            d.DblMotor4Speed = "2";
            d.DblMotor5Speed = "1";

            // 0.5초마다 장비에서 데이터를 받아 업데이트 하는 부분
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(500);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            try
                            {
                                //bteTimer.Start();
                                if (bteClock > 0)
                                {
                                    lblPsTime.Content = bteClockTest;
                                }
                                else
                                {
                                    lblPsTime.Content = "00h:00m:00s";
                                }
                                if (d.OPCItemValueTextBoxes[81] == "False")
                                {
                                    _ucCommonGAS.AirValveStatus.Content = "공압 OFF";
                                    _ucM270Pump.imgUpFilter.Visibility = Visibility.Collapsed;
                                    _ucM270Pump.imgDownFilter.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    _ucCommonGAS.AirValveStatus.Content = "공압 ON";
                                }

                                if (d.OPCItemValueTextBoxes[83] == "False")
                                {
                                    AutoStatus.Content = "자동공급 OFF";
                                    
                                }
                                else
                                {
                                   AutoStatus.Content = "자동공급 ON";
                                }

                                if (d.OPCItemValueTextBoxes[79] == "True")
                                {
                                    _ucM270Pump.imgUpFilter.Visibility = Visibility.Visible;
                                    _ucM270Pump.imgDownFilter.Visibility = Visibility.Collapsed;
                                }
                                if (d.OPCItemValueTextBoxes[80] == "True")
                                {
                                    _ucM270Pump.imgUpFilter.Visibility = Visibility.Collapsed;
                                    _ucM270Pump.imgDownFilter.Visibility = Visibility.Visible;
                                }
                                #region M270-GLOVE, 현재 사용하지않음
                                //if (tempGlovePosition != d.OPCItemValueTextBoxes[48])
                                //{
                                //    tempGlovePosition = d.OPCItemValueTextBoxes[48];
                                //    RoomImageSetting();
                                //}
                                #endregion
                                dv.SeriesCollection[0].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[4])));
                                dv.SeriesCollection[0].Values.RemoveAt(0);
                                dv.SeriesCollection[1].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[72])));
                                dv.SeriesCollection[1].Values.RemoveAt(0);
                                dv.SeriesCollection[2].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[74])));
                                dv.SeriesCollection[2].Values.RemoveAt(0);
                                dv.SeriesCollection[3].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[75])));
                                dv.SeriesCollection[3].Values.RemoveAt(0);
                                dv.SeriesCollection[4].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[69])));
                                dv.SeriesCollection[4].Values.RemoveAt(0);
                                dv.SeriesCollection[5].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[70])));
                                dv.SeriesCollection[5].Values.RemoveAt(0);
                                ChamberOxy.Content = (Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[4]), 2)).ToString() + "%";
                                GasPressure.Content = (Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[72]), 1)).ToString() + "bar";
                                ChamberDew.Content = (Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[74]), 1)).ToString() + "℃";
                                AirPressure.Content = (Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[75]), 1)).ToString() + "bar";
                                Gas1Pressure.Content = (Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[69]), 1)).ToString() + "bar";
                                Gas2Pressure.Content = (Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[70]), 1)).ToString() + "bar";
                                WindSpeed.Content = (Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[73]), 1)).ToString() + "m/s";
                                PipeOxy.Content = (Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[84]), 2)).ToString() + "%";



                                #region M270-LED

                                string led1 = d.OPCItemValueTextBoxes[0];
                                string led2 = d.OPCItemValueTextBoxes[1];
                                string led3 = d.OPCItemValueTextBoxes[2];
                                string led4 = d.OPCItemValueTextBoxes[3];

                                bool[] bLLed = { bool.Parse(led1), bool.Parse(led2), bool.Parse(led3), bool.Parse(led4) };
                                Image[] led = { _ucM270LED.imgLightL1, _ucM270LED.imgLightL2, _ucM270LED.imgLightR1, _ucM270LED.imgLightR2 };
                                for (int i = 0; i < bLLed.Length; i++)
                                {
                                    if (bLLed[i])
                                    {
                                        led[i].OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                    }
                                    else
                                    {
                                        led[i].OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                    }
                                }

                                if (bLLed[0] && bLLed[1])
                                {
                                    _ucM270LED.imgLightL3.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                }
                                else
                                {
                                    _ucM270LED.imgLightL3.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                }

                                if (bLLed[2] && bLLed[3])
                                {
                                    _ucM270LED.imgLightR3.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                                }
                                else
                                {
                                    _ucM270LED.imgLightR3.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                }

                                #endregion

                                #region M270-GAS
                                double chamberOxy = Convert.ToDouble(d.OPCItemValueTextBoxes[4]);
                                d.PUChamberOxy = chamberOxy.ToString("N2") + "%";
                                d.ChamberOxySeries[0].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[4])));
                                d.ChamberOxySeries[0].Values.RemoveAt(0);
                                if (d.OPCItemValueTextBoxes[29] == "False")
                                {
                                    var uriSourceN2 = new Uri(@"/ImgTab/2_gas/gas botten.png", UriKind.Relative);
                                    _ucCommonGAS.imgGasSupply.Source = new BitmapImage(uriSourceN2);
                                }
                                else
                                {
                                    var uriSourceN2 = new Uri(@"/ImgTab/2_gas/gas bottenOn4.png", UriKind.Relative);
                                    _ucCommonGAS.imgGasSupply.Source = new BitmapImage(uriSourceN2);
                                }
                                

                                //dv.SeriesCollection[0].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[4])));
                                //dv.SeriesCollection[0].Values.RemoveAt(0);
                                //double gloveOxy = Convert.ToDouble(d.OPCItemValueTextBoxes[5]);
                                //d.PUGloveOxy = gloveOxy.ToString("N2") + "%";
                                //d.GloveOxySeries[0].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[5])));
                                //d.GloveOxySeries[0].Values.RemoveAt(0);
                                //d.SubGloveOxySeries[0].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[5])));
                                //d.SubGloveOxySeries[0].Values.RemoveAt(0);

                                //dv.SeriesCollection[4].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[5])));
                                //dv.SeriesCollection[4].Values.RemoveAt(0);

                                //double chamberSubOxy = Convert.ToDouble(d.OPCItemValueTextBoxes[4]);
                                //d.PUChamberSubOxy = chamberSubOxy.ToString("N2") + "%";
                                //d.SubChamberOxySeries[0].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[4])));
                                //d.SubChamberOxySeries[0].Values.RemoveAt(0);

                                d.DblGasFlow = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[26]), 2);
                                d.DblAirFlow = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[76]), 2);
                                if (d.OPCItemValueTextBoxes[29] == "False")
                                {
                                    d.StrMaxOxy = "0.1";
                                    d.StrMinOxy = "0.05";
                                }
                                
                                else
                                {
                                    if (d.OPCItemValueTextBoxes[27] != tempMaxOxy)
                                    {
                                        d.StrMaxOxy = d.OPCItemValueTextBoxes[27];
                                        tempMaxOxy = d.GasMinValue;
                                    }

                                    if (d.OPCItemValueTextBoxes[28] != tempMinOxy)
                                    {
                                        d.StrMinOxy = d.OPCItemValueTextBoxes[28];
                                        tempMinOxy = d.GasMinValue;
                                    }
                                }
                                
                                if (d.OPCItemValueTextBoxes[68]=="True")
                                {
                                    var uriSourceN2 = new Uri(@"/imgTab/2_gas/off.png", UriKind.Relative);
                                    var uriSourceAr = new Uri(@"/imgTab/2_gas/on.png", UriKind.Relative);
                                    _ucCommonGAS.btnImgN2.Source = new BitmapImage(uriSourceN2);
                                    _ucCommonGAS.btnImgAr.Source = new BitmapImage(uriSourceAr);
                                }
                               
                                if (d.OPCItemValueTextBoxes[67]=="True")
                                {

                                    var uriSourceN2 = new Uri(@"/imgTab/2_gas/on.png", UriKind.Relative);
                                    var uriSourceAr = new Uri(@"/imgTab/2_gas/off.png", UriKind.Relative);
                                    _ucCommonGAS.btnImgN2.Source = new BitmapImage(uriSourceN2);
                                    _ucCommonGAS.btnImgAr.Source = new BitmapImage(uriSourceAr);
                                }

                                

                                #endregion

                                #region M270-PUMP
                                if (tempPump != d.OPCItemValueTextBoxes[5])
                                {
                                    d.StrPump = d.OPCItemValueTextBoxes[5];
                                    tempPump = d.OPCItemValueTextBoxes[5];
                                    _ucM270Pump.lblPump.Content = Convert.ToDouble(tempPump);
                                    _ucM270Pump.pumpGage.Value = Convert.ToDouble(tempPump);
                                    
                                }
                                
                                #endregion

                                #region M270-Temperature,Humity
                                double tempTemper = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[31]));
                                d.IntTemperature = Convert.ToInt32(tempTemper);
                                d.StrTemperature = Convert.ToString(d.IntTemperature) + "°C";
                                


                                //dv.SeriesCollection[1].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[31])));
                                //dv.SeriesCollection[1].Values.RemoveAt(0);

                                ////double tempTemperGlove = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[49]));
                                ////d.IntTemperatureGlove = Convert.ToInt32(tempTemperGlove);
                                ////d.StrTemperatureGlove = Convert.ToString(d.IntTemperatureGlove) + "°C";
                                
                                ////dv.SeriesCollection[5].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[49])));
                                ////dv.SeriesCollection[5].Values.RemoveAt(0);

                                double tempHumidity = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[85]));
                                d.IntHumity = Convert.ToInt32(tempHumidity);
                                d.StrHumity = Convert.ToString(d.IntHumity) + "°C";
                                PipeTemper.Content = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[85]), 1);
                                //dv.SeriesCollection[2].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[69])));
                                //dv.SeriesCollection[2].Values.RemoveAt(0);


                                double tempDewPoint = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[70]));
                                d.IntDewPoint = Convert.ToInt32(tempDewPoint);
                                d.StrDewPoint = Convert.ToString(d.IntHumity) + "°C";


                                double tempPressure = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[71]));
                                d.IntPressure = Convert.ToInt32(tempPressure);
                                d.StrPressure = Convert.ToString(d.IntPressure);
                                #endregion
                                
                                #region M270-LASER
                                if (d.OPCItemValueTextBoxes[6] != tempLaser)
                                {
                                    _ucM270Laser.IMGlaser_on.Visibility = Visibility.Collapsed;
                                    _ucM270Laser.IMGlaser_Power.Visibility = Visibility.Collapsed;

                                    tempLaser = d.OPCItemValueTextBoxes[6];

                                    Visibility laser_Visibility = Visibility.Collapsed;

                                    Uri uriSourceLaser = new Uri(@"/imgTab/1_home/laser/power_btn.png", UriKind.Relative);
                                    if (tempLaser == "False")
                                    {
                                        laser_Visibility = Visibility.Collapsed;
                                        uriSourceLaser = new Uri(@"/imgTab/1_home/laser/power_btn.png", UriKind.Relative);
                                    }
                                    else
                                    {
                                        laser_Visibility = Visibility.Visible;
                                        uriSourceLaser = new Uri(@"/imgTab/1_home/laser/power on_btn.png", UriKind.Relative);
                                    }

                                    _ucM270Laser.IMGlaser_on.Visibility = laser_Visibility;
                                    _ucM270Laser.IMGlaser_Power.Visibility = laser_Visibility;

                                    btn_laser_power.Source = new BitmapImage(uriSourceLaser);
                                }
                                

                                if (d.OPCItemValueTextBoxes[8] != tempGuideBeam)
                                {
                                    _ucM270Laser.IMGlaser_on.Visibility = Visibility.Collapsed;
                                    _ucM270Laser.IMGlaser_Power.Visibility = Visibility.Collapsed;

                                    tempGuideBeam = d.OPCItemValueTextBoxes[8];

                                    Visibility laser_Visibility = Visibility.Collapsed;

                                    Uri uriSourceGuideBeam = new Uri(@"/imgTab/1_home/laser/laser_btn.png", UriKind.Relative);
                                    if (tempGuideBeam == "False")
                                    {
                                        laser_Visibility = Visibility.Collapsed;
                                        uriSourceGuideBeam = new Uri(@"/imgTab/1_home/laser/laser_btn.png", UriKind.Relative);
                                    }
                                    else
                                    {
                                        laser_Visibility = Visibility.Visible;
                                        uriSourceGuideBeam = new Uri(@"/imgTab/1_home/laser/laser on_btn.png", UriKind.Relative);

                                    }
                                    
                                    _ucM270Laser.IMGlaser_on.Visibility = laser_Visibility;
                                    _ucM270Laser.IMGlaser_Power.Visibility = laser_Visibility;

                                    btn_Guide_Beam.Source = new BitmapImage(uriSourceGuideBeam);
                                    
                                }

                                //dv.SeriesCollection[6].Values.Add(new ObservableValue(Convert.ToDouble(d.OPCItemValueTextBoxes[51])));
                                //dv.SeriesCollection[6].Values.RemoveAt(0);
                                #endregion

                                #region 상단 실시간 레이저 파워
                                //20200730 수정
                                if (d.OPCItemValueTextBoxes[6] == "True")
                                {
                                    double OPCItemValueTextBoxes = Convert.ToDouble(dv.OPCItemValueTextBoxes[7]);
                                    //double OPCItemValueTextBoxes = 11;
                                    double W = 0;

                                    double[] Rise = {1.5, 1.7, 1.9, 2.1, 2.3, 2.5, 2.7, 2.9, 3.1, 3.3, 3.5, 3.7, 3.9, 4.1, 4.3, 4.5, 4.7, 4.9, 5.1, 5.3, 5.5, 5.7, 5.9, 6.1,
                                        6.3, 6.5, 6.7, 6.9, 7.1, 7.3, 7.5, 7.7, 7.9, 8.1, 8.3, 8.5, 8.7, 8.9, 9.1, 9.3, 9.5, 9.7, 9.9, 10};

                                    double[] arr_X = {50, 49.5, 49.5, 50, 50, 52.5, 50, 49.5, 49.5, 50.5, 49.5, 51, 50, 49, 50, 49, 50, 49.5, 50, 49.5, 50, 48.5, 50, 49.5, 48.5, 48, 49.5, 47, 48,
                                     48.5, 46, 48, 46.5, 47, 46.5, 46, 46.5, 47.5, 49, 40.5, 30};

                                    double[] arr_Y = {-40.6, -39.75, -39.75, -40.8, -40.8, -47.05, -40.3, -38.85, -38.85, -42.15, -38.65, -44.2, -40.3, -36.2, -40.5, -36, -40.7, -38.25, -40.8, -38.15, -40.9,
                                     -32.35, -41.2, -38.15, -35, -38.25, -31.55, -28.1, -38.75, -20.5, -28, -31.85, -12.1, -28.3, -15.85, -20.1, -15.75, -11.3, -15.85, -25.15, -39.4, 43.05, 147};

                                    if (OPCItemValueTextBoxes >= 1.5 && OPCItemValueTextBoxes < 10)
                                    {
                                        for (int i = 0; i < Rise.Length; i++)
                                        {
                                            if (OPCItemValueTextBoxes >= Rise[i] && OPCItemValueTextBoxes < Rise[i + 1])
                                            {
                                                W = (OPCItemValueTextBoxes * arr_X[i]) + arr_Y[i];

                                                //d.LaserSeries[0].Values.Add(new ObservableValue(Math.Round(W, 2)));
                                                //d.LaserSeries[0].Values.RemoveAt(0);
                                                W = Math.Round(W, 2);
                                                LaserPowerW.Text = W.ToString("N1") + "W";
                                            }
                                        }
                                    }
                                    else if (OPCItemValueTextBoxes == 10)
                                    {

                                        //d.LaserSeries[0].Values.Add(new ObservableValue(368.8));
                                        //d.LaserSeries[0].Values.RemoveAt(0);
                                        LaserPowerW.Text = "447W";
                                    }
                                    else
                                    {
                                        //d.LaserSeries[0].Values.Add(new ObservableValue(Convert.ToDouble("0")));
                                        //d.LaserSeries[0].Values.RemoveAt(0);
                                        LaserPowerW.Text = "0W";
                                    }
                                }
                                else
                                {
                                    //d.LaserSeries[0].Values.Add(new ObservableValue(Convert.ToDouble("0")));
                                    //d.LaserSeries[0].Values.RemoveAt(0);
                                    LaserPowerW.Text = "0W";
                                }

                                #endregion

                                #region M270-MOTOR
                                //MG-MOTOR POSITION
                                d.DblMotor1Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[10]), 3);
                                d.DblMotor2Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[11]), 3);
                                d.DblMotor3Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[12]), 3);
                                d.DblMotor4Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[13]), 3);
                                d.DblMotor5Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[14]), 3);

                                //d.DblMotor1AllDistance = d.OPCItemValueTextBoxes[46] + "m";
                                //d.DblMotor2AllDistance = d.OPCItemValueTextBoxes[47] + "m";
                                //d.DblMotor3AllDistance = d.OPCItemValueTextBoxes[48] + "m";
                                //d.DblMotor4AllDistance = d.OPCItemValueTextBoxes[71] + "m";
                                //d.DblMotor5AllDistance = d.OPCItemValueTextBoxes[72] + "m";

                                dv.PULeftHeight = Convert.ToString(310 - d.DblMotor2Position) + "mm";
                                double tempPuLeft = ((310 - d.DblMotor2Position) * 100) / 310; //수정필요
                                dv.PUPowderLeft = tempPuLeft.ToString("N2") + "%";

                                int powderBorder = Convert.ToInt32(tempPuLeft / 20);


                                if (powderBorder != tempLeft)
                                {
                                    tempLeft = powderBorder;
                                    if (tempLeft == 0)
                                    {
                                        ucccommonTemper.PowderFLeft.Height = 0;
                                        ucccommonTemper.PowderRLeft.Height = 0;
                                        ucccommonTemper.PowderSupplyIm.Height = 120;

                                    }
                                    else if (tempLeft == 1)
                                    {
                                        ucccommonTemper.PowderFLeft.Height = 24;
                                        ucccommonTemper.PowderRLeft.Height = 24;
                                        ucccommonTemper.PowderSupplyIm.Height = 96;
                                    }
                                    else if (tempLeft == 2)
                                    {
                                        ucccommonTemper.PowderFLeft.Height = 48;
                                        ucccommonTemper.PowderRLeft.Height = 48;
                                        ucccommonTemper.PowderSupplyIm.Height = 72;
                                    }
                                    else if (tempLeft == 3)
                                    {
                                        ucccommonTemper.PowderFLeft.Height = 72;
                                        ucccommonTemper.PowderRLeft.Height = 72;
                                        ucccommonTemper.PowderSupplyIm.Height = 48;

                                    }
                                    else if (tempLeft == 4)
                                    {
                                        ucccommonTemper.PowderFLeft.Height = 96;
                                        ucccommonTemper.PowderRLeft.Height = 96;
                                        ucccommonTemper.PowderSupplyIm.Height = 24;
                                    }
                                    else
                                    {
                                        ucccommonTemper.PowderFLeft.Height = 120;
                                        ucccommonTemper.PowderRLeft.Height = 120;
                                        ucccommonTemper.PowderSupplyIm.Height = 0;
                                    }

                                }


                                #region MOTOR 알람
                                string motor1Alram = d.OPCItemValueTextBoxes[15];
                                string motor2Alram = d.OPCItemValueTextBoxes[16];
                                string motor3Alram = d.OPCItemValueTextBoxes[17];
                                string motor4Alram = d.OPCItemValueTextBoxes[18];
                                string motor5Alram = d.OPCItemValueTextBoxes[19];

                                if (tempMotor1Alram != motor1Alram)
                                {
                                    tempMotor1Alram = motor1Alram;
                                    Uri uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_alram.png", UriKind.Relative);
                                    Uri uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);

                                    if (tempMotor1Alram != "2")
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_alram.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);
                                    }
                                    else
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_alram_on.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/motor_btn_reset.png", UriKind.Relative);
                                    }
                                    _ucCommonM270Motor.btnimgAlram1.Source = new BitmapImage(uriSourceAlram);
                                    _ucCommonM270Motor.btnimgReset1.Source = new BitmapImage(uriSourceReset);
                                }

                                if (tempMotor2Alram != motor2Alram)
                                {
                                    tempMotor2Alram = motor2Alram;
                                    Uri uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_alram.png", UriKind.Relative);
                                    Uri uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);

                                    if (tempMotor2Alram != "2")
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_alram.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);
                                    }
                                    else
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_alram_on.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/motor_btn_reset.png", UriKind.Relative);

                                    }
                                    _ucCommonM270Motor.btnimgAlram2.Source = new BitmapImage(uriSourceAlram);
                                    _ucCommonM270Motor.btnimgReset2.Source = new BitmapImage(uriSourceReset);
                                }
                                if (tempMotor3Alram != motor3Alram)
                                {
                                    tempMotor3Alram = motor3Alram;
                                    Uri uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram.png", UriKind.Relative);
                                    Uri uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);

                                    if (tempMotor3Alram != "2")
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);
                                    }
                                    else
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram_on.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/motor_btn_reset.png", UriKind.Relative);

                                    }
                                    _ucCommonM270Motor.btnimgAlram3.Source = new BitmapImage(uriSourceAlram);
                                    _ucCommonM270Motor.btnimgReset3.Source = new BitmapImage(uriSourceReset);
                                }

                                if (tempMotor4Alram != motor4Alram)
                                {
                                    tempMotor4Alram = motor4Alram;
                                    Uri uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram.png", UriKind.Relative);
                                    Uri uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);

                                    if (tempMotor4Alram != "2")
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);
                                    }
                                    else
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram_on.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/motor_btn_reset.png", UriKind.Relative);

                                    }
                                    _ucCommonM270Motor.btnimgAlram4.Source = new BitmapImage(uriSourceAlram);
                                    _ucCommonM270Motor.btnimgReset4.Source = new BitmapImage(uriSourceReset);
                                }

                                if (tempMotor5Alram != motor5Alram)
                                {
                                    tempMotor5Alram = motor5Alram;
                                    Uri uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram.png", UriKind.Relative);
                                    Uri uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);

                                    if (tempMotor5Alram != "2")
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/reset.png", UriKind.Relative);
                                    }
                                    else
                                    {
                                        uriSourceAlram = new Uri(@"/imgTab/4_moter/build-feed room/motor_Alram_on.png", UriKind.Relative);
                                        uriSourceReset = new Uri(@"/imgTab/4_moter/build-feed room/motor_btn_reset.png", UriKind.Relative);

                                    }
                                    _ucCommonM270Motor.btnimgAlram5.Source = new BitmapImage(uriSourceAlram);
                                    _ucCommonM270Motor.btnimgReset5.Source = new BitmapImage(uriSourceReset);
                                }

                                #endregion

                                #region MG-MOTOR POSTION ANMATION, 현재 사용하지않음
                                //tempMotorPostion = d.OPCItemValueTextBoxes[13];
                                //if (tempMotorPostion != null && fristCheck == "0")
                                //{

                                //    //Motor1SetPostion();
                                //    //Motor2SetPostion();
                                //    //Motor3SetPostion();


                                //    fristCheck = "1";
                                //}
                                #endregion
                                


                                #endregion

                                #region M270-DOOR

                                #region 챔버 문 제어
                                //챔버 라킹부분

                                if (d.OPCItemValueTextBoxes[44] == "False")
                                {
                                    _ucCommonM270Door.imgChamberDoorLock.Visibility = Visibility.Visible;
                                    _ucCommonM270Door.imgChamberDoorUnLock.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    _ucCommonM270Door.imgChamberDoorLock.Visibility = Visibility.Collapsed; //  = new BitmapImage(uriSourceDoorcheck);
                                    _ucCommonM270Door.imgChamberDoorUnLock.Visibility = Visibility.Visible;

                                }
                                
                                //도어체킹 부분
                                if (d.OPCItemValueTextBoxes[44] == "False")
                                {
                                    var uriSourceDoor = new Uri(@"/imgTab/1_home/chamber door/unlock_on.png", UriKind.Relative);
                                    _ucM270Door.imgChamberLocking.Source = new BitmapImage(uriSourceDoor);

                                }
                                else
                                {
                                    
                                    var uriSourceDoor = new Uri(@"/imgTab/1_home/chamber door/lock_on.png", UriKind.Relative);
                                    _ucM270Door.imgChamberLocking.Source = new BitmapImage(uriSourceDoor);
                                }
                                
                                #endregion

                                #endregion

                                #region M270-PRINTING

                                if (d.OPCItemValueTextBoxes[25] == "Received: {\"message\":\"Job.Pause\"}")//Lua
                                {
                                    bteTimer.Stop();
                                    imgPrintStart.Visibility = Visibility.Collapsed;
                                    imgPrintResume.Visibility = Visibility.Visible;
                                    imgPrintPause.Visibility = Visibility.Collapsed;
                                    var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_disable.png", UriKind.Relative);
                                    imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                    var uriSourcestop = new Uri(@"/imgTab/6_printing/print_btn_stop_enable.png", UriKind.Relative);
                                    imgPrintStop.Visibility = Visibility.Visible;
                                    imgPrintStop.Source = new BitmapImage(uriSourcestop);
                                    imgPrintFinish.Visibility = Visibility.Collapsed;
                                    if (estStopWatch.IsRunning)
                                    {
                                        estStopWatch.Stop();
                                    }
                                }
                                else if (d.OPCItemValueTextBoxes[25] == "Received: {\"message\":\"Job.Free\"}")//Lua
                                {
                                    bteTimer.Stop();
                                    bteSeconds = 0;
                                    lblPsTime.Content = "00h : 00m : 00s";
                                    var uriSourcestop = new Uri(@"/imgTab/6_printing/print_btn_stop_disable.png", UriKind.Relative);
                                    imgPrintStop.Source = new BitmapImage(uriSourcestop);
                                    psStopWatch.Reset();
                                    estStopWatch.Reset();
                                    psDispatcherTimer.Stop();
                                    estDispatcherTimer.Stop();
                                    imgPrintStop.Visibility = Visibility.Visible;
                                    imgPrintFinish.Visibility = Visibility.Collapsed;
                                    var uriSourcestart = new Uri(@"/imgTab/6_printing/print_btn_pause_disable.png", UriKind.Relative);
                                    imgPrintStart.Visibility = Visibility.Visible;
                                    imgPrintStart.Source = new BitmapImage(uriSourcestart);
                                    var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_disable.png", UriKind.Relative);
                                    imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                    imgPrintPause.Visibility = Visibility.Visible;
                                }
                                else if (d.OPCItemValueTextBoxes[25].Contains("Received: {\"message\":\"Job.Stop\""))//}")//Lua
                                {
                                    bteTimer.Stop();
                                    psStopWatch.Stop();
                                    estStopWatch.Stop();
                                    psDispatcherTimer.Stop();
                                    estDispatcherTimer.Stop();
                                    imgPrintFinish.Visibility = Visibility.Visible;
                                    //imgPrintStart.Visibility = Visibility.Visible;
                                    imgPrintStop.Visibility = Visibility.Collapsed;
                                    imgPrintResume.Visibility = Visibility.Collapsed;
                                    imgPrintStart.Visibility = Visibility.Collapsed;
                                    var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_enable.png", UriKind.Relative);
                                    imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                    imgPrintPause.Visibility = Visibility.Collapsed;
                                    //KBJ보면서 확인
                                    //imgPrintStart.Visibility = Visibility.Collapsed;
                                    //imgPrintStop.Visibility = Visibility.Collapsed;
                                    //imgPrintPause.Visibility = Visibility.Collapsed;

                                    //PrintFree();
                                    //d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Job.Free\"}";
                                    //d.opcWrite("OPCItemSyncWrite14", daServerMgt);
                                }
                                else if (d.OPCItemValueTextBoxes[25].Contains("Received: {\"message\":\"Job.Start\""))//}")
                                {
                                    bteTimer.Start();
                                    imgPrintStart.Visibility = Visibility.Visible;
                                    var uriSourcestart = new Uri(@"/imgTab/6_printing/print_btn_pause_disable.png", UriKind.Relative);
                                    imgPrintStart.Source = new BitmapImage(uriSourcestart);
                                    imgPrintPause.Visibility = Visibility.Visible;
                                    var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_enable.png", UriKind.Relative);
                                    imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                    var uriSourcestop = new Uri(@"/imgTab/6_printing/print_btn_stop_enable.png", UriKind.Relative);
                                    imgPrintStop.Visibility = Visibility.Visible;
                                    imgPrintStop.Source = new BitmapImage(uriSourcestop);
                                    imgPrintResume.Visibility = Visibility.Collapsed;
                                    imgPrintFinish.Visibility = Visibility.Collapsed;
                                }
                                else if (d.OPCItemValueTextBoxes[25] == "Received: {\"message\":\"Job.Resume\"}")
                                {
                                    bteTimer.Start();
                                    var uriSourcestart = new Uri(@"/imgTab/6_printing/print_btn_pause_disable.png", UriKind.Relative);
                                    imgPrintStart.Visibility = Visibility.Visible;
                                    imgPrintStart.Source = new BitmapImage(uriSourcestart);
                                    imgPrintResume.Visibility = Visibility.Collapsed;
                                    imgPrintFinish.Visibility = Visibility.Collapsed;
                                    var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_enable.png", UriKind.Relative);
                                    imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                    var uriSourcestop = new Uri(@"/imgTab/6_printing/print_btn_stop_enable.png", UriKind.Relative);
                                    imgPrintStop.Visibility = Visibility.Visible;
                                    imgPrintStop.Source = new BitmapImage(uriSourcestop);

                                }

                                else
                                {

                                }

                                


                                if (d.OPCItemValueTextBoxes[23] == "4" || d.OPCItemValueTextBoxes[23] == "2" || d.OPCItemValueTextBoxes[23] == "5")
                                {

                                    if (lvFile.SelectedItem == null)
                                    {
                                        int templvCount = 0;
                                        foreach (object o in lvFile.Items)
                                        {
                                            string[] spstring = d.OPCItemValueTextBoxes[24].Split('/');
                                            templvCount++;
                                            if (o.ToString() == spstring[0])
                                            {
                                                lvFile.SelectedIndex = templvCount - 1;
                                                break;
                                            }
                                        }
                                    }



                                    d.StrPrintStaus = "출력중";
                                    double totalLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[22]);
                                    double currentLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[21]);
                                    slLayer1.Value = Convert.ToInt32(currentLayer);
                                    txtCurrentLayer.Text = Convert.ToString(currentLayer);
                                    double printPro = Math.Truncate((currentLayer / totalLayer) * 100);
                                    d.StrPrintPercetage = Convert.ToString(printPro) + "%";

                                    var uriSourceprint = new Uri(@"/imgTab/6_printing/print_btn_start_disable.png", UriKind.Relative);
                                    //var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_enable.png", UriKind.Relative);
                                    var uriSourcestop = new Uri(@"/imgTab/6_printing/print_btn_stop_enable.png", UriKind.Relative);
                                    estStopWatch.Start();
                                    estDispatcherTimer.Start();
                                    imgPrintStart.Source = new BitmapImage(uriSourceprint);
                                    //imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                    imgPrintStop.Source = new BitmapImage(uriSourcestop);
                                    imgPrintFinish.Visibility = Visibility.Visible;

                                    printGauge.Value = printPro;

                                }
                                else if (d.OPCItemValueTextBoxes[23] == "0")
                                {
                                    double totalLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[22]);
                                    double currentLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[21]);
                                    if (totalLayer == currentLayer && totalLayer != 0)
                                    {
                                        d.StrPrintStaus = "출력완료";
                                        var uriSourceprint = new Uri(@"/imgTab/6_printing/print_btn_start_disable.png", UriKind.Relative);
                                        var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_disable.png", UriKind.Relative);
                                        psStopWatch.Stop();
                                        estStopWatch.Stop();
                                        psDispatcherTimer.Stop();
                                        estDispatcherTimer.Stop();
                                        printGauge.Value = 100;
                                        d.StrPrintPercetage = "100%";
                                        imgPrintStart.Source = new BitmapImage(uriSourceprint);
                                        imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                        imgPrintStop.Visibility = Visibility.Collapsed;
                                        imgPrintFinish.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        d.StrPrintStaus = "출력전";
                                        printGauge.Value = 0;
                                        d.StrPrintPercetage = "0%";
                                        lbltime.Text = "00h : 00m : 00s";
                                        //lblPsTime.Content = "00h : 00m : 00s";
                                        psStopWatch.Stop();
                                        estStopWatch.Stop();
                                        psDispatcherTimer.Stop();
                                        estDispatcherTimer.Stop();
                                        var uriSourceprint = new Uri(@"/imgTab/6_printing/print_btn_start_enable.png", UriKind.Relative);
                                        var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_disable.png", UriKind.Relative);
                                        var uriSourcestop = new Uri(@"/imgTab/6_printing/print_btn_stop_disable.png", UriKind.Relative);

                                        imgPrintStart.Source = new BitmapImage(uriSourceprint);
                                        imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                        imgPrintStop.Source = new BitmapImage(uriSourcestop);
                                        imgPrintFinish.Visibility = Visibility.Collapsed;

                                        if (lvFile.SelectedItem == null)
                                        {
                                            int templvCount = 0;
                                            foreach (object o in lvFile.Items)
                                            {
                                                string[] spstring = d.OPCItemValueTextBoxes[24].Split('/');
                                                templvCount++;
                                                if (o.ToString() == spstring[0])
                                                {
                                                    lvFile.SelectedIndex = templvCount - 1;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                }
                                else if (d.OPCItemValueTextBoxes[23] == "7")
                                {
                                    if (lvFile.SelectedItem == null)
                                    {
                                        int templvCount = 0;
                                        foreach (object o in lvFile.Items)
                                        {
                                            string[] spstring = d.OPCItemValueTextBoxes[24].Split('/');
                                            templvCount++;
                                            if (o.ToString() == spstring[0])
                                            {
                                                lvFile.SelectedIndex = templvCount - 1;
                                                break;
                                            }
                                        }
                                    }
                                    
                                    double totalLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[22]);
                                    double currentLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[21]);
                                    if (totalLayer == currentLayer && totalLayer != 0)
                                    {
                                        d.StrPrintStaus = "출력완료";
                                        var uriSourceprint = new Uri(@"/imgPrinting/btn_start.png", UriKind.Relative);
                                        var uriSourcepause = new Uri(@"/imgPrinting/imgPauseDisable.png", UriKind.Relative);
                                        printGauge.Value = 100;
                                        d.StrPrintPercetage = "100%";
                                        psStopWatch.Stop();
                                        estStopWatch.Stop();
                                        psDispatcherTimer.Stop();
                                        estDispatcherTimer.Stop();
                                        imgPrintStart.Source = new BitmapImage(uriSourceprint);
                                        imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                        imgPrintStop.Visibility = Visibility.Collapsed;
                                        imgPrintFinish.Visibility = Visibility.Visible;
                                    }


                                }
                                else
                                {

                                }
                                
                                #endregion

                                #region MG-CPU, 현재 사용하지않음
                                //int _tempFilecount = Convert.ToInt32(lvFile.Items.Count);

                                //if (tempFileCount != _tempFilecount)
                                //{
                                //    tempFileCount = _tempFilecount;
                                //    dv.Values = new ChartValues<ObservableValue> { new ObservableValue(_tempFilecount) };
                                //    dv.Minusvalues = new ChartValues<ObservableValue> { new ObservableValue(30 - (_tempFilecount)) };
                                //    int temp = Convert.ToInt32(30 / _tempFilecount);
                                //    d.PointLabel = chartPoint => string.Format(Convert.ToString(temp) + "%");
                                //    d.PUCPUuse = Convert.ToString(temp) + "%";
                                //}

                                #endregion

                                #region  산소센서 고장여부, Empty

                                #endregion

                                #region 파우더 잔량, 수정필요
                                double left = 100 - ((Convert.ToDouble(d.OPCItemValueTextBoxes[10]) / 106) * 100);
                                d.PUPowderLeft = left.ToString("N0") + "%";
                                d.PULeftHeight = Convert.ToString(106 - (Convert.ToDouble(d.OPCItemValueTextBoxes[10]))) + "mm";
                                #endregion
                                
                                #region METAL135-MOIROTING, 현재 사용하지않음


                                d.MonitorMotor1TotalDistance = d.OPCItemValueTextBoxes[46] + "m";
                                d.MonitorMotor2TotalDistance = d.OPCItemValueTextBoxes[47] + "m";
                                d.MonitorMotor3TotalDistance = d.OPCItemValueTextBoxes[48] + "m";
                                d.MonitorMotor4TotalDistance = d.OPCItemValueTextBoxes[71] + "m";
                                d.MonitorMotor5TotalDistance = d.OPCItemValueTextBoxes[72] + "m";
                                //d.StrCameraShot = d.OPCItemValueTextBoxes[14] + " 장";
                                //d.StrAfterBlade = d.OPCItemValueTextBoxes[64] + "m";
                                d.StrTwowayCount = d.OPCItemValueTextBoxes[57];
                                // KBJ ㄹㄹ
                                #region METAL135-PRINTINGRECODE
                                //if (tempPrintingProfile != d.OPCItemValueTextBoxes[54] && d.OPCItemValueTextBoxes[54] != null)
                                //{
                                //    //axisLabel.ShowLabels = false;
                                //    //nonePrinting.Visibility = Visibility.Collapsed;
                                //    //printingSeries.Visibility = Visibility.Visible;
                                //    bnonePrinting = false;

                                //    tempPrintingProfile = d.OPCItemValueTextBoxes[54];
                                //    char sp = '/';
                                //    char sp2 = ' ';
                                //    string[] spstring = d.OPCItemValueTextBoxes[54].Split(sp);

                                //    labels.Clear();
                                //    printingProcess.Clear();
                                //    string tempPrintingEndTime = "";
                                //    string tempPrintProcess = "";
                                //    printingEndTime.Clear();
                                //    printingStartTime.Clear();
                                //    printFinsh.Clear();

                                //    for (int i = 0; i < spstring.Count() - 1; i++)
                                //    {
                                //        if (spstring[i].Contains("START"))
                                //        {
                                //            if (bPrintingFinsh == false)
                                //            {


                                //            }
                                //            else
                                //            {
                                //                printingProcess.Add(tempPrintProcess);
                                //                printingEndTime.Add(tempPrintingEndTime);
                                //                printFinsh.Add("STOP");
                                //                tempPrintProcess = "";
                                //            }

                                //            List<string> _splitedFacets = new List<string>();
                                //            bPrintingFinsh = true;
                                //            _splitedFacets = spstring[i].Split(new string[] { ":", "$", ".job_" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                //            List<string> _splite = new List<string>();
                                //            _splite = _splitedFacets[0].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                //            tempPrintProcess = "[" + _splite[0] + "-" + _splite[1] + "-" + _splite[2] + " " + _splite[3] + ":" + _splite[4] + ":" + _splite[5] + "]" + " " + _splitedFacets[3] + "\n";
                                //            labels.Add(_splitedFacets[2] + ".job");
                                //            printingStartTime.Add(_splitedFacets[0]);
                                //            tempPrintingEndTime = _splitedFacets[0];
                                //        }
                                //        else if (spstring[i].Contains("Free"))
                                //        {

                                //        }
                                //        else
                                //        {
                                //            List<string> _splitedFacets = new List<string>();
                                //            _splitedFacets = spstring[i].Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries).ToList();


                                //            tempPrintingEndTime = _splitedFacets[0];
                                //            List<string> _splite = new List<string>();
                                //            _splite = spstring[i].Split(new string[] { "#", "_" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                //            tempPrintProcess = tempPrintProcess + "[" + _splite[0] + "-" + _splite[1] + "-" + _splite[2] + " " + _splite[3] + ":" + _splite[4] + ":" + _splite[5] + "]" + " " + _splite[6] + "\n";
                                //if (spstring[i].Contains("JobFinish"))
                                //            {
                                //                printFinsh.Add("Done");
                                //                printingProcess.Add(tempPrintProcess);
                                //                printingEndTime.Add(_splitedFacets[0]);
                                //                bPrintingFinsh = false;
                                //                tempPrintProcess = "";
                                //            }
                                //            else
                                //            {
                                //            }
                                //        }
                                //    }

                                //    if (bPrintingFinsh == true)
                                //    {
                                //        printingEndTime.Add(tempPrintingEndTime);
                                //        printingProcess.Add(tempPrintProcess);
                                //        printFinsh.Add("중단");
                                //        bPrintingFinsh = false;
                                //    }

                                //    DateTime aDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                                //    TimeSpan aWeek = new System.TimeSpan(7, 0, 0, 0);
                                //    TimeSpan addWeek = new System.TimeSpan(1, 0, 0, 0);
                                //    DateTime aDayBeforeWeek = aDay.Subtract(aWeek);
                                //    DateTime aDayAfterWeek = aDay.Add(addWeek);
                                //    string[] tempArray;
                                //    if (labels.Count > 0)
                                //    {
                                //        int tempLablesCount = labels.Count();
                                //        int tempVaulesCount = _values.Count;

                                //        int tempMinus = Math.Abs(tempVaulesCount - tempLablesCount);

                                //        //_values[0] = new GanttPoint(aDayBeforeWeek.Ticks, aDayBeforeWeek.AddDays(2).Ticks);
                                //        //_values.RemoveAt(1);

                                //        tempArray = new string[tempLablesCount];
                                //        for (int i = 0; i < tempLablesCount; i++)
                                //        {
                                //            tempArray[i] = labels[i];
                                //        }
                                //        // axisLabel.Labels = tempArray;

                                //        if (tempVaulesCount > tempLablesCount)
                                //        {
                                //            for (int i = 1; i <= tempMinus; i++)
                                //            {
                                //                //_values.RemoveAt(tempVaulesCount);
                                //                //d.PrintingSeries[0].Values.RemoveAt(tempVaulesCount-1);
                                //                _values.RemoveAt(tempVaulesCount - i);
                                //            }

                                //            for (int i = 0; i < tempLablesCount; i++)
                                //            {

                                //                List<string> _spliteStart = new List<string>();
                                //                List<string> _spliteEnd = new List<string>();

                                //                _spliteStart = printingStartTime[i].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                //                _spliteEnd = printingEndTime[i].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                                //                DateTime sampleStartTime = new DateTime(Convert.ToInt32(_spliteStart[0]), Convert.ToInt32(_spliteStart[1]), Convert.ToInt32(_spliteStart[2]), Convert.ToInt32(_spliteStart[3]), Convert.ToInt32(_spliteStart[4]), Convert.ToInt32(_spliteStart[5]));
                                //                DateTime sampleEndTime = new DateTime(Convert.ToInt32(_spliteEnd[0]), Convert.ToInt32(_spliteEnd[1]), Convert.ToInt32(_spliteEnd[2]), Convert.ToInt32(_spliteEnd[3]), Convert.ToInt32(_spliteEnd[4]), Convert.ToInt32(_spliteEnd[5]));
                                //                TimeSpan diffResult = sampleEndTime.Subtract(sampleStartTime);

                                //                double dbldiffResult = diffResult.Days;

                                //                _values[i] = new GanttPoint(sampleStartTime.Ticks, sampleStartTime.AddSeconds(dbldiffResult).Ticks);
                                //            }
                                //        }
                                //       else if (tempVaulesCount < tempLablesCount)
                                //        {
                                //            for (int i = 0; i < tempVaulesCount; i++)
                                //            {

                                //                List<string> _spliteStart = new List<string>();
                                //                List<string> _spliteEnd = new List<string>();

                                //                _spliteStart = printingStartTime[i].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                //                _spliteEnd = printingEndTime[i].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                                //                DateTime sampleStartTime = new DateTime(Convert.ToInt32(_spliteStart[0]), Convert.ToInt32(_spliteStart[1]), Convert.ToInt32(_spliteStart[2]), Convert.ToInt32(_spliteStart[3]), Convert.ToInt32(_spliteStart[4]), Convert.ToInt32(_spliteStart[5]));
                                //                DateTime sampleEndTime = new DateTime(Convert.ToInt32(_spliteEnd[0]), Convert.ToInt32(_spliteEnd[1]), Convert.ToInt32(_spliteEnd[2]), Convert.ToInt32(_spliteEnd[3]), Convert.ToInt32(_spliteEnd[4]), Convert.ToInt32(_spliteEnd[5]));
                                //                TimeSpan diffResult = sampleEndTime.Subtract(sampleStartTime);

                                //                double dbldiffResult = diffResult.TotalSeconds;

                                //                _values[i] = new GanttPoint(sampleStartTime.Ticks, sampleStartTime.AddSeconds(dbldiffResult).Ticks);
                                //            }

                                //            for (int i = tempVaulesCount; i < tempLablesCount; i++)
                                //            {
                                //                List<string> _spliteStart = new List<string>();
                                //                List<string> _spliteEnd = new List<string>();

                                //                _spliteStart = printingStartTime[i].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                //                _spliteEnd = printingEndTime[i].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                                //                DateTime sampleStartTime = new DateTime(Convert.ToInt32(_spliteStart[0]), Convert.ToInt32(_spliteStart[1]), Convert.ToInt32(_spliteStart[2]), Convert.ToInt32(_spliteStart[3]), Convert.ToInt32(_spliteStart[4]), Convert.ToInt32(_spliteStart[5]));
                                //                DateTime sampleEndTime = new DateTime(Convert.ToInt32(_spliteEnd[0]), Convert.ToInt32(_spliteEnd[1]), Convert.ToInt32(_spliteEnd[2]), Convert.ToInt32(_spliteEnd[3]), Convert.ToInt32(_spliteEnd[4]), Convert.ToInt32(_spliteEnd[5]));

                                //                TimeSpan diffResult = sampleEndTime.Subtract(sampleStartTime);
                                //                double dbldiffResult = diffResult.TotalSeconds;
                                //                _values.Add(new GanttPoint(sampleStartTime.Ticks, sampleStartTime.AddSeconds(dbldiffResult).Ticks));
                                //                //_values[i] = new GanttPoint(sampleStartTime.Ticks, sampleStartTime.AddDays(dbldiffResult).Ticks);
                                //            }
                                //        }
                                //        else if (tempVaulesCount == tempLablesCount)
                                //        {

                                //            for (int i = 0; i < tempVaulesCount; i++)
                                //            {
                                //                List<string> _spliteStart = new List<string>();
                                //                List<string> _spliteEnd = new List<string>();


                                //                _spliteStart = printingStartTime[i].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                //                _spliteEnd = printingEndTime[i].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                                //                DateTime sampleStartTime = new DateTime(Convert.ToInt32(_spliteStart[0]), Convert.ToInt32(_spliteStart[1]), Convert.ToInt32(_spliteStart[2]), Convert.ToInt32(_spliteStart[3]), Convert.ToInt32(_spliteStart[4]), Convert.ToInt32(_spliteStart[5]));
                                //                DateTime sampleEndTime = new DateTime(Convert.ToInt32(_spliteEnd[0]), Convert.ToInt32(_spliteEnd[1]), Convert.ToInt32(_spliteEnd[2]), Convert.ToInt32(_spliteEnd[3]), Convert.ToInt32(_spliteEnd[4]), Convert.ToInt32(_spliteEnd[5]));
                                //                TimeSpan diffResult = sampleEndTime.Subtract(sampleStartTime);

                                //                double dbldiffResult = diffResult.TotalSeconds;

                                //                _values[i] = new GanttPoint(sampleStartTime.Ticks, sampleStartTime.AddSeconds(dbldiffResult).Ticks);
                                //            }

                                //        }
                                //        else
                                //        { }

                                //        //printingSeries.RowSeries[0].ScrollBarFill = new SolidColorBrush(Color.FromArgb(255, 231, 162, 0));
                                //    }
                                //    else
                                //    {

                                //    }

                                //    dv.DayofWeek = values => new DateTime((long)values).ToString("MMM월dd일HH시");
                                //    dv.StrTimeStart = aDayBeforeWeek.Month.ToString() + "월 " + aDayBeforeWeek.Day.ToString() + "일 " + "00시";
                                //    //axisPrinting.MaxValue = aDayAfterWeek.Ticks;
                                //    //axisPrinting.MinValue = aDayBeforeWeek.Ticks;
                                //}
                                //else if ((d.OPCItemValueTextBoxes[54] == "Unknown" || d.OPCItemValueTextBoxes[54] == "") && bnonePrinting == false)
                                //{
                                //    bnonePrinting = true;
                                //    DateTime aboutDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                                //    TimeSpan afterWeek = new System.TimeSpan(7, 0, 0, 0);
                                //    DateTime aDayBeforeWeek = aboutDay.Subtract(afterWeek);
                                //    dv.StrTimeStart = Convert.ToString(aDayBeforeWeek.Year) + "년 " + Convert.ToString(aDayBeforeWeek.Month) + "월 " + Convert.ToString(aDayBeforeWeek.Day) + "일" + " ~ " + aboutDay.ToLongDateString();

                                //    //nonePrinting.Visibility = Visibility.Visible;
                                //    //printingSeries.Visibility = Visibility.Collapsed;

                                //    //axisLabel.ShowLabels = false;
                                //}
                                //else
                                //{ }

                                #endregion

                                #region 펌프 시간 초기 세팅
                                //if (dv.StrPumpTime == "0" && d.OPCItemValueTextBoxes[53] != null)
                                //{
                                //    DateTime newStartTimePump = DateTime.Now.AddSeconds(Convert.ToDouble(d.OPCItemValueTextBoxes[53]));
                                //    DateTime newStartTimeFilter = DateTime.Now.AddSeconds(Convert.ToDouble(d.OPCItemValueTextBoxes[51]));
                                //    DateTime newStartTimeScanner = DateTime.Now.AddSeconds(Convert.ToDouble(d.OPCItemValueTextBoxes[55]));

                                //    TimeSpan elapsedPump = newStartTimePump - DateTime.Now;
                                //    TimeSpan elapsedFilter = newStartTimeFilter - DateTime.Now;
                                //    TimeSpan elapsedScanner = newStartTimeScanner - DateTime.Now;

                                //    string textPump = "";
                                //    string textFilter = "";
                                //    string textScanner = "";

                                //    // Compose the rest of the elapsed time.
                                //    textPump +=
                                //        elapsedPump.Days.ToString("00") + "일 " + " " +
                                //        elapsedPump.Hours.ToString("00") + "시간 " + " " +
                                //        elapsedPump.Minutes.ToString("00") + "분  " + elapsedPump.Seconds.ToString("00") + "초 ";
                                //    textFilter +=
                                //        elapsedFilter.Days.ToString("00") + "일 " + " " +
                                //        elapsedFilter.Hours.ToString("00") + "시간 " + " " +
                                //        elapsedFilter.Minutes.ToString("00") + "분  " + elapsedFilter.Seconds.ToString("00") + "초 ";
                                //    textScanner +=
                                //        elapsedScanner.Days.ToString("00") + "일 " + " " +
                                //        elapsedScanner.Hours.ToString("00") + "시간 " + " " +
                                //        elapsedScanner.Minutes.ToString("00") + "분  " + elapsedScanner.Seconds.ToString("00") + "초 ";

                                //    //lblTime.Content = text;
                                //    d.StrPumpTime = textPump;
                                //    d.StrScannerTime = textScanner;
                                //    d.StrFilterTime = textFilter;
                                //}
                                #endregion

                                #endregion

                                #region M270-LOG

                                if (d.OPCItemValueTextBoxes[66] == "True") //문제있음
                                {
                                    List<LogFile> _log = new List<LogFile>();

                                    char sp = '/';
                                    char sp2 = ' ';
                                    char sp3 = '.';

                                    string[] spstring = d.OPCItemValueTextBoxes[50].Split(sp);

                                    for (int i = 0; i < spstring.Length - 1; i++)
                                    {
                                        string[] spstring2 = spstring[i].Split(sp2);
                                        string[] tempString = spstring2[0].Split(sp3);
                                        string tempWork = spstring2[1].Replace("_", " ");
                                        string tempWork2 = spstring2[1].Replace("$", "/");
                                        string tempTime = "[" + tempString[0] + "-" + tempString[1] + "-" + tempString[2] + " " + tempString[3] + ":" + tempString[4] + ":" + tempString[5] + "]";
                                        _log.Add(new LogFile() { Time = tempTime, Machine = "M270", Work = tempWork2, Image = @"/imgTab/5_log/light-blue.png" });
                                    }

                                    lvLogs.ItemsSource = _log;
                                    
                                }
                                else if (d.OPCItemValueTextBoxes[66] == "False" && lvLogs.Items.Count == 0)
                                {
                                    //bLogInital = true;
                                    d.OPCItemWriteValueTextBoxes[52] = "True";
                                    d.opcWrite("OPCItemSyncWrite52", daServerMgt);
                                }
                                else if (d.OPCItemValueTextBoxes[66] == "False")
                                {

                                }
                                else
                                {
                                    d.OPCItemWriteValueTextBoxes[52] = "True";
                                    d.opcWrite("OPCItemSyncWrite52", daServerMgt);
                                }
                                //if (lvLogs.Items.Count == 0)
                                //{
                                //    d.OPCItemWriteValueTextBoxes[52] = "True";
                                //    d.opcWrite("OPCItemSyncWrite52", daServerMgt);
                                //}
                                #endregion

                                #region lastweek profile

                                if (tempLastWeek != d.OPCItemValueTextBoxes[64])
                                {
                                    tempLastWeek = d.OPCItemValueTextBoxes[64];
                                    string sfimg = @"/imgMonitoring/imgPrintingFail.png";

                                    List<WeekFile> _wf = new List<WeekFile>();

                                    List<string> weekstring = tempLastWeek.Split(new string[] { "_JobSTART/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    
                                    //string[] weekstring = d.OPCItemValueTextBoxes[54].Split('/');
                                    string weekLog = "";
                                    string strSF = "";
                                    WeekFile wf = new WeekFile();
                                    blastweekone = false;

                                    _strFileName.Clear();
                                    strlastWeek.Clear();
                                    printFinsh.Clear();

                                    for (int i = 0; i < weekstring.Count; i++)
                                    {
                                        string[] weekstringsp = weekstring[i].Split('/');
                                        for (int j = 0; j < weekstringsp.Count(); j++)
                                        {
                                            if (weekstringsp[j].Contains("FileName"))
                                            {
                                                if (blastweekone == true)
                                                {
                                                    int tempyear = Convert.ToInt32(wf.WeekStartTime.Substring(1, 4));
                                                    int tempmonth = Convert.ToInt32(wf.WeekStartTime.Substring(6, 2));
                                                    int tempday = Convert.ToInt32(wf.WeekStartTime.Substring(9, 2));
                                                    int temphour = Convert.ToInt32(wf.WeekStartTime.Substring(12, 2));
                                                    int tempminute = Convert.ToInt32(wf.WeekStartTime.Substring(15, 2));
                                                    int tempsecond = Convert.ToInt32(wf.WeekStartTime.Substring(18, 2));

                                                    if (wf.WeekEndTime == null)
                                                    {
                                                        strSF = "강제종료";
                                                        wf.WeekEndTime = "강제종료";
                                                        wf.WeekWorkTime = "알수없음";
                                                    }
                                                    else
                                                    {
                                                        int tempyear1 = Convert.ToInt32(wf.WeekEndTime.Substring(1, 4));
                                                        int tempmonth1 = Convert.ToInt32(wf.WeekEndTime.Substring(6, 2));
                                                        int tempday1 = Convert.ToInt32(wf.WeekEndTime.Substring(9, 2));
                                                        int temphour1 = Convert.ToInt32(wf.WeekEndTime.Substring(12, 2));
                                                        int tempminute1 = Convert.ToInt32(wf.WeekEndTime.Substring(15, 2));
                                                        int tempsecond1 = Convert.ToInt32(wf.WeekEndTime.Substring(18, 2));

                                                        DateTime date1 = new DateTime(tempyear, tempmonth, tempday, temphour, tempminute, tempsecond);
                                                        DateTime date2 = new DateTime(tempyear1, tempmonth1, tempday1, temphour1, tempminute1, tempsecond1);
                                                        TimeSpan diff1 = date2.Subtract(date1);
                                                        wf.WeekWorkTime = diff1.Days.ToString("00") + "d :" + " " + diff1.Hours.ToString("00") + "h " + ":" + " " +
                                                                        diff1.Minutes.ToString("00") + "m " + ":" + " " +
                                                                        diff1.Seconds.ToString("00") + "s ";
                                                        Console.WriteLine(diff1.ToString());

                                                    }

                                                    printFinsh.Add(strSF);
                                                    strlastWeek.Add(weekLog);
                                                    _wf.Insert(0, new WeekFile() { WeekImage = sfimg, WeekStartTime = wf.WeekStartTime, WeekEndTime = wf.WeekEndTime, WeekWorkTime = wf.WeekWorkTime, WeekFileName = wf.WeekFileName, WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
                                                    wf.WeekEndTime = null;
                                                    int dad = 0;
                                                }

                                                wf = new WeekFile();
                                                blastweekone = true;
                                                weekLog = "";
                                                strSF = "";
                                                List<string> _splitedFacets = new List<string>();
                                                _splitedFacets = weekstringsp[j].Split(new string[] { ":", "$", "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                                weekLog = weekLog + "[" + _splitedFacets[0] + "-" + _splitedFacets[1] + "-" + _splitedFacets[2] + " " + _splitedFacets[3] + ":" + _splitedFacets[4] + ":" + _splitedFacets[5] + "] _JobStart\n";
                                                wf.WeekStartTime = "[" + _splitedFacets[0] + "-" + _splitedFacets[1] + "-" + _splitedFacets[2] + " " + _splitedFacets[3] + ":" + _splitedFacets[4] + ":" + _splitedFacets[5] + "] ";
                                                wf.WeekFileName = _splitedFacets[7] + ".job";
                                                _strFileName.Add(wf.WeekFileName);
                                            }
                                            else if (weekstringsp[j].Contains("JobSTOP"))
                                            {
                                                List<string> _splitedFacets = new List<string>();
                                                _splitedFacets = weekstringsp[j].Split(new string[] { ":", "$", "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                                weekLog = weekLog + "[" + _splitedFacets[0] + "-" + _splitedFacets[1] + "-" + _splitedFacets[2] + " " + _splitedFacets[3] + ":" + _splitedFacets[4] + ":" + _splitedFacets[5] + "] " + _splitedFacets[6] + "\n";
                                                wf.WeekEndTime = "[" + _splitedFacets[0] + "-" + _splitedFacets[1] + "-" + _splitedFacets[2] + " " + _splitedFacets[3] + ":" + _splitedFacets[4] + ":" + _splitedFacets[5] + "] ";
                                                sfimg = @"/imgMonitoring/imgPrintingFail.png";
                                                strSF = "중단";
                                            }
                                            else if (weekstringsp[j].Contains("JobFinish"))
                                            {
                                                List<string> _splitedFacets = new List<string>();
                                                _splitedFacets = weekstringsp[j].Split(new string[] { ":", "$", "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                                weekLog = weekLog + "[" + _splitedFacets[0] + "-" + _splitedFacets[1] + "-" + _splitedFacets[2] + " " + _splitedFacets[3] + ":" + _splitedFacets[4] + ":" + _splitedFacets[5] + "] " + _splitedFacets[6] + "\n";
                                                wf.WeekEndTime = "[" + _splitedFacets[0] + "-" + _splitedFacets[1] + "-" + _splitedFacets[2] + " " + _splitedFacets[3] + ":" + _splitedFacets[4] + ":" + _splitedFacets[5] + "] ";
                                                sfimg = @"/imgMonitoring/imgPrintingSusses.png";
                                                strSF = "완료";
                                            }
                                            else
                                            {
                                                List<string> _splitedFacets = new List<string>();
                                                _splitedFacets = weekstringsp[j].Split(new string[] { ":", "$", "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                                if (_splitedFacets.Count == 0)
                                                { }
                                                else
                                                {
                                                    weekLog = weekLog + "[" + _splitedFacets[0] + "-" + _splitedFacets[1] + "-" + _splitedFacets[2] + " " + _splitedFacets[3] + ":" + _splitedFacets[4] + ":" + _splitedFacets[5] + "] " + _splitedFacets[6] + "\n";

                                                }


                                            }
                                        }

                                        if (i == weekstring.Count - 1)
                                        {

                                            if (d.OPCItemValueTextBoxes[35] == "4" || d.OPCItemValueTextBoxes[35] == "5")
                                            {
                                                strSF = "출력중";
                                                wf.WeekEndTime = "출력중";
                                                wf.WeekWorkTime = "출력중";
                                            }
                                            else
                                            {
                                                int tempyear = Convert.ToInt32(wf.WeekStartTime.Substring(1, 4));
                                                int tempmonth = Convert.ToInt32(wf.WeekStartTime.Substring(6, 2));
                                                int tempday = Convert.ToInt32(wf.WeekStartTime.Substring(9, 2));
                                                int temphour = Convert.ToInt32(wf.WeekStartTime.Substring(12, 2));
                                                int tempminute = Convert.ToInt32(wf.WeekStartTime.Substring(15, 2));
                                                int tempsecond = Convert.ToInt32(wf.WeekStartTime.Substring(18, 2));

                                                if (wf.WeekEndTime == null)
                                                {
                                                    strSF = "강제종료";
                                                    wf.WeekEndTime = "강제종료";
                                                    wf.WeekWorkTime = "알수없음";
                                                }
                                                else
                                                {
                                                    int tempyear1 = Convert.ToInt32(wf.WeekEndTime.Substring(1, 4));
                                                    int tempmonth1 = Convert.ToInt32(wf.WeekEndTime.Substring(6, 2));
                                                    int tempday1 = Convert.ToInt32(wf.WeekEndTime.Substring(9, 2));
                                                    int temphour1 = Convert.ToInt32(wf.WeekEndTime.Substring(12, 2));
                                                    int tempminute1 = Convert.ToInt32(wf.WeekEndTime.Substring(15, 2));
                                                    int tempsecond1 = Convert.ToInt32(wf.WeekEndTime.Substring(18, 2));

                                                    DateTime date1 = new DateTime(tempyear, tempmonth, tempday, temphour, tempminute, tempsecond);
                                                    DateTime date2 = new DateTime(tempyear1, tempmonth1, tempday1, temphour1, tempminute1, tempsecond1);
                                                    TimeSpan diff1 = date2.Subtract(date1);
                                                    wf.WeekWorkTime = diff1.Days.ToString("00") + "d :" + " " + diff1.Hours.ToString("00") + "h " + ":" + " " +
                                                                    diff1.Minutes.ToString("00") + "m " + ":" + " " +
                                                                    diff1.Seconds.ToString("00") + "s ";
                                                }


                                            }

                                            printFinsh.Add(strSF);
                                            strlastWeek.Add(weekLog);

                                            _wf.Insert(0, new WeekFile() { WeekImage = sfimg, WeekStartTime = wf.WeekStartTime, WeekEndTime = wf.WeekEndTime, WeekWorkTime = wf.WeekWorkTime, WeekFileName = wf.WeekFileName, WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
                                        }

                                        if (i == weekstring.Count)
                                        {
                                            if (weekstring[i].Contains("FileName:"))
                                            {

                                                wf.WeekEndTime = "강제종료";
                                                wf.WeekWorkTime = "알수없음";
                                                _wf.Insert(0, new WeekFile() { WeekImage = sfimg, WeekStartTime = wf.WeekStartTime, WeekEndTime = wf.WeekEndTime, WeekWorkTime = wf.WeekWorkTime, WeekFileName = wf.WeekFileName, WeekImageetc = @"/imgTab/5_log/monitor_btn_log.png" });
                                            }
                                        }
                                    }
                                    lvWeekFile.ItemsSource = _wf;
                                }

                                List<WeekFile> _weekFile = new List<WeekFile>();
                                string tempPrintingProfile = d.OPCItemValueTextBoxes[54];
                                if (tempPrintingProfile != "")
                                {
                                    string[] weekstring = d.OPCItemValueTextBoxes[54].Split('/');

                                    strlastWeek = new List<string>();
                                    for (int i = 0; i < weekstring.Length - 1; i++)
                                    {
                                        //if (weekstring[i].Contains(".job_JobSTART"))
                                        //{
                                        List<string> _splitedFacets = new List<string>();
                                        bPrintingFinsh = true;
                                        _splitedFacets = weekstring[i].Split(new string[] { ":", "$" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                        List<string> _splite = new List<string>();
                                        _splite = _splitedFacets[0].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                        string tempPrintProcess = "[" + _splite[0] + "-" + _splite[1] + "-" + _splite[2] + " " + _splite[3] + ":" + _splite[4] + ":" + _splite[5] + "]" + " " + _splitedFacets[3] + "\n";
                                        int da = 0;
                                        strlastWeek.Add(tempPrintProcess);
                                        //}
                                    }

                                    int dasd = 0;
                                }
                                #endregion

                                #region M270-BuildTimeEst
                                if (Convert.ToDouble(d.OPCItemValueTextBoxes[23]) == 0)
                                {
                                    timeChnage = 0;
                                }
                                else
                                {
                                    timeChnage = Convert.ToDouble(d.OPCItemValueTextBoxes[87]);
                                }
                                estStartTime = DateTime.Now.AddSeconds(timeChnage);
                                TimeSpan currentTime = TimeSpan.FromSeconds(timeChnage);
                                int stimehour = 0;
                                string sstrTimehour = "0";
                                if (currentTime.Days >= 1)
                                {
                                    stimehour = (currentTime.Days * 24) + currentTime.Hours;
                                    sstrTimehour = Convert.ToString(stimehour);
                                }
                                else
                                {
                                    sstrTimehour = currentTime.Hours.ToString("00");
                                }
                                psCurrentTime = sstrTimehour + "h " + ":" + " " +
                                    currentTime.Minutes.ToString("00") + "m " + ":" + " " +
                                    currentTime.Seconds.ToString("00") + "s ";
                                lbltime.Text = psCurrentTime;
                                

                                if (Convert.ToDouble(d.OPCItemValueTextBoxes[87]) != timeChnage && (d.OPCItemValueTextBoxes[23] == "4" || d.OPCItemValueTextBoxes[23] == "5"))
                                {
                                        estStopWatch.Start();
                                        estDispatcherTimer.Start();
                                    
                                    
                                    if (_timerPrintingRunning == false)
                                    {
                                        _timerPrintingRunning = true;
                                        ps_StartTime = DateTime.Now;

                                        fixTime = Convert.ToDouble(d.OPCItemValueTextBoxes[73]);
                                        psStopWatch.Start();
                                        psDispatcherTimer.Start();
                                    }
                                }
                                else if (d.OPCItemValueTextBoxes[23] == "7")
                                {
                                    //if (psStopWatch.IsRunning == false)
                                    //{
                                    //    psStopWatch.Start();
                                    //}

                                    //if (estStopWatch.IsRunning)
                                    //{

                                    //    estStopWatch.Stop();
                                    //}
                                }
                                else if (d.OPCItemValueTextBoxes[23] == "8")
                                {
                                    if (psStopWatch.IsRunning)
                                    {
                                        psStopWatch.Stop();
                                    }

                                    if (estStopWatch.IsRunning)
                                    {

                                        estStopWatch.Stop();
                                    }
                                }
                                else
                                {

                                    //double totalLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[30]);
                                    //double currentLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[29]);
                                    //if (totalLayer == currentLayer && totalLayer != 0)
                                    //{
                                    //    if (psStopWatch.IsRunning)
                                    //    {
                                    //        psStopWatch.Stop();
                                    //    }

                                    //    if (estStopWatch.IsRunning)
                                    //    {
                                    //        estStopWatch.Stop();
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    psStopWatch.Reset();
                                    //    estStopWatch.Reset();

                                    //    lbltime.Text = "00h : 00m : 00s";
                                    //    lblPsTime.Content = "00h : 00m : 00s";
                                    //}
                                }



                                /*
                                if (Convert.ToDouble(d.OPCItemValueTextBoxes[29]) != timeChnage || d.OPCItemValueTextBoxes[35] != "8")
                                {
                                    timeChnage = Convert.ToDouble(d.OPCItemValueTextBoxes[29]);
                                    double dblBuideTimeEst = Convert.ToDouble(d.OPCItemValueTextBoxes[29]);
                                    DispatcherTimerEst(dblBuideTimeEst);

                                    d.OPCItemWriteValueTextBoxes[23] = "False";
                                    d.opcWrite("OPCItemSyncWrite23", daServerMgt);

                                    bResume = false;
                                }

                                if (d.OPCItemValueTextBoxes[35] == "8")
                                {

                                    double totalLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[30]);
                                    double currentLayer = Convert.ToDouble(d.OPCItemValueTextBoxes[29]);
                                    if (totalLayer == currentLayer && totalLayer != 0)
                                    {
                                        strPrintEstTime.Content = "00h : 00m : 00s";
                                        esTimer.Stop();
                                    }
                                    else
                                    {
                                        //double dblBuideTimeEst = Convert.ToDouble(d.OPCItemValueTextBoxes[29]);
                                        //TimeSpan elapsed = TimeSpan.FromSeconds(dblBuideTimeEst);


                                        //string text = "";
                                        //if (elapsed.Days > 0)
                                        //    text += elapsed.Days.ToString() + ".";

                                        //int tenths = elapsed.Milliseconds / 100;

                                        //text +=
                                        //    //elapsed.Days.ToString("0") + "d "+ ":" + " " +
                                        //    elapsed.Hours.ToString("00") + "h " + ":" + " " +
                                        //    elapsed.Minutes.ToString("00") + "m " + ":" + " " +
                                        //    elapsed.Seconds.ToString("00") + "s ";

                                        ////lblTime.Content = text;
                                        //strPrintEstTime.Content = text;
                                        strPrintEstTime.Content = "00h : 00m : 00s";
                                        esTimer.Stop();
                                    }
                                }
                                else if (d.OPCItemValueTextBoxes[35] == "0" && bScansystem0Check == false)
                                {
                                    bScansystem0Check = true;
                                    esTimer.Stop();
                                    strPrintEstTime.Content = "00h : 00m : 00s";
                                }
                                else
                                {

                                }
                                */
                                #endregion

                                #region PowderSupply, 수정필요
                                if (tempPowderSupply != Convert.ToDouble(dv.OPCItemValueTextBoxes[86]))
                                {
                                    tempPowderSupply = Convert.ToDouble(dv.OPCItemValueTextBoxes[86]);
                                    dv.DblPowderSupply4 = Convert.ToString(tempPowderSupply);
                                }
                                
                                if (d.OPCItemValueTextBoxes[77] == "True")//파우더 분말공급통 상단 센서가 켜졌을 때
                                {
                                    powder_Light02.Visibility = Visibility.Visible;
                                    pSupplyTop.Visibility = Visibility.Visible;
                                    powder_Light02_black.Visibility = Visibility.Collapsed;
                                    pSupplyTop_black.Visibility = Visibility.Collapsed;

                                }
                                else if(d.OPCItemValueTextBoxes[77] == "False")
                                {
                                    powder_Light02.Visibility = Visibility.Collapsed;
                                    pSupplyTop.Visibility = Visibility.Collapsed;
                                    powder_Light02_black.Visibility = Visibility.Visible;
                                    pSupplyTop_black.Visibility = Visibility.Visible;

                                }
                                
                                if (d.OPCItemValueTextBoxes[78] == "True")//파우더 분말공급통 하단 센서가 켜졌을 때
                                {
                                    powder_Light01.Visibility = Visibility.Visible;
                                    pSupplyUnder.Visibility = Visibility.Visible;
                                    powder_Light01_black.Visibility = Visibility.Collapsed;
                                    pSupplyUnder_black.Visibility = Visibility.Collapsed;
                                }
                                else if (d.OPCItemValueTextBoxes[78] == "False")
                                {
                                    powder_Light01.Visibility = Visibility.Collapsed;
                                    pSupplyUnder.Visibility = Visibility.Collapsed;
                                    powder_Light01_black.Visibility = Visibility.Visible;
                                    pSupplyUnder_black.Visibility = Visibility.Visible;
                                }

                                #endregion

                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });
        }

        //----------------------------------------------------------------------------------------------------------------------------------------
        #region 현재 백그라운드 색상 변경하는 부분, 현재 사용하지않음
        public void roomcheckLock()
        {
            if (d.OPCItemValueTextBoxes[48] != "0" && d.OPCItemValueTextBoxes[48] != "1")
            {
                darkbackground.Visibility = Visibility.Visible;

                //RoomMoveConfrim2 rmc = new RoomMoveConfrim2(d, daServerMgt, this);
                //rmc.Show();
            }
            else
            {
                darkbackground.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region 가스 제어부
        private bool gasMaxMin = false; // Max일때 true

        private void GasUp()
        {
            if (gasMaxMin == true)
            {
                if (double.Parse(d.GasMaxValue) < 0.1 && double.Parse(d.GasMaxValue) >= double.Parse(d.GasMinValue))
                {
                    d.GNumber1++;
                    if (d.GNumber1 == 10)
                    {
                        d.GNumber1 = 0;
                        d.GNumber2 = 1;
                    }
                    GasMaxResult();
                }
            }
            else
            {
                if (double.Parse(d.GasMinValue) < 0.1 && double.Parse(d.GasMaxValue) > double.Parse(d.GasMinValue))
                {
                    d.GNumber1++;
                    if (d.GNumber1 == 10)
                    {
                        d.GNumber1 = 0;
                        d.GNumber2 = 1;
                    }
                    GasMinResult();
                }
            }
        }

        private void GasDown()
        {
            if (gasMaxMin == true)
            {
                if (double.Parse(d.GasMaxValue) > 0.05 && double.Parse(d.GasMaxValue) > double.Parse(d.GasMinValue))
                {
                    d.GNumber1--;
                    if (d.GNumber1 == -1)
                    {
                        d.GNumber1 = 9;
                        d.GNumber2 = 0;
                    }
                    GasMaxResult();
                }
            }
            else
            {
                if (double.Parse(d.GasMinValue) > 0.05 && double.Parse(d.GasMaxValue) >= double.Parse(d.GasMinValue))
                {
                    d.GNumber1--;
                    if (d.GNumber1 == -1)
                    {
                        d.GNumber1 = 9;
                        d.GNumber2 = 0;
                    }
                    GasMinResult();
                }
            }
        }

        string minOrigin = "";
        string maxOrigin = "";


        private void GasMaxResult()
        {
            d.GasMaxValue = Convert.ToString(d.GNumber3) + "." + Convert.ToString(d.GNumber2) + Convert.ToString(d.GNumber1);
        }

        private void GasMinResult()
        {
            d.GasMinValue = Convert.ToString(d.GNumber3) + "." + Convert.ToString(d.GNumber2) + Convert.ToString(d.GNumber1);
        }

        private void GasConfirm()
        {

            //d.OPCItemWriteValueTextBoxes[6] = d.GasMaxValue.ToString();
            //d.opcWrite("OPCItemSyncWrite6", daServerMgt);
            //d.OPCItemWriteValueTextBoxes[7] = d.GasMinValue.ToString();
            //d.opcWrite("OPCItemSyncWrite7", daServerMgt);
            //MessageBox.Show(d.GasMinValue);
            gw.Close();
        }

        private void GWPopupMax()
        {
            maxOrigin = d.StrMaxOxy;
            minOrigin = d.StrMinOxy;

            gasMaxMin = true;
            string tempGasValue = Convert.ToString(d.GasMaxValue);

            d.GNumber1 = Convert.ToInt32(tempGasValue.Substring(3, 1));
            d.GNumber2 = Convert.ToInt32(tempGasValue.Substring(2, 1));
            d.GNumber3 = Convert.ToInt32(tempGasValue.Substring(0, 1));

            if (gw.IsVisible == true)
            {
                //gw.initalSetting(d, daServerMgt, 3);
                gw.Topmost = true;
            }
            else
            {
                gw = new GasWindow();
                gw.initalSetting(d, daServerMgt, 3);
                gw.Show();
                gw.Topmost = true;
            }
        }

        private void GWPopupMin()
        {
            maxOrigin = d.StrMaxOxy;
            minOrigin = d.StrMinOxy;

            gasMaxMin = false;
            string tempGasValue = Convert.ToString(d.GasMinValue);

            d.GNumber1 = Convert.ToInt32(tempGasValue.Substring(3, 1));
            d.GNumber2 = Convert.ToInt32(tempGasValue.Substring(2, 1));
            d.GNumber3 = Convert.ToInt32(tempGasValue.Substring(0, 1));

            if (gw.IsVisible == true)
            {
                //gw.initalSetting(d, daServerMgt, 3);
                gw.Topmost = true;
            }
            else
            {
                gw = new GasWindow();
                gw.initalSetting(d, daServerMgt, 3);
                gw.Show();
                gw.Topmost = true;
            }
        }

        private void GPopupClose()
        {
            d.GasMinValue = minOrigin;
            d.GasMaxValue = maxOrigin;
            gw.Close();
        }
        #endregion

        private void AirPressureSwitch()
        {
            
            if(d.OPCItemValueTextBoxes[81] == "False")
            {
                d.OPCItemWriteValueTextBoxes[57] = "True";
                d.opcWrite("OPCItemSyncWrite57", daServerMgt);
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[57] = "False";
                d.opcWrite("OPCItemSyncWrite57", daServerMgt);
            }
        }

        private void AutoSupplySwitch()
        {

            if (d.OPCItemValueTextBoxes[83] == "False")
            {
                d.OPCItemWriteValueTextBoxes[59] = "True";
                d.opcWrite("OPCItemSyncWrite59", daServerMgt);
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[59] = "False";
                d.opcWrite("OPCItemSyncWrite59", daServerMgt);
            }
        }

        #region M270-LineBackgroundworker, 작업 완료, 진행중 확인필요

        private void linethread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            lvFile.IsEnabled = true;

            progress.Visibility = Visibility.Collapsed;
            aniLoading.Visibility = Visibility.Collapsed;

            showlayer = 0;
            txtCurrentLayer.Text = Convert.ToString(showlayer);

            polyInline.Data = null;
            StringBuilder strbuilder = new StringBuilder();
            StringBuilder strbuilder3 = new StringBuilder();
            int showlayercount = 0;
            for (int i = 0; i < allFile.Count; i++)
            {
                strbuilder3.Append(_stringBuilder3[showlayercount]);
                strbuilder.Append(_stringBuilder[showlayercount]);
                showlayercount = showlayercount + allmodelLayer;
            }
            polyOutline.Data = Geometry.Parse("" + strbuilder3);
            polyInline.Data = Geometry.Parse("" + strbuilder);

            slLayer1.Maximum = Convert.ToInt32(allmodelLayer);
            slLayer1.Minimum = 0;
            txtTotalLayer.Text = Convert.ToString(allmodelLayer);

            int tempNumber = lvFile.SelectedIndex;
            lvFile.SelectedIndex = tempNumber;
            fileSize = 0;
        }

        private void linethread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int value = e.ProgressPercentage;

            // 변경 값으로 갱신
            progress.Value = value;

        }

        private void linethread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            double tempFileSize = 0;
            _facts.Clear();
            allFile.Clear();
            filePath = @"C:\\MYD_METAL\\" + adada + "\\" + adada + ".job";
            List<GeometryGroup> geoGroup = new List<GeometryGroup>();

            int lastCount = filePath.LastIndexOf("\\");
            sss = filePath.Substring(0, lastCount);

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sss);
            System.IO.FileInfo[] fi = di.GetFiles("*.bin");
            if (fi.Length == 0)
            {

            }
            else
            {
                string s = "";

                for (int i = 0; i < fi.Length; i++)
                {
                    s = fi[i].Name.ToString();
                    allFile.Add(s);
                    string attachFile = sss + "\\" + s;

                    Stream inStream = new FileStream(attachFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var reader = new BinaryReader(inStream);

                    UInt32 nHeader = ReadUInt32(reader);
                    UInt32 nHeaderSize = ReadUInt32(reader);
                    UInt32 nFixedHeader = ReadUInt32(reader);
                    UInt32 nFixedHeaderSize = ReadUInt32(reader);
                    this.Header = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(Convert.ToInt32(nFixedHeaderSize))).Trim();

                    UInt32 nVersionInfo = ReadUInt32(reader);
                    UInt32 nVersionSize = ReadUInt32(reader);
                    UInt32 nVersionMajor = ReadUInt32(reader);
                    UInt32 nVersionMinor = ReadUInt32(reader);

                    UInt32 nVersionName = ReadUInt32(reader);
                    UInt32 nVersionNameSize = ReadUInt32(reader);
                    this.Header = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(Convert.ToInt32(nVersionNameSize))).Trim();

                    UInt32 nAllLayer = ReadUInt32(reader);
                    UInt32 nAllLayerSize = ReadUInt32(reader);
                    //await GetDataAll(reader1);
                    while (true)
                    {
                        if (reader.BaseStream.Position != reader.BaseStream.Length)
                        {

                        }
                        else
                        {
                            break;
                        }

                        UInt32 tempNum = ReadUInt32(reader);

                        // 레이어 시작
                        // 21 - 15 00 00 00 
                        if (tempNum == 21)
                        {
                            UInt32 nLayerInfoSize = ReadUInt32(reader); //79 00 00 00
                        }


                        else if (tempNum == 210)
                        {
                            //Facts f = new Facts();
                            //UInt32 nlayer = ReadUInt32(reader); //d2 00 00 00
                            UInt32 nlayerSize = ReadUInt32(reader); //04 00 00 00
                            float nlayerHeight = ReadFloat(reader); //0a d7 a3 bc
                            layerthickness = nlayerHeight;
                            StringBuilder strbuilder = new StringBuilder();
                            StringBuilder strbuilder3 = new StringBuilder();
                            int transValue = 0;

                            while (true)
                            {

                                if (reader.BaseStream.Position != reader.BaseStream.Length)
                                {

                                }
                                else
                                {

                                    //_facts.Add(f);
                                    break;
                                }

                                UInt32 check0 = ReadUInt32(reader);

                                //d4
                                if (check0 == 212)
                                {

                                    UInt32 datablockSize = ReadUInt32(reader);

                                    //2120
                                    UInt32 udatablockType = ReadUInt32(reader);
                                    UInt32 udatablockTypeSize = ReadUInt32(reader);
                                    var udatablockTypeNumber = reader.ReadBytes(1);
                                    dataNumber = Convert.ToString(udatablockTypeNumber[0]);

                                    // 2121 - 49 08 00 00
                                    UInt32 part_identi = ReadUInt32(reader);
                                    var part_identiSize = ReadUInt32(reader);
                                    var part_identiNumber = ReadUInt32(reader);

                                    // 2122 - 4a 08 00 00
                                    UInt32 com_identi = ReadUInt32(reader);
                                    var com_identiSize = ReadUInt32(reader);
                                    var com_identiNumber = ReadUInt32(reader);

                                    // 2123 - 4b 08 00 00
                                    UInt32 pro_identi = ReadUInt32(reader);
                                    var pro_identiSize = ReadUInt32(reader);
                                    var pro_identiNumber = ReadUInt32(reader);

                                    // 2124 - 4c 08 00 00
                                    UInt32 pro_identii = ReadUInt32(reader);


                                    if (dataNumber == "1") //내부
                                    {
                                        uint ucountSize = ReadUInt32(reader) / 8;
                                        //int uucount = Convert.ToInt32(ucountSize);

                                        for (int h = 0; h < ucountSize; h++)
                                        {
                                            float pointX = ReadFloat(reader);
                                            float pointY = ReadFloat(reader);

                                            //strbuilder.Append(" " + Convert.ToString(pointX * 5) + "," + Convert.ToString(pointY * 5));
                                            //testString = testString + " " + Convert.ToString(pointX * 5) + "," + Convert.ToString(pointY * 5);

                                            if (transValue == 1)
                                            {

                                                strbuilder.Append("M " + Convert.ToString(beforeX1 * 8.5) + "," + Convert.ToString(beforeY1 * 8.5) + " " + Convert.ToString(pointX * 8.5) + "," + Convert.ToString(pointY * 8.5));
                                                transValue = 0;
                                            }
                                            else
                                            {

                                                //strbuilder.Append("M " + Convert.ToString(pointX * 5) + "," + Convert.ToString(pointY * 5) + " ");
                                                beforeX1 = pointX;
                                                beforeY1 = pointY;
                                                transValue = 1;
                                            }

                                        }
                                    }
                                    else if (dataNumber == "3") //외부
                                    {

                                        uint ucountSize = ReadUInt32(reader);

                                        while (true)
                                        {
                                            uint ucount = ReadUInt32(reader);
                                            //f._dataNumber3.Add(3);
                                            //f._dataposition3.Add(Convert.ToString(reader.BaseStream.Position));


                                            tempvalue2 = 3;
                                            if (ucount == 0)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                strbuilder3.Append("M ");

                                                for (int d = 0; d < ucount; d++)
                                                {
                                                    float pointX = ReadFloat(reader);
                                                    float pointY = ReadFloat(reader);
                                                    strbuilder3.Append(" " + Convert.ToString(pointX * 8.5) + "," + Convert.ToString(pointY * 8.5) + " ");
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("여기는 데이터블럭이 없습니다.");
                                    }


                                }
                                else//(check0==)
                                {
                                    uint ucountSize = ReadUInt32(reader);
                                    //_facts.Add(f);
                                    break;
                                }

                            }

                            //Console.WriteLine(Convert.ToString(reader.BaseStream.Position));

                            worker.ReportProgress(Convert.ToInt32(reader.BaseStream.Position + tempFileSize));
                            _stringBuilder.Add(strbuilder);
                            _stringBuilder3.Add(strbuilder3);
                            layercount = layercount + 1;
                        }

                        else
                        {
                            uint ucont = ReadUInt32(reader);
                            MessageBox.Show("여기는 데이터가 없습니다.");
                            break;
                        }


                    }
                    allmodelLayer = layercount;
                    layercount = 0;
                    allmodelingCount++;

                    inStream.Close();
                    tempFileSize += _totalFileSize[i];
                    Console.WriteLine("파일내용 정리 완료" + Convert.ToString(i));
                }


            }
        }

        #endregion
        private void PowderApply()
        {
            //d.OPCItemWriteValueTextBoxes[41] = d.DblPowderSupply1;
            //d.opcWrite("OPCItemSyncWrite41", daServerMgt);
            //d.OPCItemWriteValueTextBoxes[42] = d.DblPowderSupply2;
            //d.opcWrite("OPCItemSyncWrite42", daServerMgt);
            //d.OPCItemWriteValueTextBoxes[43] = d.DblPowderSupply3;
            //d.opcWrite("OPCItemSyncWrite43", daServerMgt);

        }
        private void PowderSupplyRatio()
        {
            d.OPCItemWriteValueTextBoxes[60] = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2);
            d.opcWrite("OPCItemSyncWrite60", daServerMgt);
            psupply.Close();
            //darkbackground.Visibility = Visibility.Collapsed;
        }
        private void PowderSupplyWindow1()
        {
            string tempPowder = Convert.ToString(d.DblPowderSupply1);
            int dbltempPowder = Convert.ToInt32(tempPowder);
            pCheckNumber = 1;

            if (dbltempPowder < 10)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = 0;
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(0, 1));
            }
            else if (dbltempPowder < 100)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(2, 1));
            }
            else if (dbltempPowder < 1000)
            {
                d.Pnumber1 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(3, 1));
            }
            else
            {

            }
            
            psupply = new powderSupplyWindow();
            psupply.initalSetting(d, daServerMgt, 1);
            psupply.Show();
            psupply.Topmost = true;
            
        }

        private void PowderSupplyWindow2()
        {
            string tempPowder = Convert.ToString(d.DblPowderSupply2);
            int dbltempPowder = Convert.ToInt32(tempPowder);
            pCheckNumber = 2;

            if (dbltempPowder < 10)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = 0;
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(0, 1));
            }
            else if (dbltempPowder < 100)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(2, 1));
            }
            else if (dbltempPowder < 1000)
            {
                d.Pnumber1 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(3, 1));
            }
            else
            {

            }

            psupply = new powderSupplyWindow();
            psupply.initalSetting(d, daServerMgt, 2);
            psupply.Show();
            psupply.Topmost = true;

        }

        private void PowderSupplyWindow3()
        {
            string tempPowder = Convert.ToString(d.DblPowderSupply3);
            int dbltempPowder = Convert.ToInt32(tempPowder);
            pCheckNumber = 3;

            if (dbltempPowder < 10)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = 0;
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(0, 1));
            }
            else if (dbltempPowder < 100)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(2, 1));
            }
            else if (dbltempPowder < 1000)
            {
                d.Pnumber1 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
                d.Pnumber3 = Convert.ToInt32(tempPowder.Substring(3, 1));
            }
            else
            {

            }

            psupply = new powderSupplyWindow();
            psupply.initalSetting(d, daServerMgt, 3);
            psupply.Show();
            psupply.Topmost = true;

        }

        private void PowderSupplyWindow4()//파우더 공급비율을 눌렀을때
        {
            string tempPowder = string.Format("{0:0.0}", double.Parse(d.DblPowderSupply4));
            double dbltempPowder = Convert.ToDouble(tempPowder);
            pCheckNumber = 4;

            if (dbltempPowder < 1)
            {
                d.Pnumber1 = 0;
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
                
            }
            else if (dbltempPowder < 10)
            {
                
                d.Pnumber1 = Convert.ToInt32(tempPowder.Substring(0, 1));
                d.Pnumber2 = Convert.ToInt32(tempPowder.Substring(2, 1));
            }
            
            else
            {

            }

            psupply = new powderSupplyWindow();
            psupply.initalSetting(d, daServerMgt, 4);
            psupply.Show();
            psupply.Topmost = true;

        }
        #region Time - Tick
        void est_Tick(object sender, EventArgs e)
        {
            //estDispatcherTimer.Interval = TimeSpan.FromSeconds(1);

            if (estStopWatch.IsRunning)
            {
                //TimeSpan ts = estStartTime - DateTime.Now;
                //int tenths = ts.Milliseconds / 100;

                //int timehour = 0;
                //string strTimehour = "0";
                //if (ts.Days >= 1)
                //{
                //    timehour = (ts.Days * 24) + ts.Hours;
                //    strTimehour = Convert.ToString(timehour);
                //}
                //else
                //{
                //    strTimehour = ts.Hours.ToString("00");
                //}
                //estCurrentTime = strTimehour + "h " + ":" + " " +
                //    ts.Minutes.ToString("00") + "m " + ":" + " " +
                //    ts.Seconds.ToString("00") + "s ";
                //lblPsTime.Content = estCurrentTime;
                
            }
        }

        void ps_Tick(object sender, EventArgs e)
        {
            if (psStopWatch.IsRunning)
            {

                //psStartTime = DateTime.Now.AddSeconds(fixTime);
                //this.ps_StartTime = Properties.Settings.Default.ps_StartTime;
                //TimeSpan ts = psStartTime - ps_StartTime;
                //int tenths = ts.Milliseconds / 100;

                //int timehour = 0;
                //string strTimehour = "0";
                //if (ts.Days >= 1)
                //{
                //    timehour = (ts.Days * 24) + ts.Hours;
                //    strTimehour = Convert.ToString(timehour);
                //}
                //else
                //{
                //    strTimehour = ts.Hours.ToString("00");
                //}

                //psCurrentTime = strTimehour + "h " + ":" + " " +
                //ts.Minutes.ToString("00") + "m " + ":" + " " +
                //ts.Seconds.ToString("00") + "s ";
                ////lbltime.Text = psCurrentTime;
            }
        }

        void bteTimer_Event(object sender, EventArgs e)
        {
            
            bteSeconds = bteSeconds + 1;
            bteClock = timeChnage - bteSeconds;
            double hour = System.Math.Truncate(bteClock / 3600);
            double minute = System.Math.Truncate(bteClock % 3600 / 60);
            double seconds = bteClock % 3600 % 60;
            bteClockTest = string.Format("{0:00}h:{1:00}m:{2:00}s", hour, minute, seconds);
            //lblPsTime.Content = aaa;
            
        

        }
        #endregion

        #region M270-MOTOR MOTOR RESET
        //private void M270Motor5Reset()
        //{
        //    d.OPCItemWriteValueTextBoxes[13] = "5";
        //    d.opcWrite("OPCItemSyncWrite13", daServerMgt);
        //}
        //private void M270Motor4Reset()
        //{
        //    d.OPCItemWriteValueTextBoxes[12] = "5";
        //    d.opcWrite("OPCItemSyncWrite12", daServerMgt);
        //}
        //private void M270Motor3Reset()
        //{
        //    d.OPCItemWriteValueTextBoxes[11] = "5";
        //    d.opcWrite("OPCItemSyncWrite11", daServerMgt);
        //}
        //private void M270Motor2Reset()
        //{
        //    d.OPCItemWriteValueTextBoxes[10] = "5";
        //    d.opcWrite("OPCItemSyncWrite10", daServerMgt);
        //}
        //private void M270Motor1Reset()
        //{
        //    d.OPCItemWriteValueTextBoxes[9] = "5";
        //    d.opcWrite("OPCItemSyncWrite9", daServerMgt);
        //}
        #endregion

        //안씀
        #region M270-MONITORING-CHANGEBUTTON
        private void MonitorAfterBlade()
        {
            //d.OPCItemWriteValueTextBoxes[11] = "0";
            //d.opcWrite("OPCItemSyncWrite30", daServerMgt);
        }

        private void MonitorMotorTwoway()
        {
            //d.OPCItemWriteValueTextBoxes[29] = "0";
            //d.opcWrite("OPCItemSyncWrite29", daServerMgt);
        }

        private void MonitorTotalMotor3()
        {
            //d.OPCItemWriteValueTextBoxes[28] = "0";
            //d.opcWrite("OPCItemSyncWrite28", daServerMgt);
        }

        private void MonitorTotalMotor2()
        {
            //d.OPCItemWriteValueTextBoxes[27] = "0";
            //d.opcWrite("OPCItemSyncWrite27", daServerMgt);
        }

        private void MonitorTotalMotor1()
        {
            //d.OPCItemWriteValueTextBoxes[26] = "0";
            //d.opcWrite("OPCItemSyncWrite26", daServerMgt);
        }

        private void MonitorCameraShot()
        {
            //d.OPCItemWriteValueTextBoxes[15] = "0";
            //d.opcWrite("OPCItemSyncWrite15", daServerMgt);
        }

        private void MonitorScanner()
        {
            //d.OPCItemWriteValueTextBoxes[14] = "0";
            //d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }

        private void MonitorPump()
        {
            //d.OPCItemWriteValueTextBoxes[13] = "0";
            //d.opcWrite("OPCItemSyncWrite13", daServerMgt);
        }
        private void MonitorFilter()
        {
            //d.OPCItemWriteValueTextBoxes[8] = "0";
            //d.opcWrite("OPCItemSyncWrite12", daServerMgt);
        }
        #endregion

        //안씀
        #region 0. M270-MONITORING-TIMER

        void Scannertimer_Tick(object sender, EventArgs e)
        {
            DateTime newStartTime = DateTime.Now.AddSeconds(Convert.ToDouble(d.OPCItemValueTextBoxes[55]));
            TimeSpan elapsed = newStartTime - ScannerStartTime;

            string text = "";
            //if (elapsed.Days > 0)
            //    text += elapsed.Days.ToString() + ".";

            // Convert milliseconds into tenths of seconds.
            int tenths = elapsed.Milliseconds / 100;

            // Compose the rest of the elapsed time.
            text +=
                elapsed.Days.ToString("00") + "일 " + " " +
                elapsed.Hours.ToString("00") + "시간 " + " " +
                elapsed.Minutes.ToString("00") + "분 " + elapsed.Seconds.ToString("00") + "초 ";

            d.StrScannerTime = text;
        }


        void Pumptimer_Tick(object sender, EventArgs e)
        {

            DateTime newStartTime = DateTime.Now.AddSeconds(Convert.ToDouble(d.OPCItemValueTextBoxes[52]));
            TimeSpan elapsed = newStartTime - PumpStartTime;

            string text = "";
            //if (elapsed.Days > 0)
            //    text += elapsed.Days.ToString() + ".";

            // Convert milliseconds into tenths of seconds.
            int tenths = elapsed.Milliseconds / 100;

            // Compose the rest of the elapsed time.
            text +=
                elapsed.Days.ToString("00") + "일 " + " " +
                elapsed.Hours.ToString("00") + "시간 " + " " +
                elapsed.Minutes.ToString("00") + "분 " + elapsed.Seconds.ToString("00") + "초 ";


            //lblTime.Content = text;
            d.StrPumpTime = text;
            int da = 0;
            //lblTime.Content = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds til launch.", t.Days, t.Hours, t.Minutes, t.Seconds);

        }


        void Filtertimer_Tick(object sender, EventArgs e)
        {

            DateTime newStartTime = DateTime.Now.AddSeconds(Convert.ToDouble(d.OPCItemValueTextBoxes[50]));
            TimeSpan elapsed = newStartTime - FilterStartTime;

            string text = "";
            //if (elapsed.Days > 0)
            //    text += elapsed.Days.ToString() + ".";

            // Convert milliseconds into tenths of seconds.
            int tenths = elapsed.Milliseconds / 100;

            // Compose the rest of the elapsed time.
            text +=
                elapsed.Days.ToString("00") + "일 " + " " +
                elapsed.Hours.ToString("00") + "시간 " + " " +
                elapsed.Minutes.ToString("00") + "분 " + elapsed.Seconds.ToString("00") + "초 ";


            //lblTime.Content = text;
            d.StrFilterTime = text;
            //lblTime.Content = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds til launch.", t.Days, t.Hours, t.Minutes, t.Seconds);

        }

        void Machintimer_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime newStartTime = DateTime.Now.AddSeconds(Convert.ToDouble(d.OPCItemValueTextBoxes[51]));
                TimeSpan elapsed = newStartTime - MachineStartTime;

                string text = "";
                int tenths = elapsed.Milliseconds / 100;

                text +=
                    elapsed.Days.ToString("00") + "일 " + " " +
                    elapsed.Hours.ToString("00") + "시간 " + " " +
                    elapsed.Minutes.ToString("00") + "분 " + elapsed.Seconds.ToString("00") + "초 ";

                d.StrMachineTime = text;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 1. M270-MOTOR MOTOR HOME, 수정필요
        private void M270Motor5Home()//홈버튼을 눌렀을때
        {
            //DoubleAnimation da = new DoubleAnimation();
            //da.From = bed3marginsave;
            //da.To = 370;
            //bed3marginsave = 370;
            //da.AccelerationRatio = 0.5;
            //double tempTime = Math.Abs(((da.From.Value - da.To.Value) / 5));
            //da.Duration = new Duration(TimeSpan.FromSeconds(tempTime));
            //_ucMGMotor.Rightbed3.BeginAnimation(Canvas.LeftProperty, da);

            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 5\",\"speed\":5}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        private void M270Motor4Home()
        {
            //DoubleAnimation da = new DoubleAnimation();
            //da.From = bed3marginsave;
            //da.To = 370;
            //bed3marginsave = 370;
            //da.AccelerationRatio = 0.5;
            //double tempTime = Math.Abs(((da.From.Value - da.To.Value) / 5));
            //da.Duration = new Duration(TimeSpan.FromSeconds(tempTime));
            //_ucMGMotor.Rightbed3.BeginAnimation(Canvas.LeftProperty, da);

            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 4\",\"speed\":5}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        private void M270Motor3Home()
        {
            //DoubleAnimation da = new DoubleAnimation();
            //da.From = bed3marginsave;
            //da.To = 370;
            //bed3marginsave = 370;
            //da.AccelerationRatio = 0.5;
            //double tempTime = Math.Abs(((da.From.Value - da.To.Value) / 5));
            //da.Duration = new Duration(TimeSpan.FromSeconds(tempTime));
            //_ucMGMotor.Rightbed3.BeginAnimation(Canvas.LeftProperty, da);

            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 3\",\"speed\":5}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }

        private void M270Motor2Home()
        {
            //DoubleAnimation da = new DoubleAnimation();
            //da.From = bed2marginsave;
            //da.To = 127;
            //bed2marginsave = 127;
            //da.AccelerationRatio = 0.5;
            //double tempTime = Math.Abs(((da.From.Value - da.To.Value) / 3));
            //da.Duration = new Duration(TimeSpan.FromSeconds(tempTime));
            //if (d.OPCItemValueTextBoxes[48] == "0")
            //{
            //    _ucCommonM270Motor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
            //}
            //else if (d.OPCItemValueTextBoxes[48] == "1")
            //{
            //    _ucCommonM270Motor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
            //}
            //else
            //{ }


            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 2\",\"speed\":10}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }

        private void M270Motor1Home()
        {
            //DoubleAnimation da = new DoubleAnimation();
            //da.From = bed1marginsave;
            //da.To = 24;
            //bed1marginsave = 24;
            //da.AccelerationRatio = 0.5;
            //double tempTime = Math.Abs(((da.From.Value - da.To.Value) / 3));
            //da.Duration = new Duration(TimeSpan.FromSeconds(tempTime));

            //if (d.OPCItemValueTextBoxes[48] == "0")
            //{
            //    _ucMGMotor.Rightbed1.BeginAnimation(Canvas.TopProperty, da);
            //}
            //else if (d.OPCItemValueTextBoxes[48] == "1")
            //{
            //    _ucMGMotor.leftbed1.BeginAnimation(Canvas.TopProperty, da);
            //}
            //else
            //{ }

            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 1\",\"speed\":10}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        #endregion

        #region 2. M270-MOTOR-SETTING
        private void Motor5Move()
        {

            string tempPostion = Convert.ToString(d.DblMotor5Distance);

            d.Number1 = Convert.ToInt32(tempPostion.Substring(0, 1));
            d.Number2 = Convert.ToInt32(tempPostion.Substring(1, 1));
            d.Number3 = Convert.ToInt32(tempPostion.Substring(2, 1));
            d.Number4 = Convert.ToInt32(tempPostion.Substring(4, 1));
            d.Number5 = Convert.ToInt32(tempPostion.Substring(5, 1));
            d.Number6 = Convert.ToInt32(tempPostion.Substring(6, 1));


            // MG에는 있는 이미지 애니메이션
            //Uri imgBed1 = new Uri(@"/imgMG/imgMachine/inner_bed_left.png", UriKind.Relative);
            //Uri imgBed2 = new Uri(@"/imgMG/imgMachine/inner_bed_right_on.png", UriKind.Relative);
            //Uri imgBed3 = new Uri(@"/imgMG/imgMachine/inner_recotor.png", UriKind.Relative);

            //_ucMGMotor.leftbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.Rightbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.leftbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed3.Source = new BitmapImage(imgBed3);

            checkNumber = 5;

            if (mw.IsVisible == true)
            {
                mw.initalSetting(d, daServerMgt, 5);
                mw.Topmost = true;
            }
            else
            {
                mw = new moveWindow();
                mw.initalSetting(d, daServerMgt, 5);
                mw.Show();
                mw.Topmost = true;
            }
        }
        private void Motor4Move()
        {

            string tempPostion = Convert.ToString(d.DblMotor4Distance);

            d.Number1 = Convert.ToInt32(tempPostion.Substring(0, 1));
            d.Number2 = Convert.ToInt32(tempPostion.Substring(1, 1));
            d.Number3 = Convert.ToInt32(tempPostion.Substring(2, 1));
            d.Number4 = Convert.ToInt32(tempPostion.Substring(4, 1));
            d.Number5 = Convert.ToInt32(tempPostion.Substring(5, 1));
            d.Number6 = Convert.ToInt32(tempPostion.Substring(6, 1));

            Uri imgBed1 = new Uri(@"/imgMG/imgMachine/inner_bed_left.png", UriKind.Relative);
            Uri imgBed2 = new Uri(@"/imgMG/imgMachine/inner_bed_right_on.png", UriKind.Relative);
            Uri imgBed3 = new Uri(@"/imgMG/imgMachine/inner_recotor.png", UriKind.Relative);

            //_ucMGMotor.leftbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.Rightbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.leftbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed3.Source = new BitmapImage(imgBed3);

            checkNumber = 4;

            if (mw.IsVisible == true)
            {
                mw.initalSetting(d, daServerMgt, 4);
                mw.Topmost = true;
            }
            else
            {
                mw = new moveWindow();
                mw.initalSetting(d, daServerMgt, 4);
                mw.Show();
                mw.Topmost = true;
            }
        }

        private void Motor3Move()
        {
            string tempPostion = Convert.ToString(d.DblMotor3Distance);
            
            d.Number1 = Convert.ToInt32(tempPostion.Substring(0, 1));
            d.Number2 = Convert.ToInt32(tempPostion.Substring(1, 1));
            d.Number3 = Convert.ToInt32(tempPostion.Substring(2, 1));
            d.Number4 = Convert.ToInt32(tempPostion.Substring(4, 1));
            d.Number5 = Convert.ToInt32(tempPostion.Substring(5, 1));
            d.Number6 = Convert.ToInt32(tempPostion.Substring(6, 1));

            Uri imgBed1 = new Uri(@"/imgMG/imgMachine/inner_bed_left.png", UriKind.Relative);
            Uri imgBed2 = new Uri(@"/imgMG/imgMachine/inner_bed_right.png", UriKind.Relative);
            Uri imgBed3 = new Uri(@"/imgMG/imgMachine/inner_recotor_on.png", UriKind.Relative);

            //_ucMGMotor.leftbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.Rightbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.leftbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed3.Source = new BitmapImage(imgBed3);

            checkNumber = 3;

            if (mw.IsVisible == true)
            {
                mw.initalSetting(d, daServerMgt, 3);
                mw.Topmost = true;
            }
            else
            {
                mw = new moveWindow();
                mw.initalSetting(d, daServerMgt, 3);
                mw.Show();
                mw.Topmost = true;
            }
        }
        
        private void Motor2Move()
        {
            
            string tempPostion = Convert.ToString(d.DblMotor2Distance);

            d.Number1 = Convert.ToInt32(tempPostion.Substring(0, 1));
            d.Number2 = Convert.ToInt32(tempPostion.Substring(1, 1));
            d.Number3 = Convert.ToInt32(tempPostion.Substring(2, 1));
            d.Number4 = Convert.ToInt32(tempPostion.Substring(4, 1));
            d.Number5 = Convert.ToInt32(tempPostion.Substring(5, 1));
            d.Number6 = Convert.ToInt32(tempPostion.Substring(6, 1));

            Uri imgBed1 = new Uri(@"/imgMG/imgMachine/inner_bed_left.png", UriKind.Relative);
            Uri imgBed2 = new Uri(@"/imgMG/imgMachine/inner_bed_right_on.png", UriKind.Relative);
            Uri imgBed3 = new Uri(@"/imgMG/imgMachine/inner_recotor.png", UriKind.Relative);

            //_ucMGMotor.leftbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.Rightbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.leftbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed3.Source = new BitmapImage(imgBed3);

            checkNumber = 2;

            if (mw.IsVisible == true)
            {
                mw.initalSetting(d, daServerMgt, 2);
                mw.Topmost = true;
            }
            else
            {
                mw = new moveWindow();
                mw.initalSetting(d, daServerMgt, 2);
                mw.Show();
                mw.Topmost = true;
            }
        }


        private void Motor1Move()
        {
            string tempPostion = Convert.ToString(d.DblMotor1Distance);

            d.Number1 = Convert.ToInt32(tempPostion.Substring(0, 1));
            d.Number2 = Convert.ToInt32(tempPostion.Substring(1, 1));
            d.Number3 = Convert.ToInt32(tempPostion.Substring(2, 1));
            d.Number4 = Convert.ToInt32(tempPostion.Substring(4, 1));
            d.Number5 = Convert.ToInt32(tempPostion.Substring(5, 1));
            d.Number6 = Convert.ToInt32(tempPostion.Substring(6, 1));

            Uri imgBed1 = new Uri(@"/imgMG/imgMachine/inner_bed_left_on.png", UriKind.Relative);
            Uri imgBed2 = new Uri(@"/imgMG/imgMachine/inner_bed_right.png", UriKind.Relative);
            Uri imgBed3 = new Uri(@"/imgMG/imgMachine/inner_recotor.png", UriKind.Relative);


            //_ucMGMotor.leftbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.Rightbed1.Source = new BitmapImage(imgBed1);
            //_ucMGMotor.leftbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed2.Source = new BitmapImage(imgBed2);
            //_ucMGMotor.Rightbed3.Source = new BitmapImage(imgBed3);
            
            checkNumber = 1;

            if (mw.IsVisible == true)
            {
                mw.initalSetting(d, daServerMgt, 1);
                mw.Topmost = true;

            }
            else
            {
                mw = new moveWindow();
                mw.initalSetting(d, daServerMgt, 1);
                mw.Show();
                mw.Topmost = true;
            }
        }

        private int sCheckNumber = 1;
        private int pCheckNumber = 1;

        private void Motor1Speed()
        {
            string tempSpeed = Convert.ToString(d.DblMotor1Speed);
            int dbltempSpeed = Convert.ToInt32(tempSpeed);
            sCheckNumber = 1;

            if (dbltempSpeed < 10)
            {
                d.SNumber1 = 0;
                d.SNumber2 = 0;
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(0, 1));
            }
            else if (dbltempSpeed < 100)
            {
                d.SNumber1 = 0;
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(1, 1));
            }
            else if (dbltempSpeed < 1000)
            {
                d.SNumber1 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(1, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(2, 1));
            }
            else
            {

            }
            
            //if (mw.IsVisible == true || psupply.IsVisible == true || omax.IsVisible == true || omin.IsVisible == true)
            //{
            //    mw.Close();
            //    psupply.Close();
            //    omax.Close();
            //    omin.Close();
            //}

            if (sw.IsVisible == true)
            {
                sw.initalSetting(d, daServerMgt, 1);
                sw.Topmost = true;
            }
            else
            {
                sw = new speedWindows();
                sw.initalSetting(d, daServerMgt, 1);
                sw.Show();
                sw.Topmost = true;
            }
        }

        private void Motor2Speed()
        {
            string tempSpeed = Convert.ToString(d.DblMotor2Speed);
            int dbltempSpeed = Convert.ToInt32(tempSpeed);
            sCheckNumber = 2;

            if (dbltempSpeed < 10)
            {
                d.SNumber1 = 0;
                d.SNumber2 = 0;
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(0, 1));
            }
            else if (dbltempSpeed < 100)
            {
                d.SNumber1 = 0;
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(1, 1));
            }
            else if (dbltempSpeed < 1000)
            {
                d.SNumber1 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(1, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(2, 1));
            }
            else
            {

            }

            //if (mw.IsVisible == true || psupply.IsVisible == true || omax.IsVisible == true || omin.IsVisible == true)
            //{
            //    mw.Close();
            //    psupply.Close();
            //    omax.Close();
            //    omin.Close();
            //}

            if (sw.IsVisible == true)
            {
                sw.initalSetting(d, daServerMgt, 2);
                sw.Topmost = true;
            }
            else
            {
                sw = new speedWindows();
                sw.initalSetting(d, daServerMgt, 2);
                sw.Show();
                sw.Topmost = true;
            }
        }

        private void Motor3Speed()
        {
            string tempSpeed = Convert.ToString(d.DblMotor3Speed);
            int dbltempSpeed = Convert.ToInt32(tempSpeed);
            sCheckNumber = 3;

            if (dbltempSpeed < 10)
            {
                d.SNumber1 = 0;
                d.SNumber2 = 0;
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(0, 1));
            }
            else if (dbltempSpeed < 100)
            {
                d.SNumber1 = 0;
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(1, 1));
            }
            else if (dbltempSpeed < 1000)
            {
                d.SNumber1 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(1, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(2, 1));
            }
            else
            {

            }

            //if (mw.IsVisible == true || psupply.IsVisible == true || omax.IsVisible == true || omin.IsVisible == true)
            //{
            //    mw.Close();
            //    psupply.Close();
            //    omax.Close();
            //    omin.Close();
            //}

            if (sw.IsVisible == true)
            {
                sw.initalSetting(d, daServerMgt, 3);
                sw.Topmost = true;
            }
            else
            {
                sw = new speedWindows();
                sw.initalSetting(d, daServerMgt, 3);
                sw.Show();
                sw.Topmost = true;
            }
        }

        private void Motor4Speed()
        {
            string tempSpeed = Convert.ToString(d.DblMotor4Speed);
            int dbltempSpeed = Convert.ToInt32(tempSpeed);
            sCheckNumber = 4;

            if (dbltempSpeed < 10)
            {
                d.SNumber1 = 0;
                d.SNumber2 = 0;
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(0, 1));
            }
            else if (dbltempSpeed < 100)
            {
                d.SNumber1 = 0;
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(1, 1));
            }
            else if (dbltempSpeed < 1000)
            {
                d.SNumber1 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(1, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(2, 1));
            }
            else
            {

            }

            //if (mw.IsVisible == true || psupply.IsVisible == true || omax.IsVisible == true || omin.IsVisible == true)
            //{
            //    mw.Close();
            //    psupply.Close();
            //    omax.Close();
            //    omin.Close();
            //}

            if (sw.IsVisible == true)
            {
                sw.initalSetting(d, daServerMgt, 4);
                sw.Topmost = true;
            }
            else
            {
                sw = new speedWindows();
                sw.initalSetting(d, daServerMgt, 4);
                sw.Show();
                sw.Topmost = true;
            }
        }
        
        private void Motor5Speed()
        {
            string tempSpeed = Convert.ToString(d.DblMotor5Speed);
            int dbltempSpeed = Convert.ToInt32(tempSpeed);
            sCheckNumber = 5;

            if (dbltempSpeed < 10)
            {
                d.SNumber1 = 0;
                d.SNumber2 = 0;
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(0, 1));
            }
            else if (dbltempSpeed < 100)
            {
                d.SNumber1 = 0;
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(1, 1));
            }
            else if (dbltempSpeed < 1000)
            {
                d.SNumber1 = Convert.ToInt32(tempSpeed.Substring(0, 1));
                d.SNumber2 = Convert.ToInt32(tempSpeed.Substring(1, 1));
                d.SNumber3 = Convert.ToInt32(tempSpeed.Substring(2, 1));
            }
            else
            {

            }
            
            //if (mw.IsVisible == true || psupply.IsVisible == true || omax.IsVisible == true || omin.IsVisible == true)
            //{
            //    mw.Close();
            //    psupply.Close();
            //    omax.Close();
            //    omin.Close();
            //}

            if (sw.IsVisible == true)
            {
                sw.initalSetting(d, daServerMgt, 5);
                sw.Topmost = true;
            }
            else
            {
                sw = new speedWindows();
                sw.initalSetting(d, daServerMgt, 5);
                sw.Show();
                sw.Topmost = true;
            }
        }
        
        private void BuildRoomFocus()
        {
            //_ucCommonM270Motor.BuildRoomBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            //imgBuildRoom.Visibility = Visibility.Visible;

            //_ucCommonM270Motor.RecotorBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //imgRecotor.Visibility = Visibility.Collapsed;

            //_ucCommonM270Motor.FrontBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //_ucCommonM270Motor.RearBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //_ucCommonM270Motor.SupplyBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        }

        private void RecotorFocus()
        {
            //_ucCommonM270Motor.BuildRoomBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //imgBuildRoom.Visibility = Visibility.Collapsed;
            //_ucCommonM270Motor.RecotorBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            //imgRecotor.Visibility = Visibility.Visible;

            //_ucCommonM270Motor.FrontBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //_ucCommonM270Motor.RearBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //_ucCommonM270Motor.SupplyBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void FrontFocus()
        {
            //_ucCommonM270Motor.BuildRoomBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //imgBuildRoom.Visibility = Visibility.Collapsed;
            //_ucCommonM270Motor.RecotorBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //imgRecotor.Visibility = Visibility.Collapsed;

            //_ucCommonM270Motor.FrontBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            //_ucCommonM270Motor.RearBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //_ucCommonM270Motor.SupplyBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void RearFocus()
        {
            //_ucCommonM270Motor.BuildRoomBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //imgBuildRoom.Visibility = Visibility.Collapsed;
            //_ucCommonM270Motor.RecotorBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //imgRecotor.Visibility = Visibility.Collapsed;

            //_ucCommonM270Motor.FrontBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //_ucCommonM270Motor.RearBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            //_ucCommonM270Motor.SupplyBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void SupplyFocus()
        {
            //_ucCommonM270Motor.BuildRoomBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //imgBuildRoom.Visibility = Visibility.Collapsed;
            //_ucCommonM270Motor.RecotorBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //imgRecotor.Visibility = Visibility.Collapsed;

            //_ucCommonM270Motor.FrontBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //_ucCommonM270Motor.RearBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            //_ucCommonM270Motor.SupplyBorder.OpacityMask = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }
        #endregion

        #region 3. M270-MOTOR-MOTORTWOWAY
        private void MotorTwoway()
        {
            //DoubleAnimation da = new DoubleAnimation();
            //da.From = 370;
            //da.To = 100;
            //da.AccelerationRatio = 0.5;
            //double tempTime = (182 / 40) + 2.5;
            //da.Duration = new Duration(TimeSpan.FromSeconds(tempTime));
            //da.Completed += new EventHandler(da_Completed);
            //_ucMGMotor.Rightbed3.BeginAnimation(Canvas.LeftProperty, da);


            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Script.RecoaterTwoWay\"}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }

        private void da_Completed(object sender, EventArgs e)
        {
            DoubleAnimation da1 = new DoubleAnimation();
            da1.From = 100;
            da1.To = 370;
            da1.AccelerationRatio = 0.5;
            double tempTime1 = (182 / 50) + 2.5;
            da1.Duration = new Duration(TimeSpan.FromSeconds(tempTime1));
            //_ucMGMotor.Rightbed3.BeginAnimation(Canvas.LeftProperty, da1);
        }
        #endregion

        #region 4. M270-MOTOR-MOTORSETPOSTION
        public void Motor1SetPostion()
        {
            //var tempbed1 = 24 - ((127 * Convert.ToDouble(d.DblMotor1Position)) / 106);
            //bed1marginsave = tempbed1;

            if (fristCheck == "0")
            {
                var tempbed1 = 24 - ((127 * Convert.ToDouble(d.DblMotor1Position)) / 106);
                bed1marginsave = tempbed1;
                //Canvas.SetTop(_ucMGMotor.Rightbed1, bed1marginsave);
                //Canvas.SetTop(_ucMGMotor.leftbed1, bed1marginsave);
            }
            else
            {
                
            }

            //Canvas.SetTop(_ucMGLaser.Rightbed1, bed1marginsave);
            //Canvas.SetTop(_ucMGHygrothermograph.Rightbed1, bed1marginsave);
            //Canvas.SetTop(_ucMGPUMP.Rightbed1, bed1marginsave);
            //Canvas.SetTop(_ucMGGAS.Rightbed1, bed1marginsave);
            //Canvas.SetTop(_ucMGLED.Rightbed1, bed1marginsave);

            //Canvas.SetTop(_ucMGLaser.leftbed1, bed1marginsave);
            //Canvas.SetTop(_ucMGHygrothermograph.leftbed1, bed1marginsave);
            //Canvas.SetTop(_ucMGPUMP.leftbed1, bed1marginsave);
            //Canvas.SetTop(_ucMGGAS.leftbed1, bed1marginsave);
            //Canvas.SetTop(_ucMGLED.leftbed1, bed1marginsave);

        }
        public void Motor2SetPostion()
        {

            //var tempbed2 = 127 - (127 * Convert.ToDouble(d.DblMotor2Position)) / 106;
            //bed2marginsave = tempbed2;

            if (fristCheck == "0")
            {
                var tempbed2 = 127 - (127 * Convert.ToDouble(d.DblMotor2Position)) / 106;
                bed2marginsave = tempbed2;
                //Canvas.SetTop(_ucMGMotor.Rightbed2, bed2marginsave);
                //Canvas.SetTop(_ucMGMotor.leftbed2, bed2marginsave);
            }
            else
            {

            }


            //Canvas.SetTop(_ucMGLaser.Rightbed2, bed2marginsave);
            //Canvas.SetTop(_ucMGHygrothermograph.Rightbed2, bed2marginsave);
            //Canvas.SetTop(_ucMGPUMP.Rightbed2, bed2marginsave);
            //Canvas.SetTop(_ucMGGAS.Rightbed2, bed2marginsave);
            //Canvas.SetTop(_ucMGLED.Rightbed2, bed2marginsave);

            //Canvas.SetTop(_ucMGLaser.leftbed2, bed2marginsave);
            //Canvas.SetTop(_ucMGHygrothermograph.leftbed2, bed2marginsave);
            //Canvas.SetTop(_ucMGPUMP.leftbed2, bed2marginsave);
            //Canvas.SetTop(_ucMGGAS.leftbed2, bed2marginsave);
            //Canvas.SetTop(_ucMGLED.leftbed2, bed2marginsave);
        }
        public void Motor3SetPostion()
        {
            if (fristCheck == "0")
            {
                var tempbed3 = 370 - ((370 * Convert.ToDouble(d.DblMotor3Position)) / 225);
                bed3marginsave = tempbed3;
                //Canvas.SetLeft(_ucMGMotor.Rightbed3, bed3marginsave);
            }
            else
            {

            }

            //Canvas.SetLeft(_ucMGLaser.Rightbed3, bed3marginsave);
            //Canvas.SetLeft(_ucMGHygrothermograph.Rightbed3, bed3marginsave);
            //Canvas.SetLeft(_ucMGPUMP.Rightbed3, bed3marginsave);
            //Canvas.SetLeft(_ucMGGAS.Rightbed3, bed3marginsave);
            //Canvas.SetLeft(_ucMGLED.Rightbed3, bed3marginsave);
        }
        #endregion

        #region 5. M270-PRINTING
        private void PrintStop()
        {
            var uriSourceStop = new Uri(@"/imgTab/6_printing/print_btn_stop_enable.png", UriKind.Relative);
            if (imgPrintStop.Source.ToString().Contains(uriSourceStop.ToString()))
            {
                imgPrintStart.Visibility = Visibility.Visible;
                imgPrintResume.Visibility = Visibility.Collapsed;
                psStopWatch.Stop();
                psDispatcherTimer.Stop();
                d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Job.Stop\"}";
                d.opcWrite("OPCItemSyncWrite14", daServerMgt);
            }
        }
        
        private void PrintResume()
        {
            if (imgPrintResume.Visibility == Visibility.Visible)
            {
                _timerPrintingRunning = false;
                d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Job.Resume\"}";
                d.opcWrite("OPCItemSyncWrite14", daServerMgt);
            }
        }

        private void PrintPause()
        {
            var uriSourcePause = new Uri(@"/imgTab/6_printing/print_btn_pause_enable.png", UriKind.Relative);
            if (imgPrintPause.Source.ToString().Contains(uriSourcePause.ToString()))
            {
                _timerPrintingRunning = false;

                d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Job.Pause\"}";
                d.opcWrite("OPCItemSyncWrite14", daServerMgt);
            }
        }

        private void PrintFree()
        {
            if (imgPrintFinish.Visibility == Visibility.Visible)
            {
                d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Job.Free\"}";
                d.opcWrite("OPCItemSyncWrite14", daServerMgt);
            }
        }

        private void PrintStart()
        {
            var uriSourceStart = new Uri(@"/imgTab/6_printing/print_btn_start_enable.png", UriKind.Relative);
            if (imgPrintStart.Source.ToString().Contains(uriSourceStart.ToString()))
            {
                if (printStr == "")
                {
                    MessageBox.Show("파일을 우선 선택해주세요");
                }
                else
                {
                    try
                    {
                        DispatcherTimerSample();

                        _timerPrintingRunning = false;
                        string strTime = DateTime.Now.ToString("yy.MM.dd HH-mm-ss");
                        //d.OPCItemWriteValueTextBoxes[22] = strTime + " " + lvFile.SelectedItem.ToString();
                        //d.opcWrite("OPCItemSyncWrite22", daServerMgt);

                        //FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(d.FtpName + "CameraFile/" + strTime + " " + lvFile.SelectedItem.ToString());
                        //requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                        //requestDir.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                        //requestDir.Timeout = 120000;

                        //using (var resp = (FtpWebResponse)requestDir.GetResponse())
                        //{
                        //    Console.WriteLine("asd" + resp.StatusCode);
                        //    Debug.WriteLine("asd" + resp.StatusCode);
                        //}
                        
                        d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Job.Start\",\"parameters\":{\"id\":\"" + printStr + "/" + printStr + ".job\"}}";//Lua
                        d.opcWrite("OPCItemSyncWrite14", daServerMgt);
                        
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
        }


        private static void MakeDirectory(string directory)
        {
            //Log("Making directory...");

            var request = (FtpWebRequest)WebRequest.Create(directory);

            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("user", "pass");

            try
            {
                using (var resp = (FtpWebResponse)request.GetResponse()) // Exception occurs here
                {
                    //Log(resp.StatusCode.ToString());
                }
            }
            catch (WebException ex)
            {
                //Log(ex.Message);
            }
        }

        private void PrintFinish()
        {
            estStopWatch.Reset();
            psStopWatch.Reset();
            timeChnage = 0;
            lbltime.Text = "00h : 00m : 00s";
            lblPsTime.Content = "00h : 00m : 00s";
            
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Job.Free\"}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }

        private void PrintFileDelete()
        {
            try
            {
                string tempFile = lvFile.SelectedItem.ToString();
                string _ftpUri = d.FtpName + "Job Files/" + tempFile;
                Uri ftpUri = new Uri(_ftpUri);

                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpUri);
                req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                req.Timeout = 120000;
                req.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse resFtp = (FtpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(resFtp.GetResponseStream(), System.Text.Encoding.Default, true);
                string strData = reader.ReadToEnd();
                string[] filesinDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                resFtp.Close();

                for (int i = 0; i < filesinDirectory.Count(); i++)
                {
                    string tempUri = d.FtpName + "Job Files/" + filesinDirectory[i];

                    FtpWebRequest _req = (FtpWebRequest)WebRequest.Create(tempUri);
                    _req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                    _req.Timeout = 120000;
                    _req.Method = WebRequestMethods.Ftp.DeleteFile;

                    FtpWebResponse _resFtp = (FtpWebResponse)_req.GetResponse();
                    _resFtp.Close();
                }

                string tempUriFolder = d.FtpName + "Job Files/" + tempFile;

                FtpWebRequest _reqFolder = (FtpWebRequest)WebRequest.Create(tempUriFolder);
                _reqFolder.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                _reqFolder.Timeout = 120000;
                _reqFolder.Method = WebRequestMethods.Ftp.RemoveDirectory;

                FtpWebResponse _resFtpFolder = (FtpWebResponse)_reqFolder.GetResponse();
                _resFtpFolder.Close();
                DownloadFile();

                lvFile.SelectedIndex = 0;

            }
            catch (Exception exa)
            {
                MessageBox.Show(exa.Message);
            }
        }

        DateTime StartTime;
        DispatcherTimer timer;
        private void DispatcherTimerSample()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            StartTime = DateTime.Now;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            //lblTime.Content = "0";

            TimeSpan elapsed = DateTime.Now - StartTime;

            // Start with the days if greater than 0.
            string text = "";
            if (elapsed.Days > 0)
                text += elapsed.Days.ToString() + ".";

            // Convert milliseconds into tenths of seconds.
            int tenths = elapsed.Milliseconds / 100;

            // Compose the rest of the elapsed time.
            text +=
                //elapsed.Days.ToString("0") + "d "+ ":" + " " +
                elapsed.Hours.ToString("00") + "h " + ":" + " " +
                elapsed.Minutes.ToString("00") + "m " + ":" + " " +
                elapsed.Seconds.ToString("00") + "s ";


            //lblTime.Content = text;
            //lbltime.Text = text;
            //lblTime.Content = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds til launch.", t.Days, t.Hours, t.Minutes, t.Seconds);

        }
        #endregion

        #region 6. M270-6VALUES-UP/DOWN (Speed)
        #region down
        private void SDown1()
        {
            d.SNumber1--;
            if (d.SNumber1 == -1)
            {
                d.SNumber1 = 9;
            }

            funSResult();
        }
        private void SDown2()
        {
            d.SNumber2--;
            if (d.SNumber2 == -1)
            {
                d.SNumber2 = 9;
            }

            funSResult();
        }
        private void SDown3()
        {
            d.SNumber3--;
            if (d.SNumber3 == -1)
            {
                d.SNumber3 = 9;
            }

            funSResult();
        }
        private void PDown1()
        {
            d.Pnumber1--;
            if (d.Pnumber1 == -1)
            {
                d.Pnumber1 = 9;
            }

            funPResult();
        }
        private void PDown2()
        {
            d.Pnumber2--;
            if (d.Pnumber2 == -1)
            {
                d.Pnumber2 = 9;
            }

            funPResult();
        }
        private void PDown3()
        {
            d.Pnumber3--;
            if (d.Pnumber3 == -1)
            {
                d.Pnumber3 = 9;
            }

            funPResult();
        }
        private void Down6()
        {
            d.Number6--;
            if (d.Number6 == -1)
            {
                d.Number6 = 9;
            }
            funResult();
        }
        private void Down5()
        {
            d.Number5--;
            if (d.Number5 == -1)
            {
                d.Number5 = 9;
            }
            funResult();
        }

        private void Down4()
        {
            d.Number4--;
            if (d.Number4 == -1)
            {
                d.Number4 = 9;
            }
            funResult();
        }

        private void Down3()
        {
            d.Number3--;
            if (d.Number3 == -1)
            {
                d.Number3 = 9;
            }
            funResult();
        }

        private void Down2()
        {
            d.Number2--;
            if (d.Number2 == -1)
            {
                d.Number2 = 9;
            }
            funResult();
        }

        private void Down1()
        {
            d.Number1--;
            if (d.Number1 == -1)
            {
                d.Number1 = 9;
            }
            funResult();
        }
        #endregion

        #region up

        private void funSResult()
        {
            if (sCheckNumber == 1)
            {
                string tempSNumber = Convert.ToString(d.SNumber1) + Convert.ToString(d.SNumber2) + Convert.ToString(d.SNumber3);
                int tempSint = Convert.ToInt32(tempSNumber);
                d.DblMotor1Speed = Convert.ToString(tempSint);
            }
            else if (sCheckNumber == 2)
            {
                string tempSNumber = Convert.ToString(d.SNumber1) + Convert.ToString(d.SNumber2) + Convert.ToString(d.SNumber3);
                int tempSint = Convert.ToInt32(tempSNumber);
                d.DblMotor2Speed = Convert.ToString(tempSint);
            }
            else if (sCheckNumber == 3)
            {
                string tempSNumber = Convert.ToString(d.SNumber1) + Convert.ToString(d.SNumber2) + Convert.ToString(d.SNumber3);
                int tempSint = Convert.ToInt32(tempSNumber);
                d.DblMotor3Speed = Convert.ToString(tempSint);
            }
            else if (sCheckNumber == 4)
            {
                string tempSNumber = Convert.ToString(d.SNumber1) + Convert.ToString(d.SNumber2) + Convert.ToString(d.SNumber3);
                int tempSint = Convert.ToInt32(tempSNumber);
                d.DblMotor4Speed = Convert.ToString(tempSint);
            }
            else if (sCheckNumber == 5)
            {
                string tempSNumber = Convert.ToString(d.SNumber1) + Convert.ToString(d.SNumber2) + Convert.ToString(d.SNumber3);
                int tempSint = Convert.ToInt32(tempSNumber);
                d.DblMotor5Speed = Convert.ToString(tempSint);
            }
        }
        private void funPResult()//파우더값이 변할때
        {
            if (pCheckNumber == 1)
            {
                d.DblPowderSupply1 = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2) + Convert.ToString(d.Pnumber3);
                
            }
            else if (pCheckNumber == 2)
            {
                d.DblPowderSupply2 = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2) + Convert.ToString(d.Pnumber3);
                
            }
            else if (pCheckNumber == 3)
            {
                d.DblPowderSupply3 = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2) + Convert.ToString(d.Pnumber3);
                
            }
            else if (pCheckNumber == 4)
            {
                d.DblPowderSupply4 = Convert.ToString(d.Pnumber1) + "." + Convert.ToString(d.Pnumber2) + Convert.ToString(d.Pnumber3);

            }
        }
        private void SUp1()
        {
            d.SNumber1++;
            if (d.SNumber1 == 10)
            {
                d.SNumber1 = 0;
            }

            funSResult();
        }
        private void SUp2()
        {
            d.SNumber2++;
            if (d.SNumber2 == 10)
            {
                d.SNumber2 = 0;
            }

            funSResult();
        }
        private void SUp3()
        {
            d.SNumber3++;
            if (d.SNumber3 == 10)
            {
                d.SNumber3 = 0;
            }

            funSResult();
        }
        private void PUp1()
        {
            d.Pnumber1++;
            if (d.Pnumber1 == 10)
            {
                d.Pnumber1 = 0;
            }

            funPResult();
        }
        private void PUp2()
        {
            d.Pnumber2++;
            if (d.Pnumber2 == 10)
            {
                d.Pnumber2 = 0;
            }

            funPResult();
        }
        private void PUp3()
        {
            d.Pnumber3++;
            if (d.Pnumber3 == 10)
            {
                d.Pnumber3 = 0;
            }

            funPResult();
        }
        private void Up6()
        {
            d.Number6++;
            if (d.Number6 == 10)
            {
                d.Number6 = 0;
            }
            funResult();
        }
        private void Up5()
        {
            d.Number5++;
            if (d.Number5 == 10)
            {
                d.Number5 = 0;
            }
            funResult();
        }

        private void Up4()
        {
            d.Number4++;
            if (d.Number4 == 10)
            {
                d.Number4 = 0;
            }
            funResult();
        }

        private void Up3()
        {
            d.Number3++;
            if (d.Number3 == 10)
            {
                d.Number3 = 0;
            }
            funResult();
        }

        private void Up2()
        {
            d.Number2++;
            if (d.Number2 == 10)
            {
                d.Number2 = 0;
            }
            funResult();
        }

        private void Up1()
        {
            d.Number1++;
            if (d.Number1 == 10)
            {
                d.Number1 = 0;
            }
            funResult();
        }


        private void funResult()
        {
            if (checkNumber == 1)
            {
                d.DblMotor1Distance = Convert.ToString(d.Number1) + Convert.ToString(d.Number2) + Convert.ToString(d.Number3) + "." + Convert.ToString(d.Number4) + Convert.ToString(d.Number5) + Convert.ToString(d.Number6);
            }
            else if (checkNumber == 2)
            {
                d.DblMotor2Distance = Convert.ToString(d.Number1) + Convert.ToString(d.Number2) + Convert.ToString(d.Number3) + "." + Convert.ToString(d.Number4) + Convert.ToString(d.Number5) + Convert.ToString(d.Number6);
            }
            else if (checkNumber == 3)
            {
                d.DblMotor3Distance = Convert.ToString(d.Number1) + Convert.ToString(d.Number2) + Convert.ToString(d.Number3) + "." + Convert.ToString(d.Number4) + Convert.ToString(d.Number5) + Convert.ToString(d.Number6);
            }
            else if (checkNumber == 4)
            {
                d.DblMotor4Distance = Convert.ToString(d.Number1) + Convert.ToString(d.Number2) + Convert.ToString(d.Number3) + "." + Convert.ToString(d.Number4) + Convert.ToString(d.Number5) + Convert.ToString(d.Number6);
            }
            else if (checkNumber == 5)
            {
                d.DblMotor5Distance = Convert.ToString(d.Number1) + Convert.ToString(d.Number2) + Convert.ToString(d.Number3) + "." + Convert.ToString(d.Number4) + Convert.ToString(d.Number5) + Convert.ToString(d.Number6);
            }
        }
        #endregion

        #endregion

        #region 7. M270-RIGHT/LEFT/UP/DOWN (MOTOR UP,DOWN만 사용)
        private int checkNumber = 1;
        public double bed1marginsave = 24;
        public double bed2marginsave = 127;
        public double bed3marginsave = 370;

        #region MotorRight, 현재 사용하지않음
        private void MotorRight()
        {
            var tempMove = Convert.ToSingle(d.DblMotor3Position) - Convert.ToSingle(d.DblMotor3Distance);
            string tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 3\",\"speed\":" + d.DblMotor3Speed + ",\"position\":" + tempMove + "}}";//Lua

            DoubleAnimation da = new DoubleAnimation();
            da.From = bed3marginsave;
            da.To = 370 - ((370 * Convert.ToDouble(tempMove)) / 225);
            bed3marginsave = 370 - ((370 * Convert.ToDouble(tempMove)) / 225);
            da.AccelerationRatio = 0.7;
            double tempTime = (Convert.ToDouble(d.DblMotor3Distance) / 5);
            da.Duration = new Duration(TimeSpan.FromSeconds(tempTime));
            //_ucMGMotor.Rightbed3.BeginAnimation(Canvas.LeftProperty, da);


            d.OPCItemWriteValueTextBoxes[14] = Convert.ToString(tempGo);
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        #endregion

        #region MotorLeft, 현재 사용하지않음
        private void MotorLeft()
        {
            var tempMove = Convert.ToSingle(d.DblMotor3Position) + Convert.ToSingle(d.DblMotor3Distance);
            string tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 3\",\"speed\":" + d.DblMotor3Speed + ",\"position\":" + tempMove + "}}";//Lua

            DoubleAnimation da = new DoubleAnimation();
            da.From = bed3marginsave;
            da.To = 370 - ((370 * Convert.ToDouble(tempMove)) / 225);
            bed3marginsave = 370 - ((370 * Convert.ToDouble(tempMove)) / 225);
            da.AccelerationRatio = 0.7;
            double tempTime = (Convert.ToDouble(d.DblMotor3Distance) / 5);
            da.Duration = new Duration(TimeSpan.FromSeconds(tempTime));
            //_ucMGMotor.Rightbed3.BeginAnimation(Canvas.LeftProperty, da);

            d.OPCItemWriteValueTextBoxes[14] = Convert.ToString(tempGo);
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        #endregion

        #region MotroDown
        private void MotorDown()
        {
            string tempGo = "";
           
            if (checkNumber == 1)
            {
                var tempMove = Convert.ToSingle(d.DblMotor1Position) - Convert.ToSingle(d.DblMotor1Distance);


                if (d.OPCItemValueTextBoxes[47] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[47] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 1\",\"speed\":" + d.DblMotor1Speed + ",\"position\":" + tempMove + "}}";


            }
            else if (checkNumber == 2)
            {
                var tempMove = Convert.ToSingle(d.DblMotor2Position) - Convert.ToSingle(d.DblMotor2Distance);


                if (d.OPCItemValueTextBoxes[47] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[47] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 2\",\"speed\":" + d.DblMotor2Speed + ",\"position\":" + tempMove + "}}";
            }
            else if (checkNumber == 3)
            {

                var tempMove = Convert.ToSingle(d.DblMotor3Position) - Convert.ToSingle(d.DblMotor3Distance);


                if (d.OPCItemValueTextBoxes[48] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[48] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 3\",\"speed\":" + d.DblMotor3Speed + ",\"position\":" + tempMove + "}}";
            }

            else if (checkNumber == 4)
            {

                var tempMove = Convert.ToSingle(d.DblMotor4Position) - Convert.ToSingle(d.DblMotor4Distance);


                if (d.OPCItemValueTextBoxes[48] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[48] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 4\",\"speed\":" + d.DblMotor4Speed + ",\"position\":" + tempMove + "}}";
            }

            else if (checkNumber == 5)
            {

                var tempMove = Convert.ToSingle(d.DblMotor5Position) - Convert.ToSingle(d.DblMotor5Distance);


                if (d.OPCItemValueTextBoxes[48] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[48] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 5\",\"speed\":" + d.DblMotor5Speed + ",\"position\":" + tempMove + "}}";
            }

            else
            {

            }


            d.OPCItemWriteValueTextBoxes[14] = Convert.ToString(tempGo);
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }

        #endregion

        #region MotorUp
        private void MotorUp()
        {
            string tempGo = "";

            if (checkNumber == 1)
            {
                var tempMove = Convert.ToSingle(d.DblMotor1Position) + Convert.ToSingle(d.DblMotor1Distance);


                if (d.OPCItemValueTextBoxes[47] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[47] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }
                
                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 1\",\"speed\":" + d.DblMotor1Speed + ",\"position\":" + tempMove + "}}";


            }
            else if (checkNumber == 2)
            {
                var tempMove = Convert.ToSingle(d.DblMotor2Position) + Convert.ToSingle(d.DblMotor2Distance);


                if (d.OPCItemValueTextBoxes[47] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[47] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 2\",\"speed\":" + d.DblMotor2Speed + ",\"position\":" + tempMove + "}}";
            }
            else if (checkNumber == 3)
            {

                var tempMove = Convert.ToSingle(d.DblMotor3Position) + Convert.ToSingle(d.DblMotor3Distance);


                if (d.OPCItemValueTextBoxes[48] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[48] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 3\",\"speed\":" + d.DblMotor3Speed + ",\"position\":" + tempMove + "}}";
            }

            else if (checkNumber == 4)
            {

                var tempMove = Convert.ToSingle(d.DblMotor4Position) + Convert.ToSingle(d.DblMotor4Distance);


                if (d.OPCItemValueTextBoxes[48] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[48] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 4\",\"speed\":" + d.DblMotor4Speed + ",\"position\":" + tempMove + "}}";
            }

            else if (checkNumber == 5)
            {

                var tempMove = Convert.ToSingle(d.DblMotor5Position) + Convert.ToSingle(d.DblMotor5Distance);


                if (d.OPCItemValueTextBoxes[48] == "0")
                {
                    //_ucMGMotor.Rightbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else if (d.OPCItemValueTextBoxes[48] == "1")
                {
                    //_ucMGMotor.leftbed2.BeginAnimation(Canvas.TopProperty, da);
                }
                else
                {

                }

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 5\",\"speed\":" + d.DblMotor5Speed + ",\"position\":" + tempMove + "}}";
            }

            else
            {

            }

            d.OPCItemWriteValueTextBoxes[14] = Convert.ToString(tempGo);
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        #endregion
        #endregion

        #region 8. M270-CLOSE
        private void Window_Closed(object sender, EventArgs e)
        {
            DisconnectOPCServer();
        }

        private void WindowClose()
        {
            closeWindow cw = new closeWindow(this);
            darkbackground.Visibility = Visibility.Visible;
            cw.Show();
        }

        #endregion

        #region 9 ~ 12 M270-FILE_SELECT

        int binFileCount = 0;
        int binDownCount = 0;
        private int f_download = 0;
        private int showlayer = 0;
        private long fileSize = 0;
        List<string> allFile = new List<string>();
        int allmodelingCount = 0;
        int allmodelLayer = 0;
        string printStr = "";
        public string adada = "";

        #region 9. M270-PRINTING-FILEARRAY
        private void PrintFileArray()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 10. M270-PRINTING-FILERESET
        private void PrintReset()
        {
            lvFile.SelectedIndex = 0;

            Uri ftpUri = new Uri(d.FtpName);
            WebClient wc = new WebClient();

            cd_FileLoading cdFileLoading = new cd_FileLoading();
            wc.Credentials = new NetworkCredential("ftpuser", "ftpuser");
            string[] strfile = cdFileLoading.GetFileList(d.FtpName);

            lvFile.ItemsSource = strfile;
        }
        #endregion

        #region 11. M270-PRINTING-FILESELET

        private bool bDelete = false;
        List<System.Windows.Shapes.Path> _path = new List<System.Windows.Shapes.Path>();
        private List<double> _totalFileSize = new List<double>();
        List<StringBuilder> _stringBuilder = new List<StringBuilder>();
        List<StringBuilder> _stringBuilder3 = new List<StringBuilder>();
        string[] strfile;
        int beforeIndex;
        private void lvFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (aniLoading.Visibility != Visibility.Visible)
            {
                if (bDelete == false)
                {
                    aniLoading.Visibility = Visibility.Visible;

                    _path.Clear();
                    binFileCount = 0;
                    binDownCount = 0;
                    f_download = 0;
                    allFile.Clear();


                    lvFile.IsEnabled = true;
                    _totalFileSize.Clear();
                    _stringBuilder.Clear();
                    _stringBuilder3.Clear();



                    int index = lvFile.SelectedIndex;
                    beforeIndex = lvFile.SelectedIndex;
                    string tempFile = lvFile.SelectedItem.ToString();
                    allmodelingCount = 0;

                    showlayer = 0;
                    slLayer1.Value = 0;

                    string sDirPath = @"C:\\MYD_METAL\\" + tempFile;
                    string path = @"C:\\MYD_METAL\\";
                    Directory.Delete(path, true);
                    DirectoryInfo di = new DirectoryInfo(sDirPath);
                    printStr = tempFile;
                    if (di.Exists == false)
                    {
                        di.Create();
                        progress.Visibility = Visibility.Visible;
                        aniLoading.logingText.Text = "Calculating...";
                        cd_FileLoading cdFileLoading = new cd_FileLoading();
                        strfile = cdFileLoading.GetFileList1(tempFile, d.FtpName);
                        binFileCount = strfile.Length - 2;



                        for (int i = 0; i < strfile.Length; i++)
                        {
                            if (strfile[i].Contains(".bin"))
                            {
                                string tempPath = tempFile + '/' + strfile[i];
                                string sDirPath1 = "C:/MYD_METAL/" + tempFile;
                                Download("Job Files/" + tempFile, strfile[i], sDirPath1, progress, true);
                                //loadingtheFile(tempFile);
                                allFile.Add(strfile[i]);
                                string attachFile = sDirPath1 + strfile[i];
                                adada = tempFile;
                            }
                        }
                    }
                    else
                    {
                        long length = Directory.GetFiles(sDirPath, "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));
                        aniLoading.logingText.Text = "Loading...";
                        progress.Visibility = Visibility.Visible;
                        double tempsize = DirSize(di);
                        progress.Maximum = tempsize;
                        adada = tempFile;
                        linethread.RunWorkerAsync();
                    }
                }
                else
                {

                }
            }
            else
            {
                lvFile.SelectedIndex = beforeIndex;
            }
        }

        private double DirSize(DirectoryInfo d)
        {
            double size = 0;

            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
                _totalFileSize.Add(size);
            }

            return size;
        }

        #endregion

        #region 12. M270-PRINTING-PROGRESSBAR
        private async void Download(string ftpDirectoryName, string downFileName, string localPath, ProgressBar progressBar, bool showCompleted)
        {
            try
            {
                binDownCount++;

                Uri ftpUri = new Uri(d.FtpName + ftpDirectoryName + "/" + downFileName);

                // 파일 사이즈
                FtpWebRequest reqFtp = (FtpWebRequest)WebRequest.Create(ftpUri);
                reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFtp.Credentials = new NetworkCredential("ftpuser", "ftpuser");

                WebResponse resFtp = await (Task<WebResponse>)reqFtp.GetResponseAsync();
                fileSize = resFtp.ContentLength;
                totalFileSize += fileSize;
                _totalFileSize.Add(fileSize);
                progress.Maximum = totalFileSize;

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                    request.DownloadProgressChanged += request_DownloadProgressChanged;

                    // 다운로드가 완료 된 후 메시지 보이기
                    if (showCompleted)
                    {
                        //loadingtheFile(adada);
                        request.DownloadFileCompleted += request_DownloadFileCompleted;
                        resFtp.Close();
                    }

                    // 다운로드 시작
                    request.DownloadFileAsync(ftpUri, @localPath + "/" + downFileName);

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        void request_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Value = Convert.ToInt32(Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(fileSize) * 100);
        }
        void request_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            f_download++;
            if (f_download == binFileCount)
            {
                progress.Maximum = totalFileSize;
                aniLoading.logingText.Text = "Loading...";
                linethread.RunWorkerAsync();
                Console.WriteLine(totalFileSize);

            }

        }
        #endregion

        #region 13. M270-PRINTING-OTHER
        public void testFunction()
        {
            var mmm = loadingtheFile(adada);
            testtest();
        }
        private void testtest()
        {
            loading();
            slLayer1.Maximum = Convert.ToInt32(allmodelLayer);
            txtTotalLayer.Text = Convert.ToString(allmodelLayer);
            lvFile.IsEnabled = true;

        }
        private int loadingtheFile(string _tempFile)
        {
            _facts.Clear();
            allFile.Clear();

            filePath = @"C:\\MYD_METAL\\" + _tempFile + "\\" + _tempFile + ".job";

            int lastCount = filePath.LastIndexOf("\\");
            sss = filePath.Substring(0, lastCount);

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sss);
            System.IO.FileInfo[] fi = di.GetFiles("*.bin");
            if (fi.Length == 0)
            {
                
            }
            else
            {
                string s = "";
                for (int i = 0; i < fi.Length; i++)
                {
                    s = fi[i].Name.ToString();
                    allFile.Add(s);
                    string attachFile = sss + "\\" + s;

                    Stream inStream = new FileStream(attachFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var reader1 = new BinaryReader(inStream);

                    GetData(reader1);
                    inStream.Close();
                }
            }

            int d = 0;
            return d;
        }
        #endregion

        #endregion

        #region 14. M270-PUMP
        private void sdpump_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            d.OPCItemWriteValueTextBoxes[4] = Convert.ToString(Math.Round(sdpump.Value, 0) * 0.1);
            d.opcWrite("OPCItemSyncWrite4", daServerMgt);
        }

        public void PumpPlus()
        {
            if (double.Parse(_ucM270Pump.lblPump.Content.ToString()) < 10)
            {
                double pumpStrength = double.Parse(_ucM270Pump.lblPump.Content.ToString()) + 0.1;
                if (pumpStrength > 10) { pumpStrength = 10; }
                d.OPCItemWriteValueTextBoxes[4] = Convert.ToString(pumpStrength);
                d.opcWrite("OPCItemSyncWrite4", daServerMgt);
                
            }
        }
        public void PumpPlus2()
        {
            if (double.Parse(_ucM270Pump.lblPump.Content.ToString()) < 10)
            {
                double pumpStrength = double.Parse(_ucM270Pump.lblPump.Content.ToString()) + 1;
                if (pumpStrength > 10) { pumpStrength = 10; }
                d.OPCItemWriteValueTextBoxes[4] = Convert.ToString(pumpStrength);
                d.opcWrite("OPCItemSyncWrite4", daServerMgt);

            }
        }

        public void PumpMinus()
        {
            if (double.Parse(_ucM270Pump.lblPump.Content.ToString()) > 0)
            {
                double pumpStrength = double.Parse(_ucM270Pump.lblPump.Content.ToString()) - 0.1;
                if (pumpStrength < 0) { pumpStrength = 0; }
                d.OPCItemWriteValueTextBoxes[4] = Convert.ToString(pumpStrength);
                d.opcWrite("OPCItemSyncWrite4", daServerMgt);
            }
        }
        public void PumpMinus2()
        {
            if (double.Parse(_ucM270Pump.lblPump.Content.ToString()) > 0)
            {
                double pumpStrength = double.Parse(_ucM270Pump.lblPump.Content.ToString()) - 1;
                if (pumpStrength < 0) { pumpStrength = 0; }
                d.OPCItemWriteValueTextBoxes[4] = Convert.ToString(pumpStrength);
                d.opcWrite("OPCItemSyncWrite4", daServerMgt);
            }
        }
        // 필터 상단 하단 선택
        private void Upfilter()
        {
            if (d.OPCItemValueTextBoxes[81] == "False") 
            {
                MessageBox.Show("공압밸브를 켜주세요.");
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[55] = "True";
                d.opcWrite("OPCItemSyncWrite55", daServerMgt);
                d.OPCItemWriteValueTextBoxes[56] = "False";
                d.opcWrite("OPCItemSyncWrite56", daServerMgt);
            }
            
        }

        private void Downfilter()
        {
            if (d.OPCItemValueTextBoxes[81] == "False")
            {
                MessageBox.Show("공압밸브를 켜주세요.");
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[55] = "False";
                d.opcWrite("OPCItemSyncWrite55", daServerMgt);
                d.OPCItemWriteValueTextBoxes[56] = "True";
                d.opcWrite("OPCItemSyncWrite56", daServerMgt);
            }
            
        }
        #endregion

        #region 15. M270-LAYERCHANGE
        private void slLayer1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {

                showlayer = Convert.ToInt32(slLayer1.Value);
                txtCurrentLayer.Text = Convert.ToString(showlayer);
                if (d.StrPrintStaus != "출력전")
                {
                    polyInline.Data = null;
                    StringBuilder strbuilder = new StringBuilder();
                    StringBuilder strbuilder3 = new StringBuilder();
                    int showlayercount = showlayer;
                    for (int i = 0; i < allFile.Count; i++)
                    {
                        strbuilder3.Append(_stringBuilder3[showlayercount]);
                        strbuilder.Append(_stringBuilder[showlayercount]);
                        showlayercount = showlayercount + allmodelLayer;
                    }
                    polyInline.Data = Geometry.Parse("" + strbuilder);
                    polyOutline.Data = Geometry.Parse("" + strbuilder3);
                }
                else
                {
                    polyInline.Data = null;
                    StringBuilder strbuilder3 = new StringBuilder();
                    int showlayercount = showlayer;
                    for (int i = 0; i < allFile.Count; i++)
                    {
                        strbuilder3.Append(_stringBuilder3[showlayercount]);

                        showlayercount = showlayercount + allmodelLayer;
                    }

                    polyOutline.Data = Geometry.Parse("" + strbuilder3);
                }

            }
            catch (Exception e4)
            {

            }
        }


        private void DownloadFile()
        {
            lvFile.SelectedIndex = 0;

            Uri ftpUri = new Uri(d.FtpName);
            WebClient wc = new WebClient();

            wc.Credentials = new NetworkCredential("ftpuser", "ftpuser");

            cd_FileLoading cdFileLoading = new cd_FileLoading();
            string[] strfile = cdFileLoading.GetFileList(d.FtpName);

            lvFile.ItemsSource = strfile;
        }
        #endregion

        #region 16. M270-DOORLOOKING
        private void DoorGloveLock()
        {
            if (d.OPCItemValueTextBoxes[45] == "True")
            {
                d.OPCItemWriteValueTextBoxes[20] = "False";
                d.opcWrite("OPCItemSyncWrite20", daServerMgt);
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[20] = "True";
                d.opcWrite("OPCItemSyncWrite20", daServerMgt);
            }
        }

        private void DoorChamberLock()
        {
            if (d.OPCItemValueTextBoxes[44] == "True")
            {
                d.OPCItemWriteValueTextBoxes[40] = "True";
                d.opcWrite("OPCItemSyncWrite40", daServerMgt);
                d.OPCItemWriteValueTextBoxes[58] = "False";
                d.opcWrite("OPCItemSyncWrite58", daServerMgt);

            }
            else
            {
                d.OPCItemWriteValueTextBoxes[40] = "False";
                d.opcWrite("OPCItemSyncWrite40", daServerMgt);
                d.OPCItemWriteValueTextBoxes[58] = "True";
                d.opcWrite("OPCItemSyncWrite58", daServerMgt);
            }
        }
        #endregion

        #region 17. M270-SETTING, 현재 사용하지않음
        private void imgSetting_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //settingWindow sw = new settingWindow(this, d, daServerMgt);
            //sw.Show();
        }
        
        public void realTimeMonitor(string ch1, string ch2, string ch3, string ch4, string ch5, string ch6)
        {
            
            int count = 0;
            //gridPosition.Visibility = Visibility.Collapsed;
            //gridChamberOxy.Visibility = Visibility.Collapsed;
            //gridPrinting.Visibility = Visibility.Collapsed;
            //gridGloveOxy.Visibility = Visibility.Collapsed;
            //gridCPU.Visibility = Visibility.Collapsed;
            //gridBatteryLeft.Visibility = Visibility.Collapsed;

            if (ch1 == "True")
            {
                count++;
                //gridGloveOxy.Visibility = Visibility.Visible;
                //Grid.SetColumn(gridGloveOxy, count);
            }
            
            if (ch2 == "True")
            {
                count++;
                //gridChamberOxy.Visibility = Visibility.Visible;
                //Grid.SetColumn(gridChamberOxy, count);
            }

            if (ch3 == "True")
            {
                count++;
                //gridPosition.Visibility = Visibility.Visible;
                //Grid.SetColumn(gridPosition, count);
            }

            if (ch4 == "True")
            {
                count++;
                //gridPrinting.Visibility = Visibility.Visible;
                //Grid.SetColumn(gridPrinting, count);
            }

            if (ch5 == "True")
            {
                count++;
                //gridCPU.Visibility = Visibility.Visible;
                //Grid.SetColumn(gridCPU, count);
            }

            if (ch6 == "True")
            {
                count++;
                //gridBatteryLeft.Visibility = Visibility.Visible;
                //Grid.SetColumn(gridBatteryLeft, count);
            }
        }
        #endregion

        #region 19. 실시간 변화 값 및 모니터링 쪽 이벤트들

        private void fLightImage(Visibility vlight1, Visibility vlight2, Visibility vlight3)
        {
            _ucM270LED.imgLightL1.Visibility = vlight1;
            _ucM270LED.imgLightL2.Visibility = vlight2;
            _ucM270LED.imgLightL3.Visibility = vlight3;
        }

        public void RoomImageSetting()
        {
            Visibility visiLeft = Visibility.Visible;
            Visibility visiRight = Visibility.Collapsed;
            Uri uriSourceTabBackground = new Uri(@"/imgMG/imgMachine/mg_printer_left.png", UriKind.Relative); ;

            if (d.OPCItemValueTextBoxes[48] == "0") //챔버룸
            {
                d.StrRoomPosition = "챔버 룸";
                uriSourceTabBackground = new Uri(@"/imgMG/imgMachine/mg_printer_right.png", UriKind.Relative);

                visiLeft = Visibility.Collapsed;
                visiRight = Visibility.Visible;

            }
            else
            {

            }

        }

        private void spChamberOxy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var series = (LineSeries)d.SeriesCollection[0];

            if (series.Visibility == Visibility.Visible)
            {
                series.Visibility = Visibility.Collapsed;
                //spChamberOxyText.Foreground = new SolidColorBrush(Color.FromArgb(255, 178, 178, 178));
            }
            else
            {
                series.Visibility = Visibility.Visible;
                //spChamberOxyText.Foreground = new SolidColorBrush(Colors.Black);
            }


            //series.Visibility = series.Visibility == Visibility.Visible
            //    ? Visibility.Collapsed
            //    : Visibility.Visible;
        }

        private void spChamberTemperature_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var series = (LineSeries)d.SeriesCollection[1];
            if (series.Visibility == Visibility.Visible)
            {
                series.Visibility = Visibility.Collapsed;
                //spChamberTemperatureText.Foreground = new SolidColorBrush(Color.FromArgb(255, 178, 178, 178));
            }
            else
            {
                series.Visibility = Visibility.Visible;
                //spChamberTemperatureText.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void spChamberHumity_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var series = (LineSeries)d.SeriesCollection[2];
            if (series.Visibility == Visibility.Visible)
            {
                series.Visibility = Visibility.Collapsed;
                //     spChamberHumityText.Foreground = new SolidColorBrush(Color.FromArgb(255, 178, 178, 178));
            }
            else
            {
                series.Visibility = Visibility.Visible;
                //     spChamberHumityText.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void spGloveOxy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var series = (LineSeries)d.SeriesCollection[4];
            if (series.Visibility == Visibility.Visible)
            {
                series.Visibility = Visibility.Collapsed;
                //    spGloveOxyText.Foreground = new SolidColorBrush(Color.FromArgb(255, 178, 178, 178));
            }
            else
            {
                series.Visibility = Visibility.Visible;
                //   spGloveOxyText.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void spGloveTemperature_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var series = (LineSeries)d.SeriesCollection[5];
            if (series.Visibility == Visibility.Visible)
            {
                series.Visibility = Visibility.Collapsed;
                //   spGloveTemperatureText.Foreground = new SolidColorBrush(Color.FromArgb(255, 178, 178, 178));
            }
            else
            {
                series.Visibility = Visibility.Visible;
                //   spGloveTemperatureText.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void spGloveHumity_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var series = (LineSeries)d.SeriesCollection[3];
            if (series.Visibility == Visibility.Visible)
            {
                series.Visibility = Visibility.Collapsed;
                // spGloveHumityText.Foreground = new SolidColorBrush(Color.FromArgb(255, 178, 178, 178));
            }
            else
            {
                series.Visibility = Visibility.Visible;
                //    spGloveHumityText.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void CartesianChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var series = chartPoint.Key;

            File_Status fs = new File_Status(labels[series], printingProcess[series], printFinsh[series]);
            fs.Show();

            //var series = (SeriesCollection)chartPoint.Instance;
        }

        //Week Log
        File_Status file_Status = new File_Status();

        private void imgEtc_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            int lvWeekSelected = lvWeekFile.Items.Count - lvWeekFile.SelectedIndex - 1;

            if (file_Status.IsVisible == true)
            {
                file_Status.initalSetting(_strFileName[lvWeekSelected], strlastWeek[lvWeekSelected], printFinsh[lvWeekSelected]);
                file_Status.Topmost = true;
            }
            else
            {
                file_Status = new File_Status();
                file_Status.initalSetting(_strFileName[lvWeekSelected], strlastWeek[lvWeekSelected], printFinsh[lvWeekSelected]);
                file_Status.Show();
                file_Status.Topmost = true;
            }
            //int sda = 0;
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            var vm = (DefineValue)DataContext;
            var chart = (LiveCharts.Wpf.CartesianChart)sender;

            var mouseCoordinate = e.GetPosition(chart);
            var p = chart.ConvertToChartValues(mouseCoordinate);

            vm.YPointer = p.Y;

            var series = chart.Series[0];
            var closetsPoint = series.ClosestPointTo(p.X, AxisOrientation.X);

            vm.XPointer = closetsPoint.X;
        }

        private void slLayer1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (d.StrPrintStaus == "출력전")
                {
                    StringBuilder strbuilder = new StringBuilder();
                    int showlayercount = showlayer;
                    for (int i = 0; i < allFile.Count; i++)
                    {
                        strbuilder.Append(_stringBuilder[showlayercount]);
                        showlayercount = showlayercount + allmodelLayer;
                    }

                    polyInline.Data = Geometry.Parse("" + strbuilder);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        //자동공급부 펌프
        private void AutoPumpCommand()
        {
            apw = new AutoPumpWindow();
            apw.initalSetting(d, daServerMgt);
            apw.ShowDialog();
        }

        public void PumpUp1()
        {
            if(double.Parse(apw.Mainpump.Content.ToString()) < 10)
            {
                double pump_main = double.Parse(apw.Mainpump.Content.ToString()) + 1;
                if(pump_main > 10) { pump_main = 10; }
                d.OPCItemWriteValueTextBoxes[61] = Convert.ToString(pump_main);
                d.opcWrite("OPCItemSyncWrite61", daServerMgt);
                apw.Mainpump.Content = d.OPCItemWriteValueTextBoxes[61];
            }
        }

        public void PumpDown1()
        {
            if (double.Parse(apw.Mainpump.Content.ToString()) > 0)
            {
                double pump_main = double.Parse(apw.Mainpump.Content.ToString()) - 1;
                if (pump_main < 0) { pump_main = 0; }
                d.OPCItemWriteValueTextBoxes[61] = Convert.ToString(pump_main);
                d.opcWrite("OPCItemSyncWrite61", daServerMgt);
                apw.Mainpump.Content = d.OPCItemWriteValueTextBoxes[61];
            }
        }

        public void PumpUp2()
        {
            if (double.Parse(apw.Remainpump.Content.ToString()) < 10)
            {
                double pump_remain = double.Parse(apw.Remainpump.Content.ToString()) + 1;
                if (pump_remain > 10) { pump_remain = 10; }
                d.OPCItemWriteValueTextBoxes[62] = Convert.ToString(pump_remain);
                d.opcWrite("OPCItemSyncWrite62", daServerMgt);
                apw.Remainpump.Content = d.OPCItemWriteValueTextBoxes[62];
            }
        }

        public void PumpDown2()
        {
            if (double.Parse(apw.Remainpump.Content.ToString()) > 0)
            {
                double pump_remain = double.Parse(apw.Remainpump.Content.ToString()) - 1;
                if (pump_remain < 0) { pump_remain = 0; }
                d.OPCItemWriteValueTextBoxes[62] = Convert.ToString(pump_remain);
                d.opcWrite("OPCItemSyncWrite62", daServerMgt);
                apw.Remainpump.Content = d.OPCItemWriteValueTextBoxes[62];
            }
        }

        #region 17. 카메라 파일을 다운로드, 현재 사용하지않음
        private double camFileSize = 0;
        private async void CameraDownload(string ftpDirectoryName, string downFileName, string localPath, ProgressBar progressBar, bool showCompleted)
        {
            try
            {
                Uri ftpUri = new Uri(d.FtpName + ftpDirectoryName + "/" + downFileName);

                // 파일 사이즈
                FtpWebRequest reqFtp = (FtpWebRequest)WebRequest.Create(ftpUri);
                reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFtp.Credentials = new NetworkCredential("ftpuser", "ftpuser");

                WebResponse resFtp = await (Task<WebResponse>)reqFtp.GetResponseAsync();
                camFileSize = resFtp.ContentLength;


                progress.Maximum = camFileSize;


                using (WebClient camerarequest = new WebClient())
                {
                    camerarequest.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                    camerarequest.DownloadProgressChanged += camerarequest_DownloadProgressChanged;

                    // 다운로드가 완료 된 후 메시지 보이기
                    if (showCompleted)
                    {
                        //loadingtheFile(adada);
                        camerarequest.DownloadFileCompleted += camerarequest_DownloadFileCompleted;
                        resFtp.Close();
                    }

                    // 다운로드 시작
                    camerarequest.DownloadFileAsync(ftpUri, @localPath + "/" + downFileName);

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void camerarequest_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }

        private void camerarequest_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //   cprogress.Value = Convert.ToInt32(Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(camFileSize) * 100);
        }

        #endregion
        

        #region 카메라 폴더 리스트 관련, 현재 사용하지않음
        private string seledGFolderName = "";
        private void lvFolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lvFolder.Items.Count > 0)
            //{

            //    try
            //    {
            //        cd_FileLoading cdFileLoading = new cd_FileLoading();
            //        int index = lvFolder.SelectedIndex;

            //        if (index == -1)
            //        {

            //        }
            //        else
            //        {

            //        }

            //        string[] strfile = cdFileLoading.GetFolderList("CameraFile/" + allCameraFolder[index] + "/", d.FtpName);
            //        seledGFolderName = allCameraFolder[index];
            //        lvCameraFile.ItemsSource = strfile;
            //        slcam.Maximum = strfile.Count();

            //        string sDirPath = @"C:\\MYD_METAL_CAM\\" + allCameraFolder[index];



            //        DirectoryInfo di = new DirectoryInfo(sDirPath);
            //        if (di.Exists == false)
            //        {
            //            di.Create();
            //            for (int i = 0; i < strfile.Length; i++)
            //            {
            //                lvFolder.IsEnabled = false;
            //                lvFile.IsEnabled = false;
            //                CameraDownload("CameraFile/" + allCameraFolder[index], strfile[i], sDirPath, cprogress, true);
            //            }


            //        }
            //        else
            //        {
            //            for (int i = 0; i < strfile.Length; i++)
            //            {
            //                lvFolder.IsEnabled = false;
            //                lvFile.IsEnabled = false;
            //                CameraDownload("CameraFile/" + allCameraFolder[index], strfile[i], sDirPath, cprogress, true);
            //            }
            //        }


            //        lvFolder.IsEnabled = true;
            //        lvFile.IsEnabled = true;

            //        if (lvCameraFile.Items.Count > 0)
            //        {
            //            var uriSourcestop = new Uri(@"C:\\MYD_METAL_CAM\\" + seledGFolderName + "\\shot1.jpg", UriKind.RelativeOrAbsolute);
            //            mainImage.Source = new BitmapImage(uriSourcestop);

            //        }

            //        slcam.Value = 0;
            //        tbCurrentLayer.Text = slcam.Value.ToString();
            //        tbTotalLayer.Text = slcam.Maximum.ToString();

            //        //lvCameraFile.SelectedIndex = 0;
            //        //string tempPath = tempFile + '/' + strfile[i];
            //        //string sDirPath1 = "C:/MYD_METAL/" + tempFile;
            //        //Download("CameraFile/" + allCameraFolder[index] + "/", strfile[i], sDirPath1, progress, true);
            //        ////loadingtheFile(tempFile);
            //        //allFile.Add(strfile[i]);
            //        //string attachFile = sDirPath1 + strfile[i];
            //    }
            //    catch (Exception ee)
            //    {

            //    }

            //}
            //else
            //{
            //    MessageBox.Show("저장된 파일이 없습니다.");
            //}
        }


        #endregion

        #region 20. 카메라 이미지파일 확대축소 로딩, 현재 사용하지않음
        private Point startPoint;
        private Point originalPoint;

        private void clipBorder_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoomDirection = e.Delta > 0 ? 0.1 : -0.1;
            //double slidingScale = imageScaleTransform.ScaleX / 2 * zoomDirection;

            //imageScaleTransform.ScaleX = imageScaleTransform.ScaleY += slidingScale;
        }

        private void mainImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //mainImage.CaptureMouse();
            //startPoint = e.GetPosition(clipBorder);
            //originalPoint = new Point(imageTranslateTransform.X, imageTranslateTransform.Y);
        }

        private void mainImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //     mainImage.ReleaseMouseCapture();
        }

        private void mainImage_MouseMove(object sender, MouseEventArgs e)
        {
            //if (!mainImage.IsMouseCaptured) return;

            //Vector moveVector = startPoint - e.GetPosition(clipBorder);
            //imageTranslateTransform.X = originalPoint.X - moveVector.X;
            //imageTranslateTransform.Y = originalPoint.Y - moveVector.Y;
        }
        #endregion

        #region 카메라 레이어바 및 사진 선택 연동코드, 현재 사용하지않음
        private void lvCameraFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //@"C:\\MYD_METAL\\" + adada + "\\" + adada + ".job";
            slcamlvFile("list");

        }

        private void slcam_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slcamlvFile("slider");
        }

        private void slcamlvFile(string whatControl)
        {
            if (whatControl == "list")
            {
                try
                {
                    //var uriSourcestop = new Uri(@"C:\\MYD_METAL_CAM\\" + seledGFolderName + "\\" + lvCameraFile.SelectedItem, UriKind.RelativeOrAbsolute);
                    //string[] tempResult = lvCameraFile.SelectedItem.ToString().Split(new string[] { "shot", ".jpg" }, StringSplitOptions.RemoveEmptyEntries);

                    //slcam.Value = Convert.ToInt32(tempResult[0]);
                    //tbCurrentLayer.Text = slcam.Value.ToString();
                    //tbTotalLayer.Text = slcam.Maximum.ToString();
                    //mainImage.Source = new BitmapImage(uriSourcestop);
                }
                catch (Exception ee)
                {

                }
            }
            else if (whatControl == "slider")
            {
                try
                {
                    //  string selcamName = "shot" + Convert.ToString(slcam.Value) + ".jpg";
                    //     var uriSourcestop = new Uri(@"C:\\MYD_METAL_CAM\\" + seledGFolderName + "\\" + selcamName, UriKind.RelativeOrAbsolute);
                    //tbCurrentLayer.Text = slcam.Value.ToString();
                    //tbTotalLayer.Text = slcam.Maximum.ToString();
                    //mainImage.Source = new BitmapImage(uriSourcestop);
                }
                catch (Exception ee)
                {

                }
            }
            else
            {

            }
        }
        #endregion

        #region 카메라이벤트 관련 / 저장, 삭제, 갱신, 가이드빔 포함 확인할것.

        private void CameraFolderDelete()
        {

        }

        private void CameraFolderSave()
        {

        }

        private void btnCameraSave_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //int index = lvFolder.SelectedIndex;
            //saveFileDialog.FileName = allCameraFolder[index].ToString();
            //if (saveFileDialog.ShowDialog() == true)
            //{

            //    string tempPath = saveFileDialog.FileName;

            //    cd_FileLoading cdFileLoading = new cd_FileLoading();
            //    string[] strfile = cdFileLoading.GetFolderList("CameraFile/" + allCameraFolder[index] + "/", d.FtpName);


            //    DirectoryInfo di = new DirectoryInfo(tempPath);
            //    if (di.Exists == false)
            //    {
            //        di.Create();

            //        for (int i = 0; i < strfile.Length; i++)
            //        {
            //            CameraDownload("CameraFile/" + allCameraFolder[index], strfile[i], tempPath, cprogress, true);
            //        }
            //        slcam.Value = 0;


            //    }
            //    else
            //    {
            //        MessageBox.Show("지정된 경로에 동일한 이름의 파일이 존재합니다.");
            //    }

            //}
            //else
            //{

            //}

        }
        

        private void GuideBeam()
        {
            if (d.OPCItemValueTextBoxes[8] == "False")
            {
                d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Script.GuideBeam\"}";
                d.opcWrite("OPCItemSyncWrite14", daServerMgt);
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Script.GuideBeamOff\"}";
                d.opcWrite("OPCItemSyncWrite14", daServerMgt);
            }
        }

        private void LaserControl()
        {
            if (d.OPCItemValueTextBoxes[6]=="False")
            {
                d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Script.PowerTurnOn\"}";
                d.opcWrite("OPCItemSyncWrite14", daServerMgt);
            }
            else
            {
                d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Script.PowerTurnOff\"}";
                d.opcWrite("OPCItemSyncWrite14", daServerMgt);
            }
        }
        private void CameraResfresh()
        {

            //int camindex = lvFolder.SelectedIndex;
            //cd_FileLoading cdFileLoading = new cd_FileLoading();
            //string[] camstrfile = cdFileLoading.GetFolderList("CameraFile/", d.FtpName);
            //allCameraFolder = camstrfile;

            //char camsp = ' ';

            //List<FolderList> _folderList = new List<FolderList>();

            //for (int i = 0; i < camstrfile.Length; i++)
            //{
            //    string[] spstring1 = camstrfile[i].Split(camsp);
            //    string tempTime = spstring1[0].Replace(".", "-");
            //    string tempTime2 = spstring1[1].Replace("-", ":");

            //    string[] camstrfileseper = cdFileLoading.GetFolderList("CameraFile/" + camstrfile[i], d.FtpName);
            //    _folderList.Add(new FolderList() { FileName = spstring1[2], StartTime = "20" + tempTime + " " + tempTime2, CameraFileCount = Convert.ToString(camstrfileseper.Count()), SaveImage = @"/imgCamera/imgCameraSave2.png", DeleteImageP = @"/imgCamera/imgCameraDelete2.png" });
            //}

            //lvFolder.ItemsSource = _folderList;
            //lvFolder.SelectedIndex = camindex;
        }

        private void btnCameraDelete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //int tempFile = lvFolder.SelectedIndex;
                //string _ftpUri = d.FtpName + "CameraFile/" + allCameraFolder[tempFile];
                //Uri ftpUri = new Uri(_ftpUri);

                //FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpUri);
                //req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                //req.Timeout = 120000;
                //req.Method = WebRequestMethods.Ftp.ListDirectory;

                //FtpWebResponse resFtp = (FtpWebResponse)req.GetResponse();
                //StreamReader reader = new StreamReader(resFtp.GetResponseStream(), System.Text.Encoding.Default, true);
                //string strData = reader.ReadToEnd();
                //string[] filesinDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                //resFtp.Close();

                //for (int i = 0; i < filesinDirectory.Count(); i++)
                //{
                //    string tempUri = d.FtpName + "CameraFile/" + filesinDirectory[i];

                //    FtpWebRequest _req = (FtpWebRequest)WebRequest.Create(tempUri);
                //    _req.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                //    _req.Timeout = 120000;
                //    _req.Method = WebRequestMethods.Ftp.DeleteFile;

                //    FtpWebResponse _resFtp = (FtpWebResponse)_req.GetResponse();
                //    _resFtp.Close();
                //}

                //string tempUriFolder = d.FtpName + "CameraFile/" + allCameraFolder[tempFile];

                //FtpWebRequest _reqFolder = (FtpWebRequest)WebRequest.Create(tempUriFolder);
                //_reqFolder.Credentials = new NetworkCredential("ftpuser", "ftpuser");
                //_reqFolder.Timeout = 120000;
                //_reqFolder.Method = WebRequestMethods.Ftp.RemoveDirectory;

                //FtpWebResponse _resFtpFolder = (FtpWebResponse)_reqFolder.GetResponse();
                //_resFtpFolder.Close();
                //CameraResfresh();
                ////lvFile.SelectedIndex = 0;

            }
            catch (Exception exa)
            {
                
            }
        }
        #endregion
    }
}
