using GroceriesManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceriesManagement.Services
{
    public class GroceryService
    {
        private List<Grocery> grocery;
        public GroceryService() {
            grocery = new List<Grocery>();
            grocery.Add(new Grocery()
            {
                Grocery_Id = 1,
                Grocery_Name = "Apple",
                Grocery_Description = "Very red red apple",
                Grocery_Price = 10
            });
            grocery.Add(new Grocery() { 
                Grocery_Id = 2,
                Grocery_Name = "Mango",
                Grocery_Description = "Delicious Mango",
                Grocery_Price = 20
            });
        }
        public List<Grocery> GetAllGrocery()
        {
            return grocery;
        }
        public Grocery GetGrocery(int id) {
            Grocery groceri = grocery.FirstOrDefault(grocer => grocer.Grocery_Id == id);
            if (groceri == null) {
                return null;
            }
            return groceri;
        }
        public void AddGrocery(Grocery groceri)
        {
            groceri.Grocery_Id = grocery.Count + 1;
            grocery.Add(groceri);
        }
        public int UpdateGrocery(Grocery groceri)
        {
            Grocery grocers = GetGrocery(groceri.Grocery_Id);
            if (grocers == null) { 
                return -1;
            }
            grocers.Grocery_Name = groceri.Grocery_Name;
            grocers.Grocery_Description = groceri.Grocery_Description;
            grocers.Grocery_Price = groceri.Grocery_Price;
            return 1;
        }
        public int DeleteGrocery(int id)
        {
            Grocery groceri = GetGrocery(id);
            if (groceri == null) { return -1; }
            grocery.Remove(groceri);
            return 1;
        }
    }
}
