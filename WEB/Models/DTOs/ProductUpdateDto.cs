﻿using System.ComponentModel.DataAnnotations;

namespace WEB.Models.DTOs
{
    public class ProductUpdateDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
