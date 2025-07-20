using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFBuilder.Entities
{
    class jsonNF
    {
        public int NumberNF { get; set; }

        public string NameClient { get; set; }

        public string AdressClient { get; set; }

        public string Cpforcnpj { get; set; }
        public List<string> DataProducts { get; set; } = new();  

        public double ICMS { get; set; }

        public double DiscountNota { get; set; }
        public double SubtotalNF { get; set; }  

        public double TotalNF { get; set; }

        public double TotalNFValue(double subtotal) {
            double icmsCalc = subtotal * ICMS / 100.00;
            double discountCalc = subtotal * DiscountNota / 100.00;

            subtotal += icmsCalc;
            subtotal -= discountCalc;
            return Math.Round(subtotal);
        }

    }
}
