using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProj.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public int Score { get; set; }

        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
