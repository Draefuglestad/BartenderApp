using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderApp.Models;


//A repository interface can be used for mapping the data from a business entity to the data source,
//querying the data source for the data, and persists changes in the business entity to the data source.

namespace BartenderApp.Models
{
    public interface ICocktailRepository
    {
        IEnumerable<Cocktail> Cocktails { get; }
        //This interface uses IEnumerable<T> to allow a caller to obtain a sequence of Cocktail objects, 
        //without saying how or where the data is stored or retrieved.
        //A class that depends on the ICocktailRepository interface can obtain Cocktail objects without needing 
        //to know anything about where they are coming from or how the implementation class will deliver them. 
        //We will revisit the ICocktailRepository interface throughout the development process to add features.
    }
}
