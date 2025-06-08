using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groceries.DataAccess.Models;

namespace Groceries.DataAccess.Repository
{
    public  class GroceriesRepository
    {
        private readonly GroceriesDbContext _groceryDbContext;

        public GroceriesRepository(GroceriesDbContext groceryDbContext)
        {
            _groceryDbContext = groceryDbContext;
        }
        public List<Grocery> GetAllGrocery()
        {
            return _groceryDbContext.Groceries.ToList();
        }
        public Grocery GetGrocery(int id)
        {
            Grocery groceri = _groceryDbContext.Groceries.FirstOrDefault(grocer => grocer.Id == id);
            if (groceri == null)
            {
                return null;
            }
            return groceri;
        }
        public void AddGrocery(Grocery groceri)
        {
            
            _groceryDbContext.Groceries.Add(groceri);
            _groceryDbContext.SaveChanges();
        }
        public int UpdateGrocery(Grocery groceri)
        {
            Grocery grocers = GetGrocery(groceri.Id);
            if (grocers == null)
            {
                return -1;
            }
            grocers.Name = groceri.Name;
            grocers.Category = groceri.Category;
            grocers.Price = groceri.Price;
            _groceryDbContext.SaveChanges();
            return 1;
        }
        public int DeleteGrocery(int id)
        {
            Grocery groceri = GetGrocery(id);
            if (groceri == null) { return -1; }
            _groceryDbContext.Groceries.Remove(groceri);
            _groceryDbContext.SaveChanges();
            return 1;
        }
        public List<Grocery> GetFilteredCategory(string category)
        {
            List<Grocery> groceries = _groceryDbContext.Groceries.Where(book => book.Category.Equals(category)).ToList();
            return groceries;
        }
    }
}
