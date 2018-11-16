using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace EasyMobileApp.Models
{
    public class EasyTableModel : TableEntity
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

    }
}