using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoManage.Constant {
    /// <summary>
    /// 属性描述
    /// </summary>
    public class GeoInfo {

        private String version;
        /// <summary>
        /// 格式版本号
        /// </summary>
        public String Version {
            get {
                return version;
            }
            set {
                version = value;
            }
        }

        private String dataProducer;
        /// <summary>
        /// 数据产生单位 
        /// </summary>
        public String DataProducer {
            get {
                return dataProducer;
            }
            set {
                dataProducer = value;
            }
        }

        private String dataDate;
        /// <summary>
        /// 数据产生日期
        /// </summary>
        public String DataDate {
            get {
                return dataDate;
            }
            set {
                dataDate = value;
            }
        }

        private String coordinateSystem;
        /// <summary>
        /// 坐标系
        /// </summary>
        public String CoordinateSystem {
            get {
                return coordinateSystem;
            }
            set {
                coordinateSystem = value;
            }
        }

        private String severalTimesZonation;
        /// <summary>
        /// 几度分带
        /// </summary>
        public String SeveralTimesZonation {
            get {
                return severalTimesZonation;
            }
            set {
                severalTimesZonation = value;
            }
        }

        private String projectionType;
        /// <summary>
        /// 投影类型
        /// </summary>
        public String ProjectionType {
            get {
                return projectionType;
            }
            set {
                projectionType = value;
            }
        }

        private String unit;
        /// <summary>
        /// 计量单位
        /// </summary>
        public String Unit {
            get {
                return unit;
            }
            set {
                unit = value;
            }
        }
   
        private String no;
        /// <summary>
        /// 带号
        /// </summary>
        public String No {
            get {
                return no;
            }
            set {
                no = value;
            }
        }
        private String accuracy;
        /// <summary>
        /// 精度
        /// </summary>
        public String Accuracy {
            get {
                return accuracy;
            }
            set {
                accuracy = value;
            }
        }

        private String conversionParameters;
        /// <summary>
        /// 转换参数
        /// </summary>
        public String ConversionParameters {
            get {
                return conversionParameters;
            }
            set {
                conversionParameters = value;
            }
        }
    }

    /// <summary>
    /// 项目
    /// </summary>
    public class GeoProject {
        /// <summary>
        /// 初始化
        /// </summary>
        public GeoProject() {
            geoinfo = new GeoInfo();
            name = "";
            geometries = new List<Geometries>();
        }



        private GeoInfo geoinfo;
        /// <summary>
        /// 图层属性描述
        /// </summary>
        public GeoInfo Geoinfo {
            get {
                return geoinfo;
            }
            set {
                geoinfo = value;
            }
        }

        String name;
        /// <summary>
        /// 项目名称
        /// </summary>
        public String Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        List<Geometries> geometries;
        /// <summary>
        /// 地块信息
        /// </summary>
        public List<Geometries> Geometries {
            get {
                return geometries;
            }
            set {
                geometries = value;
            }
        }
    }
    /// <summary>
    /// 地块
    /// </summary>
    public class Geometries {
        public Geometries() {

            area = null;
            name = "";
            polygons = new List<GeoPolygon>();
        }


        double? area;
        /// <summary>
        /// 面积
        /// </summary>
        public double? Area {
            get {
                return area;
            }
            set {
                area = value;
            }
        }
        String name;
        /// <summary>
        /// 地块名称
        /// </summary>
        public String Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        List<GeoPolygon> polygons;
        /// <summary>
        /// 多边形
        /// </summary>
        public List<GeoPolygon> Polygons {
            get {
                return polygons;
            }
            set {
                polygons = value;
            }
        }

    }
    /// <summary>
    /// 面
    /// </summary>
    public class GeoPolygon {



        public GeoPolygon(String wkt) {
        }

        public GeoPolygon() {
            circle = 0;
            points = new List<GeoPoint>();
        }

        int circle;
        /// <summary>
        /// 圈号
        /// </summary>
        public int Circle {
            get {
                return circle;
            }
            set {
                circle = value;
            }
        }

        List<GeoPoint> points;
        /// <summary>
        /// 坐标点
        /// </summary>
        public List<GeoPoint> Points {
            get {
                return points;
            }
            set {
                points = value;
            }
        }

    }

    /// <summary>
    /// 点
    /// </summary>
    public class GeoPoint {
        double x;
        /// <summary>
        /// X坐标
        /// </summary>
        public double X {
            get {
                return x;
            }
            set {
                x = value;
            }
        }

        double y;
        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y {
            get {
                return y;
            }
            set {
                y = value;
            }
        }

    }
}
