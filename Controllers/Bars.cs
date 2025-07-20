using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFBuilder.Controllers {
    class Bars {
        public static void WriteLine() {
            for (int i = 1; i <= 50; i++) {
                Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}
