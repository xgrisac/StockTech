using WebAPI.net9.Models;
using WebAPI.net9.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.net9.Services
{
    /// <summary>
    /// Serviço responsável pelas operações de CRUD com a entidade Produto.
    /// </summary>
    public class ProdutoService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que injeta o contexto do banco de dados.
        /// </summary>
        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna a lista de todos os produtos cadastrados.
        /// </summary>
        public async Task<List<ProdutoModel>> ListarProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        /// <summary>
        /// Busca um produto específico pelo ID.
        /// </summary>
        public async Task<ProdutoModel?> BuscarPorIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        /// <summary>
        /// Cadastra um novo produto no banco de dados.
        /// </summary>
        public async Task CadastrarProdutoAsync(ProdutoModel produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza um produto existente no banco de dados com base no ID informado.
        /// </summary>
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

        /// <summary>
        /// Remove um produto do banco de dados com base no ID informado.
        /// </summary>
        public async Task<bool> DeletarProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Busca produtos por nome ou marca.
        /// </summary>
        public async Task<List<ProdutoModel>> BuscarPorNomeOuMarcaAsync(string? nome, string? marca)
        {
            return await _context.Produtos
                .Where(p => (nome == null || p.Nome.Contains(nome)) &&
                            (marca == null || p.Marca.Contains(marca)))
                .ToListAsync();
        }
    }
}