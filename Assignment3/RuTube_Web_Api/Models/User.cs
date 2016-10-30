using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RuTube_Web_Api.SQLite
{
    public class User
    {
        [Key]
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string realName { get; set; }
    }
}