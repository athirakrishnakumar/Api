using ApiCRUDE.Controllers;
using ApiCRUDE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiCRUDE
{
    public interface  IService
    {
         Login GenereteToken(string usrname, string password);

         bool ValidateToken(string token);
    }
}