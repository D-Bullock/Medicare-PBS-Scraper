using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PBS_WebScraper {

	// A delegate type for hooking up change notifications.
	public delegate void CompetionHandler(object sender, decimal percentComplete);

	/// <summary>
	/// The interface of the 
	/// </summary>
	public partial class Parser {
		private bool HasErrored = false;
		private static bool Debug = false;

		public event CompetionHandler Changed;
		public void Parse(string outputDir, string idFile, DateTime currentMonth, DateTime endMonth, int delayPeriod) {
			IEnumerable<string> ids;
			ids = string.Join(",", File.ReadAllLines(idFile)).Split(',');
			var totalIdCount = ids.Count();

			ClearFiles(outputDir, currentMonth, endMonth);

			// This will generate delays
			var random = new Random();


			while (currentMonth <= endMonth) { // Go through each month in range
				while (ids.Count() != 0) { // Go through every ID specified
					Thread.Sleep(random.Next(1000 * delayPeriod / 2, 1000 * delayPeriod * 2));
					var currentIds = ids.Take(20).ToArray();
					try {
						Output1(outputDir, currentIds, currentMonth);
						Output2(outputDir, currentIds, currentMonth);
						Output3(outputDir, currentIds, currentMonth);
						Output4(outputDir, currentIds, currentMonth);
					} catch (NullReferenceException) {
						ErroredIds(currentIds, currentMonth, outputDir);
					}
					ids = ids.Skip(20);
					OnChanged(totalIdCount, ids.Count());
				}
				currentMonth = currentMonth.AddMonths(1);
			}
		}


		/// <summary>
		/// A wrapper for OnChanged(PercentComplete)
		/// </summary>
		/// <param name="total"></param>
		/// <param name="done"></param>
		protected virtual void OnChanged(int total, int done) {
			OnChanged(1 - (decimal)done / total);
		}

		/// <summary>
		/// Triggers the onchange event. The percent complete is in decimal (ie 15% = 0.15)
		/// </summary>
		/// <param name="percentCompelete"></param>
		protected virtual void OnChanged(decimal percentCompelete) {
			if (Changed != null) {
				Changed(this, percentCompelete);
			}
		}

		private void ErroredIds(IEnumerable<string> ids, DateTime month, string outputDir) {
			var errorFile = outputDir + "\\Errors.txt";
			if (!HasErrored) {
				File.WriteAllText(errorFile, "One or more of the following Ids have generated an error. To determine which, try loading the IDs into the web environment and it should correct you" + Environment.NewLine);
			}
			HasErrored = true;
			File.AppendAllText(errorFile, "Occured whilst parsing " + month.ToString("MMM yyyy") + Environment.NewLine);
			File.AppendAllText(errorFile, string.Join(",", ids) + Environment.NewLine);
		}


		private void ClearFiles(string outputDir, DateTime startMonth, DateTime endMonth) {
			// Delete the error file
			try {
				File.Delete(outputDir + "\\Errors.txt");
			} catch { }

			// Delete the other files in range
			var fileNames = new string[] { "PatCat_Benefits", "PatCat_Services", "State_Benefits", "State_Services" };
			while (startMonth <= endMonth) {
				foreach (var fileName in fileNames) {
					try {
						var deleteFile = outputDir + "\\" + fileName + "_" + startMonth.ToString("MMMyyyy") + ".csv";
						File.Delete(deleteFile);
					} catch { }
				}
				startMonth = startMonth.AddMonths(1);
			}
		}
	}
}
