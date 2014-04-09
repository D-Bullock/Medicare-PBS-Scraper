using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBS_WebScraper {
	class Program {
		static void Main(string[] args) {
			Parser.Output3(@"F:\Programs\DropboxWork\Dropbox\BurningFly\WebScraper", "00021D,00022E,00023F,00024G,00025H,00026J,00027K,00028L,00029M,00030N,00031P,00032Q,00033R,00034T,00035W,00036X,00037Y,00038B,00039C,00040D".Split(','), new DateTime(2013, 4, 1));
		}
	}
}
