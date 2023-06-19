﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> AllChefs = _context.Chefs.Include(c => c.AllDishes).ToList();
        return View(AllChefs);
    }

    [HttpGet("chefs/new")]
    public IActionResult NewChef()
    {
        return View();
    }

    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View("NewChef");
        }
    }

    [HttpGet("dishes")]
    public IActionResult ShowDishes()
    {
        List<Dish> AllDishes = _context.Dishes.Include(d => d.Chef).ToList();
        return View(AllDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View();
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("ShowDishes");
        }
        else
        {
            ViewBag.AllChefs = _context.Chefs.ToList();
            return View("NewDish");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}