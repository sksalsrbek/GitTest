   M S B A M L   `   `   `  ����   DPT_WPF(    "DPT_WPF.ucCommon.ucCommonM270Motor   U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� ����   �� ImageSource$"  /imgTab/4_moter/motor_bg.png�� � RowDefinitions �� .+�� $��0��$��-8,0,8,0q� � ColumnDefinitions �� $	��123 ��� $��1 ��� $
��139* ��� $
��132* ��� $
��162* ��� $
��127* ��� $
��137* ��� $	��91* � �� $
��117* ��� $
��113* ��� $
��112* ��� $
��110* ��� $
��118* ��� $
��112* �.+��� -    ��Name$ BuildRoomBorder��$��0��$��8��$��1��
������ ���#ffecd0 J� OpacityMask
 ���� $��2q�$��10,0,0,0q�$
��auto�� J� InputBindings d�  d� Gesture$ 	LeftClicka� �� Command �� *BuildRoom_Focus+ �� Mode$ TwoWay=��� -   $ RecotorBorder��$��0��$��8��$��2��
������ ���#ffecd0
 ���� $��2q�$��10,4,0,4q�$
��auto�� d� $ 	LeftClicka� �� *Recotor_Focus+$ TwoWay=��� -   $ FrontBorder��$��0��$��8��$��3��
������ ���#ffecd0
 ���� $��2q�$��10,4,0,4q�$
��auto�� d� $ 	LeftClicka� �� *Front_Focus+$ TwoWay=��� -   $ 
RearBorder��$��0��$��8��$��4��
������ ���#ffecd0
 ���� $��2q�$��10,4,0,4q�$
��auto�� d� $ 	LeftClicka� �� *
Rear_Focus+$ TwoWay=��� -   $ SupplyBorder��$��0��$��8��$��5��
������ ���#ffecd0
 ���� $��2q�$��10,4,0,4q�$
��auto�� d� $ 	LeftClicka� �� *Supply_Focus+$ TwoWay=��� ���#d2d7e1$��13(�$��Center=�$��Center=�$��	5,43,5,25q�$��2��$��1������ *DblMotor1Position+	 �� UpdateSourceTrigger$	 PropertyChanged=�$ TwoWay=��� ���#d2d7e1$��13(�$��Center=�$��Center=�$��	5,43,5,25q�$��2��$��2������ *DblMotor2Position+$	 PropertyChanged=�$ TwoWay=��� ���#d2d7e1$��13(�$��Center=�$��Center=�$��	5,43,5,25q�$��2��$��3������ *DblMotor3Position+$	 PropertyChanged=�$ TwoWay=��� ���#d2d7e1$��13(�$��Center=�$��Center=�$��	5,43,5,25q�$��2��$��4������ *DblMotor4Position+$	 PropertyChanged=�$ TwoWay=��� ���#d2d7e1$��13(�$��Center=�$��Center=�$��	5,43,5,25q�$��2��$��5������ *DblMotor5Position+$	 PropertyChanged=�$ TwoWay=��� ���#FFFFFF$��13(�$��Center=�$��Center=�$��3��$��1��
������ $��13,25,13,24q����� *DblMotor1Speed+$	 PropertyChanged=�$ TwoWay=�.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.�� d� $ 	LeftClicka� �� *Motor1SpeedCommand+.���� ���#FFFFFF$��13(�$��Center=�$��Center=�$��3��$��2��
������ $��13,30q����� *DblMotor2Speed+$	 PropertyChanged=�$ TwoWay=�.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.�� d� $ 	LeftClicka� �� *Motor2SpeedCommand+.���� ���#FFFFFF$��13(�$��Center=�$��Center=�$��3��$��3��
������ $��13,30q����� *DblMotor3Speed+$	 PropertyChanged=�$ TwoWay=�.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.�� d� $ 	LeftClicka� �� *Motor3SpeedCommand+.���� ���#FFFFFF$��13(�$��Center=�$��Center=�$��3��$��4��
������ $��13,30q����� *DblMotor4Speed+$	 PropertyChanged=�$ TwoWay=�.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.�� d� $ 	LeftClicka� �� *Motor4SpeedCommand+.���� ���#FFFFFF$��13(�$��Center=�$��Center=�$��3��$��5��
������ $��13,30q����� *DblMotor5Speed+$	 PropertyChanged=�$ TwoWay=�.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.�� d� $ 	LeftClicka� �� *Motor5SpeedCommand+.���� -   
 ��Name$
 txtMoveTest��$y�Visible=�$��	70,0,53,0q��� $��Center=�$��Center=�$��4��$��1�����#FFFFFF
������ $��28,25,28,24q����� *DblMotor1Distance+$	 PropertyChanged=�$ TwoWay=�.�� d� $ 	LeftClicka� �� *Motor1MoveCommand+.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.���� $��Center=�$��Center=�$��4��$��2�����#FFFFFF
������ $��28,30q����� *DblMotor2Distance+$	 PropertyChanged=�$ TwoWay=�.�� d� $ 	LeftClicka� �� *Motor2MoveCommand+.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.���� $��Center=�$��Center=�$��4��$��3�����#FFFFFF
������ $��28,30q����� *DblMotor3Distance+$	 PropertyChanged=�$ TwoWay=�.�� d� $ 	LeftClicka� �� *Motor3MoveCommand+.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.���� $��Center=�$��Center=�$��4��$��4�����#FFFFFF
������ $��28,30q����� *DblMotor4Distance+$	 PropertyChanged=�$ TwoWay=�.�� d� $ 	LeftClicka� �� *Motor4MoveCommand+.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.���� $��Center=�$��Center=�$��4��$��5�����#FFFFFF
������ $��28,30q����� *DblMotor5Distance+$	 PropertyChanged=�$ TwoWay=�.�� d� $ 	LeftClicka� �� *Motor5MoveCommand+.������ $1  +/imgTab/4_moter/build-feed room/textbox.png��.���� -    ��Name$ btnimgAlram1��$��5��$��1��$
��None=�$5��//imgTab/4_moter/build-feed room/motor_alram.png���� -   $ btnimgAlram2��$��5��$��2��$
��None=�$5��//imgTab/4_moter/build-feed room/motor_alram.png���� -	   $ btnimgAlram3��$��5��$��3��$
��None=�$5��//imgTab/4_moter/build-feed room/motor_alram.png���� -
   $ btnimgAlram4��$��5��$��4��$
��None=�$5��//imgTab/4_moter/build-feed room/motor_alram.png���� -   $ btnimgAlram5��$��5��$��5��$
��None=�$5��//imgTab/4_moter/build-feed room/motor_alram.png���� -   $ btnimgReset1��$��6��$��1��$
��None=�$/��)/imgTab/4_moter/build-feed room/reset.png�� d� $ 	LeftClicka� �� *Motor1ResetCommand+�� -   $ btnimgReset2��$��6��$��2��$
��None=�$/��)/imgTab/4_moter/build-feed room/reset.png�� d� $ 	LeftClicka� �� *Motor2ResetCommand+�� -   $ btnimgReset3��$��6��$��3��$
��None=�$/��)/imgTab/4_moter/build-feed room/reset.png�� d� $ 	LeftClicka� �� *Motor3ResetCommand+�� -   $ btnimgReset4��$��6��$��4��$
��None=�$/��)/imgTab/4_moter/build-feed room/reset.png�� d� $ 	LeftClicka� �� *Motor4ResetCommand+�� -   $ btnimgReset5��$��6��$��5��$
��None=�$/��)/imgTab/4_moter/build-feed room/reset.png�� d� $ 	LeftClicka� �� *Motor5ResetCommand+�� -   $ imgBuildHome��$��7��$��1��$
��None=�����  �� 	UriSource$7 1/imgTab/4_moter/build-feed room/btn_home_bage.png?� d� $ 	LeftClicka� �� *Motor1HomeCommand+�� -   $ imgPowderHome��$��7��$��2��$
��None=����� $7 1/imgTab/4_moter/build-feed room/btn_home_bage.png?� d� $ 	LeftClicka� �� *Motor2HomeCommand+�� -   $ imgFrontHome��$��7��$��3��$
��None=����� $7 1/imgTab/4_moter/build-feed room/btn_home_bage.png?��� -   $ imgRearHome��$��7��$��4��$
��None=����� $7 1/imgTab/4_moter/build-feed room/btn_home_bage.png?��� -   $ imgSupplyHome��$��7��$��5��$
��None=����� $7 1/imgTab/4_moter/build-feed room/btn_home_bage.png?��� $��Center=�$��Bottom=�$��18(����#959fb4$��2��$��0��$��Position��$
��Bold"�$��34��$
��30,0q�$��79���� $��Center=�$��Bottom=�$��18(����#959fb4$��3��$��0��$��Speed��$
��Bold"�$��34��$
��35,0q�$��62���� $��Center=�$��Bottom=�$��18(����#959fb4$��4��$��0��$��Distance to move��$
��Bold"�$��34��$	��1,0q�$	��160���� $��Center=�$��Bottom=�$��18(����#959fb4$��5��$��0��$��Motor Alram��$
��Bold"�$��34��$	��3,0q�$	��121���� $��Center=�$��Bottom=�$��18(����#959fb4$��6��$��0��$��Alram Reset��$
��Bold"�$��34��$��	12,0,11,0q�$	��114���� $��Center=�$��Bottom=�$��18(����#959fb4$��7��$��0��$
��Home��$
��Bold"�$��34��$��	15,0,14,0q�$��62���� $��Center=�$��Center=�$��0��$��1��$��
Build Room��$��18(�$
��Bold"����#a8b2c8$��34��$��Center=�$	��112���� $��Center=�$��Center=�$��0��$��2��$��Recoater��$��18(�$
��Bold"����#a8b2c8$��34��$��Center=�$��87��$��18,39q��� $��Center=�$��Center=�$��0��$��3��$��Front��$��18(�$
��Bold"����#a8b2c8$��34��$��Center=�$��76���� $��Center=�$��Center=�$��0��$��4��$
��Rear��$��18(�$
��Bold"����#a8b2c8$��34��$��Center=�$��76���� $��Center=�$��Center=�$��0��$��5��$��Supply��$��18(�$
��Bold"����#a8b2c8$��34��$��Center=�$��76���� $#��/imgTab/4_moter/title box.png��$	��180��$��50��$	��Top=�$
��Left=�$��	10,16,0,0q��� $��Build/Feed Room��$��18(����#959fb4$/��)/DPT_WPF;component/Font/#NanumSquare Bold)�$
��Bold"�$	��Top=�$
��Left=�$��	22,25,0,0q