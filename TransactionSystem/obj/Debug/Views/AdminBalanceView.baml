   M S B A M L   `   `   `  ����L  HTransactionSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TransactionSystem.ViewsTransactionSystem.Views  5      .    (TransactionSystem.Views.AdminBalanceView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:TransactionSystem.Views     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Username��6   $��15(�6$   $��10q�62   $��0��6>   $��0��6K   $��70��6[   $��20��6f   �� 5   
   -   5         ��Name$
 User��5      $��15(�6    $��5q�6.   $��0��69   $��1��6F   $	��100��6V   $��20��6b   �� 5   
   $��Amount��6   $��15(�6"   $��10q�60   $��1��6<   $��0��6I   $��70��6Y   $��20��6d   �� 5   
   -   5        $ Amount��5      $��15(�6"   $��5q�60   $��1��6;   $��1��6H   $	��100��6X   $��20��6d   �� 5   
   -   5         ��Name$ Update��5      $��Update��6!   $��5q�62   $��0��6=   $��2��6J   $��2��6Z   $��70��6k   $��40��6v   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ Trans��5      $��10q�6"   $��2��6.   $��0��6;   $��3��6K      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Username��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6T   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 67   �� *
Username+6T     5      $ Balance��6%   $ * 6R   	 66   �� *	Balance+6R   5!      5"      5#      