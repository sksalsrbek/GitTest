   M S B A M L   `   `   `  ����  DPT_WPF    DPT_WPF.ucMG.ucMGDoor   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � $��0��6   $��2��6   .+�5
   
   �� $+��%/imgMG/imgDoor/door_mg_img_shadow.png��6   $
��None=�6?   6P   �� 5   
   -   5          ��Name$  boRightOpen��5      
��� � �$��3q�69   ���	#991FDB6A$��675,243,58,295q�6d   ._�5      �� -   5         ��Name$ imgboRightOpen��5      $
��None=�6,   $%��/imgMG/imgDoor/door2_closed.png��6;   $��1,-3,-1,153q�6d   6{   5      �� 5   
   -   5        $  
boLeftOpen��5      
��� � �$��3q�68   ���	#991FDB6A$��52,240,680,295q�6c   ._�5      �� -   5        $ imgboLeftOpen��5      $
��None=�6+   $%��/imgMG/imgDoor/door2_closed.png��6:   $��-1,-3,1,153q�6c   6z   5      �� 5   
   -   5        $  boGloveDownOpen��5      
��� � �$��3q�6=   ���	#991FDB6A$��291,337,412,295q�6h   ._�5      �� -   5        $ imgboGloveDownOpen��5      $
��None=�60   $%��/imgMG/imgDoor/door2_closed.png��6?   $��-3q�6h   6v   5      �� 5   
   -   5        $  boChamberDownOpen��5      
��� � �$��3q�6?   ���	#991FDB6A$��411,337,297,295q�6j   ._�5      �� -   5        $ imgboChamberDownOpen��5      $
��None=�62   $%��/imgMG/imgDoor/door2_closed.png��6A   $��-3,-3,-2,-3q�6j   6�   5      �� 5   
   -	   5        $  boChamberOpen��5      
��� � �$��3q�6;   $��411,272,297,447q�6O   ._�5      �� -
   5        $ imgCamberLocking��5      $��40��6.   $��40��6:   $��UniformToFill=�6E   $$��/imgMG/imgDoor/door_closed.png��6]   $��34,10,35,10q�6�    J� InputBindings 5      d� 5        d� Gesture$ 	LeftClicka�6#    �� Command 67   �� *DoorChamberLockCommand+6d   5"      5#      �� 5%   
   -   5        $  boGloveOpen��5%      
��� � �$��3q�69   $��291,272,412,447q�6M   ._�5&      �� -   5        $ imgGloveLocking��5&      $��40��6-   $��40��69   $��UniformToFill=�6D   $$��/imgMG/imgDoor/door_closed.png��6\   $��38,10,36,10q�6�    5'      d� 5(      $ 	LeftClicka�6#    67   �� *DoorGloveLockCommand+6b   5*      5+      �� 5.   
   $��	LEFT SIDE��6   $��70��6$   $��30��6/   $��107,558,646,196q�6;   ���	#FF0087D36m   �� 5/   
   $��FRONT��6   $��30��6    $��386,558,389,196q�6,    J� RenderTransformOrigin$ 0.802,0.467&�6E   ���	#FF0087D36i   �� 50   
   $��
RIGHT SIDE��6   $��30��6%   $��638,556,110,198q�61   ���	#FF0087D36J   53      54      