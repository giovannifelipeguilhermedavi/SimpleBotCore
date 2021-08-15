using SimpleBotCore.Logic;

namespace SimpleBotCore.Repositories
{
    public interface IUserProfileRepository
    {
        SimpleUser TryLoadUser(string userId);

        SimpleUser Create(SimpleUser user);

        void AtualizaNome(string userId, string name);

        void AtualizaIdade(string userId, int idade);

        void AtualizaCor(string userId, string name);
    }
}