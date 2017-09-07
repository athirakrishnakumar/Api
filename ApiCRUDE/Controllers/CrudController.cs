using ApiCRUDE;
using ApiCRUDE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiCRUDE.Controllers
{
    public class CrudController : ApiController
    {
        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetAllStudents()
        {
            List<Login> Logins = new List<Login>();
            using (var context = new AthiraEntities1())
            {
                Logins = context.Logins.ToList();
            }

            if (Logins.Count > 0)
                return Ok(Logins);
            else
                return Ok("No students found");
        }

        [HttpPost]
        [Route("insert")]
        public IHttpActionResult Insert(Login person)
        {
            AthiraEntities1 db = new AthiraEntities1();
            //  Login obj = new Login();
            //obj.Id = Id;
            //obj.Name = Name;
            //obj.Password = Password;
            //db.Logins.Add(obj);
            //db.SaveChanges();
            Login log = db.Logins.Where(c => c.Id == person.Id).FirstOrDefault();

            db.Logins.Add(person);
            db.SaveChanges();
            return Ok("Sucess");

        }

        [Route("details")]
        public IHttpActionResult GetStudentById(int Id)
        {
            AthiraEntities1 db = new AthiraEntities1();
            Login log = db.Logins.Where(c => c.Id == Id).FirstOrDefault();

            if (log != null)
                return Ok(log);
            else
                return Ok("No students found");
        }

        [HttpGet]
        [Route("editget")]
        public IHttpActionResult edit(int Id)
        {
            AthiraEntities1 db = new AthiraEntities1();
            Login logs = db.Logins.Where(c => c.Id == Id).FirstOrDefault();
            return Ok(logs);

        }

        [HttpPut]
        [Route("edit")]
        public IHttpActionResult edit(Login obj)
        {
            AthiraEntities1 db = new AthiraEntities1();
            Login logs = db.Logins.Where(c => c.Id == obj.Id).FirstOrDefault();
            db.Entry(logs).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return Ok("sucess");
        }
        
        [Route("delete")]

        public IHttpActionResult delete(int Id)
        {
            AthiraEntities1 db = new AthiraEntities1();
            Login log = db.Logins.Where(c => c.Id == Id).FirstOrDefault();
            db.Logins.Remove(log);
            db.SaveChanges();
            return Ok("Sucess");
        }

        [HttpGet]
        [Route("getcostum")]
        public MyCustomResult GetAllStudentsEntities(int Id)
        {

            AthiraEntities1 db = new AthiraEntities1();
            Login log = db.Logins.Where(c => c.Id == Id).FirstOrDefault();
            if (log != null)
            {
                return new MyCustomResult(log.Name, Request, HttpStatusCode.OK);
            }
            else
            {
                return new MyCustomResult("invalid id", Request, HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("returns")]

        public HttpResponseMessage Show(int id)
        {
            if (id == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "invalid");
        }



        [HttpGet]
       // [Route("Authen")]
        [AutherizationRequired]

        public HttpResponseMessage GetAuthToken(string usrname,string password)
            
        {
            ServiceClass _ServiceClass = new ServiceClass();
            var token = _ServiceClass.GenereteToken(usrname, password);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Autherized");
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", DateTime.Now.AddHours(2).ToString());
            response.Headers.Add("Acess Control Expose Header", "token, token expiry");
            return response;
        }
        [Route("Authen")]
        [HttpGet]
        [AutherizationRequired]
        public HttpResponseMessage Authenticate(string usrname, string password)
        {
            return GetAuthToken(usrname, password);
        }

        [HttpPost]
        [Route("Ather")]
        [AutherizationRequired]
        public IHttpActionResult PassCheck(string ide, string pw)
        {
            AthiraEntities1 db = new AthiraEntities1();
            Login api = new Login();
            using (var ctx = new AthiraEntities1())
            {
    api = (from n in ctx.Logins where n.Name== ide&& n.Password == pw   select n).FirstOrDefault();
            if (api.Name !=null)
            {
                    //api.Name = ide;
                    //api.Password=pw;
                    string Ntoken = Guid.NewGuid().ToString();
                    DateTime issueon = DateTime.Now;
                    DateTime expiredon = DateTime.Now.AddSeconds(50000000);
                    api.AuthToken = Ntoken;
                    api.IssuedOn = issueon;
                    api.ExpiredOn = expiredon;
                    db.Entry(api).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
}

            return Ok();
        }


    }


}

