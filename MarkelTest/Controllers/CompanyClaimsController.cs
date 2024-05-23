using MarkelTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Company = MarkelTest.Models.Company;
using Claim = MarkelTest.Models.Claim;

namespace MarkelTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyClaimsController : ControllerBase
    {
        private readonly List<Company> companies = Helpers.GenerateCompanies().ToList();
        private readonly List<Claim> claims = Helpers.GenerateClaims().ToList();
        private readonly List<ClaimType> claimTypes = Helpers.GenerateClaimTypes().ToList();

        [HttpGet]
        [Route("GetCompanyById")]
        public IActionResult GetCompanyById(int companyId)
        {
            if (companyId < companies.Count())
            {
                var result = companies.Where(x => x.Id == companyId).FirstOrDefault();
                if (result != null)
                {
                    return this.Ok(result);
                }
            }

            return this.NotFound($"Could not find company matching ID: {companyId}");
        }

        [HttpGet]
        [Route("GetClaimsByCompany")]
        public IActionResult GetClaimsByCompany(int companyId)
        {
            var result = claims.Where(x => x.CompanyId == companyId).ToList();
            if (result != null && result.Count() > 0)
            {
                return Ok(result);
            }

            return NotFound($"No claims found for company ID: {companyId}");
        }

        [HttpGet]
        [Route("GetClaimById")]
        public IActionResult GetClaimById(int claimId) 
        {
            if (claimId < claims.Count())
            {
                var result = this.claims.Where(x => x.Id == claimId).FirstOrDefault();
                if (result != null)
                {
                    return Ok(result);
                }
            }

            return NotFound($"No claims found for claim ID: {claimId}");
        }

        [HttpPut]
        [Route("UpdateClaim")]
        public IActionResult UpdateClaim([FromBody] Claim claim)
        {
            var existingClaim = claims.Where(x => x.Id == claim.Id).FirstOrDefault();
            if (existingClaim == null)
            {
                return NotFound($"No claims found for claim ID: {claim.Id}");
            }

            existingClaim.UCR = claim.UCR;
            existingClaim.CompanyId = claim.CompanyId;
            existingClaim.ClaimDate = claim.ClaimDate;
            existingClaim.LossDate = claim.LossDate;
            existingClaim.AssuredName = claim.AssuredName;
            existingClaim.IncurredLoss = claim.IncurredLoss;
            existingClaim.Closed = claim.Closed;

            return Ok(existingClaim);
        }
    }
}
