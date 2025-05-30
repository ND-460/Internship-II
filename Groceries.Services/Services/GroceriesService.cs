using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groceries.DataAccess.Models;
using Groceries.DataAccess.Repository;

namespace Groceries.Services.Services
{
    public class GroceriesService
    {
        private readonly GroceriesRepository _groceriesRepository;
        public GroceriesService(GroceriesRepository groceriesRepository)
        {
            _groceriesRepository = groceriesRepository;
        }
        public List<Grocery> GetAllGrocery()
        {
            return _groceriesRepository.GetAllGrocery();
        }
        public Grocery GetGrocery(int id)
        {
            return _groceriesRepository.GetGrocery(id);
        }
        public void AddGrocery(Grocery groceri)
        {
             _groceriesRepository.AddGrocery(groceri);
        }
        public int UpdateGrocery(Grocery groceri)
        {
            return _groceriesRepository.UpdateGrocery(groceri);
        }
        public int DeleteGrocery(int id)
        {
           return _groceriesRepository.DeleteGrocery(id);
        }
        public List<Grocery> GetFilteredCategory(string category) 
        {
            return _groceriesRepository.GetFilteredCategory(category);
        }
    }
}
