using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

/// <summary>
/// 二维码扫描器
/// </summary>
namespace Txq_csharp_sdk
{
    class Vbarapi
    {
        IntPtr dev = IntPtr.Zero;

        //打开信道
        [DllImport("vbar.dll", EntryPoint = "vbar_channel_open", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr vbar_channel_open(int type, long parm);
        //发送数据
        [DllImport("vbar.dll", EntryPoint = "vbar_channel_send", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vbar_channel_send(IntPtr dev, byte[] data, int length);
        //接收数据
        [DllImport("vbar.dll", EntryPoint = "vbar_channel_recv", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vbar_channel_recv(IntPtr dev, byte[] buffer, int size, int milliseconds);
        //关闭信道
        [DllImport("vbar.dll", EntryPoint = "vbar_channel_close", CallingConvention = CallingConvention.Cdecl)]
        public static extern void vbar_channel_close(IntPtr dev);
        
     
  

        //连接设备
        public bool openDevice()
        {
            dev = vbar_channel_open(1, 1);
            if (dev == IntPtr.Zero)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void closeDevice()
        {
            if (dev != null)
            {
                vbar_channel_close(dev);
                dev = IntPtr.Zero;
            }
            
        }
        byte[] iSetByte_ctl = new byte[64];
        //扫码开关
        public void controlScan(bool cswitch)
        {
            if (dev != null)
            {
                if (cswitch)
                {
                    iSetByte_ctl[0] = 0x55;
                    iSetByte_ctl[1] = 0xAA;
                    iSetByte_ctl[2] = 0x05;
                    iSetByte_ctl[3] = 0x01;
                    iSetByte_ctl[4] = 0x00;
                    iSetByte_ctl[5] = 0x00;
                    iSetByte_ctl[6] = 0xfb;
                }
                else
                {
                    iSetByte_ctl[0] = 0x55;
                    iSetByte_ctl[1] = 0xAA;
                    iSetByte_ctl[2] = 0x05;
                    iSetByte_ctl[3] = 0x01;
                    iSetByte_ctl[4] = 0x00;
                    iSetByte_ctl[5] = 0x01;
                    iSetByte_ctl[6] = 0xfa;
                }
                vbar_channel_send(dev, iSetByte_ctl, 64);
            }

        }
        //背光控制
        byte[] iSetByte = new byte[64];
        public void backlight(bool bswitch)
        {
            if (dev != null)
            {
                if (bswitch)
                {
                    iSetByte[0] = 0x55;
                    iSetByte[1] = 0xAA;
                    iSetByte[2] = 0x24;
                    iSetByte[3] = 0x01;
                    iSetByte[4] = 0x00;
                    iSetByte[5] = 0x01;
                    iSetByte[6] = 0xDB;
                }
                else
                {
                    iSetByte[0] = 0x55;
                    iSetByte[1] = 0xAA;
                    iSetByte[2] = 0x24;
                    iSetByte[3] = 0x01;
                    iSetByte[4] = 0x00;
                    iSetByte[5] = 0x00;
                    iSetByte[6] = 0xDA;
                }
                vbar_channel_send(dev, iSetByte, 64);
            }
            
        }
        //解码设置
        public bool getResultStr(out byte[] result_buffer, out int result_size)
        {
            byte[] c_result = new byte[256];
            if (dev != null)
            {
                byte[] bufferrecv = new byte[1024];
                vbar_channel_recv(dev, bufferrecv, 1024, 200);
                if (bufferrecv[0] == 85 && bufferrecv[1] == 170 && bufferrecv[3] == 0)
                {
                    int datalen = (bufferrecv[4] & 0xff) + ((bufferrecv[5] << 8) & 0xff);
                    byte[] readBuffers = new byte[datalen];
                    for (int s1 = 0; s1 < datalen; s1++)
                    {
                        readBuffers[s1] = bufferrecv[6 + s1];
                    }
                    result_buffer = readBuffers;
                    result_size = datalen;
                    return true;
                }
                else
                {
                    result_buffer = null;
                    result_size = 0;
                    return false;
                }
            }
            else
            {
                result_buffer = null;
                result_size = 0;
                return false;
            }
        }
    }
}
