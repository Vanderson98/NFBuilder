
# 🧾 NFBUilder - Gerador de Nota Fiscal (C# Console App)

Este projeto é uma aplicação de terminal desenvolvida em **C#** com o objetivo de gerar **notas fiscais personalizadas** de forma simples e prática. Ele foi criado como exercício para reforçar conceitos de **lógica de programação**, **manipulação de dados** e **estruturação de arquivos**.

## ✅ Funcionalidades

- Inserção de dados diretamente pelo terminal:
  - Nome do cliente
  - Nome do produto
  - Quantidade
  - Preço unitário
  - Percentual de **ICMS**
  - Percentual de **desconto**
- Cálculo automático do valor total da nota fiscal
- Geração da nota fiscal em dois formatos:
  - 📄 **JSON** (armazenamento estruturado dos dados)
  - 🧾 **PDF** (formato visual, pronto para impressão)

## 🛠️ Tecnologias Utilizadas

- [.NET / C#](https://dotnet.microsoft.com/)
- Leitura e escrita de arquivos (`System.IO`)
- Serialização JSON com `System.Text.Json`
- Geração de PDF com [QuestPDF](https://www.questpdf.com/) *(ou biblioteca similar)*
- Console como interface principal

## 🎯 Objetivo do Projeto

Este projeto foi desenvolvido com os seguintes propósitos:

- Praticar entrada de dados no console
- Implementar lógica de cálculo com **impostos e descontos percentuais**
- Trabalhar com **serialização de dados** e **formatação de arquivos**
- Gerar relatórios (PDF/JSON) de maneira automatizada
