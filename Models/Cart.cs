using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//The Cart class uses the CartLine class, defined in the same file, 
//to represent a cocktail selected by the customer and the quantity the user wants to buy.
//We defined methods to add an item to the cart, remove a previously added item from the cart, 
//calculate the total cost of the items in the cart, and reset the cart by removing all the items.
//We also provided a property that gives access to the contents of the cart using an IEnumerable<CartLine>. 
//This is all straightforward stuff, easily implemented in C# with the help of a little LINQ.

namespace BartenderApp.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Cocktail cocktail, int quantity)
        {
            CartLine line = lineCollection
            .Where(p => p.Cocktail.CocktailID == cocktail.CocktailID)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Cocktail = cocktail,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Cocktail cocktail) => 
            lineCollection.RemoveAll(l => l.Cocktail.CocktailID == cocktail.CocktailID);
        public virtual decimal ComputeTotalValue() =>
        lineCollection.Sum(e => e.Cocktail.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Cocktail Cocktail { get; set; }
        public int Quantity { get; set; }
    }
}