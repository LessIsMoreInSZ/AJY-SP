using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms
{
    public enum eUserRole
    {
        技术员 = 1,
        工艺修改 = 2,
        设备管理员 = 3
    }
    public enum eSpecialUserRole
    {

        厂家 = 4
    }
    public enum ePfLog
    {
        新建 = 1,
        删除 = 2,
        修改 = 3,
        重命名
    }

    public enum NowUi
    {
        主页 = 0,
        配方,
        通道概览,
        登录,
        配方Log,
        报警log,
        手动测试,
        图表,
        自动,
        真空源图片,
        真空源设置,
        数据下载,
        设备管理,
        系统设定,
        液压站,
        设备维护,
        泵组维护,
        IO,
        设备保养,
        压射杆,
        传感器校准,
        主机信号模拟
        
    }
    public enum SetTongdaoButtonOrder
    {
        联动抽真空 = 0,
        联动吹扫 = 1,
        单动抽真空 = 2,
        单动抽吹扫 = 3,
        单动抽顶出 = 4,
        单动抽退回 = 5
    }
    public enum SetManualButtonOrder
    {
        管道测试 = 0,
        模腔测试 = 1,
        测试开始 = 2,
        测试停止 = 3,
        测试时间 = 4,
        保压时间 = 5
    }
    public enum SetAutoButtonOrder
    {
        自动启动 = 0,
        退出循环 = 1,
        热模真空 = 2
    }
    public enum SetVacButtonOrder
    {
        自动启动 = 0,
        自动停止 = 1
    }
    public enum Set6001Mode
    {
        Mode1,
        Mode2,
        Mode3
    }
    public enum VacSetShort
    {
        VAC_D,
        VAC_C,
        VAC_E,
        PUMP_C,
        PUMP_E
    }
}
