   M S B A M L   `   `   `  ����  DPT_WPF'    !DPT_WPF.ucMETAL200.ucMETAL200Door   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $
��443* �6   �� 5      $
��342* �6   .+�5   
   �� $'��!/imgMetal200/imgDoor/door_img.png��6   $
��None=�6;   $��2��6J   �� 5   
   -   5         ��Name$ boChamberOff��5      ���	#FF00A2FF$��3q�6?   ���	#0000A2FF$��317,350,410,289q�6j   $��2��6�   �� 5   
   -   5        $ 	boLeftOff��5      ���	#FF00A2FF$��3q�6<   ���	#0000A2FF$��53,242,686,289q�6g   $��2��6   �� 5   
   -   5        $ 
boRightOff��5      ���	#FF00A2FF$��3q�6=   ���	#0000A2FF$��317,282,315,93q�6h   �� 5   
   -   5        $ boRightOpen��5      
��� � �$��3q�69   ���	#991FDB6A$��685,242,53,289q�6d   $��2��6|   ._�5      �� -   5         ��Name$ imgboRightOpen��5      $
��None=�6,   $%��/imgMG/imgDoor/door2_closed.png��6;   $��-3,0,-3,150q�6d   6{   5      �� 5   
   -   5        $ 
boLeftOpen��5      
��� � �$��3q�68   ���	#991FDB6A$��53,242,686,289q�6c   $��2��6{   ._�5      �� -   5        $ imgboLeftOpen��5      $
��None=�6+   $%��/imgMG/imgDoor/door2_closed.png��6:   $��-3,-3,-3,150q�6c   6{   5      �� 5   
   -   5        $ boChamberDownOpen��5      
��� � �$��3q�6?   ���	#991FDB6A$��317,350,410,289q�6j   $��2��6�   ._�5      �� -	   5        $ imgboChamberDownOpen��5      $
��None=�62   $%��/imgMG/imgDoor/door2_closed.png��6A   $��-3,-7,-3,-3q�6j    J� RenderTransformOrigin$ -0.871,0.557&�6   6�   5      �� 5    
   -
   5        $ boChamberRightDownOpen��5       
��� � �$��3q�6D   ���	#991FDB6A$��412,350,315,289q�6o   $��2��6�   ._�5!      �� -   5        $ imgboChamberRightDownOpen��5!      $
��None=�67   $%��/imgMG/imgDoor/door2_closed.png��6F   $��-3,-7,-3,-3q�6o   $ -0.871,0.557&�6�   6�   5"      �� 5$   
   -   5        $ boChamberOpen��5$      
��� � �$��3q�6;   ���	#991FDB6A$��317,282,315,93q�6f   ._�5%      �� -   5        $ imgCamberLocking��5%      $
��None=�6.   $$��/imgMG/imgDoor/door_closed.png��6=   $��3,0,-3,0q�6e    J� InputBindings 5&      d� 5'       d� Gesture$ 	LeftClicka�6#    �� Command 67   �� *DoorChamberLockCommand+6d   5)      5*      �� 5,   
   $��	LEFT SIDE��6   $��70��6$   $��30��6/   $��122,115,631,197q�6;   ���	#FF0087D3$��1��6k   6z   �� 5-   
   $��FRONT��6   $��30��6    $��386,115,389,197q�6,   $ 0.802,0.467&�6E   ���	#FF0087D3$��1��6�   �� 5.   
   $��
RIGHT SIDE��6   $��30��6%   $��640,113,108,199q�61   ���	#FF0087D3$��1��6a   51      52      