using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
    public class SubscriptionViewModel
    {

        public Subscription Subscription { get; set; }

        public List<int>? BenefitIDs { get; set; }

        public IEnumerable<SelectListItem>? Benefits { get; set; }
    }
}
