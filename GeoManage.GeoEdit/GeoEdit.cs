using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Topology;
using GeoManage.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GeoManage.GeoEdit {
    public class GeoWrite {
        FeatureSet fs = new FeatureSet(FeatureType.Polygon);
        GeoProject project;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="project"></param>
        public GeoWrite(GeoProject project) {
            this.project = project;
        }

        public void shpWrite() {
            fs.DataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            fs.DataTable.Columns.Add(new DataColumn("Project", typeof(string)));


            Polygon[] pgs = new Polygon[project.Geometries[0].Polygons.Count];
            foreach(Geometries geometry in project.Geometries) {
                foreach(GeoPolygon polygon in geometry.Polygons) {
                    List<Coordinate> vertices = new List<Coordinate>();
                    foreach(GeoPoint point in polygon.Points) {
                        Coordinate vertice = new Coordinate();
                        vertice.X = point.X;
                        vertice.Y = point.Y;
                        vertices.Add(vertice);
                    }
                    Polygon geom = new Polygon(vertices);
                    pgs[polygon.Circle - 1] = geom;
                }
            }


            MultiPolygon geoms = new MultiPolygon(pgs);
            geoms.ToText();
            IFeature feature = fs.AddFeature(geoms);

            feature.DataRow.BeginEdit();
            feature.DataRow["ID"] = 1;
            feature.DataRow["Project"] = project.Name;
            feature.DataRow.EndEdit();
            //fs.Projection = ProjectionInfo.(@"F:\数据\2013SHP\DLTB.shp");
            //fs.ProjectionString = " +x_0=40500000 +y_0=0 +lat_0=0 +lon_0=120 +proj=tmerc +a=6378140 +b=6356755.28815753 +no_defs";
            IFeatureSet fsource = FeatureSet.Open(@"F:\数据\2013SHP\DLTB.shp");
            fs.Projection = fsource.Projection;
            fsource.Close();
            fs.SaveAs("G:\\TEST\\a.shp", true);
            ;
        }
    }

    public class GeoRead {
        string shppath;



        public GeoRead(string path) {
            this.shppath = path;
        }

        public void shpRead() {
            IFeatureSet fs = FeatureSet.Open(shppath);
            string str = fs.ProjectionString;
            ProjectionInfo info= fs.Projection;
            str.ToCharArray();
            int i;
            i = 1;
        }
    }
}
