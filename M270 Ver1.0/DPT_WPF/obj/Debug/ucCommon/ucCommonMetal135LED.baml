   M S B A M L   `   `   `  ����B  >DPT_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DPT_WPFDPT_WPF  5      *    $DPT_WPF.ucCommon.ucCommonMetal135LED   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $ddclr-namespace:DPT_WPF     5      .��5	      � .+�5   
   �� -   5          ��Name$  testimg��5      $
��None=�6!   $#��/imgMetal135/imgLED/Shape.png��60   5      �� 5   
   -   5        $  btnImgLedOff��5      $��67��6&   $��67��62   $
��None=�6=   $��48,110,706,111q�6L    J� RenderTransformOrigin$ 0.359,0.508&�6d   ��5      �� 5      .�5      ��  �� Property � �� �� Value$A ;/DPT_WPF;component/imgMetal135/imgLED/led_all_off_off_1.png��65    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��$@ :/DPT_WPF;component/imgMetal135/imgLED/led_all_off_on_1.png��6=   5      5       J� InputBindings 5      d� 5       d� Gesture$ 	LeftClicka�6   	 �� Command	 63   �� *LEDOffCommand+6X   5!      �� 5#   
   -   5        $  btnImgLedAllOn��5#      $.��(/imgMetal135/imgLED/led_all_on_off_1.png��6(   $��67��6Z   $��67��6f   $
��None=�6q   $��705.5,111,49,112q�6�   $ 0.366,0.978&�6�    5$      d� 5%      $ 	LeftClicka�6   	 63   �� *LED3Command+6U   5'      �� 5(   
   -   5        $  
btnImgLed2��5(      $)��#/imgMetal135/imgLED/led_2_off_1.png��6$   $��67��6Q   $��67��6]   $
��None=�6h   $��376,102,379,121q�6w   $ 0.366,0.978&�6�    53      d� 54      $ 	LeftClicka�6   	 63   �� *LED2Command+6U   56      �� 57   
   -   5        $  
btnImgLed1��57      $)��#/imgMetal135/imgLED/led_1_off_1.png��6$   $��67��6Q   $��67��6]   $
��None=�6h   $��206,102,548,121q�6w    58      d� 59      $ 	LeftClicka�6   	 63   �� *LED1Command+6V   5;      �� 5<   
   -   5        $  
btnImgLed3��5<      $)��#/imgMetal135/imgLED/led_3_off_1.png��6$   $��67��6Q   $��67��6]   $
��None=�6h   $��545,102,210,121q�6w   $ 0.366,0.978&�6�    5=      d� 5>      $ 	LeftClicka�6   	 63   �� *LED3Command+6U   5@      �� 5B   
   $y�	Collapsed=�6   $��25��6'   $��60��63   $��LED ALL��6>   ���	#FFEBEDF0$��55,125,708,141q�6g   $��Center=�6   $��Center=�6�   6�   �� 5C   
   $y�	Collapsed=�6   $��25��6'   $��60��63   $��LED ALL��6>   ���	#FFEBEDF0$��714,125,49,141q�6g   $��Center=�6   $��Center=�6�   6�   �� 5E   
   $y�	Collapsed=�6   $��25��6'   $��60��63   $��ON��6>   
�������$��69,142,694,124q�6^   $��Center=�6v   $��Center=�6�   6�   �� 5F   
   $y�	Collapsed=�6   $��25��6(   $��60��64   $	��OFF��6?   
�������$��724,142,39,124q�6`   $��Center=�6x   $��Center=�6�   6�   �� 5H   
   $y�	Collapsed=�6   $��30��6'   $��30��63   $��1��6>   
�������$��Center=�6]   $��Center=�6z   $��MoeumT R)�6�   $��18(�6�   $��230,131,563,130q�6�   �� 5I   
   $y�	Collapsed=�6   $��30��6'   $��30��63   $��2��6>   
�������$��Center=�6]   $��Center=�6z   $��MoeumT R)�6�   $��18(�6�   $��402,130,391,131q�6�   �� 5J   
   $y�	Collapsed=�6   $��30��6'   $��30��63   $��3��6>   
�������$��Center=�6]   $��Center=�6z   $��MoeumT R)�6�   $��18(�6�   $��571,129,222,132q�6�   5M      5N      