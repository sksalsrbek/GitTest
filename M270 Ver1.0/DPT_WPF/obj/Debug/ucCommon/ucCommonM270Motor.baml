   M S B A M L   `   `   `  ����  DPT_WPF$    DPT_WPF.ucCommon.ucCommonMotor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � ColumnDefinitions  5	   
   �� 5
      $	��140 �6   �� 5      $��30 �6   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      $��1 �6   �� 5      �� 5      $��1 �6   �� 5      .+�5   
   �� $��2��6   $��8��6   ���	#FFD4DEE8$	��765��6I   6W   �� 5   
   $��4��6   $��8��6   ���	#FFD4DEE8$	��765��6I   6W   �� 5   
   $��0��6   $��1��6    $
��None=�6-   $��Right=�6<   ��5       �� 5!       �� 	UriSource$8 2/imgTabSubCommon/imgCommonMotor/text_box_motor.png?�6   6_   5#      �� 5%   
   $��0��6   $��3��6    $
��None=�6-   $��Right=�6<   ��5&      �� 5'      $8 2/imgTabSubCommon/imgCommonMotor/text_box_motor.png?�6   6_   5)      �� 5+   
   $��0��6   $��5��6    $
��None=�6-   $��Right=�6<   ��5,      �� 5-      $8 2/imgTabSubCommon/imgCommonMotor/text_box_motor.png?�6   6_   5/      �� 51   
   $��2��6   $��0��6    $
��None=�6-   $��Bottom=�6<   ��52      �� 53      $9 3/imgTabSubCommon/imgCommonMotor/text_box_motor2.png?�6   6`   55      �� 57   
   $��3��6   $��0��6    $
��None=�6-   $��Bottom=�6<   ��58      �� 59      $9 3/imgTabSubCommon/imgCommonMotor/text_box_motor2.png?�6   6`   5;      �� 5=   
   $��4��6   $��0��6    $
��None=�6-   $��Bottom=�6<   ��5>      �� 5?      $9 3/imgTabSubCommon/imgCommonMotor/text_box_motor2.png?�6   6`   5A      �� 5C   
   $��5��6   $��0��6    $
��None=�6-   $��Bottom=�6<   ��5D      �� 5E      $9 3/imgTabSubCommon/imgCommonMotor/text_box_motor2.png?�6   6`   5G      �� 5I   
   $��6��6   $��0��6    $
��None=�6-   $��Bottom=�6<   ��5J      �� 5K      $9 3/imgTabSubCommon/imgCommonMotor/text_box_motor2.png?�6   6`   5M      �� 5O   
   $��7��6   $��0��6    $
��None=�6-   $��Bottom=�6<   ��5P      �� 5Q      $9 3/imgTabSubCommon/imgCommonMotor/text_box_motor2.png?�6   6`   5S      �� 5U   
   ���	#FF0C82B9$��13(�6   $��Center=�6�   $��Center=�6�   $��5q�6�   $��2��6�   $��1��6�   $��35��6�   ��6   �� *DblMotor1Position+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   �� 5V   
   ���	#FF0C82B9$��13(�6   $��Center=�6�   $��Center=�6�   $��5q�6�   $��2��6�   $��3��6�   $��35��6�   ��6   �� *DblMotor2Position+$ PropertyChanged=�$ TwoWay=�6	  �� 5W   
   ���	#FF0C82B9$��13(�6   $��Center=�6�   $��Center=�6�   $��5q�6�   $��2��6�   $��5��6�   $��35��6�   ��6   �� *DblMotor3Position+$ PropertyChanged=�$ TwoWay=�6	  �� 5Y   
   $��5q�6d   $��25��6o   $��90��6{   ���	#FF0C82B9$��13(�6�   $��Center=�6�   $��Center=�6�   $��3��6�   $��1��6  ��6   �� *DblMotor1Speed+$ PropertyChanged=�$ TwoWay=�6  �� 5Z   
   $��5q�6d   $��25��6o   $��90��6{   ���	#FF0C82B9$��13(�6�   $��Center=�6�   $��Center=�6�   $��3��6�   $��3��6  ��6   �� *DblMotor1Speed+$ PropertyChanged=�$ TwoWay=�6  �� 5[   
   $��5q�6d   $��25��6o   $��90��6{   ���	#FF0C82B9$��13(�6�   $��Center=�6�   $��Center=�6�   $��3��6�   $��5��6  ��6   �� *DblMotor1Speed+$ PropertyChanged=�$ TwoWay=�6  �� 5]   
   -   5         ��Name$ txtMoveTest��5]      $��1��6'   $y�Visible=�61   6H   �� 5`   
   $��Center=�6g   $��Center=�6�   $��5q�6�   $��4��6�   $��1��6�   $��25��6�   $��90��6�   ���	#FF636363��6   �� *DblMotor1Distance+$ PropertyChanged=�$ TwoWay=�.��6   J� InputBindings 5a      d� 5b       d� Gesture$ 	LeftClicka�6    �� Command 63   �� *Motor1MoveCommand+.��5c   %   5d      �� 5e   
   $��Center=�6g   $��Center=�6�   $��5q�6�   $��4��6�   $��3��6�   $��25��6�   $��90��6�   ���	#FF636363��6   �� *DblMotor2Distance+$ PropertyChanged=�$ TwoWay=�.��6   5f      d� 5g      $ 	LeftClicka�6    63   �� *Motor2MoveCommand+.��5h   %   5i      �� 5j   
   $��Center=�6g   $��Center=�6�   $��5q�6�   $��4��6�   $��5��6�   $��25��6�   $��90��6�   ���	#FF636363��6   �� *DblMotor3Distance+$ PropertyChanged=�$ TwoWay=�.��6   5k      d� 5l      $ 	LeftClicka�6    63   �� *Motor3MoveCommand+.��5m   %   5n      �� 5p   
   -   5        	 ��Name$	 btnimgAlarm1��5p      $��5��6&   $��1��66   $
