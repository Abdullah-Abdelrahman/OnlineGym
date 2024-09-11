using Microsoft.AspNetCore.Http;
using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
    public class ClientViewModel
    {
        public Client Client { get; set; }

        public IFormFile? formFile { get; set; }
    }
}
