using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeoManage.Constant {
    public partial class FileReadHelper {
        public class GeoRead {
            /// <summary>
            /// 批次信息
            /// </summary>
            GeoProject project = new GeoProject();
            /// <summary>
            /// 地块信息
            /// </summary>
            Geometries geometries = new Geometries();

            GeoPolygon polygon = new GeoPolygon();
            /// <summary>
            /// 文件名
            /// </summary>
            String path;

            public GeoRead(String path) {
                this.path = path;
            }
            /// <summary>
            /// 获取wkt文本
            /// </summary>
            /// <returns>wkt</returns>
            public String GeoWKT() {
                String wkt = "";
                return wkt;
            }
            /// <summary>
            /// 文本读取
            /// </summary>
            public GeoProject FileRead() {
                StreamReader sr = new StreamReader(path,Encoding.Default);
                FileInfo file = new FileInfo(path);
                project.Name = file.Name.Split('.')[0];
                int section = 0;
                String line;
                while((line = sr.ReadLine()) != null) {
                    if(line == "[属性描述]") {
                        section = 0;
                    } else if(line == "[地块坐标]") {
                        section = 1;
                    }
                    switch(section) {
                        case 0://读取项目信息
                            GeoInfoRead(line);
                            break;
                        case 1://读取地块信息
                            GeometriesRead(line);
                            break;
                        default:
                            break;
                    }
                }
                geometries.Polygons.Add(polygon);
                project.Geometries.Add(geometries);
                return project;
            }
            /// <summary>
            /// 图层属性读取
            /// </summary>
            /// <param name="line"></param>
            private void GeoInfoRead(String line) {
                if(line.Contains('=')) {
                    String key = line.Split('=')[0];
                    String value = line.Split('=')[1];
                    switch(key) {
                        case "格式版本号":
                            project.Geoinfo.Version = value;
                            break;
                        case "数据产生单位":
                            project.Geoinfo.DataProducer = value;
                            break;
                        case "数据产生日期":
                            project.Geoinfo.DataDate = value;
                            break;
                        case "坐标系":
                            project.Geoinfo.CoordinateSystem = value;
                            break;
                        case "几度分带":
                            project.Geoinfo.SeveralTimesZonation = value;
                            break;
                        case "投影类型":
                            project.Geoinfo.ProjectionType = value;
                            break;
                        case "计量单位":
                            project.Geoinfo.Unit = value;
                            break;
                        case "带号":
                            project.Geoinfo.No = value;
                            break;
                        case "精度":
                            project.Geoinfo.Accuracy = value;
                            break;
                        case "转换参数":
                            project.Geoinfo.ConversionParameters = value;
                            break;
                    }
                }
            }
            /// <summary>
            /// 读取地块
            /// </summary>
            /// <param name="line"></param>
            private void GeometriesRead(String line) {
                int count = Methods.getCharCout(line, ',');
                //判断是否为地块信息行
                if(count == 8) {
                    if(geometries.Polygons.Count != 0) {//地块信息是否为空
                        geometries.Polygons.Add(polygon);
                        project.Geometries.Add(geometries);//不为空则将该地块添加入批次中
                    }
                    //初始化地块
                    geometries = new Geometries();
                    //初始化多边形
                    polygon = new GeoPolygon();
                    String[] geo = line.Split(',');
                    geometries.Area = Double.Parse(geo[1]);//地块面积
                    geometries.Name = geo[3].ToString();//地块名称

                }
                //判断是否为坐标点行
                else if(count == 3) {
                    GeoPoint point = new GeoPoint();
                    String[] geo = line.Split(',');
                    point.X = Double.Parse(geo[3]);
                    point.Y = Double.Parse(geo[2]);
                    int circle = Int32.Parse(geo[1]);
                    if(polygon.Circle != circle) {//判断当前圈号是否一致
                        if(polygon.Points.Count != 0) {//多边形信息是否为空
                            geometries.Polygons.Add(polygon);//不为空则将该地块加入地块中
                        }
                        polygon = new GeoPolygon(); //不一致则初始化多边形
                        polygon.Circle = circle;    //多边形圈号赋值
                        polygon.Points.Add(point);
                    } else {
                        polygon.Points.Add(point);
                    }
                }

            }
        }
    }
}
