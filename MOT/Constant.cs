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


        // admin 二维码扫描出结果后，下一跳
        public const int ADMIN_NEXT_WITHOUT_OLD = 0;

        public const int ADMIN_NEXT_CHANGE = 1;

        // 订单中刀具更换类型
        public const int CHANGE_TYPE_DEFAULT = 0;   // 默认以旧换新

        public const int CHANGE_TYPE_EXCEPTION = 1; // 异常领取

        public const int CHANGE_TYPE_ADD = 2;       // 工艺新增
    }
}
