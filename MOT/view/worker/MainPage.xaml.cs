using AccountHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MOT.view.worker
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // 控制宿主Windows的关闭
            Window workerMain = Window.GetWindow(this);
            workerMain.Close();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            QRCodePage page = new QRCodePage();
            Account.Instance.GetUser().changeType = Constant.CHANGE_TYPE_DEFAULT;
            this.NavigationService.Navigate(page);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            labelWelcome.Content = Account.Instance.GetUser().user_name + ", 欢迎您！";
        }
    }
}
