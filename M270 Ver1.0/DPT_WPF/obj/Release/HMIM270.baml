   M S B A M L   `   `   `  ���� B  >DPT_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DPT_WPF.ucCommonDPT_WPF.ucCommon  /clr-namespace:DPT_WPF.ucM270DPT_WPF.ucM270  !clr-namespace:DPT_WPFDPT_WPF  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 7 clr-namespace:DPT_WPF.imgLoadingDPT_WPF.imgLoading      DPT_WPF.HMIM270   +ucCommonclr-namespace:DPT_WPF.ucCommon  'ucM270clr-namespace:DPT_WPF.ucM270  U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     'localclr-namespace:DPT_WPF     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     7
LoadingAni clr-namespace:DPT_WPF.imgLoading     UoptionsAhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/options     -     � Title$  HMIM270��$
��1920��$
��1080�� � WindowStyle$
 None=� � WindowState$ 	Maximized=�$��Center=�$��Center=�.��� ����  �� ImageSource$ /allbackground.png�� � ColumnDefinitions �� ��  � RowDefinitions �� $��95 ��� $��4 ��� $	��118 ��� $	��315 ��� $
��128* ��� $��73 �.+�� $��0��$��2��$��0,0,0,3.667q�$��2��.+�� .+��� $��2��$��2��$��/imgTop/top_basebox.png��$
��None=��� $��/imgTop/login.png��$��44��$��44��$��40,27,988,24q�� $��80,0,0,0q� �� $	��110 ��� $��40 ��� $
��auto ��� $��20 ��� $
��auto ��� $��20 ��� $
��auto � �� $��50 ��� .+��� $��0��$��0��$��Center=�$��Bottom=����	#FF45BDFF J� RenderTransformOrigin$ 0,0.5&�$��10(�$��99��$��	14,0,39,0q�$��2������ *	FtpName+�� $��0��$��1��$��Center=�$	��Top=�
�������$ 0,0.5&�$��12,0,0,0q�$��18(�$��2��$	��138������ *
UserName+�� $��2��$��2��$
��None=�$ 0.5,0.5&�$��
0,10,0,-10q����� .���  �� Property � �� �� Value$ /imgTop/top_btn_info.png��	 J� InputBindings	 d� 
 d� Gesture$
 	LeftClicka� �� Command �� *PowderSupplyWindowCommand+�� -    ��Name$ 	LogoutBTN��$��4��$��2��$ ��/imgTop/top_btn_logout.png��$
��None=�$��
0,10,0,-10q�	 d� $
 	LeftClicka� �� *WindowCloseCommand+� $��2��$��2��$��0,0.333,0,314.333q�$��2��.+��� $#��/imgTab/4_moter/title box.png��$	��180��$��50��$��Center=�$��Center=�$��41,11,851,57.334q��� -    ��Name$ 
AutoStatus��$��자동공급��$��18(����#D3D5D9$/��)/DPT_WPF;component/Font/#NanumSquare Bold)�$
��Bold"�$��Center=�$��Center=�$��74,24,874,64.334q�$��30��$	��124���� $��51�� �� Left$
 -564��
 �� Top$	 271��$	��182��$��40,13,850,54.334q�����  �� Color$ Black�� �� Opacity$ 0Q�	 d� $
 	LeftClicka� �� *AutoSupplyCommand+�� $#��/imgTab/4_moter/title box.png��$	��180��$��50��$��Center=�$��Center=�$��41,63,851,5.334q��� -   $ SupplyRatio��$��분말공급비율��$��18(����#D3D5D9$/��)/DPT_WPF;component/Font/#NanumSquare Bold)�$
��Bold"�$��Center=�$��Center=�$��74,71,874,17.334q�$��30��$	��124���� $��51��$
 -564��$	 271��$	��182��$��40,64,850,3.334q����� $ Black��$ 0Q�	 d� $
 	LeftClicka� �� *SupplyRatioCommand+�� $��imgTop/title_deagun.png��$��Center=�$	��315��$��34��� $��3��$��2��$��1��$��0,0.333q� �� �� �� .+�� $��0��$��3��.+��� $ ��imgPrinting/base shape.png��$	��850��$	��232���� $*��$imgPrinting/printing_State_shape.png��$	��248��$	��248��$
��Left=�$��	105,0,0,0q��� -    ��Name$ LaserPowerW��$��0W�����#F3FBFF$��Center=�$��Center=����	#FF404040$/��)/DPT_WPF;component/Font/#NanumBarunGothic)�$
��Bold"�$��762,205,185,77q�.������ $ imgTab/2_gas/textbox.png��.��� $��0��.+��� $��18(����#ffecd0$��Center=�$��Right=�$��0,-70,105,0q����� *StrPrintStaus+�� -    ��Name$ printingPercent��$��50(����#ffecd0 �� TextAlignment$ Center=�$��Center=�$��Center=�$��
160,0,70,0q����� *StrPrintPercetage+�� -   $ 	lblPsTime��$��00h:00m:00s��$��18(����#ffecd0$��Center=�$��Right=�$��	0,70,85,0q�  LiveCharts.Wpf.Gauge  -     Name$ 
printGauge��$	��224��$	��224��$��Right=�   Uses360Mode .    Value$ 0Q�   From$ 0Q�	   To$	 100Q�   InnerRadius$	 107X�$��
0,10,19,10q�   	FromColor$ 	#ffffecd0��   ToColor$ 	#ffffecd0�� �� ContentStringFormat$  ��$ 0.5,0.5&�
������    GaugeBackground
 ����    GaugeRenderTransform V� .~���   �� Angle$  90Q�� $��1��$��2�� �� �� .+�� $��0�� �� $	��20* ��� $	��60* �.+�� $��0��.+��� $��imgTop/oxygen.png��$��82��$��76��$
��Left=�$��Bottom=�$��	50,0,0,20q��� $��Oxygen�����#ffffff$/��)/DPT_WPF;component/Font/#NanumBarunGothic)�$��14(�$	��Top=�$��Center=�$��
60,76,59,0q��� -	   $
 oxya�����#ffffff$/��)/DPT_WPF;component/Font/#NanumBarunGothic)�$��15(�$��Bottom=�$��Center=�$��0,0,0,30q����� *PUChamberOxy+���� $ imgTop/round.png��� $��1��.+�#  LiveCharts.Wpf.CartesianChart  $	��320��$	��100��$
��Left=�$��Bottom=�&   LiveCharts.Wpf.Charts.Base.Chart!   Zoom$! Y=�"   Series" �� *ChamberOxySeries+#   AxisY#   LiveCharts.Wpf.Axis  $   MinValue$$ 0Q�%   MaxValue$% 30Q�&   BarUnit$& 5Q�'   AxisX'   (   Unit$( 10Q�� $��1�� �� $	��20* ��� $	��60* �.+�� $��0��.+��� $��imgTop/powder.png��$��82��$��76��$
��Left=�$	��Top=�$��	50,20,0,0q��� $��Powder Supply�����#ffffff$/��)/DPT_WPF;component/Font/#NanumBarunGothic)�$��Center=�$��Center=�$��-20,-40,0,0q��� $��Laser Power�����#ffffff$/��)/DPT_WPF;component/Font/#NanumBarunGothic)�$��Center=�$��Center=�$��420,20,-341,112q�� $��1��.+��� $��imgTop/Black.png��$	��135��$��10��$
��Left=�$��	0,-65,0,0q��� -
   $ pSupplyLoop��$��imgTop/Beige.png��$	��135��$��12��$
��Left=�$��	0,-65,0,0q��� -   $ powder_Light01��$��imgTop/green light.png��$��20��$��20��$
��Left=�$��
27,58,0,79q��� -   $ powder_Light02��$��imgTop/green light.png��$��20��$��20��$
��Left=�$��
87,58,0,79q��� -   $ powder_Light01_black��$��imgTop/light.png��$��9��$��8��$
��Left=�$��
33,64,0,85q��� -   $ powder_Light02_black��$��imgTop/light.png��$��9��$��8��$
��Left=�$��
93,64,0,85q�� $��3��$��	0,330,0,0q�$��2��$��2��.+��� ) � 	Resources) * �� 
TargetType��%�   '��       �� "* ��.���  � ��
 ���� ��  � ��$ 0q���  � ��
 ���� ��  � ��$ 0,0,0,0q���  � ��$ 0,0,0,0q���  � ��$	 980����  � ��$
 1280��.@��� $��69��$��96��$��
70,0,-70,0q�$	��Top=���Z� ) + �� 
TargetType, J� OpacityMask- �� Triggers. ]� 
Conditions/ �� Property0 �� Value��%�  '��       �� "* ��.���  � ��
 ���� ��  � �� �� "+ ��.R��� 
, ���� ��  � ��$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� - ]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/home_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/home_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/home_click.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/home.png��$	��Top=�$��Uniform=�$��69��.���  �� $	��500 ��� $	��682 �.+�� $��0��$	��482��$��Bottom=�$��0,0,0,0q�.+�!   DPT_WPF.ucM270.ucM270Device  $	��912��$	��482��$��Bottom=�� $��5,2,5,0q�$��1������ $ imgTab/1_home/box.png�� �� $
��110* ��� $
��230* �.+�� $��0��$	��165��$��
30,30,30,0q�$	��Top=� �� �� $	��121 �.+�� $��0��$��30��$	��Top=�.+��� 1 �� Padding$1 	60,10,0,0q�$	��LED�����#d3d5d9$*��$/DPT_WPF;component/Font/#NanumGothic)�$
��Bold"�$��16(�$��Center=�� $��1��.+�&    DPT_WPF.ucCommon.ucCommonM270LED  -   2  Name$2 
_ucM270LED��$	��820��� $��1��$��0,0,0,70q� �� �� .+�� $��0��.+��� $��Chamber Door��$	��Top=����#d3d5d9$��16(�$*��$/DPT_WPF;component/Font/#NanumGothic)�$
��Bold"�$��50, 20, 0, 0q�   DPT_WPF.ucM270.uc270Door  -   3  Name$3 _ucM270Door��$	��186��$	��201��$��	0,-50,0,0q�'   !DPT_WPF.ucCommon.ucCommonM270Door  -   4  Name$4 _ucCommonM270Door��$��Bottom=�$��0,0,0,25q�� $��1��.+��� $��Laser��$	��Top=����#d3d5d9$��16(�$*��$/DPT_WPF;component/Font/#NanumGothic)�$
��Bold"�$��50, 20, 0, 0q� 	   DPT_WPF.ucM270.ucM270Laser	  -   5 	 Name$5 _ucM270Laser��$	��222��$	��179��$��	0,-50,0,0q��� -   $ btn_laser_power��$y�Visible=�$'��!imgTab/1_home/laser/power_btn.png��$��76��$��76��$��-250,270,0,0q�	 d� $
 	LeftClicka� �� *LaserControlCommand+�� -   $ btn_Guide_Beam��$y�Visible=�$'��!imgTab/1_home/laser/laser_btn.png��$��76��$��76��$��250,270,0,0q�	 d� $
 	LeftClicka� �� *GuideBeamCommand+�� $��69��$��96��$��110,0,-110,0q�$	��Top=���Z� ) ��%�  '��       �� "* ��.���  � ��
 ���� ��  � �� �� "+ ��.R��� 
, ���� ��  � ��$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� - ]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/gas_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/gas_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/gas_click.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/gas.png��$	��Top=�$��Uniform=�$��69��.���  �� $	��500 ��� $	��682 �.+�� $��0��$	��482��$��Bottom=�.+�
   DPT_WPF.ucM270.ucM270Gas
  $	��912��$	��482��� $��1�� �� $
��417* ��� $
��275* � �� �� �� .+�� $��0��$��0��$��3������ $ imgTab/2_gas/box.png��� $��3��$��2��.+�"   DPT_WPF.ucCommon.ucCommonGAS  -   6  Name$6 _ucCommonGAS���� $��68��$��96��$��150,0,-150,0q�$	��Top=���Z� ) ��%�  '��       �� "* ��.���  � ��
 ���� ��  � �� �� "+ ��.R��� , �� $ imgMenu/pump.png����  � ��$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� - ]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/pump_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/pump_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/pump_click.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/pump.png��$	��Top=�$��Uniform=�$��69��.���  �� $	��500 ��� $	��682 �.+�� $��0��.+��� $ ��imgDevice/Device_Inner.png��$	��912��$	��480��$��Bottom=��� -   $ 	pumpLight��$"��imgTab/3_pump/pumponHigh.png��$	��414��$	��134��$��Center=�$��Bottom=�$��0,0,0,25q�� $��1��.+�'   !DPT_WPF.ucCommon.ucCommonM270Pump  -   7  Name$7 _ucM270Pump��$��
33,220,0,0q��� $#��/imgTab/4_moter/title box.png��$	��180��$��50��$	��Top=�$��Center=�$��42,24,758,0q��� -   $ AutoPump_Label��$��자동공급펌프��$��18(����#D3D5D9$/��)/DPT_WPF;component/Font/#NanumSquare Bold)�$
��Bold"�$	��Top=�$��Center=�$��75,35,794,0q�$��30��$	��111��$ 1.363,0.617&��� $��51��$
 -564��$	 271��$	��182��$��42,24,756,607q����� $ Black��$ 0Q�	 d� $
 	LeftClicka� �� *AutoPumpCommand+�� -   8 ��Name$8 sdpump��$
��Left=�$��
75,545,0,0q�   SliderStyle#��[  $	��Top=�9 �� TickFrequency$9 1Q�: � Value$: 0Q�; � Maximum$	; 100Q�$	��190���� -   $ 	WindSpeed��$��0��$��80(����#eeeeee$��Center=�$��Center=�$��333,290,347,252q��� $��68��$��96��$��190,0,-190,0q�$	��Top=���Z� ) ��%�  '��       �� "* ��.���  � ��
 ���� ��  � �� �� "+ ��.R��� 
, ���� ��  � ��$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� - ]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/motor_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/motor_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/moter_click.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/motor.png��$	��Top=�$��Uniform=�$��69��.���  �� $	��500 ��� $	��682 �.+�� $��0��.+��� $ ��imgDevice/Device_Inner.png��$	��912��$	��480��$��Bottom=��� -   $ 
imgRecotor��$*��$imgTab/4_moter/recotor_highright.png��$	��400��$	��105��$��Center=�$��Bottom=�$��
0,0,10,105q�$y�	Collapsed=��� -   $ imgBuildRoom��$,��&imgTab/4_moter/buildroom_highright.png��$	��266��$��79��$��Center=�$��Bottom=�$��0,0,0,35q�$y�	Collapsed=�� $��1��.+�(   "DPT_WPF.ucCommon.ucCommonM270Motor  -   <  Name$< _ucCommonM270Motor��$	��912��$	��568��$��Bottom=�$��
34,0,34,53q��� $��69��$��96��$��230,0,-230,0q�$	��Top=���Z� ) ��%�  '��       �� "* ��.���  � ��
 ���� ��  � �� �� "+ ��.R��� 
, ���� ��  � ��$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� - ]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/log_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/log_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/log click.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/log.png��$	��Top=�$��Uniform=�$��69��.��� .+��� $!��imgTab/5_log/text shape.png��$	��904��$	��955��$��Center=�$��Center=��� -   = ��Name$= lvLogs��$	��900��$	��390��$	��Top=�$��Center=�$��40,115,40,0q�
������ 
������ $
��auto=�$��Disabled=�$��0q����	#FFFFECD0���� "* ��- P� > P� Property> �@J�IsMouseOver? P� Value? . . ���  � ��
 ���� ��  � ��$ 0q���  � �� �#4C5364P� > �@��
IsSelected? . . ���  � ��
 ���� ��  � ��$ 0q���  � �� �#4C5364@ �� View@ �� !A �� ColumnHeaderContainerStyleA �� "* ��.���  � y�$ 	Collapsed=���  � ��$ 18��.P��� B �� Width$	B 230��C �� DisplayMemberBindingC �� *Time+�� $	B 140��C �� *	Machine+�� $	B 430��C �� *Work+�� $	B 100��D �� CellTemplateD �� .R�� .+��� $��Uniform=�$��20������ *Image+�� $��21(�$�� ���� -   $= 
lvWeekFile��$	��900��$	��388��$��Bottom=�$��Center=�$��40,0,40,124q�
������ 
������ $��0q�$��Disabled=�$
��Auto=����	#FFFFECD0���� "* ��- P� > �@J�IsMouseOver? . . ���  � ��
 ���� ��  � ��$ 0q���  � �� �#4C5364P� > �@��
IsSelected? . . ���  � ��
 ���� ��  � ��$ 0q���  � �� �#4C5364@ �� A �� "* ��.���  � y�$ 	Collapsed=�.P��� $B 90��D �� .R�� .+��� $��Uniform=�$��25��$��90������ *	WeekImage+�� $
��21.4(�$�� ���� $��시작 시간��$	B 180��C �� *WeekStartTime+�� $��종료 시간��$	B 180��C �� *WeekEndTime+�� $��총 소요시간��$	B 180��C �� *WeekWorkTime+�� $��파일 이름��$	B 180��C �� *WeekFileName+�� $B 90��D �� .R�� .+��� $ imgEtc��-    $��Uniform=�$��28��$��90������ *WeekImageetc+�� $��
Log 내용�����#ffffff$��18(�$*��$/DPT_WPF;component/Font/#NanumSquare)�$
��Left=�$	��Top=�$��	20,80,0,0q��� $!��최근 7일간 출력이력�����#ffffff$��18(�$*��$/DPT_WPF;component/Font/#NanumSquare)�$
��Left=�$	��Top=�$��
20,640,0,0q��� $��68��$��96��$��270,0,-270,0q�$	��Top=���Z� ) E �� Right��%�  '��       �� "* ��.���  � ��
 ���� ��  � �� �� "+ ��.R��� 
, ���� ��  � ��$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� - ]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��69��$��97��.+��� $��/imgMenu/printing_on.png��$	��Top=�$��Uniform=�$��88��$	E -10��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��69��$��97��.+��� $��/imgMenu/printing_on.png��$	��Top=�$��Uniform=�$��88��$	E -10��]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/print_click.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/printing.png��$	��Top=�$��Uniform=�$��69��.���  �� $��48 ��� $	��671 ��� $��90 ��� $
��284* ��� $	��89* �.+�� $��4������ $" /imgTab/6_printing/shape.png���� -!   $ lbltime��$��00h : 00m : 00s�����	#FF257BB4$��20(�$��92,14,727,627q�$��1���� $��3D Printing object�����	#FF676767$��20(�$��285,12,388,631q�$��1��� $��20,0,0,0q�$��0�� �� $
��159* ��� $
��611* �.+�� $��0��.+�� -"   F �Name$F progress��$��0q�$��11��$��10q�$	��Top=�$��
15,15,15,0q�$	; 100Q����	#FF0078D7���� G �� EndPoint$G 0.5,1&�H �� 
StartPoint$H 0.5,0&�.��� I � Color$I Black��J � Offset$J 0Q�� $I 	#FFC2C2C2��� $��1��.+��� -#   K ��Name$K myCanvas��$��0��$	��Top=�$	��650��$	��650��$ 	0.5,-0.09&�$��-86,748,416,-727q�z�V� .~��� L �� ScaleY$	L 0.3Q�M �� ScaleX$
M -0.3Q��� N �� AngleY$N 0Q�O �� AngleX$O 0Q��� $
  -180Q�T� .+�F� -$   P F�Name$P 
polyInline��
����  �$��1��F� -%   $P polyOutline��
���  ��$��1��� $��2��$��	35,0,20,0q� �� $��30 ��� $��30 �.+�� $��0��.+��� 
������ 
������ $��0q�Q �� CornerRadius$Q 10,10,10,10��$��	0,-20,0,0q�$	��Top=�._��� -&   $8 slLayer1��  print_Slider#��[ 
������ $: 0Q�$	��808��$��1��$
��Left=�$��5,0,0,0q��� R �� Orientation$R 
Horizontal=�$��	0,-25,0,0q�$��1��$��Right=�.+��� -'   $ txtCurrentLayer��$ ��/textResource/#NanumGothic)�$��0�����#FFECD0$��15(�$��Center=��� $�� �����#7F8797$��1(��� $��/�����#7F8797$��20(�$ ��/textResource/#NanumGothic)�$��Center=��� $�� �����#7F8797$��1(��� -(   $ txtTotalLayer��$��0�����#7F8797$��15(�$ ��/textResource/#NanumGothic)�$��Center=�� $��1��.+��� $
��LIST��$��18(����#FFFFFF$ ��/textResource/#NanumGothic)�$��Bottom=�$��20,20,0,-10q��� -)   $= lvFile��$��3��S �� SelectionMode$S Single=�$��
60,7,50,45q�
������ $��0q�
������ $��Disabled=�) T ��NameU �� TriggersV �� 
TargetName��%�  '��       �� "* ��.���  � �� �� "+ ��.R��� $T 
ItemBorder��$
��auto��$��40��._��� ���#C0C7D6$1 0,10,0,0q�$
 Left=�$��Center=�$��13(�$
��auto��$��40������ U P� > � {�? .  . ��� # � �� � ����  � �� �#C0C7D6��  � ��
 ���� ��  � ��$ 0q���  � ��$ 40��P� > �@��
IsSelected? . . ��� $V 
ItemBorder�� � �� �#C0C7D6�� $V 
ItemBorder�� � ��$ 1q����� W �� ResourceKey$W ItemDataTemplate��@ �� A �� "* ��.���  � y�$ 	Collapsed=�.P��� -*   chMultiFile��$B 0��D �� .R�� $��40��.+��� $��0,15,0,0q��� $	B 200��C �� � $��1��.+�)   #DPT_WPF.imgLoading.LoadingAnimation  -+   X  Name$X 
aniLoading��$y�	Collapsed=�$��Center=�$��Center=��� -,   $ imgPrintStart��$y�Visible=�$��4��$3��-/imgTab/6_printing/print_btn_start_enable.png��$
��None=�$��Center=�$��136,9,686,10q�$��70��	 d� $
 	LeftClicka� �� *PrintStartCommand+�� --   $ imgPrintPause��$y�Visible=�$��4��$3��-/imgTab/6_printing/print_btn_pause_enable.png��$
��None=�$��Center=�$��411,9,411,10q�$��70��	 d� $
 	LeftClicka� �� *PrintPauseCommand+�� -.   $ imgPrintFinish��$y�	Collapsed=�$��4��$��70��$��686,9,136,10q�$-��'/imgTab/6_printing/print_btn_finish.png��$��Center=�	 d� $
 	LeftClicka� �� *PrintFinishCommand+�� -/   $ imgPrintStop��$y�Visible=�$��4��$2��,/imgTab/6_printing/print_btn_stop_enable.png��$
��None=�$��Center=�$��686,9,136,10q�$��70��	 d� $
 	LeftClicka� �� *PrintStopCommand+�� -0   $ imgPrintResume��$y�	Collapsed=�$��4��$-��'/imgTab/6_printing/print_btn_resume.png��$
��None=�$��Center=�$��136,9,686,10q�$��70��	 d� $
 	LeftClicka� �� *PrintResumeCommand+�� -1   $ btnReset��$,��&/imgTab/6_printing/print_btn_reset.png��$
��Left=�$��
901,54,0,0q�$	��Top=�$��27��$��2��$��27��	 d� $
 	LeftClicka� �� *PrintResetCommand+�� $��85��$��96��$��310,0,-310,0q�$	��Top=���Z� ) ��%  '��       �� "* ��.���  � ��
 ���� ��  � �� �� "+ ��.R��� 
, ���� ��  � ��$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� - ]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��86��$��97��.+��� $��/imgMenu/tembtn_on.png��$	��Top=�$��Uniform=�$	��111��$	E -13��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��86��$��97��.+��� $��/imgMenu/tembtn_on.png��$	��Top=�$��Uniform=�$	��111��$	E -13��]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��87��$��97��$	��Top=�.+��� $��/imgMenu/tem_click.png��$	��Top=�$��Uniform=�$��86��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��86��$��97��$	��Top=�.+��� $��/imgMenu/temperature.png��$	��Top=�$��Uniform=�$��86��.���  �� $	��500 ��� $	��682 ��� .+�� $��0��.+��� $ ��imgDevice/Device_Inner.png��$	��912��$	��480��$��Bottom=��� $.��(imgTab/7_temperature/sensorhighlight.png��$	��247��$��86��$��Bottom=�$��	0,0,0,270q�� $��1��.+�/   )DPT_WPF.ucCommon.ucCommonM270TemperHumity  -2   Y  Name$Y _ucCommonTemp��$	��635��$	��Top=�$��0,50,0,0q��� -3   $ 
pSupplyTop��$/��)imgTab/4_moter/powder/POINT-highlight.png��$��31��$��31��$��54,529,895,122q��� -4   $ pSupplyUnder��$/��)imgTab/4_moter/powder/POINT-highlight.png��$��31��$��31��$��54,593,895,58q��� -5   $ pSupplyTop_black��$%��imgTab/4_moter/powder/POINT.png��$��9��$��9��$��65,540,906,133q��� -6   $ pSupplyUnder_black��$%��imgTab/4_moter/powder/POINT.png��$��9��$��9��$��65,604,906,69q��� $��350,0,-350,0q�$	��Top=�$��98����Z� ) ��%�  '��       �� "* ��.���  � ��
 ���� ��  � �� �� "+ ��.R��� 
, ���� ��  � ��$ 0q���  � ��$ 0q���  � ��$ 0q���  � ��
 ���� - ]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/log_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 . ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/log_on.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 . �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/log click.png��$	��Top=�$��Uniform=�$��69��]� . �� / �@J�IsMouseOver0 .  �� / �@��
IsSelected0 .  ./���  � �� �� ���#292e38$��70��$��97��.+��� $��/imgMenu/log.png��$	��Top=�$��Uniform=�$��69��.��� .+�� $��	0,0,0,725q�.+��� $1��+/imgMonitoring/imgLineSeriseBackground3.png��$	��950��$
��Fill=�$��15,22,15,-75q�  Z   LegendLocation$
Z None=�$��33,39,20,-19q�" �� *SeriesCollection+#   -7   [  Name$[ lvcOxys��$$ 0Q�$% 25Q�\   
Foreground
\ ����]   Title$] Oxygen��  -8   $[ 	lvcTemper��$	$ -60Q�$% 60Q�\ �#F23E3B$& 5Q�$] DewPoint��  -9   $[ 	lvcHumity��$$ 0Q�$% 6Q�\ �#FEBC00$	& 0.5Q�$] Pressure��^   Position$^ RightTop=�'   _   	Separator_   LiveCharts.Wpf.Separator  `   Step$` 1Q�� $��181,397,132,-145q� �� �� �� �� �� �� .+��� -:   a ��Name$a spChamberOxy��$��0��$R 
Horizontal=�$
��Left=�.+�A� $��15��$��15��
����������	#FF148FF2$��2���� -;   $ spChamberOxyText��$��챔버 산소(%)��$��Center=�$��Center=�$��12(�$��	0,86,0,91q��� -<   $a spChamberTemperature��$��1��$R 
Horizontal=�$
��Left=�.+�A� $��15��$��15��
����������	#FFF23E3B$��2���� -=   $ spChamberTemperatureText��$��압력(Bar)��$��Center=�$��Center=�$��12(�$��0,-2,0,2q��� ->   $a spChamberHumity��$��2��$R 
Horizontal=�$
��Left=�.+�A� $��15��$��15��
����������	#FFFEBC00$��2���� -?   $ spChamberHumityText��$��이슬점(°C)��$��Center=�$��Center=�$��12(�$��0,-2,0,2q��� -@   $a spGloveHumity��$��6��$R 
Horizontal=�$
��Left=�.+�A� $��15��$��15�����	#FFFEBC00$��2��
��������� -A   $ spGloveHumityText��$��Gas2압력(Bar)��$��Center=�$��Center=�$��12(�$��0,-2,0,2q��� -B   $a 
spGloveOxy��$��3��$R 
Horizontal=�$
��Left=�.+�A� $��15��$��15��
����������	#FF12BFD5$��2���� -C   $ spGloveOxyText��$��공압(Bar)��$��Center=�$��Center=�$��12(�$��0,-2,0,2q��� -D   $a spGloveTemperature��$��4��$R 
Horizontal=�$
��Left=�.+�A� $��15��$��15��
����������	#FFE70E58$��2���� -E   $ spGloveTemperatureText��$��Gas1압력(Bar)��$��Center=�$��Center=�$��12(�$��0,-2,0,2q��� -F   $ 
ChamberOxy��$��0��$��65(����#eeeeee$��54,602,601,480q�$��Center=�$��Center=��� -G   $ GasPressure��$��0��$��65(����#eeeeee$��367,601,345,481q�$��Center=�$��Center=��� -H   $ 
ChamberDew��$��0��$��65(����#eeeeee$��630,599,84,483q�$��Center=�$��Center=��� -I   $ AirPressure��$��0��$��65(����#eeeeee$��94,800,632,282q�$��Center=�$��Center=��� -J   $ Gas1Pressure��$��0��$��65(����#eeeeee$��367,801,348,281q�$��Center=�$��Center=��� -K   $ Gas2Pressure��$��0��$��65(����#eeeeee$��632,802,83,280q�$��Center=�$��Center=��� $��Gas2Pressure��$��14(����#a8b2c8$��727,895,156,251q��� $��Gas1Pressure��$��14(����#a8b2c8$��461,895,426,251q��� $��AirPressure��$��14(����#a8b2c8$��181,895,718,251q��� $��
ChamberDew��$��14(����#a8b2c8$��727,693,159,453q��� $��GasPressure��$��14(����#a8b2c8$��463,693,428,453q��� $��
ChamberOxy��$��14(����#a8b2c8$��180,693,705,453q��� -L   $ PipeOxy��$��0��$��65(����#eeeeee$��54,996,601,86q�$��Center=�$��Center=��� $��PipeOxy��$��14(����#a8b2c8$��191,1084,694,62q��� -M   $ 
PipeTemper��$��0��$��65(����#eeeeee$��338,996,317,86q�$��Center=�$��Center=��� $��PipeTemperature��$��14(����#a8b2c8$��443,1084,418,62q�� $��6��$��2��.+��� $'��!Copyrightⓒ 2018 DAEGUN Co,. Ltd��$��Bottom=�$��41��$��12(����#676767$ Center=�$
��Wrap=��� -N   b ��Name$b darkbackground��$y�	Collapsed=�$��0��$��2��$��0��$��7��$��	0,0,-8,-8q����	#8C000000$
��Left=�$
��1920�����	#00000000$	��Top=�$
��1080��