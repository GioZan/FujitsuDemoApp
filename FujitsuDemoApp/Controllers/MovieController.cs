using FujitsuDemoApp.App_Start;
using FujitsuDemoApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FujitsuDemoApp.Controllers
{
    public class MovieController : Controller
    {
        MongoContext _dbContext;
        public MovieController()
        {
            _dbContext = new MongoContext();
        }
        // GET: Movie
        public ActionResult Index()
        {
            var movieDetails = _dbContext._database.GetCollection<Movie>("movie");
            return View(movieDetails.Find(_ => true).ToList());
        }
        // GET: Movie/Details/5
        public ActionResult Details(string id)
        {
            var movieDetail = _dbContext._database.GetCollection<Movie>("movie").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();
            return View(movieDetail);
        }
        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            var document = _dbContext._database.GetCollection<Movie>("movie");
            // Check duplicates
            var count = document.CountDocuments(x => x.title == movie.title);
            if (count == 0)
                document.InsertOne(movie);
            else
            {
                TempData["Message"] = "Movie Title already exists.";
                return View("Create", movie);
            }
            return RedirectToAction("Create");
        }
        // GET: Movie/Edit/5
        public ActionResult Edit(string id)
        {
            var document = _dbContext._database.GetCollection<Movie>("movie");
            var moviecount = document.CountDocuments(x => x.Id == new ObjectId(id));
            if (moviecount > 0)
            {
                var moviedetail = document.Find(x => x.Id == new ObjectId(id)).FirstOrDefault();
                return View(moviedetail);
            }
            return RedirectToAction("Index");
        }
        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Movie movie)
        {
            try
            {
                movie.Id = new ObjectId(id);
                var collection = _dbContext._database.GetCollection<Movie>("movie");
                var result = collection.FindOneAndReplace<Movie>(x => x.Id == new ObjectId(id), movie);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var filter = Builders<Movie>.Filter.Eq("id", id);
            var movie = _dbContext._database.GetCollection<Movie>("movie").Find(x => x.Id == new ObjectId(id)).FirstOrDefault();

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Movie Movie)
        {
            try
            {
                var collection = _dbContext._database.GetCollection<Movie>("movie");
                var result = collection.DeleteOne(x => x.Id == new ObjectId(id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}