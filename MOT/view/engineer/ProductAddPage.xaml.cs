using Dapper;
using MOT.domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MOT.view.engineer
{
    /// <summary>
    /// ProductAddPage.xaml 的交互逻辑
    /// 只可以默认选择，无法增加和减少
    /// </summary>
    public partial class ProductAddPage : Page
    {
        private String productId;

        private ObservableCollection<ProductItem> productItems;

        public ProductAddPage(String productId)
        {
            InitializeComponent();
            this.productId = productId;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string sqlServer = ConfigurationManager.AppSettings["MySQLUrl"];
            // String productQuery = "select *  FROM product WHERE product.pid = @pid;";

            string query = "SELECT product_item.plid, product_item.pid, product_item.mid, product_item.num, product_item.maxsafe_repo,product_item.minwarning_repo,product_item.pred_knife_num,material.rest " +
                "FROM product_item, material WHERE product_item.pid = @pid AND material.mid = product_item.mid;";
            using (IDbConnection connection = new MySqlConnection(sqlServer))
            {
                List<ProductItem> items = connection.Query<ProductItem>(query, new { pid = productId }).ToList();

                //   var product = connection.Query<Product>(productQuery, new { pid = productId });
                foreach (ProductItem item in items)
                {
                    // 如果所需刀具个数多于库存，以库存为主
                    item.Num = item.Num > item.rest ? item.rest : item.Num;
                }
                productItems = new ObservableCollection<ProductItem>(items);
                lvMaterials.ItemsSource = productItems;
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            ConfirmPage confirmPage = new ConfirmPage(productItems.AsList());
            this.NavigationService.Navigate(confirmPage);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // 点击返回
            this.NavigationService.GoBack();
        }
    }
}
