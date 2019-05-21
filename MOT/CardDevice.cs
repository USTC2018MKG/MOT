using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitMark;
using System.Runtime.InteropServices;

namespace MOT
{
    /// <summary>
    /// 单例的刷卡设备对象。
    /// 第一步IsDeviceOk判断设备状态，可用进入第二步，否则调用Prepare进行初始化
    /// 第二步GetCardNo获取卡号
    /// </summary>
    class CardDevice
    {

        private static volatile CardDevice cardDevice;

        // 锁
        private static readonly object lockObject = new object();

        // 私有构造方法
        private CardDevice(){}

        public int rt;  //返回值

        private int port;//串口号

        private Boolean isDeviceOk = false;

        public static CardDevice Instance
        {
            get
            {
                if (null == cardDevice)
                {
                    lock (lockObject)
                    {
                        if (null == cardDevice)
                        {
                            cardDevice = new CardDevice();
                        }
                    }

                }
                return cardDevice;
            }
        }

        public int Port
        {
            get { return this.port; }

            set { this.port = value; }
        }

        public Boolean IsDeviceOk
        {
            get { return this.isDeviceOk; }

            set { this.isDeviceOk = value; }
        }

        // 检测可用端口
        public void Prepare()
        {
            if (!isDeviceOk)
            {
                for (int i = 0; i < 50; i++)
                {
                    int result = WSR.ws_openPort(i);
                    if (result >= 0)
                    {
                        port = i;
                        isDeviceOk = true;
                        break;
                    }
                }
            } 
        }

        // 获取卡号，如果读取失败或没有Prepare成功，那么返回null
        public String GetCardNo()
        {
            if (isDeviceOk)
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
            else
            {
                return null;
            }
        }

        public void Beep()
        {
            if (isDeviceOk)
            {
                // 蜂鸣
                rt = WSR.ws_beep(port);
            }
           
        }
    }
}
