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

namespace MOT.view.admin
{
    /// <summary>
    /// AdminMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class AdminMainPage : Page
    {
        public AdminMainPage()
        {
            InitializeComponent();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            // 以旧换新
            Page page = new QRCodePage(Constant.ADMIN_NEXT_CHANGE);
            this.NavigationService.Navigate(page);
        }

        private void BtnGetWithoutOld_Click(object sender, RoutedEventArgs e)
        {
            // 无实物领取
            Page page = new QRCodePage(Constant.ADMIN_NEXT_WITHOUT_OLD);
            this.NavigationService.Navigate(page);
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            Window workerMain = Window.GetWindow(this);
            workerMain.Close();
        }
    }
}
