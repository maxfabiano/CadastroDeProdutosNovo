using System.Net;
using CadastroDeProdutosNovo.Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApisGroups
{
    [Route("api/v1/CadastroProdutos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProduto([FromBody] Produto produto)
        {
            try
            {
                var createdProduto = await _produtoRepository.AddProdutoAsync(produto);
                return CreatedAtAction(nameof(GetProduto), new { id = createdProduto.Id }, createdProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProduto(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutoByIdAsync(id);
                if (produto == null)
                {
                    return NotFound();
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Produto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllProdutos()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllProdutosAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] Produto produto)
        {
            try
            {
                var updatedProduto = await _produtoRepository.UpdateProdutoAsync(id, produto);
                if (updatedProduto == null)
                {
                    return NotFound();
                }
                return Ok(updatedProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                var isDeleted = await _produtoRepository.DeleteProdutoAsync(id);
                if (!isDeleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
