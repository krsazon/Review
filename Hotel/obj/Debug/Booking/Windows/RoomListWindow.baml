   M S B A M L   `   `   `  ����	  Hotel*    $Hotel.Booking.Windows.RoomListWindow   5   	   a ]DevExpress.Xpf.Core.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5   	   f bDevExpress.Xpf.Grid.v14.1.Core, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  
Office2013 5   G    � Title$ RoomListWindow��5   	   $	��650��6    $	��450��6-    � WindowStyle$
 None=�69    � 
ResizeMode$ NoResize=�6L   
�������.��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� .+�5      �� $��	Room List��6   $��Center=�6)   $
��Left=�6D   $
��Bold"�6_   
�������$��25(�6�   $��10 40 10 10q�6�   � 5      $��1��6   $
��20 0q�6    .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ 	txtSearch��5      $	��175��61   $��0 5 5 5q�6=   $��Center=�6N   $	��3 0q�6p   $��25��6~      TextBoxRoundedStyle1#���   6�   $��0q�6�   6�   �� 5      -   5         ��Name$ 	btnSearch��5      $��5q�60   $��40��6;   $��40��6G     ButtonEllipseTransparent#���  6R   .��5      �� $��/Images/Find.png��6    $��Uniform=�6:   5      �� 5      -   5        $ 	btnReload��5      $��5q�60   $��40��6;   $��40��6G   #���  6R   .��5      �� $��/Images/Reload.png��6    $��Uniform=�6<   5      5      5      �� 5      $ 
Horizontal=�6   $	��427��62   .+�5      %  DevExpress.Xpf.Grid.GridControl  -   5          Name$ dgRoom��5   #   $��2��63   )  #DevExpress.Xpf.Grid.DataControlBase	   AutoGenerateColumns$
	 None=�6@   $��20,10q�6[   $	��400��6j   
   View
 5      #  DevExpress.Xpf.Grid.TableView  5      -   5          Name$ viewRoom��5   (      MultiSelectMode$	 Row=�6:   &   DevExpress.Xpf.Grid.DataViewBase   ShowTotalSummary .  &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .     NavigationStyle$	 Row=�6�      AllowEditing . 6�      Columns 5!      $  DevExpress.Xpf.Grid.GridColumn  5"      $	  DevExpress.Xpf.Grid.BaseColumn 	  Header)
  #DevExpress.Xpf.Core.ObjectConverter$ Room ID
 6)   $  DevExpress.Xpf.Grid.ColumnBase   	FieldName$ RoomId��6:     5#      $
 Name
 6)   $ 
RoomNumber��67     5$      $ 	Room Type
 6)   $ RoomType��6<     5%      $ Room Equipment
 6)   $ RoomEquipment��6A   5'      5(      �� 5)      $ 
Horizontal=�6   5*      �� 5+   
   $��3��6   $ 
Horizontal=�6"   .+�5,      �� -   5        $ btnOK��5,      $��Bottom=�6$   #���  6<   $��50��6o   $��50��6{   $
��Left=�6�   $��Bottom=�6�   $��	20 0 5 20q�6�   .��5-      �� $��/Images/Save2.png��6   5.      �� 5/      -   5        $ btnClose��5/      $��Bottom=�6'   #���  6?   $��50��6r   $��50��6~   $
��Left=�6�   $��Bottom=�6�   $��5 0 5 20q�6�   .��50      �� $��/Images/Close2.png��6   51      52      %  DevExpress.Xpf.Editors.TextEdit  53   
   -	   5          Name$ txtRoomNumber��53      $��0��6.   $��0��69     54   
   -
   5        $ txtRoomType��54      $��0��6,   $��0��67     55   
   -   5        $ 	txtStatus��55      $��0��6+   $��0��66     56   
   -   5        $ txtFloor��56      $��0��6)   $��0��64     57   
   -   5        $ txtEquipments��57      $��0��6.   $��0��69     58   
   -   5        $ txtDescription��58      $��0��6/   $��0��6:   59      5:      