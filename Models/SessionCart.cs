using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using BartenderApp.Infrastructure;
namespace BartenderApp.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
            ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Cocktail cocktail, int quantity)
        {
            base.AddItem(cocktail, quantity); ;
            Session.SetJson("Cart", this);
        }
        public override void RemoveLine(Cocktail cocktail)
        {
            base.RemoveLine(cocktail);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
//The SessionCart class subclasses the Cart class and overrides the AddItem, RemoveLine, and Clear methods,
//so they call the base implementations and then store the updated state in the session using the extension 
//methods on the ISession interface. The static GetCart method is a factory for creating SessionCart objects
//and providing them with an ISession object so they can store themselves.
//Getting hold of the ISession object is a little complicated.We have to obtain an instance of the 
//IHttpContextAccessor service, which provides me with access to an HttpContext object that, in turn, 
//provides us with the ISession.This around-about approach is required because the session isn’t provided as a regular service.