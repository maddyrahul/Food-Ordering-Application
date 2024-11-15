using Data_Accesss_Layer.Data;
using Data_Accesss_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accesss_Layer.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApplicationDbContext _context;

        public RestaurantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            return restaurant;
        }

        public Menu AddMenuItem(Menu menuItem)
        {
            _context.Menus.Add(menuItem);
            return menuItem;
        }

        public Menu GetMenuItemById(string menuId)
        {
            return _context.Menus.Find(menuId);
        }

        public void UpdateMenuItem(Menu menuItem)
        {
            _context.Menus.Update(menuItem);
        }

        public void DeleteMenuItem(Menu menuItem)
        {
            _context.Menus.Remove(menuItem);
        }

        public List<Review> GetReviewsByRestaurantId(string restaurantId)
        {
            return _context.Reviews.Where(r => r.RestaurantId == restaurantId).ToList();
        }

        public Review GetReviewById(string reviewId)
        {
            return _context.Reviews.Find(reviewId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
