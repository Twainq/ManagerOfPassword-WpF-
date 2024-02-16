using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class Product
    {

        public string Service { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Product() {
        
        }

        public Product(string nameService, string loginService, string passwordService)
        {
            this.Service = nameService;
            this.Login = loginService;
            this.Password = passwordService;
        }   
    }


}
