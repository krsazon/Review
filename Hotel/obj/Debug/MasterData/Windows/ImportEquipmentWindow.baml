   M S B A M L   `   `   `  ����	  Hotel4    .Hotel.MasterData.Windows.ImportEquipmentWindow   5   	   a ]DevExpress.Xpf.Core.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     6@   f bDevExpress.Xpf.Grid.v14.1.Core, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5   	   <dxc2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	   -   5        &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  
Office2013 5   H    � Title$ ImportStudentsWindow��5   	   $	��650��6&   $	��450��63    � WindowStyle$
 None=�6?    � 
ResizeMode$ NoResize=�6R   
�������.��5      �  � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� .+�5      �� $��Import Equipments��6   $��Center=�61   $
��Left=�6L   $
��Bold"�6g   
�������$��25(�6�   $��10 40 10 10q�6�   � 5      $��1��6   $
��20 0q�6    .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ 	txtSearch��5      $	��175��61   $��0 5 5 5q�6=   $��Center=�6N   $	��3 0q�6p   $��25��6~      TextBoxRoundedStyle1#���   6�   $��0q�6�   6�   �� 5      -   5         ��Name$ 	btnSearch��5      $��5q�60   $��40��6;   $��40��6G     ButtonEllipseTransparent#���  6R   .��5      �� $��/Images/Find.png��6    $��Uniform=�6:   5      �� 5      -   5        $ 	btnReload��5      $��5q�60   $��40��6;   $��40��6G   #���  6R   .��5      �� $��/Images/Reload.png��6    $��Uniform=�6<   5      5      5      �� 5      $ 
Horizontal=�6   .+�5      %  DevExpress.Xpf.Grid.GridControl  -   5          Name$ dgEquip��5   #   $��2��64   )  #DevExpress.Xpf.Grid.DataControlBase	   AutoGenerateColumns$
	 None=�6A   $��
20,3,20,10q�6\   $	��400��6p   $	��169��6|   
   View
 5      #  DevExpress.Xpf.Grid.TableView  5      -   5          Name$ 	viewEquip��5   (   &   DevExpress.Xpf.Grid.DataViewBase   ShowTotalSummary .  &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .     NavigationStyle$	 Row=�6l      AllowEditing . 6�      Columns 5       $  DevExpress.Xpf.Grid.GridColumn  5!      $	  DevExpress.Xpf.Grid.BaseColumn 	  Header)
  #DevExpress.Xpf.Core.ObjectConverter$ 	Equipment
 6)   $  DevExpress.Xpf.Grid.ColumnBase   	FieldName$ EquipmentName��6<   5#      5$      �� 5%      $ 
Horizontal=�6   $��35��62   $��	20,0,30,0q�6>   .+�5&      �� $��Center=�6   
�������$��Segoe UI)�6J   $��	Equipment��6`   $��62��6q   $	��3,5q�6|    �� Padding$	 0 5q�6�   %  DevExpress.Xpf.Editors.TextEdit  5'      -   5          Name$ txtEquipment��5'         Mask$ n0��65      MaskType$ Numeric=�6?   $��3q�6R   $	��0 5q�6]   $��Segoe UI)�6k   $	��129��6�   {�.  6�   �� 5(      $��Center=�6   
�������$��Segoe UI)�6J   $
��Qty.��6`   $��24��6l   $��3,4,3,5q�6w   $	 0 5q�6�     5)      -   5        $ txtStock��5)      $ n0��61   $ Numeric=�6;   $��3q�6N   $	��0 5q�6Y   $��Segoe UI)�6g   $	��119��6}   �� 5*      -	   5        $ btnCheck��5*      $��5,0,5,3q�6+   $��32��6<   #���  6G   .��5+      �� $��/Images/Check.png��6   $��Uniform=�67   $��36��6I   $��30��6U   5,      5-      �� 5.      $ 
Horizontal=�6   .+�5/        -
   5        $ dgEquipRoom��5/   #   $��2��68   $
	 None=�6E   $��20,10,20,20q�6`   $	��400��6u   $	��155��6�   
 50        51      -   5        $ viewEquipRoom��51   (      MultiSelectMode$	 Row=�6@    .   .  $	 Row=�6�    . 6�    53        54      $ 	Equipment
 6)   $ 	Equipment��6<     55      $ Quantity
 6)   $ Quantity��6;   57      58      �� 59      $ 
Horizontal=�6   .+�5:      �� -   5        $ 	btnDelete��5:      $��Bottom=�6,   #���  6D   $��40��6w   $��40��6�   $
��Left=�6�   $��Bottom=�6�   $��	380 0 5 0q�6�   .��5;      �� $��/Images/Close2.png��6   5<      5=      5>      �� 5?   
   $��3��6   $ 
Horizontal=�6"   .+�5@      �� -   5        $ btnOK��5@      $��Bottom=�6$   #���  6<   $��50��6o   $��50��6{   $
��Left=�6�   $��Bottom=�6�   $��	20 0 5 20q�6�   .��5A      �� $��/Images/Save2.png��6   5B      �� 5C      -   5        $ btnClose��5C      $��Bottom=�6'   #���  6?   $��50��6r   $��50��6~   $
��Left=�6�   $��Bottom=�6�   $��5 0 5 20q�6�   .��5D      �� $��/Images/Close2.png��6   5E      5F        5G   
   -   5        $ txtRoomNumber��5G      $��0��6.   $��0��69     5H   
   -   5        $ txtRoomType��5H      $��0��6,   $��0��67     5I   
   -   5        $ 	txtStatus��5I      $��0��6+   $��0��66     5J   
   -   5        $ txtFloor��5J      $��0��6)   $��0��64     5K   
   -   5        $ txtEquipments��5K      $��0��6.   $��0��69     5L   
   -   5        $ txtDescription��5L      $��0��6/   $��0��6:   5N      5O      