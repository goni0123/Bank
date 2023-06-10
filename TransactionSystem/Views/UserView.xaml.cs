using System.Windows;
using System.Windows.Controls;

namespace TransactionSystem.Views
{
    public partial class UserView : UserControl
    {
        public int UserId { get; set; }

        public UserView()
        {
            InitializeComponent();

            MainView mainView = Window.GetWindow(this) as MainView;
            UserId = mainView.AuthenticatedUserId;
        }
    }
}
