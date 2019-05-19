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
    /// 工人进入后。点击按钮后，需要开启Timer等待二维码扫描结果返回
    /// </summary>
    public partial class WorkerMainWindow : NavigationWindow
    {
        private String employeeId;
        public WorkerMainWindow(String employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
        }

        
    }
}
