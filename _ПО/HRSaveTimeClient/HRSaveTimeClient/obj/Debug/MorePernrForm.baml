   M S B A M L   `   `   `  ����  HRSaveTimeClient$    HRSaveTimeClient.MorePernrForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  MorePernrForm��5   	   $	��400��6   $	��300��6,    � WindowStyle$
 None=�68    � 
ResizeMode$ NoResize=�6K   
����� � � WindowStartupLocation$ CenterScreen=�6t   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Pernr_dG��5      $
��Left=�6&   $	��Top=�6B   $	��350��6Z   $	��300��6g   $��
0,0,0,-195q�6�   ��6s   Z�    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$# Табельный номер��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6>      FontSize$ 20Q�6H   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6V   �� 
 �� Path$
 Pernr1�5
      � 5   
   -   5         �Name$ OkMorePernrReportsButton��5      ���#0049db$��Right=�6G   $��Bottom=�6c   $��52��6~   $��	0,0,10,10q�6�   .+�5      �� $��Resources/Reports/OK.png��6   5      � 5   
   -   5        $" CencelMorePernrReportsButton��5      ���#8d8d8d$��Right=�6K   $��Bottom=�6g   $	��100��6�   $��	0,0,70,10q�6�   .+�5      �� $!��Resources/People/Cancel.png��6   5      5      5      