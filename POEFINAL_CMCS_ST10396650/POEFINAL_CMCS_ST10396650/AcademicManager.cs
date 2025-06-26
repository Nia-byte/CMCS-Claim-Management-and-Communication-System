using System.ComponentModel.DataAnnotations;

namespace POEFINAL_CMCS_ST10396650
{
    public class AcademicManager
    {
        [Key] // This annotation marks the property as the primary key
        public int ManagerId { get; set; }
        public string fullName { get; set; }
        public string password { get; set; }

    }
}
