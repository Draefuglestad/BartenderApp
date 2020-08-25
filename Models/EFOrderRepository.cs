using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace BartenderApp.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Order> Orders => context.Orders
        .Include(o => o.Lines)
        .ThenInclude(l => l.Cocktail);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Cocktail));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}