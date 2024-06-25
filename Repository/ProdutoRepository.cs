using Newtonsoft.Json;
using RestSharp;
using CadastroDeProdutosNovo.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace Repository;

#region ProdutoRepository'


public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Produto> AddProdutoAsync(Produto produto)
        {
            var query = "INSERT INTO Produto (Nome, Descricao, Preco, Quantidade) VALUES (@Nome, @Descricao, @Preco, @Quantidade); SELECT LAST_INSERT_ID();";

            using (var connection = new MySqlConnection(_connectionString))
            {
                produto.Id = await connection.QuerySingleAsync<int>(query, produto);
                return produto;
            }
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            var query = "SELECT * FROM Produto WHERE Id = @Id";

            using (var connection = new MySqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Produto>(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            var query = "SELECT * FROM Produto";

            using (var connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Produto>(query);
            }
        }

        public async Task<Produto> UpdateProdutoAsync(int id, Produto produto)
        {
            var query = "UPDATE Produto SET Nome = @Nome, Descricao = @Descricao, Preco = @Preco, Quantidade = @Quantidade WHERE Id = @Id";

            using (var connection = new MySqlConnection(_connectionString))
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { produto.Nome, produto.Descricao, produto.Preco, produto.Quantidade, Id = id });
                return rowsAffected > 0 ? produto : null;
            }
        }

        public async Task<bool> DeleteProdutoAsync(int id)
        {
            var query = "DELETE FROM Produto WHERE Id = @Id";

            using (var connection = new MySqlConnection(_connectionString))
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }
    }



#endregion