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
    }
}
