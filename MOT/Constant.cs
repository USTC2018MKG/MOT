using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT
{
    class Constant
    {
        // 刷卡用户类型 0:员工 1：工程师 2:现场主管 3:现场经理
        public const int USER_TYPE_WORKER = 0;

        public const int USER_TYPE_ENGINEER = 1;

        public const int USER_TYPE_ADMIN = 2;

        public const int USER_TYPE_MANAGER = 3;


        // engineer 二维码扫描出结果后，下一跳
        public const int ENGINEER_NEXT_ADD = 0;

        public const int ENGINEER_NEXT_CHANGE = 1;
    }
}
