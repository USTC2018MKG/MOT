using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitMark;
using System.Runtime.InteropServices;

namespace MOT
{
    class CardDevice
    {
        public int rt;  //返回值
        public int port;//串口号
       // public int data_format; // data数据格式: 0 - char  ;  1 - Hex

        public bool TestPort()
        {
            for (int i = 0; i < 100; i++)
            {
                int result = -1;
                try
                {
                    result = WSR.ws_openPort(i);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    result = -1;
                }

                if (result >= 0)
                {
                    port = i;
                    return true;
                }
            }
            return false;
        }

        public String GetCardNo()
        {
            uint cardno = 0;
            rt = WSR.ws_getCardNo(port, ref cardno);
            if (rt >= 0)
            {
                return cardno.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
