using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProj.Models
{
    public class User : IdentityUser
    {
        public int Score { get; set; }

        [InverseProperty("User")]
        public List<Question> Questions { get; set; }

        [InverseProperty("User")]
        public List<Answer> Answers { get; set; }
    }
}
