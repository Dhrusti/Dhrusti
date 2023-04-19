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

        public List<string> GetUnitTypeList(int FundId)
        {
            List<string> unitType = new List<string>();
            var unitTypeList = new List<string>();
            var fundDynamicField = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundId).ToList();
            if (fundDynamicField != null)
            {
                unitType.Add("Unit A");
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
            return unitTypeList;
        }
    }
}
