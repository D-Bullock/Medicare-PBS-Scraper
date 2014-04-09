using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace PBS_WebScraper {
	class Parser {
		private static string[] States = new String[] { "NSW", "VIC", "QLD", "SA", "WA", "TAS", "ACT" };
		public static void Output1(string outputFolder, string[] IDs, DateTime month) {
			//https://www.medicareaustralia.gov.au/cgi-bin/broker.exe?_PROGRAM=sas.pbs_item_standard_report.sas&_SERVICE=default&itemlst=%2700021D%27%2C%2700022E%27%2C%2700023F%27%2C%2700024G%27%2C%2700025H%27%2C%2700026J%27%2C%2700027K%27%2C%2700028L%27%2C%2700029M%27%2C%2700030N%27%2C%2700031P%27%2C%2700032Q%27%2C%2700033R%27%2C%2700034T%27%2C%2700035W%27%2C%2700036X%27%2C%2700037Y%27%2C%2700038B%27%2C%2700039C%27%2C%2700040D%27&ITEMCNT=20&_DEBUG=0&LIST=00021D%2C00022E%2C00023F%2C00024G%2C00025H%2C00026J%2C00027K%2C00028L%2C00029M%2C00030N%2C00031P%2C00032Q%2C00033R%2C00034T%2C00035W%2C00036X%2C00037Y%2C00038B%2C00039C%2C00040D&VAR=SERVICES&RPT_FMT=1&start_dt=201304&end_dt=201304
			var date = string.Format(month.ToString("yyyyMM"));//201304
			var url = "https://www.medicareaustralia.gov.au/cgi-bin/broker.exe?_PROGRAM=sas.pbs_item_standard_report.sas&_SERVICE=default&itemlst=" +
				HttpUtility.UrlEncode("'" + string.Join("','", IDs) + "'") +
				"&ITEMCNT=" +
				IDs.Length +
				"&LIST=" +
				HttpUtility.UrlEncode(string.Join(",", IDs)) +
				"&VAR=SERVICES&RPT_FMT=1&start_dt=" + date + "&end_dt=" + date
			;


			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load(url);

			var tables = doc.DocumentNode.SelectNodes("//table[@class='table']//tbody");

			HtmlNodeCollection rows = tables[0].SelectNodes(".//tr");
			var data = new List<DataRow>();
			for (var i = 0; i < rows.Count; i++) {
				Console.WriteLine(rows[i].WriteContentTo());
				var headers = rows[i].SelectNodes(rows[i].XPath + "//th");

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
				// Edge case: Last lines that are totals (end of document)
				if (dataRow.Type.Equals("All items", StringComparison.CurrentCultureIgnoreCase)) {
					break;
				}

				// Edge case: "total" line
				if (dataRow.Type.Equals("Total", StringComparison.CurrentCultureIgnoreCase)) {
					continue;
				}

				// Get the data
				var values = rows[i].SelectNodes(rows[i].XPath + "//td");
				if (null == values) {
					continue;
				}
				values.ToList().ForEach(v => dataRow.Values.Add(v.InnerText.Trim()));

				// Edge case: First row (when Item|Scheme are in the ID/Type slots)
				if (dataRow.Id.Equals("Item", StringComparison.CurrentCultureIgnoreCase)) {
					i++; //moving the iterator along because that next row shouldn't have any data
					// Get the next row's ID and type
					headers = rows[i].SelectNodes(rows[i].XPath + "//th");
					dataRow.Id = headers[0].InnerText.Trim();
					dataRow.Type = headers[1].InnerText.Trim();
				}

				// Edge case: when the Scheme is in the type slot 
				if (dataRow.Type.Equals("Scheme", StringComparison.CurrentCultureIgnoreCase)) {
					i++; //moving the iterator along because that next row shouldn't have any data
					// Get the next row's ID and type
					headers = rows[i].SelectNodes(rows[i].XPath + "//th");
					dataRow.Type = headers[0].InnerText.Trim();
				}

				// Edge case: When there isn't an ID set on the row
				if (string.IsNullOrWhiteSpace(dataRow.Id)) {

				}

				// Add to results
				data.Add(dataRow);
			}
			// write the output to file
			var outputFile = outputFolder + @"\State_Services_" + month.ToString("MMMyyyy") + ".csv"; // TODO better checking
			File.AppendAllLines(outputFile, data.Select(d => d.ToString(month)));
		}


		public static void Output3(string outputFolder, string[] IDs, DateTime month) {
		// 
		https://www.medicareaustralia.gov.au/cgi-bin/broker.exe?_PROGRAM=sas.pbs_item_standard_report.sas&_SERVICE=default&itemlst=%2700021D%27%2C%2700022E%27%2C%2700023F%27%2C%2700024G%27%2C%2700025H%27%2C%2700026J%27%2C%2700027K%27%2C%2700028L%27%2C%2700029M%27%2C%2700030N%27%2C%2700031P%27%2C%2700032Q%27%2C%2700033R%27%2C%2700034T%27%2C%2700035W%27%2C%2700036X%27%2C%2700037Y%27%2C%2700038B%27%2C%2700039C%27%2C%2700040D%27&ITEMCNT=20&_DEBUG=0&LIST=00021D%2C00022E%2C00023F%2C00024G%2C00025H%2C00026J%2C00027K%2C00028L%2C00029M%2C00030N%2C00031P%2C00032Q%2C00033R%2C00034T%2C00035W%2C00036X%2C00037Y%2C00038B%2C00039C%2C00040D&VAR=SERVICES&RPT_FMT=5&start_dt=201304&end_dt=201304
			var date = string.Format(month.ToString("yyyyMM"));//201304
			var url = "https://www.medicareaustralia.gov.au/cgi-bin/broker.exe?_PROGRAM=sas.pbs_item_standard_report.sas&_SERVICE=default&itemlst=" +
				HttpUtility.UrlEncode("'" + string.Join("','", IDs) + "'") +
				"&ITEMCNT=" +
				IDs.Length +
				"&LIST=" +
				HttpUtility.UrlEncode(string.Join(",", IDs)) +
				"&VAR=SERVICES&RPT_FMT=5&start_dt=" + date + "&end_dt=" + date
			;


			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load(url);

			// Parse the header to work out how many of each category and what they are called.
			var thead = doc.DocumentNode.SelectSingleNode("//table[@class='table']//thead");
			var headerRows = thead.SelectNodes(thead.XPath + "//tr");
			int numberPBSColumns = 0;
			int numberRPBSColumns = 0;
			// work out the how many PBS and RPBS items there are
			foreach (var headerItem in headerRows[0].SelectNodes(headerRows[0].XPath + "//th")) {
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
			var _colHeaders = headerRows[1].SelectNodes(headerRows[1].XPath + "//th");
			for (int i = 0; i < _colHeaders.Count; i++) {
				if (i < numberPBSColumns) {
					PBSHeaders.Add(_colHeaders[i].InnerText.Trim());
				} else if (i < numberPBSColumns + numberRPBSColumns) { // This will mean that it's not part of what we want, such as totals
					RPBSHeaders.Add(_colHeaders[i].InnerText.Trim());
				}
			}

			// Now on to the actual data fetching
			var tbody = doc.DocumentNode.SelectNodes("//table[@class='table']//tbody");

			HtmlNodeCollection rows = tbody[0].SelectNodes(".//tr");
			var data = new List<DataRow>();
			for (var i = 0; i < rows.Count; i++) {
				Console.WriteLine(rows[i].WriteContentTo());
				var headers = rows[i].SelectNodes(rows[i].XPath + "//th");

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
				// Edge case: Last lines that are totals (end of document)
				if (dataRow.Type.Equals("All items", StringComparison.CurrentCultureIgnoreCase)) {
					break;
				}

				// Edge case: "total" line
				if (dataRow.Type.Equals("Total", StringComparison.CurrentCultureIgnoreCase)) {
					continue;
				}

				// Get the data
				var values = rows[i].SelectNodes(rows[i].XPath + "//td");
				if (null == values) {
					continue;
				}
				values.ToList().ForEach(v => dataRow.Values.Add(v.InnerText.Trim()));

				// Edge case: First row (when Item|Scheme are in the ID/Type slots)
				if (dataRow.Id.Equals("Item", StringComparison.CurrentCultureIgnoreCase)) {
					i++; //moving the iterator along because that next row shouldn't have any data
					// Get the next row's ID and type
					headers = rows[i].SelectNodes(rows[i].XPath + "//th");
					dataRow.Id = headers[0].InnerText.Trim();
					dataRow.Type = headers[1].InnerText.Trim();
				}

				// Edge case: when the Scheme is in the type slot 
				if (dataRow.Type.Equals("Scheme", StringComparison.CurrentCultureIgnoreCase)) {
					i++; //moving the iterator along because that next row shouldn't have any data
					// Get the next row's ID and type
					headers = rows[i].SelectNodes(rows[i].XPath + "//th");
					dataRow.Type = headers[0].InnerText.Trim();
				}

				// Edge case: When there isn't an ID set on the row
				if (string.IsNullOrWhiteSpace(dataRow.Id)) {

				}

				// Add to results
				data.Add(dataRow);
			}
			// write the output to file
			var outputFile = outputFolder + @"\State_Services_" + month.ToString("MMMyyyy") + ".csv"; // TODO better checking
			File.AppendAllLines(outputFile, data.Select(d => d.ToString(month)));
		}


		class DataRow {
			public DataRow() {
				Values = new List<string>();
			}
			public string Id { get; set; }
			public string Type { get; set; }
			public string FormatedId {
				get {
					var output = Id.Trim();
					// Pad out with leadng 0s
					while (output.Length != 6) {
						output = "0" + output;
					}
					return output;
				}
			}
			public List<string> Values { get; set; }
			public string ToString(DateTime Montm) {
				//Desired format: PBS ,00013Q ,APR2013 ,ACT ,71 
				var outputStart = String.Format("{0},{1},{2}", Type, FormatedId, Montm.ToString("MMMyyyy"));
				var outputList = new List<string>();
				for (int i = 0; i < Values.Count && i < States.Length; i++) { // This will ignore any extra values
					outputList.Add(string.Format("{0},{1},{2}", outputStart, States[i], Values[i]));
				}
				return string.Join(Environment.NewLine, outputList);

			}
			public override string ToString() {
				return String.Format("{0},{1},{2}", FormatedId, Type, string.Join(",", Values.Select(v => v)));
			}

		}
	}
}
