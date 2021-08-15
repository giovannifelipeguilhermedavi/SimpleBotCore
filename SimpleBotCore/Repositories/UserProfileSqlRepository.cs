using Microsoft.Extensions.Options;
using SimpleBotCore.Config;
using SimpleBotCore.Logic;
using System;
using System.Data.SqlClient;
using Dapper;

namespace SimpleBotCore.Repositories
{
    public class UserProfileSqlRepository : IUserProfileRepository
    {
        private readonly SQLConfigConnection _config;

        public UserProfileSqlRepository(IOptions<SQLConfigConnection> options)
        {
            _config = options.Value;            
        }

        public void AtualizaCor(string userId, string cor)
        {
            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            using (SqlConnection connection = new SqlConnection(_config.GetConnectionDefault()))
            {
                connection.Execute("UPDATE User Cor = @cor WHERE Id = @id",
                    new
                    {
                        id = userId,
                        cor = cor
                    });
            }
        }

        public void AtualizaIdade(string userId, int idade)
        {
            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            using (SqlConnection connection = new SqlConnection(_config.GetConnectionDefault()))
            {
                connection.Execute("UPDATE User Idade = @idade WHERE Id = @id",
                    new
                    {
                        id = userId,
                        idade = idade
                    });
            }
        }

        public void AtualizaNome(string userId, string name)
        {
            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            using (SqlConnection connection = new SqlConnection(_config.GetConnectionDefault()))
            {
                connection.Execute("UPDATE User Name = @name WHERE Id = @id",
                    new
                    {
                        id = userId,
                        name = name
                    });
            }
        }

        public SimpleUser Create(SimpleUser user)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionDefault()))
            {
                if (Exists(user.Id))
                    throw new InvalidOperationException("Usuário ja existente");

                connection.Execute("INSERT User (Id, Nome, Idade, Cor) VALUES (@id, @nome, @idade, @cor)",
                    new { 
                        id = user.Id, 
                        nome = user.Nome,
                        idade = user.Idade,
                        cor = user.Cor
                    });
            }
            return user;
        }

        public SimpleUser TryLoadUser(string userId)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionDefault()))
            {
                SimpleUser user = (SimpleUser)connection.Query("SELECT Id, Nome, Idade, Cor from User WHERE Id = @id",
                    new
                    {
                        id = userId
                    });

                return user;
            }
            return null;
        }

        private bool Exists(string userId)
        {
            return (!(TryLoadUser(userId) == null));
        }
    }
}
