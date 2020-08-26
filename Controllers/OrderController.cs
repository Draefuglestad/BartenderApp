using Microsoft.AspNetCore.Mvc;
using BartenderApp.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BartenderApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BartenderApp.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }
        public ViewResult CompletedList() => View(repository.Orders.Where(o => !o.PickUpDrink && o.DrinksMade));
        [HttpPost]
        public IActionResult PickedUp(int orderID)
        {
            Order order = repository.Orders
            .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.PickUpDrink = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(CompletedList));
        }//testing push

        [Authorize]
        public ViewResult List()
        {
            return View(repository.Orders.Where(o => !o.DrinksMade)); 
        }
        [HttpPost]
        [Authorize]
        public IActionResult MarkMade(int orderID)
        {
            Order order = repository.Orders
            .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.DrinksMade = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }

    }
}