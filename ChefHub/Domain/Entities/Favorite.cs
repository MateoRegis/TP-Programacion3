using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public List<User>? Users { get; set; }
        public List<Recipe>? Recipes { get; set; }
    }
}
