//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiCRUDE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Login
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
        public Nullable<System.DateTime> IssuedOn { get; set; }
        public Nullable<System.DateTime> ExpiredOn { get; set; }
    }
}
