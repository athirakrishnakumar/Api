using ApiCRUDE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ApiCRUDE.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            IEnumerable <Login> Logins = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63320/");
                //HTTP GET
                var responseTask = client.GetAsync("list");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Login>>();
                    readTask.Wait();

                    Logins = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Logins = Enumerable.Empty<Login>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Logins);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Login log)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63320/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("insert", log);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(log);
        }

        public ActionResult Details(int Id)
        {
            Login log = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63320/");
                //HTTP GET
                var responseTask = client.GetAsync("details?id=" + Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Login>();
                    readTask.Wait();

                    log = readTask.Result;
                }
            }

            return View(log);
        }

        public ActionResult Edit(int Id)
        {
            Login log = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63320/");
                //HTTP GET
                var responseTask = client.GetAsync("editget?id=" + Id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Login>();
                    readTask.Wait();

                    log = readTask.Result;
                }
            }

            return View(log);
        }

        [HttpPost]
        public ActionResult Edit(Login log)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63320/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync("edit", log);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(log);
        }

        public ActionResult Delete(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63320/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("delete?id=" + Id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

    }
}
    
