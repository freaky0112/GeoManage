﻿using DotSpatial.Data;
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

        public void shpWrite(string path) {
            fs.DataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            DataColumn col = new DataColumn("Project", typeof(string));
            col.MaxLength = 50;
            fs.DataTable.Columns.Add(col);
            fs.DataTable.Columns.Add(new DataColumn("Area", typeof(double)));
            IFeatureSet fsource = FeatureSet.Open(@"Sample\Sample.shp");
            fs.Projection = fsource.Projection;
            fsource.Close();
            int ID = 0;
            foreach (Geometries geometry in project.Geometries) {
                ID++;
                Polygon[] pgs = new Polygon[geometry.Polygons.Count];
                int i = 0;
                //foreach (Geometries geometry in project.Geometries) {

                foreach (GeoPolygon polygon in geometry.Polygons) {
                    List<Coordinate> vertices = new List<Coordinate>();

                    //polygon.Points.Reverse();

                    if (polygon.Circle>1&&!polygon.GetDirection()) {
                        polygon.Points.Reverse();
                    } else if (polygon.Circle==1&&polygon.GetDirection()) {
                        polygon.Points.Reverse();
                    }
                    
                    foreach (GeoPoint point in polygon.Points) {
                        Coordinate vertice = new Coordinate();
                        vertice.X = point.X;
                        vertice.Y = point.Y;
                        vertices.Add(vertice);
                    }

                    Polygon geom = new Polygon(vertices);
                    
                    pgs[i] = geom;

                    i++;
                }
                //}


                MultiPolygon geoms = new MultiPolygon(pgs);
                geoms.ToText();
                IFeature feature = fs.AddFeature(geoms);
                feature.DataRow.BeginEdit();
                feature.DataRow["ID"] = ID;
                feature.DataRow["Project"] = project.Name;
                //feature.DataRow["Area"] = feature.Area();
                feature.DataRow.EndEdit();
            }
            //fs.Projection = ProjectionInfo.(@"F:\数据\2013SHP\DLTB.shp");
            //fs.ProjectionString = " +x_0=40500000 +y_0=0 +lat_0=0 +lon_0=120 +proj=tmerc +a=6378140 +b=6356755.28815753 +no_defs";
         
            fs.SaveAs(path, true);
            
            fs.Dispose();
            GeoRead gr = new GeoRead(path);
            gr.shpAreaReCalculate();
            //fs = FeatureSet.Open(path);

        }
    }

    public class GeoRead {
        //string shppath;
        IFeatureSet fs;
        GeoProject project;

        public GeoRead(string path) {
            this.fs = FeatureSet.Open(path);
        }

        public void shpAreaReCalculate() {
            fs.DataTable.Columns.Add(new DataColumn("ShpArea", typeof(double)));
            foreach (IFeature feature in fs.Features) {
                feature.DataRow.BeginEdit();
                feature.DataRow["ShpArea"] = feature.Area();
                feature.DataRow.EndEdit();
            }
            fs.Save();
            fs.Dispose();
        }

        public GeoProject shpRead() {
            //IFeatureSet fs = FeatureSet.Open(shppath);
            string str = fs.ProjectionString;
            ProjectionInfo info = fs.Projection;
            project = new GeoProject();
            for (int i = 0; i < fs.Features.Count; i++) {
                Geometries geometries = new Geometries();
                IList<Coordinate> vertics = fs.Features[i].Coordinates;
                GeoPolygon polygon = new GeoPolygon();
                int circle = 1;
                foreach (Coordinate vertic in vertics) {
                    GeoPoint point = new GeoPoint();
                    point.X = vertic.X;
                    point.Y = vertic.Y;
                    if (polygon.Points.Contains(point)) {
                        polygon.Circle = circle;
                        geometries.Polygons.Add(polygon);
                        circle++;
                        polygon = new GeoPolygon();
                    }
                    polygon.Points.Add(point);
                }
                polygon.Circle = circle;
                geometries.Polygons.Add(polygon);
                project.Geometries.Add(geometries);
            }


            return project;
        }


    }


}
