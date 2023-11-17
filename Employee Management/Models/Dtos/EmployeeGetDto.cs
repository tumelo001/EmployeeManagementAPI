using Employee_Management.Data;
using Employee_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models.Dtos
{
    public class EmployeeGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }
    }
}
