using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.SwitchForms
{
    public class PfPageConfigModel
    {
        public PfPageConfigSystemModel systemModel { set; get; } = new PfPageConfigSystemModel();
        public PfPageAlarmSystemModel alarmModel { set; get; } = new PfPageAlarmSystemModel();
    }
    public class PfPageConfigSystemModel
    {
        //public int checkCount { set; get; }
        // public int inputCount { set; get; }
        public List<PfPageConfigSystemSingleModel> inputs { set; get; } = new List<PfPageConfigSystemSingleModel>();
        public List<PfPageConfigSystemCheckModel> checks { set; get; } = new List<PfPageConfigSystemCheckModel>();
    }
    public class PfPageConfigSystemCheckModel
    {
        public string parName { set; get; }

    }
    public class PfPageConfigSystemSingleModel
    {
        public bool numType { set; get; } = false;//false INT true DOUBLE
        public string parName { set; get; }

    }
    public class PfPageAlarmSystemModel
    {
        //public int rowCount { set; get; }
        public List<PfPageConfigAlarmSingleModel> alarms { set; get; } = new List<PfPageConfigAlarmSingleModel>();
    }

    public class PfPageConfigAlarmSingleModel
    {

        public List<bool> numUse { set; get; } = new List<bool> { true, true, true };
        public string parName { set; get; }
    }
}
