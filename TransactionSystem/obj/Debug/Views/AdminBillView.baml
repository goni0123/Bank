   M S B A M L   `   `   `  ����L  HTransactionSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TransactionSystem.ViewsTransactionSystem.Views  5      +    %TransactionSystem.Views.AdminBillView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:TransactionSystem.Views     5      .��5	      � .+�5
   
   �� $��Username��6   $��15(�6$   $
��Left=�62   $	��Top=�6M   $��	44,30,0,0q�6e   �� 5   
   -   5          ��Name$
  User��5      $��20��6    $	��100��6,   $
��Left=�68   $	��Top=�6S   $��
124,30,0,0q�6k   �� 5   
   $��Amount��6   $��15(�6"   $
��Left=�60   $	��Top=�6K   $��
351,60,0,0q�6c   �� 5   
   -   5        $  Amount��5      $��20��6"   $	��100��6.   $
��Left=�6:   $	��Top=�6U   $��
431,60,0,0q�6m   �� 5   
   $��	Bill Name��6   $��15(�6%   $
��Left=�63   $	��Top=�6N   $��
351,90,0,0q�6f   �� 5   
   -   5        $  BillName��5      $��20��6$   $	��100��60   $
��Left=�6<   $	��Top=�6W   $��
431,90,0,0q�6o   �� 5   
   -   5         ��Name$ AddBill��5      $	��Add��6   $��20��60   $	��100��6<   $
��Left=�6H   $	��Top=�6c   $��
540,30,0,0q�6{   6�   �� 5   
   -   5        $ 
UpdateBill��5   "   $��Update��6   $��20��66   $	��100��6B   $
��Left=�6N   $	��Top=�6i   $��
540,60,0,0q�6�   6�   �� 5   
   -   5        $ 
DeleteBill��5   "   $��Delete��6   $��20��66   $	��100��6B   $
��Left=�6N   $	��Top=�6i   $��
540,90,0,0q�6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ BillsD��5      $	��200��6#      AutoGenerateColumns .  $��124,125,160,125q�6L      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Bill Id��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *BillId+  5      $ Username��6%    67   �� *
Username+  5      $ 	Bill Name��6%    68   �� *Name+  5      $ Amount��6%    65   �� *Amount+4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Status��6)    69   �� *Status+5      �� 5   
   -   5        $ Search��5   "   $��Search��6   $��20��62   $	��100��6>   $
��Left=�6J   $	��Top=�6e   $��
229,30,0,0q�6}   6�   �� 5   
   $��Bill id��6   $��15(�6#   $
��Left=�61   $	��Top=�6L   $��
351,30,0,0q�6d   �� 5   
   -	   5        $  BillId��5      $��20��6"   $	��100��6.   $
��Left=�6:   $	��Top=�6U   $��
431,30,0,0q�6m   5      5       