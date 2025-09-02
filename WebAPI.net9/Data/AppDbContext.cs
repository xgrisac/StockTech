using Microsoft.EntityFrameworkCore; // Esta parte da aplicação é responsável por ciar a conexão, enviar e coletar dados do banco de dados
using WebAPI.net9.Interfaces;
using WebAPI.net9.Models; 

namespace WebAPI.net9.Data 
{
    public class AppDbContext : DbContext, IAppDbContext // Herança de DbContext e implementação da interface IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) // Construtor que recebe as opções de configuração do DbContext
        {
        }

        // Criação da tabela Produtos com as confgs ProdutoModel no banco de dados
        public DbSet<ProdutoModel> Produtos { get; set; } // DbSet é uma coleção de entidades que podem ser consultadas e manipuladas

    }
}
