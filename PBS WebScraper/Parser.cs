using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PBS_WebScraper {

	/// <summary>
	/// The interface of the 
	/// </summary>
	partial class Parser {
		private bool HasErrored = false;
		public void Parse(string outputDir, string idFile, DateTime startMonth, DateTime endMonth, int delayPeriod) {
			var ids = ReadIdsFromFile(idFile);

			ClearFiles(outputDir, startMonth, endMonth);

			// This will generate delays
			var random = new Random();


			while (startMonth <= endMonth) { // Go through each month in range
				while (ids.Count() != 0) { // Go through every ID specified
					Thread.Sleep(TimeSpan.FromSeconds(random.Next(delayPeriod / 2, delayPeriod * 2)));
					var currentIds = ids.Take(20).ToArray();
					try {
						Output1(outputDir, currentIds, startMonth);
						Output2(outputDir, currentIds, startMonth);
						Output3(outputDir, currentIds, startMonth);
						Output4(outputDir, currentIds, startMonth);
					} catch (NullReferenceException) {
						ErroredIds(ids, outputDir);
					}
					ids = ids.Skip(20);
				}
				startMonth = startMonth.AddMonths(1);
			}
		}

		private void ErroredIds(IEnumerable<string> ids, string outputDir) {
			var errorFile = outputDir + "\\Errors.txt";
			if (!HasErrored) {
				File.WriteAllText(errorFile, "One or more of the following Ids have generated an error. To determine which, try loading the IDs into the web environment and it should correct you" + Environment.NewLine);
			}
			HasErrored = true;
			File.AppendAllText(errorFile, string.Join(",", ids) + Environment.NewLine);
		}


		private IEnumerable<string> ReadIdsFromFile(string idFile) {
			return string.Join(",", File.ReadAllLines(idFile)).Split(',');
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
