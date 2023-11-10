using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Dtos
{
    public class EmployeeCreateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
    }
}
