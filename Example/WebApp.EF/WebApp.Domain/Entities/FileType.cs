using SharpLite.Domain;
using System;
using System.Collections.Generic;

namespace WebApp.Domain.Entities
{
    public partial class FileType : Entity
    {
        public FileType()
        {
        }

        public string Label { get; set; }
        public string DescriptionKey { get; set; }
    }
}
