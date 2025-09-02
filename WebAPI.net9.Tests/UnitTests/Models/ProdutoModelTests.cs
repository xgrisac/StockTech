using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.net9.Models;
using Xunit;

namespace WebAPI.net9.Tests.UnitTests.Models
{
    public class ProdutoModelTests
    {
        [Fact]
        public void QuantidadeEstoque_NaoPodeSerNegativa()
        {
            // Arrange
            var produto = new ProdutoModel
            {
                QuantidadeEstoque = -5, // valor inválido
            };

            // Act
            bool estoqueValido = produto.QuantidadeEstoque >= 0;

            // Assert
            estoqueValido.Should().BeFalse("A quantidade de estoque não pode ser negativa");
        }

        [Fact]

        public void Nome_NaoPodeSerVazio()
        {
            // Arrange
            var produto = new ProdutoModel
            {
                Nome = "", 
            };

            // Act
            bool nomeValido = !string.IsNullOrWhiteSpace(produto.Nome); // Ação de teste

            // Assert
            nomeValido.Should().BeFalse("O nome do produto não pode ser vazio");
        }

        [Fact]
        public void CodigoDeBarras_DeveTerPeloMenos8Caracteres()
        {
            // Arrange
            var produto = new ProdutoModel
            {
                CodigoDeBarras = "12321" // valor inválido
            };

            // Act
            bool codigoValido = produto.CodigoDeBarras.Length >= 8;

            // Assert
            codigoValido.Should().BeFalse("O código de barras deve ter pelo menos 8 caracteres"); // Resultado esperado
        }
    }
}
