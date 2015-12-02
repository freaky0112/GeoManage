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
        public Form1() {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c://";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "txt文件|*.txt|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                String file = openFileDialog.FileName;
                readFile(file);
                
            }
        }

        private void readFile(String file){
            FileReadHelper.INIHelper ini = new FileReadHelper.INIHelper(file);
            FileReadHelper.GeoRead fr=new FileReadHelper.GeoRead(file);
            project = fr.FileRead();
            MessageBox.Show(project.Name);

        }

        private void btnGenerate_Click(object sender, EventArgs e) {
            GeoWrite geo = new GeoWrite(project);
            geo.shpWrite();
        }
    }
}
