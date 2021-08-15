using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class QuestionMockRepository : IQuestionRepository
    {
        private Dictionary<int, string> _questions = new Dictionary<int, string>();

        public Task CreateAsync(string question)
        {
            return Task.Factory.StartNew(() => SaveQuestion(question));
        }

        private void SaveQuestion(string text)
        {
            Random random = new Random();

            _questions.Add(random.Next(int.MaxValue), text);
        }
    }
}