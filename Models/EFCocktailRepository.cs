using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderApp.Models
{
    public class EFCocktailRepository : ICocktailRepository
    {
        private ApplicationDbContext context;
        public EFCocktailRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Cocktail> Cocktails => context.Cocktails;
    }
}
