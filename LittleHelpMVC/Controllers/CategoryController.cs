﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleHelpMVC.Data;
using LittleHelpMVC.Models;
using LittleHelpMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LittleHelpMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly LittleHelpMVCDbContext context;

        public CategoryController(LittleHelpMVCDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<HelpCategory> categories = context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                HelpCategory helpCategory = new HelpCategory
                {
                    Name = addCategoryViewModel.Name
                };

                context.Categories.Add(helpCategory);
                context.SaveChanges();

                return RedirectToAction("Category","Admin");
            }
            return View(addCategoryViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.categories = context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] catIds)
        {
            foreach (int catId in catIds)
            {
                HelpCategory theCategory = context.Categories.Single(c => c.ID == catId);

                context.Categories.Remove(theCategory);
            }
            context.SaveChanges();
            return RedirectToAction("Category", "Admin");
        }


    }
}
