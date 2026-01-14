namespace ASP_NET_Middleware.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(1, 150)]
        public int Age { get; set; }

        [Required]
        public string Group { get; set; } = string.Empty;

        public bool IsGrade { get; set; }
    }
}