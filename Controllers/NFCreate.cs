using NFBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NFBuilder.Controllers {
    public class NFCreate {
        public static void Create() {
            Console.WriteLine("Seja bem vindo ao sistema de notas fiscais.");
            Bars.WriteLine();

            Console.Write("Nome do cliente: ");
            string nameClient = Console.ReadLine();

            Console.Write("CPF/CNPJ do cliente (SEM PONTOS): ");
            string cpfNumber = Console.ReadLine();
            int countCpf = cpfNumber.Length;
            string cpfText = "";

            if (countCpf == 11) cpfText = "CPF";
            else if (countCpf == 14) cpfText = "CNPJ";
            else cpfText = "NONE";

            Console.WriteLine();

            Console.Write("Digite o endereço: ");
            string adressClient = Console.ReadLine();
            if (adressClient == "" || adressClient == null) {
                adressClient = "Nenhum endereço vinculado";
            }

            Console.Write("Quantidade de itens comprados: ");
            int quantityItems = int.Parse(Console.ReadLine());

            Bars.WriteLine();

            List<Items> items = new List<Items>();
            for (int i = 1; i <= quantityItems; i++) {
                Console.WriteLine();
                Console.WriteLine($"Digite os dados do produto #{i}");

                Console.Write("Nome do produto: ");
                string nameProduct = Console.ReadLine();

                Console.Write("Preço unitário: ");
                double priceProduct = double.Parse(Console.ReadLine());

                Console.Write("Quantidade: ");
                int quantityProduct = int.Parse(Console.ReadLine());

                Items item = new Items(i, nameProduct, priceProduct, quantityProduct);

                items.Add(item);
            }

            Console.WriteLine();

            Console.Write("Digite a porcetagem do ICMS: ");
            double icms = int.Parse(Console.ReadLine());

            Console.Write("Digite a porcetagem de desconto: ");
            double discountClient = double.Parse(Console.ReadLine());

            CreateJSON(nameClient, adressClient, cpfText, cpfNumber, icms, discountClient, items);
        }

        private static void CreateJSON(
            string nameCLient,
            string adressCLient,
            string cpfText,
            string cpfNumber,
            double icms,
            double discountClient,
            List<Items> items) {

            try {
                string folder = $@"C:\Notas\{nameCLient}";
                if (!Directory.Exists(folder)) {
                    Directory.CreateDirectory(folder);
                }

                Random randomNumber = new Random();
                int nfNumber = randomNumber.Next(999999);
                string path = folder + $@"\NotaFiscal#{nfNumber}.json";

                if (!File.Exists(path)) {
                    CreateArchiveJson(nfNumber, path, folder);
                } else {
                    nfNumber = randomNumber.Next(999999);
                    CreateArchiveJson(nfNumber, path, folder);
                }

            } catch (IOException e) {
                Console.WriteLine($"Erro sistemico detectado: {e.Message}");
            }

            void CreateArchiveJson(int nfNumber, string path, string folder) {
                var jsonNF = new jsonNF {
                    NameClient = nameCLient,
                    NumberNF = nfNumber,
                    AdressClient = adressCLient,
                    Cpforcnpj = $"{cpfText} - {cpfNumber}",
                    ICMS = icms,
                    DiscountNota = discountClient
                };

                foreach (Items item in items) {
                    string dataNF =
                        $"Produto: {item.Name} " +
                        $"| Preço: {item.Price.ToString("F2")} " +
                        $"| Quantidade: {item.Quantity}  " +
                        $"| Total: {(item.Price * item.Quantity).ToString("F2")}";

                    jsonNF.DataProducts.Add(dataNF);
                    jsonNF.SubtotalNF += (item.Price * item.Quantity);
                }

                jsonNF.TotalNF = jsonNF.TotalNFValue(jsonNF.SubtotalNF);

                var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

                string jsonConfig = JsonSerializer.Serialize(jsonNF, options);

                File.WriteAllText(path, jsonConfig);
                Console.WriteLine($"Arquivo Salvo como JSON. \nCaminho {path}.");

                TransformToPDF.Transform(path, nfNumber, folder);

            }
        }
    }
}
