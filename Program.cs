
using NFBuilder.Controllers;
using NFBuilder.Entities;
using QuestPDF.Infrastructure;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

QuestPDF.Settings.License = LicenseType.Community;
NFCreate.Create();

Console.WriteLine();
Console.WriteLine("Programa finalizado!");
Console.WriteLine("Digite qualquer tecla para fechar o terminal.");
Console.ReadLine();