��None=�6C   $3��-/imgTabSubCommon/imgCommonMotor/alarm_off.png��6R   6�   �� 5q   
   -   5        $	 btnimgAlarm2��5q      $��5��6&   $��3��66   $
��None=�6C   $3��-/imgTabSubCommon/imgCommonMotor/alarm_off.png��6R   6�   �� 5r   
   -   5        $	 btnimgAlarm3��5r      $��5��6&   $��5��66   $
��None=�6C   $3��-/imgTabSubCommon/imgCommonMotor/alarm_off.png��6R   6�   �� 5t   
   -   5        $	 btnimgReset1��5t      $��6��6&   $��1��66   $
��None=�6C   $8��2/imgTabSubCommon/imgCommonMotor/btn_reset_none.png��6R    5u      d� 5v      $ 	LeftClicka�6    63   �� *Motor1ResetCommand+6\   5x      �� 5y   
   -   5        $	 btnimgReset2��5y      $��6��6&   $��3��66   $
��None=�6C   $8��2/imgTabSubCommon/imgCommonMotor/btn_reset_none.png��6R    5z      d� 5{      $ 	LeftClicka�6    63   �� *Motor2ResetCommand+6\   5}      �� 5~   
   -   5        $	 btnimgReset3��5~      $��6��6&   $��5��66   $
��None=�6C   $8��2/imgTabSubCommon/imgCommonMotor/btn_reset_none.png��6R    5      d� 5�      $ 	LeftClicka�6    63   �� *Motor3ResetCommand+6\   5�      �� 5�   
   -   5        $	 imgBuildHome��5�      $��7��6&   $��1��66   $
��None=�6C   ��5�      �� 5�      $2 ,/imgTabSubCommon/imgCommonMotor/btn_home.png?�6   6Y    5�      d� 5�      $ 	LeftClicka�6    63   �� *MGMotor1HomeCommand+6]   5�      �� 5�   
   -	   5        $	 imgPowderHome��5�      $��7��6'   $��3��67   $
��None=�6D   ��5�      �� 5�      $2 ,/imgTabSubCommon/imgCommonMotor/btn_home.png?�6   6Y    5�      d� 5�      $ 	LeftClicka�6    63   �� *MGMotor2HomeCommand+6]   5�      �� 5�   
   -
   5        $	 imgRecotorHome��5�      $��7��6(   $��5��68   $
��None=�6E   ��5�      �� 5�      $2 ,/imgTabSubCommon/imgCommonMotor/btn_home.png?�6   6Y    5�      d� 5�      $ 	LeftClicka�6    63   �� *MGMotor3HomeCommand+6]   5�      �� 5�   
   $��Center=�6   $��Center=�6-   $��2��6H   $��0��6X   $��Position��6e   $
��Bold"�6x   ���	#FF878484$��
26,41,27,5q�6�   6�   �� 5�   
   $��Center=�6   $��Center=�6-   $��3��6H   $��0��6X   $��Speed��6e   $
��Bold"�6u   ���	#FF878484$��
32,41,32,5q�6�   6�   �� 5�   
   $��Center=�6   $��Center=�6-   $��4��6H   $��0��6X   $��이동할 거리��6e   $
��Bold"�6v   ���	#FF878484$��
19,40,16,6q�6�   6�   �� 5�   
   $��Center=�6   $��Center=�6-   $��5��6H   $��0��6X   $��모터알람��6e   $
��Bold"�6t   ���	#FF878484$��
25,40,26,6q�6�   6�   �� 5�   
   $��Center=�6   $��Center=�6-   $��6��6H   $��0��6X   $��모터리셋��6e   $
��Bold"�6t   ���	#FF878484$��
25,41,25,5q�6�   6�   �� 5�   
   $��Center=�6   $��Center=�6-   $��7��6H   $��0��6X   $
��Home��6e   $
��Bold"�6t   ���	#FF878484$��
32,40,33,6q�6�   6�   �� 5�   
   $��Center=�6   $��Center=�6-   $��0��6H   $��1��6X   $��	빌드룸��6e   $
��Bold"�6s   ���	#FF0C82B9$��60,23,34,23q�6�   6�   �� 5�   
   $��Center=�6   $��Center=�6-   $��0��6H   $��3��6X   $��	피드룸��6e   $
��Bold"�6s   ���	#FF0C82B9$��61,23,33,23q�6�   6�   �� 5�   
   $��Center=�6   $��Center=�6-   $��0��6H   $��5��6X   $��블레이드��6e   $
��Bold"�6t   ���	#FF0C82B9$��55,24,27,23q�6�   6�   5�      5�      