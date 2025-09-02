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
    }
}
