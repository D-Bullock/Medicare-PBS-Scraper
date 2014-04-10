using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBS_WebScraper;
namespace WebScraperGUI {
	public partial class Main : Form {
		public Main() {
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
			// Work out directory
			var outputDir = string.IsNullOrWhiteSpace(txtOutputDir.Text) ?
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) :
				txtOutputDir.Text;
			button3.Visible = false;
			progressBar1.Visible = true;
			progressBar1.Enabled = true;

			var parser = new Parser();
			parser.Changed += new CompetionHandler(delegate(object parser2, decimal complete) {
				backgroundWorker.ReportProgress((int)(complete * 100));
			});

			backgroundWorker.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args) {
				try {
					parser.Parse(outputDir, txtIDsFile.Text, FromDateTimePicker.Value, ToDateTimePicker.Value, (int)numDelay.Value);
				} catch (Exception ex) {
					MessageBox.Show(ex.Message, "Error:" + ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			});

			backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(
				delegate(object o, ProgressChangedEventArgs args) {
					progressBar1.Value = args.ProgressPercentage;
				}
			);

			backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
				delegate(object o, RunWorkerCompletedEventArgs args) {
					progressBar1.Visible = false;
					button3.Visible = true;
					MessageBox.Show("Finished scraping data");
				}
			);

			backgroundWorker.RunWorkerAsync();
		}

	}
}
