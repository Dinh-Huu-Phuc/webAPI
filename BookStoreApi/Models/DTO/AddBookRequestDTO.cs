﻿using System.ComponentModel.DataAnnotations;

namespace BookAPIStore.Models.DTO
{
    public class AddBookRequestDTO
    {
        [Required]
        [MinLength(10)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        //[Range(0,5)]
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        // Navigation Properties
        public int PublisherID { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
