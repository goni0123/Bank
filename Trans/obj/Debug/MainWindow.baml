   M S B A M L   `   `   `  ����@  <Trans, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TransTrans  5   	       Trans.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Trans     5   	     � Title$  
MainWindow��5   	   $	��600��6   $
��1000��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ 
senderName��5
      $
��Left=�6'   $��
339,65,0,0q�6B   $	��Top=�6V   $	��120��6n   �� 5   
   $��Sender��6   $
��Left=�6!   $��
287,63,0,0q�6<   $	��Top=�6P   �� 5   
   -   5        $ reciverName��5      $
��Left=�6(   $��
562,65,0,0q�6C   $	��Top=�6W   $	��120��6o   �� 5   
   $��Reciver��6   $
��Left=�6"   $��
510,63,0,0q�6=   $	��Top=�6Q   �� 5   
   -   5         ��Name$ 
sendAmount��5      $
��Left=�6&   $��438,116,0,0q�6A    �� TextWrapping$
 Wrap=�6V   $	��Top=�6j   $��22��6�   $	��120��6�   �� 5   
   $��Amount��6   $
��Left=�6!   $��385,114,0,0q�6<   $	��Top=�6Q   �� 5   
   -   5         ��Name$ 	SendMoney��5      $��
Send Money��6$   $
��Left=�69   $��582,117,0,0q�6T   $	��Top=�6i   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ Transactions��5      $��170,195,174,65q�6)      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Sender��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 6P   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 65   �� *Sender+6P     5      $ Reciver��6%   $		 150 6S   
 66   �� *
Receiver+6S     5      $ Amount��6%   $		 150 6P   
 65   �� *Amount+6P     5      $	 Fee��6%   $		 100 6J   
 62   �� *Fee+6J     5      $
 Date��6%   $		 100 6W   
 63   �� *TransactionDate+6W   5      5      5      