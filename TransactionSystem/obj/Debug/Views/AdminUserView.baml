   M S B A M L   `   `   `  ����L  HTransactionSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TransactionSystem.ViewsTransactionSystem.Views  5      +    %TransactionSystem.Views.AdminUserView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:TransactionSystem.Views     5      .��5	      � .+�5
   
   �� $��Username��6   $	��Top=�6$   $
��Left=�6<   $��15(�6W   $��
100,50,0,0q�6e   �� 5   
   -   5          ��Name$
  User��5      $	��Top=�6    $
��Left=�68   $��20��6S   $	��100��6_   $��
190,50,0,0q�6k   �� 5   
   $��Balance��6   $	��Top=�6#   $
��Left=�6;   $��15(�6V   $��
300,50,0,0q�6d   �� 5   
   -   5        $  Amount��5      $	��Top=�6"   $
��Left=�6:   $��20��6U   $	��100��6a   $��
390,50,0,0q�6m   �� 5   
   $��Password��6   $	��Top=�6$   $
��Left=�6<   $��15(�6W   $��
300,80,0,0q�6e   �� 5   
   -   5        $  Password��5      $	��Top=�6$   $
��Left=�6<   $��20��6W   $	��100��6c   $��
390,80,0,0q�6o   �� 5   
   -   5         ��Name$ NewUser��5      $��New User��6"   $	��Top=�65   $
��Left=�6M   $��20��6h   $	��100��6t   $��
510,50,0,0q�6�   6�   �� 5   
   -   5        $ 
DeleteUser��5      $��Delete User��6%   $	��Top=�6;   $
��Left=�6S   $��20��6n   $	��100��6z   $��
510,80,0,0q�6�   6�   �� 5   
   -   5        $ PasswordChange��5      $��New Password��6)   $	��Top=�6@   $
��Left=�6X   $��20��6s   $	��100��6   $��510,110,0,0q�6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ UsersD��5      $	��200��6#      AutoGenerateColumns .  $��100,144,190,106q�6L      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Username��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
Username+  5      $ Balance��6%    66   �� *	Balance+  5      $ Transactions��6%    6;   �� *Transaction_count+  5      $
 Role��6%    63   �� *Role+5      �� 5   
   $
��Role��6   $	��Top=�6    $
��Left=�68   $��15(�6S   $��
100,80,0,0q�6a   �� 5   
   -   5        $
  Role��5      $	��Top=�6    $
��Left=�68   $��20��6S   $	��100��6_   $��
190,80,0,0q�6k   5      5      