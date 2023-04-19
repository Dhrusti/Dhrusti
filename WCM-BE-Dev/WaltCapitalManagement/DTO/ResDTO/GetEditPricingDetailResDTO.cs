using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetEditPricingDetailResDTO
    {
        public dynamic DynamicPricingInputValidation { get; set; }
        public List<string> DynamicPricingInputFields { get; set; }
    }
    public class RequiredDataModel
    {
        public dynamic Required { get; set; }
        public decimal InputValue { get; set; }
    }
}
