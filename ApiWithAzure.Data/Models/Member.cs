using ApiWithAzure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiWithAzure.Data.Models
{
   public class Member : BaseModel
    {
        [Required]
        [MaxLength(50)]       
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(12)]
        public string PhoneNo { get; set; }

        [MaxLength(2)]
        public string Age { get; set; }
    }
}
