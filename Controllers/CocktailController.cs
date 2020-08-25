using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BartenderApp.Models;
using BartenderApp.Models.ViewModels;

namespace BartenderApp.Controllers
{
    public class CocktailController : Controller
    {
        private ICocktailRepository repository;
        public int PageSize = 4;
        public CocktailController(ICocktailRepository repo)
        {
            repository = repo;
        }

        //the List parameters are set to default to page one if the method call does not specify it
        public ViewResult List(int page = 1) => View(new CocktailsListViewModel { 
                Cocktails = repository.Cocktails
                .OrderBy(p => p.CocktailID)
                .Skip((page - 1) * PageSize)//skip over the cocktails that occur before the start of the current page
                .Take(PageSize),//take the number of cocktails specified by the PageSize field.
             PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Cocktails.Count()
                }
            });
    }
}


