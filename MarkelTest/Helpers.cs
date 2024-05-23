using Bogus;
using MarkelTest.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Claim =  MarkelTest.Models.Claim;


namespace MarkelTest
{
    public static class Helpers
    {
        private static int count { get; } = 500;
        public static IEnumerable<Claim> GenerateClaims()
        {
            var ucrs = new[] { "qwe", "asd", "zxc", "rty", "fgh", "vbn" };
            var claimFaker = new Faker<Claim>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.UCR, f => f.PickRandom(ucrs))
                .RuleFor(x => x.CompanyId, f => f.Random.Int(0, count))
                .RuleFor(x => x.ClaimDate, f => f.Date.Past(f.System.Random.Int(1, 50)))
                .RuleFor(x => x.LossDate, f => f.Date.Past())
                .RuleFor(x => x.AssuredName, f => f.Hacker.IngVerb())
                .RuleFor(x => x.IncurredLoss, f => f.Random.Decimal())
                .RuleFor(x => x.Closed, f => f.Random.Bool());
            var claims = claimFaker.Generate(count*100); // generate loads of claims so we can actually find one by company id later.
            return claims;
        }

        public static IEnumerable<ClaimType> GenerateClaimTypes()
        {
            var claimTypeFaker = new Faker<ClaimType>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Name, f => f.Music.Random.Word());
            var claimTypes = claimTypeFaker.Generate(count);
            return claimTypes;
        }

        public static IEnumerable<Company> GenerateCompanies() 
        {
            var companyFaker = new Faker<Company>()
                .RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Name, f => f.Company.CompanyName())
                .RuleFor(x => x.Address1, f => f.Address.StreetAddress())
                .RuleFor(x => x.Postcode, f => f.Address.ZipCode())
                .RuleFor(x => x.Active, f => f.Random.Bool())
                .RuleFor(x => x.InsuranceEndDate, f => f.Date.Future());    
            var companies = companyFaker.Generate(count);
            return companies;
        }
    }
}
