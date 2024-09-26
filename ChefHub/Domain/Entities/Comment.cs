using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int? Score { get; set; }
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
