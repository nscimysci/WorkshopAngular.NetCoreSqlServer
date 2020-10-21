using System;
using System.Collections.Generic;

namespace APPAPI.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public int? Roleid { get; set; }
        public bool? Active { get; set; }
        public DateTime? Createddate { get; set; }
        public string Createdby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public string Modifiedby { get; set; }
    }
}
