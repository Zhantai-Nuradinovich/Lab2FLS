   M S B A M L   `   `   `  ����H  DLab1Nuradinov, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Lab1NuradinovLab1Nuradinov  5   	       Lab1Nuradinov.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Lab1Nuradinov     5   	   -   5          �Name$  
TestWindow��5	   	    � Title$&  База данных угроз��5   	   $	��701��6#   $��1031.5��60   .��5      � .+�5   
   �  � DataContext 6   ��  �� ElementName$ 
TestWindow�� �� Path$ .�.+�5      &   System.Windows.Controls.DataGrid  -   5          Name$
 data��5      $
��Left=�6%      AutoGenerateColumns .  $	��442��6\   $��	10,14,0,0q�6i   $	��Top=�6|   $	��862��6�      CanUserResizeRows .     CanUserReorderColumns .  	   CanUserResizeColumns	 .  
   
IsReadOnly
 . ��6�   �� *
Collection+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ УБИ��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� $ Id�  5      $+ %Наименование угрозы��6)    6F   �� $
 Name�5      �� 5      -   5        $��updateButton��5   (   $��Обновить��6   $
��Left=�6<   $��
890,14,0,0q�6W   $	��Top=�6k   $	��123��6�    J� RenderTransformOrigin$ 
1.08,5.025&�6�   6�   �� 5      -   5        $��
saveButton��5   )   $��Сохранить��6   $
��Left=�6;   $��890,254,0,0q�6V   $	��Top=�6k   $	��123��6�   6�   �� 5      -   5        $��nextPageButton��5   2   $)��#Следующая страница��6   $
��Left=�6H   $��689,514,0,0q�6c   $	��Top=�6x   $	��123��6�   $��22��6�   �� 5      -   5        $��prevPageButton��5   3   $+��%Предыдущая страница��6   $
��Left=�6I   $��162,514,0,0q�6d   $	��Top=�6y   $	��124��6�   $��22��6�   �� 5      -   5        $��backComplexButton��5   9   $��
Назад��6   $y�Hidden=�6%   $
��Left=�6R   $��889,120,0,0q�6m   $	��Top=�6�   $	��124��6�   $��22��6�   �� 5      -   5        $
��Page��5   '   $��Страница��6   $
��Left=�63   $��920,568,0,0q�6N   $	��Top=�6c   $��Showcard Gothic)�6{   $��Italic$�6�   $
��Bold"�6�   �� 5      $+��%/: Created by Zhantai Nuradinovich :\��6   $
��Left=�6D   $��382,568,0,0q�6_   $	��Top=�6t   $��Italic$�6�   $��Showcard Gothic)�6�   �� 5      -	   5        $��listOfUpdatedThreats��5   7   $4��.Список обновленных угроз��6   $y�Hidden=�6S   $
��Left=�6g   $��418,461,0,0q�6�   $	��Top=�6�   $��Italic$�6�   $��Showcard Gothic)�6�   $��14(�6�   $
��Bold"�6�   5      �� 5   
   -
   5         ��Name$ 
PageNumber��5      $
��Left=�6b   $��911,573,0,0q�6}   $
��Wrap=�6�   $	��Top=�6�   $��Showcard Gothic)�6�   $��Italic$�6�    6(   �� $ TextBlockPage��$ .���6�   �� $ 
pageNumber�6�   5      5      