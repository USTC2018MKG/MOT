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
    /// ProductPage.xaml 的交互逻辑
    /// 根据上一个页面传入的产品编号，显示相应的刀具，并且可以增加和减少
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            // 点击下一步
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // 点击返回
        }
    }
}
