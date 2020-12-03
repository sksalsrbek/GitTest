using DPT_WPF.ucCommon;
using DPT_WPF.ucM270;
using Kepware.ClientAce.OpcDaClient;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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

        private string tempMotor1Alram = "0";
        private string tempMotor2Alram = "0";
        private string tempMotor3Alram = "0";
        private string tempMotor4Alram = "0";
        private double bteSeconds = 0;
        private double bteClock = 0;
        private string bteClockTest = "00h:00m:00s";

        private double totalFileSize = 0;

        private string fristCheck = "0";

        private bool _timerPrintingRunning = false;

        public string tempPrintingProfile = "";
        private bool bPrintingFinsh = false;

        //7일간의 최근 이력 저장
        List<string> strlastWeek = new List<string>();
        string tempLastWeek = "";
        List<string> _strFileName = new List<string>();
        private bool blastweekone = false;

        //Windows
        moveWindow mw = new moveWindow();
        speedWindows sw = new speedWindows();
        AutoPumpWindow apw = new AutoPumpWindow();

        //흘러가는 시간
        DateTime ps_StartTime;
        DispatcherTimer psDispatcherTimer = new DispatcherTimer();
        Stopwatch psStopWatch = new Stopwatch();
        string psCurrentTime = string.Empty;


        //출력예상 시간 
        DispatcherTimer estDispatcherTimer = new DispatcherTimer();
        Stopwatch estStopWatch = new Stopwatch();
        DateTime estStartTime;
        DateTime firstStartTime;
        double fixTime = 0;
        System.Timers.Timer bteTimer = new System.Timers.Timer();

        private double timeChnage = 0;

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

            ucLogin uclogin = new ucLogin(dv);

            ucCommonM270Motor uccommonmotor = new ucCommonM270Motor(dv, daServerMgt);

            // 이벤트 선언부
            dv.Motor1MoveCommand = new DelegateCommand(this.Motor1Move);
            dv.Motor2MoveCommand = new DelegateCommand(this.Motor2Move);
            dv.Motor3MoveCommand = new DelegateCommand(this.Motor3Move);
            dv.Motor4MoveCommand = new DelegateCommand(this.Motor4Move);

            dv.Motor1SpeedCommand = new DelegateCommand(this.Motor1Speed);
            dv.Motor2SpeedCommand = new DelegateCommand(this.Motor2Speed);
            dv.Motor3SpeedCommand = new DelegateCommand(this.Motor3Speed);
            dv.Motor4SpeedCommand = new DelegateCommand(this.Motor4Speed);

            dv.Up1SpeedCommand = new DelegateCommand(this.SUp1);
            dv.Up2SpeedCommand = new DelegateCommand(this.SUp2);
            dv.Up3SpeedCommand = new DelegateCommand(this.SUp3);
            dv.Down1SpeedCommand = new DelegateCommand(this.SDown1);
            dv.Down2SpeedCommand = new DelegateCommand(this.SDown2);
            dv.Down3SpeedCommand = new DelegateCommand(this.SDown3);

            dv.AutoSupplyCommand = new DelegateCommand(this.AutoSupplySwitch);

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

            dv.MotorTwowayCommand = new DelegateCommand(this.MotorTwoway);

            dv.PrintStartCommand = new DelegateCommand(this.PrintStart);
            dv.PrintPauseCommand = new DelegateCommand(this.PrintPause);
            dv.PrintResumeCommand = new DelegateCommand(this.PrintResume);
            dv.PrintStopCommand = new DelegateCommand(this.PrintStop);
            dv.PrintResetCommand = new DelegateCommand(this.PrintReset);
            dv.PrintFinishCommand = new DelegateCommand(this.PrintFinish);
            dv.PrintFileDeleteCommand = new DelegateCommand(this.PrintFileDelete);
            dv.PrintFileArrayCommand = new DelegateCommand(this.PrintFileArray);

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

            DateTime datesXStatr = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

            dv.PrintWeekend = labels.ToArray();
            dv.AxisStep = TimeSpan.FromDays(1).Ticks;
            dv.DayofWeek = values => new DateTime((long)values).ToString("MMM월dd일HH시");

            dv.Formatter = value => new DateTime((long)value).ToString("dd MMM");
            dv.PrintingFormatter = x => x.ToString("N2") + "일";
            dv.XPointer = 0;
            dv.YPointer = 0;

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

                                if (d.OPCItemValueTextBoxes[83] == "False")
                                {
                                    AutoStatus.Content = "자동공급 OFF";
                                }
                                else
                                {
                                    AutoStatus.Content = "자동공급 ON";
                                }

                                #region M270-MOTOR
                                d.DblMotor1Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[10]), 3);
                                d.DblMotor2Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[11]), 3);
                                d.DblMotor3Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[12]), 3);
                                d.DblMotor4Position = Math.Round(Convert.ToDouble(d.OPCItemValueTextBoxes[13]), 3);

                                dv.PULeftHeight = Convert.ToString(310 - d.DblMotor2Position) + "mm";
                                double tempPuLeft = ((310 - d.DblMotor2Position) * 100) / 310; //수정필요
                                dv.PUPowderLeft = tempPuLeft.ToString("N2") + "%";

                                int powderBorder = Convert.ToInt32(tempPuLeft / 20);

                                #region MOTOR 알람
                                string motor1Alram = d.OPCItemValueTextBoxes[15];
                                string motor2Alram = d.OPCItemValueTextBoxes[16];
                                string motor3Alram = d.OPCItemValueTextBoxes[17];
                                string motor4Alram = d.OPCItemValueTextBoxes[18];

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
                                    imgPrintStop.Visibility = Visibility.Collapsed;
                                    imgPrintResume.Visibility = Visibility.Collapsed;
                                    imgPrintStart.Visibility = Visibility.Collapsed;
                                    var uriSourcepause = new Uri(@"/imgTab/6_printing/print_btn_pause_enable.png", UriKind.Relative);
                                    imgPrintPause.Source = new BitmapImage(uriSourcepause);
                                    imgPrintPause.Visibility = Visibility.Collapsed;
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
                                    var uriSourcestop = new Uri(@"/imgTab/6_printing/print_btn_stop_enable.png", UriKind.Relative);
                                    estStopWatch.Start();
                                    estDispatcherTimer.Start();
                                    imgPrintStart.Source = new BitmapImage(uriSourceprint);
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

                                #endregion

                                #region 파우더 잔량, 수정필요
                                double left = 100 - ((Convert.ToDouble(d.OPCItemValueTextBoxes[10]) / 106) * 100);
                                d.PUPowderLeft = left.ToString("N0") + "%";
                                d.PULeftHeight = Convert.ToString(106 - (Convert.ToDouble(d.OPCItemValueTextBoxes[10]))) + "mm";
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
                                        List<string> _splitedFacets = new List<string>();
                                        bPrintingFinsh = true;
                                        _splitedFacets = weekstring[i].Split(new string[] { ":", "$" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                        List<string> _splite = new List<string>();
                                        _splite = _splitedFacets[0].Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                        string tempPrintProcess = "[" + _splite[0] + "-" + _splite[1] + "-" + _splite[2] + " " + _splite[3] + ":" + _splite[4] + ":" + _splite[5] + "]" + " " + _splitedFacets[3] + "\n";
                                        int da = 0;
                                        strlastWeek.Add(tempPrintProcess);
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
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
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


                                            if (transValue == 1)
                                            {

                                                strbuilder.Append("M " + Convert.ToString(beforeX1 * 8.5) + "," + Convert.ToString(beforeY1 * 8.5) + " " + Convert.ToString(pointX * 8.5) + "," + Convert.ToString(pointY * 8.5));
                                                transValue = 0;
                                            }
                                            else
                                            {
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
                                    break;
                                }

                            }

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

        #region Time - Tick
        void est_Tick(object sender, EventArgs e)
        {

            if (estStopWatch.IsRunning)
            {

            }
        }

        void ps_Tick(object sender, EventArgs e)
        {
            if (psStopWatch.IsRunning)
            {
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
        }
        #endregion

        #region 1. M270-MOTOR MOTOR HOME, 수정필요
        private void M270Motor5Home()//홈버튼을 눌렀을때
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 5\",\"speed\":5}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        private void M270Motor4Home()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 4\",\"speed\":5}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        private void M270Motor3Home()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 3\",\"speed\":5}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }

        private void M270Motor2Home()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 2\",\"speed\":10}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }

        private void M270Motor1Home()
        {
            d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Axis.Home\",\"parameters\": {\"id\":\"Modbus Motion 1\",\"speed\":10}}";//Lua
            d.opcWrite("OPCItemSyncWrite14", daServerMgt);
        }
        #endregion

        #region 2. M270-MOTOR-SETTING

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

        private int sCheckNumber = 1;

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

        #endregion

        #region 3. M270-MOTOR-MOTORTWOWAY
        private void MotorTwoway()
        {
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
        }
        #endregion

        #region 4. M270-MOTOR-MOTORSETPOSTION

        public double bed1marginsave = 24;
        public double bed2marginsave = 127;
        public double bed3marginsave = 370;

        public void Motor1SetPostion()
        {

            if (fristCheck == "0")
            {
                var tempbed1 = 24 - ((127 * Convert.ToDouble(d.DblMotor1Position)) / 106);
                bed1marginsave = tempbed1;
            }
        }
        public void Motor2SetPostion()
        {
            if (fristCheck == "0")
            {
                var tempbed2 = 127 - (127 * Convert.ToDouble(d.DblMotor2Position)) / 106;
                bed2marginsave = tempbed2;
            }
        }
        public void Motor3SetPostion()
        {
            if (fristCheck == "0")
            {
                var tempbed3 = 370 - ((370 * Convert.ToDouble(d.DblMotor3Position)) / 225);
                bed3marginsave = tempbed3;
            }
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

                        d.OPCItemWriteValueTextBoxes[14] = "{\"message\":\"Job.Start\",\"parameters\":{\"id\":\"" + printStr + "/" + printStr + ".job\"}}";//Lua
                        d.opcWrite("OPCItemSyncWrite14", daServerMgt);

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
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

                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
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
        }
        #endregion

        #endregion

        #region 7. M270-RIGHT/LEFT/UP/DOWN (MOTOR UP,DOWN만 사용)
        private int checkNumber = 1;
        

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

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 1\",\"speed\":" + d.DblMotor1Speed + ",\"position\":" + tempMove + "}}";


            }
            else if (checkNumber == 2)
            {
                var tempMove = Convert.ToSingle(d.DblMotor2Position) - Convert.ToSingle(d.DblMotor2Distance);

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 2\",\"speed\":" + d.DblMotor2Speed + ",\"position\":" + tempMove + "}}";
            }
            else if (checkNumber == 3)
            {

                var tempMove = Convert.ToSingle(d.DblMotor3Position) - Convert.ToSingle(d.DblMotor3Distance);

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 3\",\"speed\":" + d.DblMotor3Speed + ",\"position\":" + tempMove + "}}";
            }

            else if (checkNumber == 4)
            {
                var tempMove = Convert.ToSingle(d.DblMotor4Position) - Convert.ToSingle(d.DblMotor4Distance);

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 4\",\"speed\":" + d.DblMotor4Speed + ",\"position\":" + tempMove + "}}";
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

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 1\",\"speed\":" + d.DblMotor1Speed + ",\"position\":" + tempMove + "}}";
            }
            else if (checkNumber == 2)
            {
                var tempMove = Convert.ToSingle(d.DblMotor2Position) + Convert.ToSingle(d.DblMotor2Distance);

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 2\",\"speed\":" + d.DblMotor2Speed + ",\"position\":" + tempMove + "}}";
            }
            else if (checkNumber == 3)
            {
                var tempMove = Convert.ToSingle(d.DblMotor3Position) + Convert.ToSingle(d.DblMotor3Distance);

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 3\",\"speed\":" + d.DblMotor3Speed + ",\"position\":" + tempMove + "}}";
            }

            else if (checkNumber == 4)
            {
                var tempMove = Convert.ToSingle(d.DblMotor4Position) + Convert.ToSingle(d.DblMotor4Distance);

                //Motor2SetPostion();
                tempGo = "{\"message\":\"Axis.Move\",\"parameters\": {\"id\":\"Modbus Motion 4\",\"speed\":" + d.DblMotor4Speed + ",\"position\":" + tempMove + "}}";
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
                MessageBox.Show(e4.Message);
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

        #region 19. 실시간 변화 값 및 모니터링 쪽 이벤트들

        private void CartesianChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var series = chartPoint.Key;

            File_Status fs = new File_Status(labels[series], printingProcess[series], printFinsh[series]);
            fs.Show();
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
            if (double.Parse(apw.Mainpump.Content.ToString()) < 10)
            {
                double pump_main = double.Parse(apw.Mainpump.Content.ToString()) + 1;
                if (pump_main > 10) { pump_main = 10; }
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
                Console.WriteLine("헤응");
            }
        }
    }
}
