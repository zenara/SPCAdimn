using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminSiteMVC.Models;

namespace AdminSiteMVC.Models.ViewModels
{
    public class CurrentPolicy
    {
        public IEnumerable<policy> policies { get; set; }
        public IEnumerable<version> versions { get; set; }
        public IEnumerable<section> sections { get; set; }
    }
}