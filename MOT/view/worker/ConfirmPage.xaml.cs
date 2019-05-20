using MOT.domain;
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
    /// ConfirmPage.xaml 的交互逻辑
    /// 员工选择后，确认清单页。点击确认后，向数据库中写入。
    /// </summary>
    public partial class ConfirmPage : Page
    {
        private List<ProductItem> productItems;

        public ConfirmPage()
        {
            InitializeComponent();
        }


        public ConfirmPage(List<ProductItem> productItems)
        {
            InitializeComponent();
            this.productItems = productItems;
        }



        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            // 确认下单,员工主管刷卡后，生成订单
            Page adminPage = new AdminConfirmPage(productItems);
            this.NavigationService.Navigate(adminPage);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // 返回选择
            this.NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            lvMaterials.ItemsSource = productItems;
        }
    }
}
