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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MOT.view
{
    /// <summary>
    /// PreOrderPage.xaml 的交互逻辑。预下单。
    /// </summary>
    public partial class PreOrderPage : Page
    {
        private int changeType; 

        private List<ProductItem> productItems;

        private String employeeId;

        private String adminId;

        public PreOrderPage(List<domain.ProductItem> productItems, String employeeId, String adminId, int changeType)
        {
            InitializeComponent();
            this.productItems = productItems;
            this.employeeId = employeeId;
            this.adminId = adminId;
            this.changeType = changeType;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Window workerMain = Window.GetWindow(this);
            workerMain.Close();
        }

        // 页面加载完成后，去下单
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 1.生成订单
            // 2.生成订单项
            String sqlServer = ConfigurationManager.AppSettings["MySQLUrl"];
            using (IDbConnection connection = new MySqlConnection(sqlServer))
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    DateTime dt = DateTime.Now;
                    // 时间作为订单id
                    string outId = GetOutId(dt);
                    String now = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    String query = "insert into out_order values (@out_id, @out_time, @employee_id, @admin_id, @state, @mode, @change_type);";
                    int orderRows = connection.Execute(query, new { out_id = outId, out_time = now, employee_id = employeeId, admin_id = adminId,
                        state = 0, mode = "刷卡", change_type = changeType}, transaction);

                    if (orderRows > 0)
                    {
                        // 插入订单成功，插入表项
                        String insert = "insert into out_item(out_id, mid, num) values (@out_id, @mid, @num);";
                        foreach (ProductItem item in productItems)
                        {
                            int knifeRows = connection.Execute(insert, new { out_id = outId, item.mid, num = item.Num }, transaction);
                            if (knifeRows > 0)
                            {
                                String knifeSql = "update material set rest = @rest where mid = @mid; ";
                                int newRest = item.rest - item.Num;
                                // 修改刀具表中可用余量
                                if (newRest >= 0)
                                {
                                    connection.Execute(knifeSql, new { rest = newRest, mid = item.mid }, transaction);
                                }
                                else
                                {
                                    throw new Exception("刀具可用数量出错");
                                }
                            }
                        }
                    }

                    transaction.Commit();
                    labelTip.Content = "下单成功，请尽快到货柜领取！";
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    transaction.Rollback();
                    labelTip.Content = "请求出错，请返回重试。";
                    btnBack.Visibility = Visibility.Visible;
                }

            }
        }

        // 构造订单ID
        private string GetOutId(DateTime date)
        {
            String type;
            switch (changeType)
            {
                case Constant.CHANGE_TYPE_ADD: // 工艺新增
                    type = "GYXZ";
                    break;
                case Constant.CHANGE_TYPE_EXCEPTION: // 异常领取
                    type = "YCLQ";
                    break;
                default:                        // 以旧换新
                    type = "YJHX";
                    break;
            }
            string time = date.ToString("yyyyMMddhhmmss");

            return type + "_" + time;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Window workerMain = Window.GetWindow(this);
            workerMain.Close();
        }
    }
}
