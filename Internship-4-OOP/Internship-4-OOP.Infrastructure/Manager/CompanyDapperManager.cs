using Internship_4_OOP.Domain.Entities.Company;

namespace Internship_4_OOP.Infrastructure.Manager;

public class CompanyDapperManager(string connectionString) : DapperManager<Company>(connectionString);
