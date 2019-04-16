using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskDocuments.Models
{
    public class RolesOFUser
    {
        public Role KindOfRole { get; set; }

        public enum Role
        {
            [Display(Name = "Subscriber")]
            Subscriber,
            [Display(Name = "Editor")]
            Editor,
            [Display(Name = "Admin")]
            Admin
        }
    }
}
