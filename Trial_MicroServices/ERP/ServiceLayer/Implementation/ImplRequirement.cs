using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer;
using DTO.ReqDTO;
using Helper;
using Helper.Models;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class ImplRequirement : IRequirement
	{
		private readonly RequirementBLL _requirementBLL;
		private readonly CommonRepo _commonRepo;
		public ImplRequirement(RequirementBLL requirementBLL, CommonRepo commonRepo)
		{
			_requirementBLL = requirementBLL;
			_commonRepo = commonRepo;
		}

		public CommonResponse GetAllRequirement()
		{
			return _requirementBLL.GetAllRequirement();
		}

		public CommonResponse GetRequirementById(GetRequirementReqDTO getRequirementReqDTO)
		{
			return _requirementBLL.GetRequirementById(getRequirementReqDTO);
		}

		public CommonResponse AddRequirements(AddRequirementsReqDTO addRequirementsReqDTO)
		{
			return _requirementBLL.AddRequirements(addRequirementsReqDTO);
		}
	}
}
