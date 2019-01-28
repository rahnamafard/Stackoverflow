using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProj.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Vote { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [InverseProperty("Answer")]
        public List<AnswerLike> Likes { get; set; }

        [InverseProperty("Answer")]
        public List<AnswerDislike> Dislikes { get; set; }
    }
}
