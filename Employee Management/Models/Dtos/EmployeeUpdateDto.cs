namespace Employee_Management.Models.Dtos
{
    public class EmployeeUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
