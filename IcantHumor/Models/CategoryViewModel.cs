﻿using System.ComponentModel.DataAnnotations;

namespace IcantHumor.Models
{
    public class CategoryViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<MediaViewModel> Posts { get; set; } = new();
    }
}
