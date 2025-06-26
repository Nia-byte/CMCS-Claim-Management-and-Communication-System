using System.ComponentModel.DataAnnotations;

namespace POEFINAL_CMCS_ST10396650
{
    public class Lecture
    {
        [Key] // Ensure this is marked as the primary key
        public int lecturerId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }

        public string Course { get; set; }

        [Required]
        [EmailAddress] // Validates email format
        public string Email { get; set; }

        [Required]
        [Phone] // Validates phone number format
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)] // Ensures proper date format
        public DateTime HireDate { get; set; }

    }
}
