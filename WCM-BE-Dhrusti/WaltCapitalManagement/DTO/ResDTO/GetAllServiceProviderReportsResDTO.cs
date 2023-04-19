using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllServiceProviderReportsResDTO
    {
        public int Id { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ExternalAccountNo { get; set; }
        public string ClientAccountNo { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public decimal AccountValue { get; set; }
        public decimal InvestmentValue { get; set; }
        public decimal PortfolioValue { get; set; }
        public string AccountValueStr { get; set; }
        public string InvestmentValueStr { get; set; }
        public string PortfolioValueStr { get; set; }
        public string Currency { get; set; }
        public int IFAId { get; set; }
        public string IFAFirstName { get; set; }
        public string IFAMiddleName { get; set; } = "";
        public string IFALastName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
