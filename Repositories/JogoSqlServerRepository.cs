using DIO.CatalogoDeJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DIO.CatalogoDeJogos.Repositories
{
    public class JogoSqlServerRepository: IJogoRepository
    {
        private readonly SqlConnection sqlConnection;
        public JogoSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();
            var sql = $"SELECT * FROM Jogos ORDER BY id OFFSET {((pagina - 1) * quantidade)} ROWS FETCH NEXT {quantidade} ROWS ONLY";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                   Id = (Guid) sqlDataReader["Id"],
                   Nome = (string) sqlDataReader["Nome"],
                   Produtora = (string) sqlDataReader["Produtora"],
                   Preco = (double) sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();
            return jogos;
        }

        public async Task<Jogo> Obter(Guid id)
        {
            Jogo jogo = null;

            var sql = $"SELECT * FROM Jogos WHERE Id = {id}";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogo = new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();
            return jogo;
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();
            var sql = $"SELECT * FROM Jogos WHERE Nome = '{nome}' AND Produtora = '{produtora}'";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();
            return jogos;
        }


        public async Task Inserir(Jogo jogo)
        {
            var sql = $"INSERT Jogos (Id, Nome, Produtora, Preco) VALUES ('{jogo.Id}', '{jogo.Nome}', '{jogo.Produtora}', {jogo.Preco.ToString().Replace(",", ".")}";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Jogo jogo)
        {
            var sql = $"UPDATE Jogos SET Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco = {jogo.Preco.ToString().Replace(",", ".")} WHERE Id = '{jogo.Id}'";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var sql = $"DELETE FROM Jogos WHERE Id = {id}";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }
        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
