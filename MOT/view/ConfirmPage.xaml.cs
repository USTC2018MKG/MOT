using AccountHelper;
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

namespace MOT.view
{
    /// <summary>
    /// ConfirmPage.xaml 的交互逻辑
    /// 选定刀具数量后，再一次确认
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
            // 如果用户是主管的话，不再需要管理员刷卡
            User user = Account.Instance.GetUser();
            if (user.type == Constant.USER_TYPE_ADMIN)
            {
                User loginUser = Account.Instance.GetUser();
                Page page = new PreOrderPage(productItems, user.employee_id, user.employee_id,loginUser.changeType);
                this.NavigationService.Navigate(page);
            }
            else
            {
                Page adminPage = new AdminConfirmPage(productItems);
                this.NavigationService.Navigate(adminPage);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // 返回选择
            this.NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 不显示选择数量为0的刀具
            for (int i = productItems.Count - 1; i >= 0; i--)
            {
                if (productItems[i].Num == 0)
                {
                    productItems.Remove(productItems[i]);
                }
            }
            lvMaterials.ItemsSource = productItems;
        }
    }
}
