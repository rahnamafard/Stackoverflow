using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProj.Models
{
    public class AnswerLike
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }
    }
}
