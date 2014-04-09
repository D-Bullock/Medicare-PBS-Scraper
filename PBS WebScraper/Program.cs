using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PBS_WebScraper {
	class Program {
		static void Main(string[] args) {
			var outputDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/output";

			var idFile = outputDir + "//List_of_items.csv";
			var startMonth = new DateTime(2013, 4, 1);
			var endMonth = startMonth;
			var delayPeriod = 3;
			new Parser().Parse(outputDir, idFile, startMonth, endMonth, delayPeriod);
		}

	}
}
