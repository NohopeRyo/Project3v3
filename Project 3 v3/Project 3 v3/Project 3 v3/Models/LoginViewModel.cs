using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_3_v3.Models
{
    public class LoginViewModel
    {
        
        public int UserID { get; set; }
       
        public string UserName { get; set; }
        
        public string Email { get; set; }
       
        public string Password { get; set; }
        

    }
}