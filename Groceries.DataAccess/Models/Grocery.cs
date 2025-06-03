using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groceries.DataAccess.Models
{
    public class Grocery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }


    }
}
