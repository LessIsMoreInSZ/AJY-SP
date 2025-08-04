using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace SPWindowsForms
{
    public static class CommonVar
    {
        //public static TcAdsClient adsClient = new AdsClient();
        public static void IniAllVars()
        {
            try
            {

            }
            catch(Exception ex)
            {
                
            }
        }
    }
    public class UserSelectItem
    {
        public int id { set; get; }
        public string name { set; get; }
    }
}
