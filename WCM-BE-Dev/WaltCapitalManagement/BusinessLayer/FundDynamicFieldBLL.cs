using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class FundDynamicFieldBLL
    {
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public FundDynamicFieldBLL(CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public List<string> GetUnitTypeList(int FundId, bool? FundIsActive = null)
        {
            List<string> unitType = new List<string>();
            var unitTypeList = new List<string>();
            var FundList = _commonRepo.fundList();
            if (FundIsActive == null)
            {
                FundList = FundList.Where(x => x.IsActive == true);
            }
            var FundDetails = FundList.FirstOrDefault(x => x.Id == FundId);
            if (FundDetails != null)
            {
                var fundDynamicField = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundId).ToList();
                if (fundDynamicField != null)
                {
                    if (FundDetails.PerformanceFeeA > 0 && FundDetails.ManagementFeeA > 0)
                        unitType.Add("Unit A");
                    if (FundDetails.PerformanceFeeB > 0 && FundDetails.ManagementFeeB > 0)
                        unitType.Add("Unit B");
                    foreach (var item in fundDynamicField)
                    {
                        string label = Convert.ToString(item.Label).ToLower();
                        if (label.Contains("management fee"))
                        {
                            unitType.Add(Regex.Replace(item.Label, "management fee", string.Empty, RegexOptions.IgnoreCase).Trim());
                        }
                        else if (label.Contains("performance fee"))
                        {
                            unitType.Add(Regex.Replace(item.Label, "performance fee", string.Empty, RegexOptions.IgnoreCase).Trim());
                        }
                    }
                    unitTypeList = unitType.Distinct().ToList();
                }
            }
            return unitTypeList;
        }
    }
}
