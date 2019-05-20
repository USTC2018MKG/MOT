using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT.domain
{
    public class Product
    {
        // 产品id
        public String pid { get; set; }

        // 预测产能
        public int pro_pred { get; set; }

        // 机床名称
        public String tool { get; set; }

        // 所需刀具
        public List<ProductItem> items { get; set; }
    }
}
