   M S B A M L   `   `   `  ����  DPT_WPF*    $DPT_WPF.ucCommon.ucCommonMetal200LED   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $��80 �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      $��1 �6   �� 5      .+�5   
   �� $
��None=�6   $��0��6   $��0��6/   ��5      �� 5       �� 	UriSource$8 2/imgTabSubCommon/imgCommonLaser/text_box_laser.png?�6   6_   5      �� 5   
   $
��None=�6   $��2��6   $��0��6/   ��5      �� 5      $8 2/imgTabSubCommon/imgCommonLaser/text_box_laser.png?�6   6_   5      �� 5   
   ���	#FF636363$��0��6(   $��0��68   $��Center=�6E   $��Center=�6b   $��Left LED��6}   $
��Bold"�6�   $��180,27,172,27q�6�   �� 5   
   ���	#FF636363$��2��6'   $��0��67   $��Center=�6D   $��Center=�6a   $��	Right LED��6|   $
��Bold"�6�   $��175,27,166,27q�6�   $��68��6�   6�   �� 5!   
   -   5         ��Name$ imgRightLED��5!      $
��None=�6%   $��2��64   $��1��6D   $	��Top=�6Q   $/��)/imgTabSubCommon/imgCommonLED/led_off.png��6i    J� InputBindings 5"      d� 5#       d� Gesture$ 	LeftClicka�6    �� Command 63   �� *LEDRightCommand+6Y   5%      �� 5&   
   -   5        $ 
imgLeftLED��5&      $
��None=�6$   $��0��63   $��1��6C   $	��Top=�6P   $%��/imgMetal200/imgLED/led_off.png��6h    5'      d� 5(      $ 	LeftClicka�6    63   �� *LEDLeftCommand+6X   5*      �� 5,   
   $	��200��6   ���	#FFBCC1C5$��1��65   $��2��6E   5-      5/      50      