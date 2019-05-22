using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// 读卡器
/// </summary>
namespace WitMark
{

    public class WSR
    {
        //全局环境变量
        public const int MODE_CARDBYTES    = 0;  //[ID 读卡器专用] 卡号字节数 ： 4/5   
        public const int MODE_CARDTYPE     = 1;  //卡型：         0-M1（S50）卡            1-M1 (S70)卡       20 - ID卡      30 - 磁卡
        public const int MODE_CARDSEQ      = 2;  //卡号顺序       0-默认顺序(默认)         1-反顺序
        public const int MODE_DATATYPE     = 3;  //数据格式       0-char（默认）           1-Hex
        public const int MODE_TIMEOUT      = 4;  //通讯超时       默认100ms    范围：30 - 1000ms
        public const int MODE_COMMAND      = 5;  //命令类型       0-高级命令(默认)         1-单步命令
        public const int MODE_MULTI_PORT   = 6;  //端口模式       0-单端口单设备（默认）   1-多端口多设备
        public const int MODE_REQUEST      = 7;  //寻卡模式       0-IDLE                   1-ALL

        public const int MODE_CARDISO      = 10; //协议标准       0x41 'A'   ---   ISO14443 TYPE A 方式
		                                         //               0x42 'B'   ---   ISO14443 TYPE B 方式
												 //               0x72 'r'   ---   AT88RF020卡 方式
												 //               0x73 's'   ---   ST卡 方式
												 //               0x31 '1'   ---   ISO15693 方式



        public WSR()
        { 
        }


        //-------------------------------------------------------------------------------------------------
        // 与串口通讯相关函数
        //-------------------------------------------------------------------------------------------------
        //打开串口
        [DllImport("WSR.dll")]
        public static extern int ws_openPort(int Port);
        //关闭串口
        [DllImport("WSR.dll")]
        public static extern int ws_closePort(int Port);
        //设置串口波特率
        [DllImport("WSR.dll")]
        public static extern int ws_setBaud(int Port,int Baud);
        //读取串口波特率
        [DllImport("WSR.dll")]
        public static extern int ws_getBaud(int Port,ref int Baud);
        
        //-------------------------------------------------------------------------------------------------
        // 设备操作函数
        //-------------------------------------------------------------------------------------------------
        //点名
        [DllImport("WSR.dll")]
        public static extern int ws_callDevice(int Port);
        [DllImport("WSR.dll")]
        public static extern int ws_callDevice1(int Port, StringBuilder Model, ref int CPUType, ref double Version);
        [DllImport("WSR.dll")]
        public static extern void ws_setNumber(int Port,int Number);
        [DllImport("WSR.dll")]
        public static extern int ws_beep(int Port);



        // 设置设备操作卡型
        [DllImport("WSR.dll")]
        public static extern int ws_set_device_mode(int Port, int Mode);
        // 读取设备操作卡型
        [DllImport("WSR.dll")]
        public static extern int ws_get_device_mode(int Port, ref int Mode);

        // 设置设备上电默认操作卡型
        [DllImport("WSR.dll")]
        public static extern int ws_set_device_default_mode(int Port, int Mode);
        // 读取设备上电默认操作卡型
        [DllImport("WSR.dll")]
        public static extern int ws_get_device_default_mode(int Port, ref int Mode);



        //-------------------------------------------------------------------------------------------------
        // Mifare卡基本操作函数
        //-------------------------------------------------------------------------------------------------
        //取卡号，速度快
        [DllImport("WSR.dll",EntryPoint="ws_getCardNo_DWORD")]
        public static extern int ws_getCardNo(int Port,ref uint CardNo);


        //装载密码
        [DllImport("WSR.dll")]
        public static extern int ws_loadKey(int Port, string Key, int KeyType);

        //读块数据
        [DllImport("WSR.dll")]
        public static extern int ws_readBlock(int Port, int Block, StringBuilder Data);
        [DllImport("WSR.dll")]
        public static extern int ws_readData(int Port, int Block, StringBuilder Data, int Len);
        [DllImport("WSR.dll")]
        public static extern int ws_readSector(int Port, int Block, StringBuilder Data);

        //写块数据
        [DllImport("WSR.dll")]
        public static extern int ws_writeBlock(int Port, int Block, string Data);
        [DllImport("WSR.dll")]
        public static extern int ws_writeData(int Port, int Block, string Data, int Len);
        [DllImport("WSR.dll")]
        public static extern int ws_writeSector(int Port, int Block, string Data);

        //修改密码
        [DllImport("WSR.dll")]
        public static extern int ws_changeKey(int Port, int Sector, string NewKeyA, string NewAC, string NewKeyB);


        //-------------------------------------------------------------------------------------------------
        // 增减值
        //-------------------------------------------------------------------------------------------------
        //初始化值
        [DllImport("WSR.dll")]
        public static extern int ws_initValue(int Port, int Block, int Value);
        //读值
        [DllImport("WSR.dll")]
        public static extern int ws_readValue(int Port, int Block,ref int Value);
        //增值
        [DllImport("WSR.dll")]
        public static extern int ws_incValue(int Port, int Block, int Value);
        //减值
        [DllImport("WSR.dll")]
        public static extern int ws_decValue(int Port, int Block, int Value);


        //=================================================================================================
        // ISO15693 卡基本操作函数
        //=================================================================================================

