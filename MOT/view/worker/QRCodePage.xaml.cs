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
using Txq_csharp_sdk;

namespace MOT.view.worker
{
    /// <summary>
    /// CardPage.xaml 的交互逻辑。
    /// </summary>
    public partial class QRCodePage : Page
    {
        System.Windows.Threading.DispatcherTimer dtimer;

        // 二维码扫描API对象
        private Vbarapi Api = new Vbarapi();

        // 控制扫描灯开关
        private Boolean lightOn = true;

        public QRCodePage()
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
            // 直接跳转到产品列表

            if (Api.openDevice())
            {
                Api.backlight(lightOn);
                Api.controlScan(true);
                dtimer.Start();
            }
            else
            {
                labelTip.Content = "连接设备失败";
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (dtimer != null && dtimer.IsEnabled)
            {
                dtimer.Stop();
            }
           // Api.controlScan(false);
        }

        // 定时任务
        void dtimer_Tick(object sender, EventArgs e)
        {
            string decoderesult = Decoder();
            Console.WriteLine(decoderesult);
            if (!String.IsNullOrEmpty(decoderesult))
            {
                tbProductNum.Text = decoderesult;
                dtimer.Stop();
            }

        }

        private void BtnInputDone_Click(object sender, RoutedEventArgs e)
        {
            // 暂时没有二维码扫描，使用手动输入
            if (String.IsNullOrEmpty(tbProductNum.Text))
            {
                MessageBox.Show("请输入产品号");
            }
            else
            {
                Page p = new ProductPage(tbProductNum.Text);
                this.NavigationService.Navigate(p);
            }
        }

        private void BtnLight_Click(object sender, RoutedEventArgs e)
        {
            lightOn = !lightOn;
            Api.backlight(lightOn);
        }

        private void BtnReScan_Click(object sender, RoutedEventArgs e)
        {
            tbProductNum.Text = "";
            
            if (dtimer != null && !dtimer.IsEnabled)
            {
                dtimer.Start();
            }
        }

        public string Decoder()
        {
            byte[] result;
            string sResult = null;
            int size;
            if (Api.getResultStr(out result, out size))
            {

                string msg = System.Text.Encoding.Default.GetString(result);
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                sResult = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
            else
            {
                sResult = null;
            }
            Console.WriteLine(sResult);
            return sResult;
        }

       
    }
}
