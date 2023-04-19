using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper.Models;

namespace ServiceLayer.Interface
{
	public interface IRequirement
	{
		public CommonResponse GetAllRequirement();

		public CommonResponse GetRequirementById(GetRequirementReqDTO getRequirementReqDTO);

		public CommonResponse AddRequirements(AddRequirementsReqDTO addRequirementsReqDTO);
	}
}
