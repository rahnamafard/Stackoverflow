using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProj.Models
{
    public class QuestionPageModel
    {
        public Question Question { get; set; }
        public string Questioner { get; set; }

        public Answer newAnswer { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
