using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GeoManage.Constant {
    public partial class FileReadHelper {
        /// <summary>
        /// 配置文件读取
        /// </summary>
        public class INIHelper {
            public String inipath;
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(String section, String key, String val, String filePath);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, String filePath);
            /// <summary>
            /// 构造方法
            /// </summary>
            /// <param name="INIPath">文件路径</param>
            public INIHelper(String INIPath) {
                inipath = INIPath;
            }
            /// <summary>
            /// 写入INI文件
            /// </summary>
            /// <param name="Section">项目名称(如 [TypeName] )</param>
            /// <param name="Key">键</param>
            /// <param name="Value">值</param>
            public void IniWriteValue(String Section, String Key, String Value) {
                WritePrivateProfileString(Section, Key, Value, this.inipath);
            }
            /// <summary>
            /// 读出INI文件
            /// </summary>
            /// <param name="Section">项目名称(如 [TypeName] )</param>
            /// <param name="Key">键</param>
            public String IniReadValue(String Section, String Key) {
                StringBuilder temp = new StringBuilder(500);
                int i = GetPrivateProfileString(Section, Key, "无法读取对应数值", temp, 500, this.inipath);
                return temp.ToString();
            }
            /// <summary>
            /// 验证文件是否存在
            /// </summary>
            /// <returns>布尔值</returns>
            public bool ExistINIFile() {
                return File.Exists(inipath);
            }
        }
    }
}
