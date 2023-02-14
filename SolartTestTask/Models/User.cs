using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolartTestTask.Models
{
    public  class User
    {
        public int UserID { get; set; }

        public string? UserName { get; set; }

        public DateTime birthDay { get; set; }

        public DateTime nearestbirthDay { get; set; }

        public TimeSpan TimeBeforeBirthday
        {
            get => DateTime.Now - nearestbirthDay;
        }
       
    }
}
