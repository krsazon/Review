   M S B A M L   `   `   `  ����	  Hotel2    ,Hotel.MasterData.Windows.StaffPositionWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   a ]DevExpress.Xpf.Core.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5   	   -   5        &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  
Office2013 5   G    � Title$ StaffPositionWindow��5   	   
�������$	��500��68   $	��450��6E    � WindowStyle$
 None=�6R    � 
ResizeMode$ NoResize=�6e   .��5      ]� .+�5	   
   ��  �� Orientation$ 
Horizontal=�6   $��Bottom=�6.   .+�5
      �� -   5         ��Name$ btnSave��5
      $��Bottom=�6&      ButtonEllipseTransparent#���   6>   $��50��6q   $��50��6}   $
��Left=�6�   $��Bottom=�6�   $��	20 0 5 20q�6�   .��5      �� $��/Images/Save2.png��6   5      �� 5      -   5        $ btnClose��5      #���   6'   $��50��6Z   $��50��6f   $
��Left=�6q   $��Bottom=�6�   $��5 0 0 20q�6�   .��5      �� $��/Images/Close.png��6   5      5      �� 5   
   .+�5      �� $��STAFF POSITION��6   $��2��6.   $��Center=�6B   $
��Left=�6]   $��Segoe UI)�6x   $
��Bold"�6�   
�������$��25(�6�   $��10 40 10 10q�6�   �� 5      $ 
Horizontal=�6   $
��20 5q�62   .+�5      �� $��Center=�6   
�������$��Segoe UI)�6J   $��STAFF POSITION��6`   $	��117��6v   $��3q�6�    �� Padding$	 0 5q�6�   %  DevExpress.Xpf.Editors.TextEdit  5      -   5          Name$ txtStaffPosition��5      $��1��69   $��3q�6I   $	��0 5q�6T   $��Segoe UI)�6b   $	��255��6x   5      �� 5      $ 
Horizontal=�6   $
��20 5q�62   .+�5      �� $��Center=�6   
�������$��Segoe UI)�6J   $��ASSIST��6`   $	��117��6n   $��3q�6z   $	 0 5q�6�   &   DevExpress.Xpf.Editors.CheckEdit  5      -   5          Name$ 	chkAssist��5       $��28��63   $	��110��6?   $��0,0,24,0q�6K   5      5      5      	 � 	Resources	 5      
 �� 
TargetType �� Property �� Value �� 
TargetType'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   Transitions%  System.Windows.VisualTransition   GeneratedDuration	   To   From   States   System.Windows.VisualState   
Storyboard �� TargetProperty �� 
TargetName9	  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value R� KeyTime R� Value   GeneratedEasingFunction/
  )System.Windows.Media.Animation.CircleEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   
Storyboard g� Duration W� From	  W� To! W� EasingFunction" �� KeyTime# �� Value$ �� CornerRadius% �Name& J� Opacity' ��Name��%  '        �� 5   
   "
  6   .�5      ��  � �� 5       �� 5!      "  6&   .R�5"      �  5#        5$   "   CommonStates�� 5%   &     5&   *   $ 0:0:0.1D�6;   $ 	MouseOver��6W     5'   *   $ 	MouseOver��6;   $ 0:0:0.15D�6L   $ Normal��6i   . 5)   &     
Normal��62     5*   &   	MouseOver��. 5+   *   �� .��5,   .   U� $ (UIElement.Opacity)�6L   $ on��6|   .\�5-   2   	  $ 0��6G   $ 1Q�6S   5.   /   T� 5/   .   $ (UIElement.Visibility)�6L   $	 off��6   ..�50   2   i� $ 0��6I   $ Hidden��6U   51   /   52   +   53   '     54   &   Pressed��62     55   &   Disabled��62   56   #     57   "   CheckStates�� 58   &     59   *   $ 0:0:0.15D�6;   $ Checked��6X    5:   .   
  5;   2   $ EaseIn=�6=   . 5=   .   �� .��5>   2   W� $ 0:0:0.15D�6B   $ 1X�6V   $  1X�6_   $ (UIElement.Opacity)�6f   $ on��6�   ! 5?   6   
  5@   :   $ 	EaseInOut=�6E   5B   3   U� 5C   2   $ X�6P   $ 	translate��6n   .\�5D   6   	  $ 0��6K   $ 0Q�6W   	  5E   6   $ 0:0:0.15��6K   $ 80Q�6^   5F   3   5H   /   5I   +     5J   *   $ 0:0:0.15D�6;   $ 	Unchecked��6X   . 5K   .   �� .��5L   2   U� $ X�6P   $ 	translate��6n   .\�5M   6   	  $ 0��6K   $ 52Q�6W   	  5N   6   $ 0:0:0.15��6K   $ 0Q�6^   5O   3   5Q   /   5R   +   . 5T   &     Checked��. 5U   *   �� .��5V   .   U� $ X�6L   $ 	translate��6j   .\�5W   2   	  $ 0��6G   $ 80Q�6S   5X   /   U� 5Y   .   $ (UIElement.Opacity)�6L   $ on��6|   .\�5Z   2   	  $ 0��6G   $ 1Q�6S   5[   /   U� 5\   .   $ (UIElement.Opacity)�6L   $ on_Copy��6|   .\�5]   2   	  $ 0��6G   $ 1Q�6S   5^   /   U� 5_   .   $ (UIElement.Opacity)�6L   $ off_Copy��6|   .\�5`   2   	  $ 0��6G   $ 0Q�6S   5a   /   �� 5b   .   $1 +(Border.Background).(SolidColorBrush.Color)�6K   $ 
backBorder��6�   .g�5c   2   �� $" 0��6F   $	# Red��6R   5d   /   5e   +   5f   '     5g   &   	Unchecked��62     5h   &   Indeterminate��62   5i   #   .+�5k      �� $��
backBorder��67   $$ 12��6%   $��0,0,1,1q�6I   
�������6Z   � 5l      $% off_Copy��6#   $��Right=�65   $��0,1,23,0q�6Q   $��Center=�6c   $��LeftToRight=�6~   $& 1Q�6�   .+�5m   "   �� $��Disabled��6,   
�������6<   5n      � 5o      $% on_Copy��6#   $
��Left=�64   $��27,1,0,0q�6O   $��Center=�6a   $��LeftToRight=�6|   $& 0Q�6�   .+�5p   "   �� $��Enabled��6,   
�������6;   5q      � 5r      $��2q�6#   .+�5s   "   � $% thumb��6'   $��24��66   $
��Left=�6A   .+�5t   &   �� $	' off��6-   $$ 12��6:   ._�5u   *   �� $$ 11��61   $��1q�6C   ���	#FFE5E5E5
���   �6f   5v   '   �� 5w   &   $' on��6-   $$ 12��69   $& 0Q�6K   ._�5x   *   �� $$ 11��61   $��1q�6C   ���	#FFBFD8F3
��� ���6g   5y   '   z�5z   &   T� 5{   *   	translate��6=   5}   #   5~      5      5�      5�      5�      5�      5�      