   M S B A M L   `   `   `  ���� B  >DPT_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DPT_WPFDPT_WPF      DPT_WPF.File_Status   U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:DPT_WPF     -     �Name$  window1�� � Title$ File_Status��$	��200��$	��500��
 � Top$	 700�� � Left$	 300�� � WindowStyle$
 None=����	#00F0F8FF���	#00000000 � AllowsTransparency . .��� .+��� ����  �� ImageSource$- '/imgTab/4_moter/move_popup/Popup_bg.png������ ��  �� Orientation$ Vertical=�.+�� $��40��.+��� -    ��Name$ btnClose��$3��-/imgTabSubCommon/imgCommonMotor/btn_close.png��$
��None=�$��17��$��17��	 J� RenderTransformOrigin$	 15.5,1&�$��438,16,44,7q�� $��0,0,0,0q�
 � ColumnDefinitions
 �� $	��.8* ��� $��2* �.+��� ���#ffecd0$��35��$��Center=�$ ��/textResource/#NanumGothic)�$��30,0,0,0q�$��14(�$��	File Name��$
��Bold"��� -    ��Name$ 	dFileName��$��5,0,0,0q�$��1��$��0��$��14(�$��Center=����#FFFFFF$��2018-05-26.job��$ ��/textResource/#NanumGothic)�� $��10��� $
��auto��
 �� $	��.8* ��� $��2* �.+��� ���#ffecd0$��35��$��Center=�$	��Top=�$ ��/textResource/#NanumGothic)�$��30,0,0,0q�$��14(�$
��Bold"�$��Printing Record��� $��1��$��5,0,0,0q�.+��� .+�� $��8���� -    ��Name$ txtLog�� �� 
LineHeight$ 25�����#FFFFFF$��14(�$��Line1&#x0a;Line 2��� $��10��� $y�Visible=�
 �� $	��.8* ��� $��2* �.+��� ���#ffecd0$��35��$��Center=�$ ��/textResource/#NanumGothic)�$��30,0,0,0q�$��14(�$
��Bold"�$��Printing Result���� -   $ 
dFileFinsh��$��5,0,0,0q�$��1��$��35��$��Center=�$ ��/textResource/#NanumGothic)�$��14(����#FFFFFF$
��Done��� $��20��