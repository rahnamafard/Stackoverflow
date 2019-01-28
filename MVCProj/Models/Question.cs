using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProj.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Vote { get; set; }

        [InverseProperty("Question")]
        public List<Answer> Answers { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}