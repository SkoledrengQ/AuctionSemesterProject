using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}