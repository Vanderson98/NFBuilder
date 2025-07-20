using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFBuilder.Entities {
    class Items {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int Quantity { get; set; }

        public Items() { }

        public Items(int id, string name, double price, int quantity) {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }   
    }
}
