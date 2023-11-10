using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string IdNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int PositionId { get; set; }
        [Required]
        public int DepartmentId { get; set; }

       
        public Position Position { get; set; }
        
        public Department Department { get; set; }
    }
}
