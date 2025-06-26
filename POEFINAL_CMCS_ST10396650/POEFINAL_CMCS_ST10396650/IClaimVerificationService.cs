using System.Security.Claims;

namespace POEFINAL_CMCS_ST10396650
{
    public interface  IClaimVerificationService
    {
        bool VerifyClaim(Claim claim);
        string[] GetValidationErrors(Claim claim);
    }
}
