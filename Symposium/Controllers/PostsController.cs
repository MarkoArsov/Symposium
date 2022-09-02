using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Symposium.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Symposium.Controllers
{

    [Authorize]
    public class PostsController : Controller
    {


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [AllowAnonymous]
        public ActionResult Home()
        {
            ViewBag.userName = User.Identity.Name;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return View("AdminIndex", db.Posts.ToList());
            }
            var posts = db.Posts.ToList();
            posts.Reverse();
            return View(posts);
        }

        [AllowAnonymous]
        public ActionResult Search(string str)
        {
            if(str == "")
            {
                return RedirectToAction("Index");
            }
            var posts = db.Posts.Where(p => p.Title.Contains(str));
            ViewBag.SearchTerm = str;
            posts.Reverse();
            return View(posts);
        }



        public ActionResult RandomPost()
        {
            var posts = db.Posts.ToList();
            var random = new Random();
            var index = random.Next(posts.Count);
            var post = posts[index];
            return RedirectToAction("Details", new { id = post.PostId });
        }

        public ActionResult MyPosts()
        {
            string UserName = User.Identity.Name;
            var posts = db.Posts.Where(p => p.UserName == UserName).ToList();
            posts.Reverse();
            return View(posts);
        }

        public ActionResult MyLikedPosts()
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            var likedPosts = db.Posts.Where(p => p.LikedBy.Contains(user.UserName)).ToList();
            likedPosts.Reverse();
            return View(likedPosts);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            ViewBag.isLiked = false;
            if (post.LikedBy.Contains(user.UserName))
            {
                ViewBag.isLiked = true;
            }


            return View(post);
        }

        public ActionResult LikePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            
            
            if (post.LikedBy.Contains(user.UserName))
            {
                return RedirectToAction("Details", new { id = post.PostId });
            }

            post.LikedBy += (" " + user.UserName);
      
            post.Likes++;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = post.PostId });
        }

        public ActionResult DislikePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = UserManager.FindByName(User.Identity.Name);


            if (!post.LikedBy.Contains(user.UserName))
            {
                return RedirectToAction("Details", new { id = post.PostId });
            }


            var len = user.UserName.Length;
            var index = post.LikedBy.IndexOf(user.UserName);
            if (index != 0) index--;

            String start = post.LikedBy.Substring(0, index + 1);
            String end = post.LikedBy.Substring(index + len + 1);
            post.LikedBy = start + end;

            post.Likes--;

            db.SaveChanges();

            return RedirectToAction("Details", new { id = post.PostId });
        }


        public ActionResult AddComment(int postId, string newComment)
        {
            Comment comment = new Comment()
            {
                Content = newComment,
                PostId = postId
            };
           
            comment.UserName = User.Identity.Name;

            db.Posts.Find(postId).Comments.Add(comment);
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = postId });
        }

        
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,UserName,Title,Content,Likes,ImgURL,CreatedDate,LikedBy")] Post post)
        {
            if (ModelState.IsValid)
            {
             
                post.CreatedDate = DateTime.Now;
                post.UserName = User.Identity.Name;
                post.Likes = 0;
                post.LikedBy = "";
                
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,UserName,Title,Content,Likes,ImgURL,CreatedDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                post.CreatedDate = DateTime.Now;
                post.UserName = User.Identity.Name;
                post.Likes = 0;
                post.LikedBy = "";

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult MyDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("MyPosts");
        }

        public ActionResult AdminDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