        // 设置Flag
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_setFlag(int Port, int flag_select, int flag_address, int flag_tagType);
        // 读取Flag
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_getFlag(int Port, ref int flag_select, ref int flag_address, ref int flag_tagType);


        //-----------
        // 检索单张
        [DllImport("WSR.dll")]
		public static extern int ws_iso15693_getTag(int Port, ref byte uid);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_getTagE(int Port, ref byte uid, ref byte dsfid);
        // 检索多张卡
        [DllImport("WSR.dll")]
		public static extern int ws_iso15693_getTags(int Port, ref byte uids);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_getTagsE(int Port, ref byte uids, ref byte dsfids);

        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_getTagInfo(int Port, ref byte uid, ref int flag_memory, ref int block_count, ref int block_size);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_getTagInfoE(int Port, 
                                                         ref int flag_dsfid,ref int flag_afi,
                                                         ref int flag_memory,ref int flag_producer,
                                                         ref byte uid, ref int DSFID, ref int AFI,
                                                         ref int block_count, ref int block_size);
        //----------
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_select(int Port, ref byte uid);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_setUID(int Port, ref byte uid);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_reset(int Port);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_quiet(int Port);


        //---------
        // 读写标签
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_readBlock(int Port, int block, int block_count, ref byte data);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_readBlockE(int Port, int block, int block_count, ref byte data, ref byte status);

        // 读块安全状态
        // （即锁定状态，按顺序每个块对应一个字节，为0，则未锁定；为1，则锁定）
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_readBlock_security(int Port, int block, int block_count, ref byte data);

        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_writeBlock(int Port, int block, int block_count, ref byte data);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_lockBlock(int Port,int block);



        //---------
        // AFI标志
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_writeAFI(int Port,int AFI);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_lockAFI(int Port);

        //---------
        // DSFID标志
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_writeDSFID(int Port, int DSFID);
        [DllImport("WSR.dll")]
        public static extern int ws_iso15693_lockDSFID(int Port);

	    //-------------------------------------------------------------------------------------------------
        // 液晶显示
        //-------------------------------------------------------------------------------------------------
        [DllImport("WSR.dll")]
        public static extern int ws_clearText(int Port);
        [DllImport("WSR.dll")]
        public static extern int ws_displayText(int Port, int LineNo,int Length,string Text);


        //-------------------------------------------------------------------------------------------------
        // 动态库相关信息函数
        //-------------------------------------------------------------------------------------------------
        [DllImport("WSR.dll")]
        public static extern void ws_getDLLInfo(ref double Version, ref double BuildDate);


        //-------------------------------------------------------------------------------------------------
        // 全局参数设定
        // 版本：V5.01.00之后
        //

        [DllImport("WSR.dll")]
        public static extern int ws_set_mode(int mark_mode,int mode);
        [DllImport("WSR.dll")]
        public static extern int ws_get_mode(int mark_mode,ref int mode);



        //-------------------------------------------------------------------------------------------------
        // 多端口多设备
        // 版本：V5.01.00之后
        //

        // 开启监控
        [DllImport("WSR.dll")]
        public static extern int wm_monitor_begin(int port);
        // 停止监控
        [DllImport("WSR.dll")]
        public static extern int wm_monitor_end(int port);


        // 设置返回卡数据类型
        // datatype:   0 - DWORD    1 - String    2 - Hex    3 - char
        [DllImport("WSR.dll")]
        public static extern int wm_monitor_cardno_datatype(int port, int datatype);

        // 回调函数委托声明
        public delegate void CALLBACK_CARDNO_DWORD(int port, int number, uint cardno_dword);
        public delegate void CALLBACK_CARDNO_STRING(int port, int number, string cardno_string);

        // 设置处理卡回调函数
        [DllImport("WSR.dll")]
        public static extern int wm_monitor_callback_dword(int port, CALLBACK_CARDNO_DWORD cb);
        [DllImport("WSR.dll")]
        public static extern int wm_monitor_callback_string(int port, CALLBACK_CARDNO_STRING cb);



        //------------------------------------------------------------------------------
        // 添加读写设备
        [DllImport("WSR.dll")]
        public static extern int wm_dev_add(int port, int number);
        // 获取设备数
        [DllImport("WSR.dll")]
        public static extern int wm_dev_count();
        // 获取设备信息
        [DllImport("WSR.dll")]
        public static extern int wm_dev_get(int index, ref int port, ref int number,ref int status);
        // 清除设备信息
        [DllImport("WSR.dll")]
        public static extern int wm_dev_clear();


        //------------------------------------------------------------------------------
        // 获取端口数
        [DllImport("WSR.dll")]
        public static extern int wm_port_count();
        // 取端口信息
        [DllImport("WSR.dll")]
        public static extern int wm_port_get(int index, ref int port, ref int status);
        // 设置刷卡返回消息值
        [DllImport("WSR.dll")]
        public static extern int wm_port_setmsg(int port, int msg);
        // 设置消息返回至对象句柄
        [DllImport("WSR.dll")]
        public static extern int wm_port_setowner(int port, int handle);

        //-------------------------------------------------------------------------------------------------
        // 动态库相关信息函数
        //-------------------------------------------------------------------------------------------------
        [DllImport("WSR.dll")]
        public static extern void ws_charToStrHex(ref byte cData, int len, ref byte hData);
        [DllImport("WSR.dll")]
        public static extern void ws_strHexToChar(ref byte hData, int len, ref byte cData);

    }
}
