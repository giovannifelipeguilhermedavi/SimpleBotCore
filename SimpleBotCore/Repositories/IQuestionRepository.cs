using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public interface IQuestionRepository
    {
        Task CreateAsync(string question);
    }
}