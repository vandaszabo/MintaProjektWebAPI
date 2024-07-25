using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MintaProjekt.Models
{
    [Table("tbl_employee")]
    public class Employee
    {
        [Key]
        [Column("employee_id")]
        public int EmployeeID { get; set; }
        [Column("first_name")]
        public string? FirstName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("hire_date")]
        public DateOnly HireDate { get; set; }
        [Column("job_title")]
        public string? JobTitle { get; set; }

        [Column("department_id")]
        public int DepartmentID { get; set; }

        // Computed property for full name
        public string FullName => $"{FirstName} {LastName}";

        public Employee()
        {
        }

        public Employee(int employeeID, string firstName, string lastName, string email, string phoneNumber, DateOnly hireDate, string jobTitle, int departmentId)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            HireDate = hireDate;
            JobTitle = jobTitle;
            DepartmentID = departmentId;
        }

        // Check for null values, must invoke before database insert
        public bool HasInvalidProperties()
        {
            return string.IsNullOrWhiteSpace(FirstName) ||
                   string.IsNullOrWhiteSpace(LastName) ||
                   string.IsNullOrWhiteSpace(Email) ||
                   PhoneNumber == null ||
                   string.IsNullOrWhiteSpace(PhoneNumber) ||
                   HireDate == DateOnly.MinValue ||
                   string.IsNullOrWhiteSpace(JobTitle) ||
                   (DepartmentID > 0 && DepartmentID < 8 );
        }

        // Create string representation
        public override string ToString()
        {
            return $"Employee ID: {EmployeeID}\n" +
                   $"First Name: {FirstName}\n" +
                   $"Last Name: {LastName}\n" +
                   $"Email: {Email}\n" +
                   $"Phone Number: {PhoneNumber}\n" +
                   $"Hire Date: {HireDate:yyyy-MM-dd}\n" +
                   $"Job Title: {JobTitle}\n" +
                   $"Department ID: {DepartmentID}";
        }
    }

}
