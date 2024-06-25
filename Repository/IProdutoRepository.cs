using CadastroDeProdutosNovo.Domain;

namespace Repository;

public interface IProdutoRepository
{
    Task<Produto> AddProdutoAsync(Produto produto);
    Task<Produto> GetProdutoByIdAsync(int id);
    Task<IEnumerable<Produto>> GetAllProdutosAsync();
    Task<Produto> UpdateProdutoAsync(int id, Produto produto);
    Task<bool> DeleteProdutoAsync(int id);


}