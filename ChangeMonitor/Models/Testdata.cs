using System;
using System.ComponentModel.DataAnnotations;

namespace ChangeMonitor.Models {
    public class Testdata {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        [Required]
        [MaxLength(256)]
        public string Value { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
