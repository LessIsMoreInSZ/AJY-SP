using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace SPWindowsForms.BasicTool
{
    public class INIFile
    {
        public string Filepath;

        public Dictionary<string, Dictionary<string, string>> allData;
        public INIFile(string INIPath)
        {
            Filepath = INIPath;
        }
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        /// <summary>直接获得字符串类型</summary>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>获取字符串类型对应的字节</summary>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);




        /// <summary>获取节点下所有键</summary>
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSection")]
        private static extern uint GetPrivateProfileSection(string lpAppName, sbyte[] lpReturnedString, int nSize, string lpFileName);

        /// <summary>获取所有节点名称</summary>
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNames")]
        private static extern uint GetPrivateProfileSectionNames(sbyte[] lpszReturnBuffer, uint nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section,
 string key, string val, string filePath);
        /// <summary>
        /// [扩展]写入String字符串，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="strVal">写入值</param>
        public void WriteString(string section, string name, string strVal)
        {
            WritePrivateProfileString(section, name, strVal, this.Filepath);
        }


        /// <summary>
        /// 读Int数值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public int ReadInt(string section, string name, int def)
        {
            return GetPrivateProfileInt(section, name, def, this.Filepath);
        }

        /// <summary>
        /// 读取string字符串
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public string ReadString(string section, string name, string def)
        {
            StringBuilder vRetSb = new StringBuilder(2048);
            GetPrivateProfileString(section, name, def, vRetSb, 2048, this.Filepath);
            return vRetSb.ToString();
        }

        /// <summary>
        /// 读取double
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public double ReadDouble(string section, string name, double def)
        {
            StringBuilder vRetSb = new StringBuilder(2048);
            GetPrivateProfileString(section, name, "", vRetSb, 2048, this.Filepath);
            if (vRetSb.Length < 1)
            {
                return def;
            }
            return Convert.ToDouble(vRetSb.ToString());
        }
        /// <summary>
        /// 删除指定字段
        /// </summary>
        /// <param name="sectionName">段落</param>
        /// <param name="keyName">键</param>
        public void iniDelete(string sectionName, string keyName)
        {
            WritePrivateProfileString(sectionName, keyName, null, this.Filepath);
        }
        /// <summary>
        /// 删除字段重载
        /// </summary>
        /// <param name="sectionName">段落</param>
        public void iniDelete(string sectionName)
        {
            WritePrivateProfileString(sectionName, null, null, this.Filepath);
        }
        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public void ClearAllSection()
        {
            WriteString(null, null, null);
        }
        /// <summary>
        /// 删除ini文件下personal段落下的所有键
        /// </summary>
        /// <param name="Section"></param>
        public void ClearSection(string Section)
        {
            WriteString(Section, null, null);
        }
        /// <summary>
        /// 读取所有段落名，写死读4096个大小字节，如果段落加起来字符过长会读不完整
        /// </summary>
        /// <returns>返回字符串数组，所有Section</returns>
        public string[] GetAllSections()
        {
            byte[] allSectionByte = new byte[4096];
            int length = GetPrivateProfileString(null, null, "", allSectionByte, 1024, this.Filepath);
            //int bufLen = GetPrivateProfileString(section, key, Default, Buffer, Buffer.GetUpperBound(0), INI_Path);
            //返回的是所有段落名称拼接起来，以“\0”为分割符
            string allSectionStr = Encoding.GetEncoding(Encoding.ASCII.CodePage).GetString(allSectionByte, 0, length);
            //用'|'代替‘\0’作为分隔符
            string allSectionStrEx = allSectionStr.Replace('\0', '|');
            //去除追后一个分割符"|"，不然会有一个空数据
            allSectionStrEx = allSectionStrEx.Substring(0, length - 1);
            //以'|'为分隔符分割allSectionStrEx到字符串数组
            string[] allSections = new string[length];
            char[] separator = { '|' };
            allSections = allSectionStrEx.Split(separator);
            return allSections;
        }
        /// <summary>
        /// 获取段落下的所有键，写死读4096个大小字节，如果段落加起来字符过长会读不完整
        /// </summary>
        /// <param name="section">段落名</param>
        /// <returns>返回字符串数组，所有Key</returns>
        public string[] GetAllKeys(string section)
        {
            byte[] allSectionByte = new byte[4096];
            int length = GetPrivateProfileString(section, null, "", allSectionByte, 4096, this.Filepath);
            string allSectionStr = Encoding.GetEncoding(Encoding.ASCII.CodePage).GetString(allSectionByte, 0, length);
            //用'|'代替‘\0’作为分隔符
            string allSectionStrEx = allSectionStr.Replace('\0', '|');
            //去除追后一个分割符"|"，不然会有一个空数据
            allSectionStrEx = allSectionStrEx.Substring(0, length - 1);
            //以'|'为分隔符分割allSectionStrEx到字符串数组
            string[] allSections = new string[length];
            char[] separator = { '|' };
            allSections = allSectionStrEx.Split(separator);
            return allSections;
        }

        /// <summary>
        /// 遍历INI文件,返回二维字典所有内容
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> TraverseIni()
        {
            //二维字典
            Dictionary<string, Dictionary<string, string>> ConfPlugInfo = new Dictionary<string, Dictionary<string, string>>();
            //读取配置算子文件中所有算子段落
            string[] sections = GetAllSections();
            int sectionsLength = sections.Length;
            //遍历所有段落
            for (int i = 0; i < sectionsLength; i++)
            {
                string section = sections[i];
                //获取该段落的所有键
                string[] keys = GetAllKeys(section);
                int keysLength = keys.Length;
                //一维存键值对
                Dictionary<string, string> keyValue = new Dictionary<string, string>();
                //遍历所有的键，提取其值
                for (int j = 0; j < keysLength; j++)
                {
                    string key = keys[j];
                    string value = ReadString(section, key, "");
                    keyValue.Add(key, value);
                }
                ConfPlugInfo.Add(section, keyValue);
            }
            return ConfPlugInfo;
        }


    }
}
