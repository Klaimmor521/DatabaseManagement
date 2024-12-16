using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; } 

        public int UserId { get; set; } 

        public int GameId { get; set; } 

        public DateTime AddedDate { get; set; } = DateTime.Now; 

        public virtual User User { get; set; } 
        public virtual Game Game { get; set; } 

        public void AddToWishlist()
        {

        }
        public void DeleteFromWishlist()
        {

        }
        public void ListAllGamesFromWishlist()
        {

        }
    }
}