using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.net9.Data;
using WebAPI.net9.Models;
using WebAPI.net9.Services;

namespace WebAPI.net9.Tests.UnitTests.Services
{
    public class ProdutoServiceTests
    {
        [Fact]
        public async Task ListarProdutosAsync_DeveRetornarListaDeProdutos()
        {
            // Arrange : Criação do banco fictício em memória

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DbTest_ListarProdutos").Options; // Criação do banco de dados isolado em memória apenas para testes

            using(var context = new AppDbContext(options)) // Criação do contexto do banco temporário
            {
                context.Produtos.AddRange(new List<ProdutoModel> // Adiciona produtos fictícios para teste
                {
                    new ProdutoModel { Id = 1, Nome = "Teclado" },
                    new ProdutoModel { Id = 2, Nome = "Mouse" }
                });
                await context.SaveChangesAsync();
            }

            // Act: Cria a instância do serviço e chama o método a ser testado

            using (var context = new AppDbContext(options))
            {
                var service = new ProdutoService(context); // Cria a instância do serviço com o contexto do banco temporário
                var resultado = await service.ListarProdutosAsync(); // Chama o método que quero testar

                // Assert: Verifica se o resultado está correto

                Assert.NotNull(resultado); // Garante que o resultado não é nulo
                Assert.Equal(2, resultado.Count); // Verifica se a quantidade de produtos retornados é igual a 2
                Assert.Contains(resultado, p => p.Nome == "Mouse"); // Verifica se o produto "Mouse" está na lista retornada

            }

        }
    }
}
