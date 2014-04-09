namespace WebScraperGUI {
	partial class Main {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.txtOutputDir = new System.Windows.Forms.TextBox();
			this.BtnOutputFile = new System.Windows.Forms.Button();
			this.outputDirectory = new System.Windows.Forms.FolderBrowserDialog();
			this.FromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ToDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.BtnInputFile = new System.Windows.Forms.Button();
			this.txtIDsFile = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.numDelay = new System.Windows.Forms.NumericUpDown();
			this.button3 = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(49, 81);
			this.label1.Margin = new System.Windows.Forms.Padding(40, 0, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Ids File";
			// 
			// txtOutputDir
			// 
			this.txtOutputDir.Location = new System.Drawing.Point(94, 224);
			this.txtOutputDir.Name = "txtOutputDir";
			this.txtOutputDir.Size = new System.Drawing.Size(508, 20);
			this.txtOutputDir.TabIndex = 1;
			// 
			// BtnOutputFile
			// 
			this.BtnOutputFile.Location = new System.Drawing.Point(611, 222);
			this.BtnOutputFile.Name = "BtnOutputFile";
			this.BtnOutputFile.Size = new System.Drawing.Size(75, 23);
			this.BtnOutputFile.TabIndex = 2;
			this.BtnOutputFile.Text = "Browse";
			this.BtnOutputFile.UseVisualStyleBackColor = true;
			this.BtnOutputFile.Click += new System.EventHandler(this.BtnOutputFile_Click);
			// 
			// FromDateTimePicker
			// 
			this.FromDateTimePicker.CustomFormat = "MM / yyyy";
			this.FromDateTimePicker.Location = new System.Drawing.Point(97, 104);
			this.FromDateTimePicker.Name = "FromDateTimePicker";
			this.FromDateTimePicker.Size = new System.Drawing.Size(200, 20);
			this.FromDateTimePicker.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(49, 110);
			this.label2.Margin = new System.Windows.Forms.Padding(40, 0, 3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "From";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(369, 110);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "To";
			// 
			// ToDateTimePicker
			// 
			this.ToDateTimePicker.CustomFormat = "MM / yyyy";
			this.ToDateTimePicker.Location = new System.Drawing.Point(405, 104);
			this.ToDateTimePicker.Name = "ToDateTimePicker";
			this.ToDateTimePicker.Size = new System.Drawing.Size(200, 20);
			this.ToDateTimePicker.TabIndex = 5;
			// 
			// BtnInputFile
			// 
			this.BtnInputFile.Location = new System.Drawing.Point(611, 76);
			this.BtnInputFile.Name = "BtnInputFile";
			this.BtnInputFile.Size = new System.Drawing.Size(75, 23);
			this.BtnInputFile.TabIndex = 7;
			this.BtnInputFile.Text = "Browse";
			this.BtnInputFile.UseVisualStyleBackColor = true;
			this.BtnInputFile.Click += new System.EventHandler(this.BtnInputFile_Click);
			// 
			// txtIDsFile
			// 
			this.txtIDsFile.Location = new System.Drawing.Point(97, 78);
			this.txtIDsFile.Name = "txtIDsFile";
			this.txtIDsFile.Size = new System.Drawing.Size(508, 20);
			this.txtIDsFile.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(49, 227);
			this.label4.Margin = new System.Windows.Forms.Padding(40, 0, 3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Output";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(49, 253);
			this.label5.Margin = new System.Windows.Forms.Padding(40, 0, 3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Delay  ";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(12, 177);
			this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(719, 45);
			this.label6.TabIndex = 12;
			this.label6.Text = "Optional fields";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(12, 31);
			this.label7.Margin = new System.Windows.Forms.Padding(3, 0, 3, 20);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(719, 24);
			this.label7.TabIndex = 13;
			this.label7.Text = "Required Fields";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(156, 253);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 13);
			this.label8.TabIndex = 14;
			this.label8.Text = "seconds";
			// 
			// numDelay
			// 
			this.numDelay.Location = new System.Drawing.Point(94, 251);
			this.numDelay.Name = "numDelay";
			this.numDelay.Size = new System.Drawing.Size(56, 20);
			this.numDelay.TabIndex = 15;
			this.numDelay.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
			this.button3.Location = new System.Drawing.Point(304, 292);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(135, 51);
			this.button3.TabIndex = 16;
			this.button3.Text = "Go!";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Enabled = false;
			this.progressBar1.Location = new System.Drawing.Point(52, 320);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(634, 23);
			this.progressBar1.TabIndex = 17;
			this.progressBar1.UseWaitCursor = true;
			this.progressBar1.Visible = false;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(743, 355);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.numDelay);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtIDsFile);
			this.Controls.Add(this.BtnInputFile);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ToDateTimePicker);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.FromDateTimePicker);
			this.Controls.Add(this.BtnOutputFile);
			this.Controls.Add(this.txtOutputDir);
			this.Controls.Add(this.label1);
			this.Name = "Main";
			this.Text = "Web Scraper";
			((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtOutputDir;
		private System.Windows.Forms.Button BtnOutputFile;
		private System.Windows.Forms.FolderBrowserDialog outputDirectory;
		private System.Windows.Forms.DateTimePicker FromDateTimePicker;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker ToDateTimePicker;
		private System.Windows.Forms.Button BtnInputFile;
		private System.Windows.Forms.TextBox txtIDsFile;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numDelay;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
	}
}

