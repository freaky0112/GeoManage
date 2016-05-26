using GeoManage.Constant;
using GeoManage.GeoEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeoManage {
    public partial class Form1 : Form {

        GeoProject project;
        FileInfo file;
        public Form1() {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c://";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "txt文件|*.txt|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                //File file=openFileDialog.

                file = new FileInfo(openFileDialog.FileName);
                //String file = openFileDialog.FileName;
                readFile(file.FullName);

            }
        }

        private void readFile(String file) {
            FileReadHelper.INIHelper ini = new FileReadHelper.INIHelper(file);
            FileReadHelper.GeoRead fr = new FileReadHelper.GeoRead(file);
            project = fr.FileRead();
            MessageBox.Show(project.Name);

        }

        private void btnGenerate_Click(object sender, EventArgs e) {
            GeoWrite geo = new GeoWrite(project);

            geo.shpWrite(file.FullName.Replace("txt", "shp"));
        }

        private void btnExport_Click(object sender, EventArgs e) {
            GeoRead geo = new GeoRead(@"C:\Users\Freaky\Desktop\a.shp");
            project = geo.shpRead();
        }
    }
}
