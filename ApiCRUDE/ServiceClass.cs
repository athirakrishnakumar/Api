using ApiCRUDE.Controllers;
using ApiCRUDE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCRUDE
{
    public class ServiceClass : IService
    {
        public Login GenereteToken(string usrname, string password)
        {

            AthiraEntities1 db = new AthiraEntities1();
            //Login obj = new Login();

            Login obj = db.Logins.Where(c => c.Name == usrname).FirstOrDefault();
            if (obj != null)
            {
                if (obj.Password == password)
                {
                    string token = Guid.NewGuid().ToString();
                    DateTime IssuedOn = DateTime.Now;
                    DateTime ExpiredOn = DateTime.Now.AddSeconds(50000000);
                    var TokenKey = new TokenEntity()
                    {
                        Name = usrname,
                        AuthToken = token,
                        IssuedOn = IssuedOn,
                        ExpiresOn = ExpiredOn

                    };
                   
                    // obj.Id = Id;
                    //obj.Name = TokenKey.Name;
                    //obj.Password = TokenKey.Password;
                    obj.AuthToken = TokenKey.AuthToken;
                    obj.IssuedOn = TokenKey.IssuedOn;
                    obj.ExpiredOn = TokenKey.ExpiresOn;
                    db.Logins.Add(obj);
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }

                //}
                //AthiraEntities1 db = new AthiraEntities1();
                //Login api = new Login();
                //using (var ctx = new AthiraEntities1())
                //{
                //    api = (from n in ctx.Logins where n.Name == usrname && n.Password == password select n).FirstOrDefault();
                //    if (api.Name != null)
                //    {
                //        //api.Name = ide;
                //        //api.Password=pw;
                //        string Ntoken = Guid.NewGuid().ToString();
                //        DateTime issueon = DateTime.Now;
                //        DateTime expiredon = DateTime.Now.AddSeconds(50000000);
                //        api.AuthToken = Ntoken;
                //        api.IssuedOn = issueon;
                //        api.ExpiredOn = expiredon;
                //        db.Entry(api).State = System.Data.Entity.EntityState.Modified;
                //        db.SaveChanges();






                //    }
                //}
               
            }
            return obj;
        }

        public bool ValidateToken(string token)
        {
            AthiraEntities1 db = new AthiraEntities1();
            TokenEntity tok = new TokenEntity();
            Login log = db.Logins.Where(c => c.AuthToken == token).FirstOrDefault();
            if (log.AuthToken != null)
                return true;
            else
             return  false;
           
        }
    }
}