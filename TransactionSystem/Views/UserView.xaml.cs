﻿using System;
using System.Windows.Controls;

namespace TransactionSystem.Views
{
    public partial class UserView : UserControl
    {
        public int UserId { get; set; }

        public UserView(int userId)
        {
            InitializeComponent();
            UserId = userId;
        }
    }
}
