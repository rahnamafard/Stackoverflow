using System;
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
            if (ModelState.IsValid)
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
            QuestionPageModel qpm = new QuestionPageModel();

            Question question = db.Questions.Find(id);

            int numOfLikes = db.QuestionLikes.Where(l => l.UserId == UserManager.GetUserId(User)).Count();
            if (numOfLikes > 0)
            {
                ViewData["upVoteValue"] = "Liked";
                ViewData["upVoteClass"] = "btn-success";
            }
            else
            {
                ViewData["upVoteValue"] = "Like";
                ViewData["upVoteClass"] = "btn-outline-success";

            }

            int numOfDislikes = db.QuestionDislikes.Where(l => l.UserId == UserManager.GetUserId(User)).Count();
            if (numOfDislikes > 0)
            {
                ViewData["downVoteValue"] = "Disliked";
                ViewData["downVoteClass"] = "btn-danger";
            }
            else
            {
                ViewData["downVoteValue"] = "Dislike";
                ViewData["downVoteClass"] = "btn-outline-danger";
            }

            qpm.Question = question;
            qpm.Questioner = db.Users.Find(question.UserId).UserName;

            qpm.Answers = db.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();

            return View(qpm);
        }


        [HttpPost]
        public IActionResult Answer(QuestionPageModel qpm)
        {
            if (ModelState.IsValid)
            {
                qpm.newAnswer.UserId = UserManager.GetUserId(User);
                db.Answers.Add(qpm.newAnswer);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Show), new { id = qpm.newAnswer.QuestionId });
        }


        public IActionResult upVoteQuestion(QuestionLike ql)
        {
            if (ModelState.IsValid)
            {
                int numOfLikes = db.QuestionLikes.Where(l => l.UserId == UserManager.GetUserId(User)).Count();
                if (numOfLikes > 0)
                {
                    User user = db.Users.Find(db.Questions.Find(ql.QuestionId).UserId);
                    user.Score--;
                    db.QuestionLikes.Remove(db.QuestionLikes.Where(qls => qls.UserId == UserManager.GetUserId(User)).ToList()[0]);
                    db.SaveChanges();
                }
                else
                {
                    User user = db.Users.Find(db.Questions.Find(ql.QuestionId).UserId);
                    user.Score++;
                    db.QuestionLikes.Add(ql);
                    db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Show), new { id = ql.QuestionId });
        }

        public IActionResult downVoteQuestion(QuestionDislike qdl)
        {
            if (ModelState.IsValid)
            {
                int numOfDislikes = db.QuestionDislikes.Where(l => l.UserId == UserManager.GetUserId(User)).Count();
                if (numOfDislikes > 0)
                {
                    User user = db.Users.Find(db.Questions.Find(qdl.QuestionId).UserId);
                    user.Score++;
                    db.QuestionDislikes.Remove(db.QuestionDislikes.Where(qdls => qdls.UserId == UserManager.GetUserId(User)).ToList()[0]);
                    db.SaveChanges();
                }
                else
                {
                    User user = db.Users.Find(db.Questions.Find(qdl.QuestionId).UserId);
                    user.Score--;
                    db.QuestionDislikes.Add(qdl);
                    db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Show), new { id = qdl.QuestionId });
        }

        public IActionResult upVoteAnswer(AnswerLike al)
        {
            if (ModelState.IsValid)
            {
                int numOfLikes = db.AnswerLikes.Where(l => l.UserId == UserManager.GetUserId(User)).Count();
                if (numOfLikes > 0)
                {
                    User user = db.Users.Find(db.Answers.Find(al.AnswerId).UserId);
                    user.Score--;
                    db.AnswerLikes.Remove(db.AnswerLikes.Where(als => als.UserId == UserManager.GetUserId(User)).ToList()[0]);
                    db.SaveChanges();
                }
                else
                {
                    User user = db.Users.Find(db.Answers.Find(al.AnswerId).UserId);
                    user.Score++;
                    db.AnswerLikes.Add(al);
                    db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Show), new { id = al.AnswerId });
        }

        public IActionResult downVoteAnswer(AnswerDislike adl)
        {
            if (ModelState.IsValid)
            {
                int numOfDislikes = db.AnswerDislikes.Where(l => l.UserId == UserManager.GetUserId(User)).Count();
                if (numOfDislikes > 0)
                {
                    User user = db.Users.Find(db.Answers.Find(adl.AnswerId).UserId);
                    user.Score++;
                    db.AnswerDislikes.Remove(db.AnswerDislikes.Where(adls => adls.UserId == UserManager.GetUserId(User)).ToList()[0]);
                    db.SaveChanges();
                }
                else
                {
                    User user = db.Users.Find(db.Answers.Find(adl.AnswerId).UserId);
                    user.Score--;
                    db.AnswerDislikes.Add(adl);
                    db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Show), new { id = adl.AnswerId });
        }
    }
}