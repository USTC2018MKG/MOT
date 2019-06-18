using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT.domain
{
   public class User
    {
        public string employee_id { get; set; }
        public string user_name { get; set; }
        public string user_pwd { get; set; }
        public Nullable<int> sex { get; set; }
        public string phone { get; set; }
        public Nullable<int> state { get; set; }
        public int type { get; set; }
        private int num_auth { get; set; }

        // 用户产生领取订单时的的更换类型
        public int changeType { get; set; }

        // 管理员用户的无实物领取权限，普通用户数据库中字段为null
        public int NumAuth()
        {
            if(type == Constant.USER_TYPE_ADMIN){
                return num_auth;
            }
            else
            {
                return -1;
            }
            
        }
    }
}
