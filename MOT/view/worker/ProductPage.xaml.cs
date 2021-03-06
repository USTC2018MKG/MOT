﻿using Dapper;
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

namespace MOT.view.worker
{
    /// <summary>
    /// ProductPage.xaml 的交互逻辑
    /// 根据上一个页面传入的产品编号，显示相应的刀具，并且可以增加和减少
    /// </summary>
    public partial class ProductPage : Page
    {
        private String productId;

        private ObservableCollection<ProductItem> productItems;

        public ProductPage(string productId)
        {
            InitializeComponent();
            this.productId = productId;
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            // 将选择数量为0的，从数据中删除。倒叙保证下标位置。
            // 如果选择的全是零，那么应该有提示
            int count = productItems.Count();
            foreach(ProductItem item in productItems)
            {
                if(item.Num == 0)
                {
                    count--;
                }
            }

            if(count == 0)
            {
                MessageBox.Show("请至少选择一种刀具，且数量不为零");
                return;
            }
            ConfirmPage confirmPage = new ConfirmPage(productItems.AsList());
            this.NavigationService.Navigate(confirmPage);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // 点击返回
            this.NavigationService.GoBack();
        }

        // 页面加载成功后，数据库中查询所需刀具信息
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

        // 刀具数量减一
        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            lvMaterials.SelectedItem = ((Button)sender).DataContext;
            ProductItem item = lvMaterials.SelectedItem as ProductItem;
            if (item.Num > 0)
            {
                item.Num--;
            }
            else
            {
                MessageBox.Show("刀具数量不能为负");
            }
        }

        // 刀具数量+1
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            lvMaterials.SelectedItem = ((Button)sender).DataContext;
            ProductItem item = lvMaterials.SelectedItem as ProductItem;
            if (item.Num < item.rest)
            {
                item.Num++;
            }
            else
            {
                MessageBox.Show("数量不可超过库存！");
            }
        }

        // 不需要某种刀具，直接删除条目
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定不需要此种刀具吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.OK)
            {
                lvMaterials.SelectedItem = ((Button)sender).DataContext;
                ProductItem item = lvMaterials.SelectedItem as ProductItem;              
                productItems.Remove(item);
            }
        }
    }
}
