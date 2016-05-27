using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoManage.Constant {
    public class Methods {
        public static int getCharCout(String line, char ch) {
            int i = 0;
            foreach(char var in line.ToCharArray()) {
                if(var.Equals(ch)) {
                    i++;
                }
            }
            return i;
        }
        /// <summary>
        /// 获取项目WKT
        /// </summary>
        /// <param name="project">项目</param>
        /// <returns>项目WKT</returns>
        public static List<string> getWkts(GeoProject project){
            List<string> wkts=new List<string>();
            foreach(Geometries geometry in project.Geometries) {
                wkts.Add(getWkt(geometry));
            }
            return wkts;
        }
        /// <summary>
        /// 获取WKT
        /// </summary>
        /// <param name="geometries">地块</param>
        /// <returns>地块WKT</returns>
        private static string getWkt(Geometries geometries) {
            string wkt = "";
            return wkt;
        }

        public static double crossProduct(GeoPoint current, GeoPoint pre, GeoPoint next) {
            return (current - pre) * (next - current);
        }
    }
}
