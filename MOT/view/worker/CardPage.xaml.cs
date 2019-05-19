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
    /// CardPage.xaml 的交互逻辑
    /// </summary>
    public partial class CardPage : Page
    {
        System.Windows.Threading.DispatcherTimer dtimer;

        public CardPage()
        {
            InitializeComponent();
            if (dtimer == null)
            {
                dtimer = new System.Windows.Threading.DispatcherTimer();
                dtimer.Interval = TimeSpan.FromSeconds(1);
                dtimer.Tick += dtimer_Tick;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 页面加载完成，需要开启Timer等待二维码动作
            // dtimer.Start();
            // 直接跳转到产品列表
        }

        void dtimer_Tick(object sender, EventArgs e)
        {
            // this.Label_Result.Content = DateTime.Now.ToString();
        }

        private void BtnInputDone_Click(object sender, RoutedEventArgs e)
        {
            // 暂时没有二维码扫描，使用手动输入
        }
    }
}
