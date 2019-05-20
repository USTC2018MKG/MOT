using AccountHelper;
using Dapper;
using MOT.domain;
using MOT.view.worker;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace MOT
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 1. 定时器监听刷卡动作，刷卡成功后，根据角色进入不同界面
        /// </summary>
        System.Windows.Threading.DispatcherTimer dtimer;

        public MainWindow()
        {
            InitializeComponent();
            if (dtimer == null)
            {
                dtimer = new System.Windows.Threading.DispatcherTimer();
                dtimer.Interval = TimeSpan.FromSeconds(1);
                dtimer.Tick += dtimer_Tick;
            }

        }

        void dtimer_Tick(object sender, EventArgs e)
        {
            // 判断设备是否可用，不可用则继续检测
            if (CardDevice.Instance.IsDeviceOk)
            {
                String cardNo = CardDevice.Instance.GetCardNo();
                if (!String.IsNullOrEmpty(cardNo))
                {
                    User u = login(cardNo);
                    Account.Instance.Login(u);
                    // TODO 根据卡号类型，跳转相应的员工界面
                    WorkerMainWindow workerMainWindow = new WorkerMainWindow(cardNo);  //Login为窗口名，把要跳转的新窗口实例化
                    workerMainWindow.WindowStartupLocation = WindowStartupLocation.Manual;   //使新窗口位置在原来的位置上
                    workerMainWindow.Left = this.Left;  //使新窗口位置在原来的位置上
                    workerMainWindow.Top = this.Top;  //使新窗口位置在原来的位置上
                    workerMainWindow.Show();  //打开新窗口
                    // 关闭定时器
                    dtimer.Stop();
                }
            }
            else
            {
                CardDevice.Instance.Prepare();
                if (!CardDevice.Instance.IsDeviceOk)
                {
                    labelTip.Content = "未检测到刷卡机!";
                }
                else
                {
                    labelTip.Content = "";
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        // 变为活动窗口，load之前以及其他窗口切回
        private void Window_Activated(object sender, EventArgs e)
        {
            if (!dtimer.IsEnabled)
            {
                dtimer.Start();
            }
        }

        // 非活动
        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (dtimer.IsEnabled)
            {
                dtimer.Stop();
            }
        }

        private User login(String id)
        {
            string sqlServer = ConfigurationManager.AppSettings["MySQLUrl"];
            using (IDbConnection connection = new MySqlConnection(sqlServer))
            {
                string query = "select *  FROM user WHERE employee_id = @employee_id";
                User u = connection.Query<User>(query, new { employee_id = 1 }).SingleOrDefault();
                return u;
            }
        }
    }
}
