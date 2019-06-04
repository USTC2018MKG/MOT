using AccountHelper;
using Dapper;
using MOT.domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MOT.view
{
    /// <summary>
    /// AdminConfirmPage.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class AdminConfirmPage : Page
    {
        private DispatcherTimer dtimer;

        private List<ProductItem> productItems;

        public AdminConfirmPage(List<ProductItem> productItems)
        {
            InitializeComponent();

            this.productItems = productItems;

            if (dtimer == null)
            {
                dtimer = new System.Windows.Threading.DispatcherTimer();
                dtimer.Interval = TimeSpan.FromSeconds(1);
                dtimer.Tick += dtimer_Tick;
            }

        }

        private void dtimer_Tick(object sender, EventArgs e)
        {
            // 判断设备是否可用，不可用则继续检测
            if (CardDevice.Instance.IsDeviceOk)
            {
                String cardNo = CardDevice.Instance.GetCardNo();
                if (!String.IsNullOrEmpty(cardNo))
                {
                   // CardDevice.Instance.Beep();
                    User admin = CheckAdmin(cardNo);
                    if(admin.type == 2 || admin.type == 3)
                    {
                        // 去下单
                        Page page = new PreOrderPage(productItems, Account.Instance.GetUser().employee_id, admin.employee_id);
                        this.NavigationService.Navigate(page);
                        dtimer.Stop();
                    }
                    else
                    {
                        labelTip.Content = "该卡不属于主管";
                    }
                  
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

        private User CheckAdmin(String id)
        {
            string sqlServer = ConfigurationManager.AppSettings["MySQLUrl"];
            using (IDbConnection connection = new MySqlConnection(sqlServer))
            {
                //TODO 员工id跟卡号匹配
                string query = "select *  FROM user WHERE employee_id = @employee_id";
                User u = connection.Query<User>(query, new { employee_id = id }).SingleOrDefault();
                return u;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (dtimer != null)
            {
                dtimer.Start();
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (dtimer != null && dtimer.IsEnabled)
            {
                dtimer.Stop();
                dtimer = null;
            }
        }
    }
}
