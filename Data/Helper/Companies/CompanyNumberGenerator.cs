using Entity.Companies;
using Microsoft.EntityFrameworkCore;

namespace Data.Helper.Companies
{
    public static class CompanyNumberGenerator
    {
        public static async Task HandleNewCompanyNumbersAsync(DbContext context)
        {
            var newCompanies = context.ChangeTracker.Entries<Company>()
                .Where(e => e.State == EntityState.Added && string.IsNullOrEmpty(e.Entity.CompanyNumber))
                .Select(e => e.Entity)
                .ToList();

            foreach (var company in newCompanies)
            {
                company.CompanyNumber = await GenerateUniqueCompanyNumberAsync(context);
            }
        }

        private static async Task<string> GenerateUniqueCompanyNumberAsync(DbContext context)
        {
            string today = DateTime.UtcNow.ToString("yyyyMMdd");
            string prefix = $"TCL-{today}";

            var existingNumbers = await context.Set<Company>()
                .Where(c => c.CompanyNumber.StartsWith(prefix))
                .Select(c => c.CompanyNumber)
                .ToListAsync();

            int maxSuffix = existingNumbers
                .Select(n => int.Parse(n.Substring(prefix.Length)))
                .DefaultIfEmpty(0)
                .Max();

            return $"{prefix}{(maxSuffix + 1):D3}";
        }
    }
}
