using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.Util
{
    public class AlarmCodeStatus
    {
        public List<string> OkCodes { set; get; } = new List<string>();
        public List<string> NgCodes { set; get; } = new List<string>();
    }
    public class ManualTestResultModel
    {
        public DateTime dateTime { set; get; }
        public List<ManualTestResultChnModel> results { set; get; } = new List<ManualTestResultChnModel>();
    }
    public class ManualTestResultChnModel
    {
        public int chn { set; get; }
        public float Fr_P { set; get; }               // 检测初值*)
        public float Se_P { set; get; }                // 检测终值*)
        public float close_time { set; get; }                // 反应时间*)
        public float deltaP { set; get; }            // 泄漏量*)
        public float sulv_P { set; get; }                // 泄露速度*)
        public DateTime dateTime { set; get; }
    }
}
