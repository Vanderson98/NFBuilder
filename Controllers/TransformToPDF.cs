using NFBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NFBuilder.Controllers {
    class TransformToPDF {
        public static void Transform(string path, int nfNumber, string folder) {
            string json = File.ReadAllText(path);
            jsonNF nf = JsonSerializer.Deserialize<jsonNF>(json);

            var documentQuest = Document.Create(Container => {
                Container.Page(page => {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Content().Column(column =>
                    {
                        column.Item().PaddingBottom(20).Text("NOTA FISCAL")
                            .FontSize(24)
                            .Bold()
                            .AlignCenter()
                            ;

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Número: {nfNumber}");
                        });

                        column.Item().PaddingTop(5).Text($"Cliente: {nf.NameClient}")
                            .FontSize(12);

                        column.Item().PaddingBottom(10).Text($"CPF/CNPJ: {nf.Cpforcnpj}")
                            .FontSize(12);

                        column.Item().PaddingBottom(10).LineHorizontal(1);

                        column.Item().PaddingBottom(5).Text("Itens").Bold().FontSize(14);

                        foreach (var product in nf.DataProducts) {
                            column.Item().Row(itemRow =>
                            {
                                itemRow.ConstantItem(10).Text("•");
                                itemRow.RelativeItem().Text(product);
                            });
                        }

                        column.Item().PaddingVertical(10).LineHorizontal(1);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Subtotal:");
                            row.ConstantItem(100).AlignRight().Text($"R$ {nf.SubtotalNF:F2}");
                        });

                        double calcIcms = nf.SubtotalNF * (nf.ICMS / 100.0);
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"ICMS ({nf.ICMS}%):");
                            row.ConstantItem(100).AlignRight().Text($"R$ {calcIcms:F2}");
                        });

                        double calcDiscount = nf.SubtotalNF * (nf.DiscountNota / 100.0);
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Desconto ({nf.DiscountNota}%):");
                            row.ConstantItem(100).AlignRight().Text($"R$ {calcDiscount:F2}");
                        });

                        column.Item().PaddingVertical(10).LineHorizontal(1);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text("TOTAL:").Bold();
                            row.ConstantItem(100).AlignRight().Text($"R$ {nf.TotalNFValue(nf.SubtotalNF):F2}").Bold();
                        });
                    });
                });
            });

            documentQuest.GeneratePdf(@"" + folder + $"_nota_fiscal#{nfNumber}.pdf");
            Console.WriteLine("PDF Gerado com sucesso!");
        }
    }
}
