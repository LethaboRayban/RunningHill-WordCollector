using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordCollector.Shared
{
    public class WordEntry
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Word is required.")]
        [StringLength(100)]
        public string Word { get; set; } = string.Empty;

        [Required]
        public WordType Type { get; set; }

    }
}
