using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.net9.Models;
using WebAPI.net9.Services;

// Controller responsável por chamar os métodos do ProdutoService

namespace WebAPI.net9.Controllers
{
    /// <summary>
    /// Controller para gerenciar produtos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        private readonly ILogger<ProdutoController> _logger; // Logger para registrar informações, avisos e erros

        /// <summary>
        /// Construtor que injeta o ProdutoService e o logger.
        /// </summary>
        /// <param name="produtoService">Instância do ProdutoService.</param>
        /// <param name="logger">Instância do ILogger</param>
        public ProdutoController(ProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados.
        /// </summary>
        /// <returns>Lista de produtos ou erro 500.</returns>
        [HttpGet("Estoque")]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarProdutos()
        {
            _logger.LogDebug("Iniciando requisição: BuscarProdutos");

            try
            {
                var produtos = await _produtoService.ListarProdutosAsync(); // Pega todos os produtos da camada service e transforma em lista
                _logger.LogInformation("Lista de produtos retornada com sucesso. Total {Total}", produtos.Count); // Log informativo
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produto.");
                return StatusCode(500, $"Erro interno. Por favor, tente novamente mais tarde. {ex.Message}"); // ex.Message retorna o erro capturado pela variável ex
            }
        }

        /// <summary>
        /// Busca um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Erro 404, produto encontrado ou erro 500.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> BuscarProdutoPorId(int id)
        {
            _logger.LogDebug("Iniciando requisição: BuscarProdutosPorId");

            try
            {
                var produto = await _produtoService.BuscarPorIdAsync(id); // Busca produto na camada service
                if (produto == null)
                {
                    _logger.LogWarning("Produto com ID {Id} não encontrado.", id);
                    return NotFound("Registro não localizado"); // Erro 404
                }
                _logger.LogInformation("Produto com ID {Id} encontrado com sucesso.", id);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produto com ID {Id}", id);
                return StatusCode(500, $"Erro interno. Por favor, tente novamente mais tarde. {ex.Message}");
            }
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="produtoModel">Objeto do produto a ser criado.</param>
        /// <returns>Erro 400, produto encontrado ou erro 500.</returns>
        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> CriarProduto([FromBody] ProdutoModel produtoModel)
        {
            _logger.LogDebug("Iniciando requisição: CriarProduto");

            try
            {
                if (produtoModel == null) // Verifica se o valor está vazio
                {
                    _logger.LogWarning("Tentativa de criar produto com valor nulo");
                    return BadRequest("Produto inválido"); // Erro 400
                }

                if (produtoModel.Id != 0)
                {
                    _logger.LogWarning("Tentativa de criar produto com ID informado manualmente");
                    return BadRequest("O campo 'Id' não deve ser informado. Ele é gerado automaticamente, favor excluir o campo ID do seu JSON.");
                }

                await _produtoService.CadastrarProdutoAsync(produtoModel); // Cadastra produto pela camada service

                _logger.LogInformation("Produto criado com sucesso. ID: {Id}", produtoModel.Id);
                return CreatedAtAction(nameof(BuscarProdutoPorId), new { id = produtoModel.Id }, produtoModel); // Retorna produto criado
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar produto");
                return StatusCode(500, $"Erro interno. Por favor, tente novamente mais tarde. {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="produtoModel">Dados atualizados do produto.</param>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <returns>Erro 404, produto encontrado ou erro 500.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> EditarProduto([FromBody] ProdutoModel produtoModel, int id)
        {
            _logger.LogDebug("Iniciando requisição: EditarProduto");

            try
            {
                var produtoatualizado = await _produtoService.AtualizarProdutoAsync(id, produtoModel); // Atualiza produto via service
                if (!produtoatualizado)
                {
                    _logger.LogWarning("Produto com ID {Id} não localizado", id);
                    return NotFound("Registro não localizado"); // Erro 404
                }

                _logger.LogInformation("Produto com ID {Id} atualizado com sucesso", id);
                return Ok(produtoModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar produto com ID {Id}", id);
                return StatusCode(500, $"Erro interno. Por favor, tente novamente mais tarde. {ex.Message}");
            }
        }

        /// <summary>
        /// Deleta um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>Erro 404, mensagem de sucesso ou erro 500.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarProduto(int id)
        {
            _logger.LogDebug("Iniciando requisição: DeletarProduto");

            try
            {
                var produtodeletado = await _produtoService.DeletarProdutoAsync(id); // Deleta produto via service
                if (!produtodeletado)
                {
                    _logger.LogWarning("Produto com ID {Id} não localizado", id);
                    return NotFound("Registro não localizado"); // Erro 404
                }

                _logger.LogInformation("Produto com ID {Id} deletado com sucesso", id);
                return Ok($"Conteúdo do ID {id} deletado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar produto com ID {Id}", id);
                return StatusCode(500, $"Erro interno. Por favor, tente novamente mais tarde. {ex.Message}");
            }
        }

        /// <summary>
        /// Busca produtos pelo nome ou marca.
        /// </summary>
        /// <param name="nome">Nome do produto (opcional).</param>
        /// <param name="marca">Marca do produto (opcional).</param>
        /// <returns>Lista de produtos encontrados ou erro 500.</returns>
        [HttpGet("Buscar")] // Busca os produtos por nome ou marca
        public async Task<ActionResult<List<ProdutoModel>>> BuscarPorNomeOuMarca(string? nome, string? marca)
        {
            _logger.LogDebug("Iniciando requisição: BuscarPorNomeOuMarca");

            try
            {
                var produtos = await _produtoService.BuscarPorNomeOuMarcaAsync(nome, marca);
                _logger.LogInformation("Busca por produtos realizada com sucesso. Total {Total}", produtos.Count);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produtos por nome ou marca. Nome: {Nome}, Marca: {Marca}", nome, marca);
                return StatusCode(500, $"Erro interno. Por favor, tente novamente mais tarde. {ex.Message}");
            }
        }
    }
}