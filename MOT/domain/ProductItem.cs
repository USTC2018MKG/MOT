using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT.domain
{
    public class ProductItem : INotifyPropertyChanged
    {
        // 自己id
        public string plid { get; set; }

        // 所属ProductId
        public string pid { get; set; }

        // 刀具id
        public string mid { get; set; }

        // 一个产品需要此种刀具的数量
        private int num;

        public int Num
        {
            get { return this.num; }

            set
            {
                if (this.num != value)
                {
                    this.num = value;
                    this.NotifyPropertyChanged("Num");
                }
            }
        }

        // 最大安全库存
        public int maxsafe_repo { get; set; }

        // 最小警戒库存
        public int minwarning_repo { get; set; }

        // 预测刀片数量
        public int pred_knife_num { get; set; }

        public int rest { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
