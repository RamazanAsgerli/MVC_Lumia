﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Team : BaseEntity
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Position { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Description { get; set; }

        public string? ImgUrl {  get; set; }

        [NotMapped]
        public IFormFile PhotoFile { get; set; }
    }
}
