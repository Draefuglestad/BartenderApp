using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderApp.Models;

namespace BartenderApp.Models.ViewModels
{
    public class CocktailsListViewModel
    {
        public IEnumerable<Cocktail> Cocktails { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
