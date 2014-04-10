using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NDesk.Options;

namespace PBS_WebScraper {
	class Program {
		static void Main(string[] args) {
			// Default outputDir
			var outputDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			bool help = false;
			string idFile = null;
			var startMonth = new DateTime(2013, 4, 1);
			var endMonth = startMonth;
			var delayPeriod = 0;

			var p = new OptionSet() {
   				{ "i|idFile=","File containing a CSV of ids (required)",      v => idFile = v },
   				{ "o|outputDir=","Where to store the files (default this directory)",      v => outputDir = v },
   				{ "s|start=", "When to start processing (default this month)" ,   (DateTime v) => startMonth = v },
   				{ "f|finish=","When to finish processing (default this month)",      (DateTime v) => endMonth = v },
   				{ "d|delay=", "How long to wait between quieries on average (default 0)",     (int v) => delayPeriod = v },
   				{ "h|?|help",   v => help = v != null }
			   };
			List<string> extra = p.Parse(args);

			if (help ) {
				p.WriteOptionDescriptions(Console.Out);
				return;
			}

			if (null == idFile){
				Console.WriteLine("ERROR: No id file specified. See --help for usage guides");
				return;
			}
			var parser = new Parser();
			parser.Changed += parser_Changed;
			parser.Parse(outputDir, idFile, startMonth, endMonth, delayPeriod);
		}

		static void ShowHelp(OptionSet p) {
			Console.WriteLine("Usage: WebScraperCommandLine [OPTIONS]");
			Console.WriteLine("Scrape data from Medicare.");
			Console.WriteLine();
			Console.WriteLine("Options:");
			p.WriteOptionDescriptions(Console.Out);
		}

		static void parser_Changed(object sender, decimal percentComplete) {
			Console.Clear();
			Console.WriteLine(Math.Round(percentComplete * 100,2) + "% complete");
		}
	}
}
