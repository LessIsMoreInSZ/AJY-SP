using SPWindowsForms.BasicTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms
{
    public class CommonIniFileModel
    {
        //public static string iniSystemName = Environment.CurrentDirectory.ToString() + "\\system.ini";
        public INIFile iniSystem { set; get; }/* = new INIFile(iniSystemName)*/
        public INIFile iniLanguage { set; get; }
    }
}
