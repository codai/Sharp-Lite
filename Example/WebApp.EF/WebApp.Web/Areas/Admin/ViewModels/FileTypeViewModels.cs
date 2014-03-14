using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Web.Areas.Admin.ViewModels
{
    public class EditFileTypeViewModel
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string DescriptionKey { get; set; }
    }
}