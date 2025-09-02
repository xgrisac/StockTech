using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.net9.Models;

namespace WebAPI.net9.Interfaces 
{
    /// <summary>
    ///  Define os contratos que o AppDbontext deve cumprir, sem se importar com com isso é implementado.
    /// </summary>
    public interface IAppDbContext
    {
        DbSet<ProdutoModel> Produtos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); // Método assíncrono para salvar as alterações no banco de dados
    }
}
