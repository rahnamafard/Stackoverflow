﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProj.Models;

namespace MVCProj.Controllers
{
    public class QuestionController : Controller
    {
        StackContext db;
        SignInManager<User> SignInManager;
        UserManager<User> UserManager;

        public QuestionController(StackContext db, SignInManager<User> SignInManager, UserManager<User> UserManager)
        {
            this.db = db;
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
        }

        public IActionResult Index()
        {
            var QuestionList = db.Questions.ToList();
            return View(QuestionList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Question question)
        {
            if(ModelState.IsValid)
            {
                question.UserId = UserManager.GetUserId(User);
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index", "Question");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Show(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

    }
}