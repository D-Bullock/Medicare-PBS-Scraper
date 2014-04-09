using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBS_WebScraper;
namespace WebScraperGUI {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
			ToDateTimePicker.Value = DateTime.Now;
			FromDateTimePicker.Value = DateTime.Now;
		}


		private void BtnInputFile_Click(object sender, EventArgs e) {
			openFileDialog1.Multiselect = false;
			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				txtIDsFile.Text = openFileDialog1.FileName;
			}
		}

		private void BtnOutputFile_Click(object sender, EventArgs e) {
			if (outputDirectory.ShowDialog() == DialogResult.OK) {
				txtOutputDir.Text = outputDirectory.SelectedPath;
			}
		}


		private void button3_Click(object sender, EventArgs e) {
			if (string.IsNullOrWhiteSpace(txtIDsFile.Text)) {
				MessageBox.Show("Please enter where the IDs are located.");
				return;
			}
			//new Parser(
			//TODO input data into parser
		}
	}
}
