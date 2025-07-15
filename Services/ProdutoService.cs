using WebAPI.net9.Models;
using WebAPI.net9.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.net9.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoModel>> ListarProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<ProdutoModel?> BuscarPorIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task CadastrarProdutoAsync(ProdutoModel produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AtualizarProdutoAsync(int id, ProdutoModel produto)
        {
            var produtoExistente = await _context.Produtos.FindAsync(id);
            if (produtoExistente == null)
                return false;

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Marca = produto.Marca;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.QuantidadeEstoque = produto.QuantidadeEstoque;
            produtoExistente.CodigoDeBarras = produto.CodigoDeBarras;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProdutoModel>> BuscarPorNomeOuMarcaAsync(string? nome, string? marca)
        {
            return await _context.Produtos
                .Where(p => (nome == null || p.Nome.Contains(nome)) &&
                            (marca == null || p.Marca.Contains(marca)))
                .ToListAsync();
        }
    }
}