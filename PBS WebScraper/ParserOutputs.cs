using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace PBS_WebScraper {
	partial class Parser {
		private static void Output1(string outputFolder, string[] IDs, DateTime month) {
			var url = GenerateUrl(IDs, month, "SERVICES", 1);

			var data = Shared_Output1And2(month, url);

			// write the output to file
			var outputFile = outputFolder + @"\State_Services_" + month.ToString("MMMyyyy") + ".csv"; // TODO better checking of the folder name
			File.AppendAllText(outputFile, url + Environment.NewLine);
			File.AppendAllLines(outputFile, data.Select(d => d.ToString()));
		}

		private static void Output2(string outputFolder, string[] IDs, DateTime month) {
			var url = GenerateUrl(IDs, month, "BENEFIT", 1);

			var data = Shared_Output1And2(month, url);

			// write the output to file
			var outputFile = outputFolder + @"\State_Benefits_" + month.ToString("MMMyyyy") + ".csv"; // TODO better checking of the folder name
			File.AppendAllText(outputFile, url + Environment.NewLine);
			File.AppendAllLines(outputFile, data.Select(d => d.ToString()));
		}

		private static List<DataRow> Shared_Output1And2(DateTime month, string url) {
			// The accepted titles
			string[] States = new String[] { "NSW", "VIC", "QLD", "SA", "WA", "TAS", "ACT" };

			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load(url);

			var tbody = doc.DocumentNode.SelectNodes("//table[@class='table']//tbody");

			HtmlNodeCollection rows = tbody[0].SelectNodes(".//tr");
			var data = new List<DataRow>();

			// Go through the rows
			for (var i = 0; i < rows.Count; i++) {
				// Get the data
				var values = rows[i].SelectNodes(".//td");
				if (null == values) {
					continue;
				}
				// Go through and make a row for each value
				for (var j = 0; j < values.Count && j < States.Length; j++) { // Using for loop so we can know the index.
					var headers = rows[i].SelectNodes(".//th");

					DataRow dataRow;
					// Check if we have the ID or just the type
					if (headers.Count == 1) {
						dataRow = new DataRow() {
							// Fetch ID from previous entry
							Id = data.Last().Id,
							// Fetch type as normal
							Type = headers[0].InnerText.Trim()
						};
					} else {
						// work out the ID and type
						dataRow = new DataRow() {
							Id = headers[0].InnerText.Trim(),
							Type = headers[1].InnerText.Trim()
						};
					}
					// Add the common values
					dataRow.Month = month;
					dataRow.Value = values[j].InnerText.Trim();
					dataRow.Column = States[j];

					// Edge case: Last lines that are totals (end of document) We don't want to know anything after this point
					if (dataRow.Type.Equals("All items", StringComparison.CurrentCultureIgnoreCase)) {
						i = rows.Count; // Force all loops to break
						break;
					}

					// Edge case: "total" line
					if (dataRow.Type.Equals("Total", StringComparison.CurrentCultureIgnoreCase)) {
						continue;
					}

					// Edge case: First row (when Item|Scheme are in the ID/Type slots)
					if (dataRow.Id.Equals("Item", StringComparison.CurrentCultureIgnoreCase)) {
						i++; //moving the iterator along because that next row shouldn't have any data
						// Get the next row's ID and type
						headers = rows[i].SelectNodes(".//th");
						dataRow.Id = headers[0].InnerText.Trim();
						dataRow.Type = headers[1].InnerText.Trim();
					}

					// Edge case: when the Scheme is in the type slot 
					if (dataRow.Type.Equals("Scheme", StringComparison.CurrentCultureIgnoreCase)) {
						i++; //moving the iterator along because that next row shouldn't have any data
						// Get the next row's ID and type
						headers = rows[i].SelectNodes(".//th");
						dataRow.Type = headers[0].InnerText.Trim();
					}

					// Add to results
					data.Add(dataRow);
				}
			}
			return data;
		}

		private static void Output3(string outputFolder, string[] IDs, DateTime month) {
			var url = GenerateUrl(IDs, month, "SERVICES", 5);

			var data = Shared_Output3And4(month, url);

			// write the output to file
			var outputFile = outputFolder + @"\PatCat_Services_" + month.ToString("MMMyyyy") + ".csv"; // TODO better checking of the folder name
			File.AppendAllText(outputFile, url + Environment.NewLine);
			File.AppendAllLines(outputFile, data.Select(d => d.ToString()));
		}

		private static void Output4(string outputFolder, string[] IDs, DateTime month) {
			var url = GenerateUrl(IDs, month, "BENEFIT", 5);

			var data = Shared_Output3And4(month, url);
			// write the output to file
			var outputFile = outputFolder + @"\PatCat_Benefits_" + month.ToString("MMMyyyy") + ".csv"; // TODO better checking of the folder name
			File.AppendAllText(outputFile, url + Environment.NewLine);
			File.AppendAllLines(outputFile, data.Select(d => d.ToString()));
		}

		private static List<DataRow> Shared_Output3And4(DateTime month, string url) {
			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load(url);

			// Parse the header to work out how many of each category and what they are called.
			var thead = doc.DocumentNode.SelectSingleNode("//table[@class='table']//thead");
			var headerRows = thead.SelectNodes(".//tr");
			int numberPBSColumns = 0;
			int numberRPBSColumns = 0;
			// work out the how many PBS and RPBS items there are
			foreach (var headerItem in headerRows[0].SelectNodes(".//th")) {
				if (headerItem.InnerText.Trim().Equals("PBS", StringComparison.CurrentCultureIgnoreCase)) {
					if (null == headerItem.Attributes["colspan"]) { // If there isn't any colspan that means there is only one entry
						numberPBSColumns = 1;
					} else {
						numberPBSColumns = int.Parse(headerItem.Attributes["colspan"].Value);
					}
				} else if (headerItem.InnerText.Trim().Equals("RPBS", StringComparison.CurrentCultureIgnoreCase)) {
					if (null == headerItem.Attributes["colspan"]) { // If there isn't any colspan that means there is only one entry
						numberRPBSColumns = 1;
					} else {
						numberRPBSColumns = int.Parse(headerItem.Attributes["colspan"].Value);
					}
				}
			}
			//// Work out the actual categories
			var PBSHeaders = new List<string>();
			var RPBSHeaders = new List<string>();
			var _colHeaders = headerRows[1].SelectNodes(".//th");
			for (int i = 0; i < _colHeaders.Count; i++) {
				if (i < numberPBSColumns) {
					PBSHeaders.Add(_colHeaders[i].InnerText.Trim());
				} else if (i < numberPBSColumns + numberRPBSColumns) {
					RPBSHeaders.Add(_colHeaders[i].InnerText.Trim());
				}
			}

			// Now on to the actual data fetching
			var tbody = doc.DocumentNode.SelectNodes("//table[@class='table']//tbody");

			HtmlNodeCollection rows = tbody[0].SelectNodes(".//tr");
			var data = new List<DataRow>();
			for (var i = 0; i < rows.Count; i++) {
				// Get the data
				var values = rows[i].SelectNodes(".//td");
				if (null == values) {
					continue;
				}
				for (var j = 0; j < values.Count; j++) { // Using for loop so we can know the index.
					var value = values[j];

					// Work out the ID
					DataRow dataRow = dataRow = new DataRow() {
						Id = rows[i].SelectNodes(".//th")[0].InnerText.Trim(),
						Value = value.InnerText.Trim(),
						Month = month
					};

					// Edge case: "total" line
					if (dataRow.Id.Equals("Total", StringComparison.CurrentCultureIgnoreCase)) {
						continue; // Ignore Line
					}
					// Edge case: First row (when Item is in the ID column)
					if (dataRow.Id.Equals("Item", StringComparison.CurrentCultureIgnoreCase)) {
						i++; //moving the iterator along because that next row shouldn't have any data. This won't affect the values as they are already fetched
						// Get the next row's ID
						dataRow.Id = rows[i].SelectNodes(".//th")[0].InnerText.Trim();
					}

					// Add the column and type
					if (j < numberPBSColumns) {
						dataRow.Type = "PBS";
						dataRow.Column = PBSHeaders[j];
					} else if (j < numberPBSColumns + numberRPBSColumns) {
						dataRow.Type = "RPBS";
						dataRow.Column = RPBSHeaders[j - numberPBSColumns];
					} else {
						continue; // Not required data - such as totals
					}
					// Add to results
					data.Add(dataRow);
				}
			}
			return data;
		}

		private static string GenerateUrl(string[] IDs, DateTime month, string reportOn, int reportFormat) {
			//https://www.medicareaustralia.gov.au/cgi-bin/broker.exe?_PROGRAM=sas.pbs_item_standard_report.sas&_SERVICE=default&itemlst=%2700021D%27%2C%2700022E%27%2C%2700023F%27%2C%2700024G%27%2C%2700025H%27%2C%2700026J%27%2C%2700027K%27%2C%2700028L%27%2C%2700029M%27%2C%2700030N%27%2C%2700031P%27%2C%2700032Q%27%2C%2700033R%27%2C%2700034T%27%2C%2700035W%27%2C%2700036X%27%2C%2700037Y%27%2C%2700038B%27%2C%2700039C%27%2C%2700040D%27&ITEMCNT=20&_DEBUG=0&LIST=00021D%2C00022E%2C00023F%2C00024G%2C00025H%2C00026J%2C00027K%2C00028L%2C00029M%2C00030N%2C00031P%2C00032Q%2C00033R%2C00034T%2C00035W%2C00036X%2C00037Y%2C00038B%2C00039C%2C00040D&VAR=SERVICES&RPT_FMT=1&start_dt=201304&end_dt=201304
			var date = string.Format(month.ToString("yyyyMM"));//201304
			var url = "https://www.medicareaustralia.gov.au/cgi-bin/broker.exe?_PROGRAM=sas.pbs_item_standard_report.sas&_SERVICE=default&itemlst=" +
				HttpUtility.UrlEncode("'" + string.Join("','", IDs) + "'") +
				"&ITEMCNT=" +
				IDs.Length +
				"&LIST=" +
				HttpUtility.UrlEncode(string.Join(",", IDs)) +
				"&VAR=" + reportOn +
				"&RPT_FMT=" + reportFormat +
				"&start_dt=" + date + "&end_dt=" + date
			;
			return url;
		}

		class DataRow {
			public string Id { get; set; }
			public string Type { get; set; } // PBS/RPBS
			public DateTime Month { get; set; }
			public string Column { get; set; }
			private int value { get; set; }
			public string Value {
				get { return value.ToString(); }
				set { this.value = int.Parse(value, NumberStyles.AllowThousands); }
			}
			public string FormatedId {
				get {
					var output = Id.Trim();
					// Pad out with leading 0s
					while (output.Length != 6) {
						output = "0" + output;
					}
					return output;
				}
			}

			public override string ToString() {
				return Type + "," + FormatedId + "," + Month.ToString("MMMyyyy") + "," + Column + "," + Value;
			}
		}
	}
}
