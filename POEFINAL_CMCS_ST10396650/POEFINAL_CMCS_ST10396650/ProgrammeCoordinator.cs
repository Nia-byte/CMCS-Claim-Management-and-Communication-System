using System.ComponentModel.DataAnnotations;

namespace POEFINAL_CMCS_ST10396650
{
    public class ProgrammeCoordinator
    {
        [Key]
        public int CoordinatorId { get; set; }
        public string fullName { get; set; }
        public string password { get; set; }


    }
}
