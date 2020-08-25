using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BartenderApp.Models;
using BartenderApp.Models.ViewModels;
namespace BartenderApp.Controllers
{
    public class CartController : Controller
    {
        private ICocktailRepository repository;
        private Cart cart;
        public CartController(ICocktailRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToCart(int cocktailId, string returnUrl)
        {
            Cocktail cocktail = repository.Cocktails
            .FirstOrDefault(p => p.CocktailID == cocktailId);
            if (cocktail != null)
            {
                cart.AddItem(cocktail, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int cocktailId, string returnUrl)
        {
            Cocktail cocktail = repository.Cocktails
            .FirstOrDefault(p => p.CocktailID == cocktailId);
            if (cocktail != null)
            {
                cart.RemoveLine(cocktail);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}