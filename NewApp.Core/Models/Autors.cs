using NewApp.Core.Entities;
using NewApp.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewApp.Core.Models
{
    public class Autors : AuditableEntity
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string ProfileImage { get; set; }
        public string OtherInfo { get; set; }
        public List<Book> Books { get; set; }
    }
}
