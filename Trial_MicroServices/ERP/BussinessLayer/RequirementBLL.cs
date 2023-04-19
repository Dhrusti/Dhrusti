using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer
{
	public class RequirementBLL
	{
		private readonly CommonRepo _commonRepo;
		private readonly ErpDbContext _dbContext;
		public RequirementBLL(CommonRepo commonRepo, ErpDbContext dbContext)
		{
			_commonRepo = commonRepo;
			_dbContext = dbContext;
		}

		public CommonResponse GetAllRequirement()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var requirements = _commonRepo.requirements().ToList();
				if(requirements.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Data not found.";
				}
				commonResponse.Data = requirements.Adapt<List<GetReportingManagerResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetRequirementById(GetRequirementReqDTO getRequirementReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var requirement = _commonRepo.requirements().Where(x => x.RequirementId == getRequirementReqDTO.RequirementId).FirstOrDefault();
				if (requirement != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Data not found.";
				}
				commonResponse.Data = requirement.Adapt<List<GetReportingManagerResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse AddRequirements(AddRequirementsReqDTO addRequirementsReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddRequirementsResDTO addRequirementsResDTO = new AddRequirementsResDTO();
			try
			{
				var Requirements = _commonRepo.requirements().Where(x => x.MainSkills.ToLower() == addRequirementsReqDTO.MainSkills.ToLower()).FirstOrDefault();
				if (Requirements == null)
				{
					RequirementMst requirementMst = new RequirementMst();
					requirementMst.MainSkills = addRequirementsReqDTO.MainSkills;
					requirementMst.NoOfPosition = addRequirementsReqDTO.NoOfPosition;
					requirementMst.TotalMinExp = addRequirementsReqDTO.TotalMinExp;
					requirementMst.TotalMaxExp = addRequirementsReqDTO.TotalMaxExp;
					requirementMst.RelevantMinExp = addRequirementsReqDTO.RelevantMinExp;
					requirementMst.RelevantMaxExp = addRequirementsReqDTO.RelevantMaxExp;
					requirementMst.TypeofEmployement = addRequirementsReqDTO.TypeofEmployement;
					requirementMst.Pocname = addRequirementsReqDTO.Pocname;
					requirementMst.MandatorySkill = addRequirementsReqDTO.MandatorySkill;
					requirementMst.IsActive = true;
					requirementMst.IsDeleted = false;
					requirementMst.CreatedDate = DateTime.Now;
					requirementMst.UpdatedDate = DateTime.Now;

					_dbContext.RequirementMsts.Add(requirementMst);
					_dbContext.SaveChanges();

					addRequirementsResDTO.RequirementId = requirementMst.RequirementId;
					addRequirementsResDTO.MainSkills = requirementMst.MainSkills;

					commonResponse.Message = "Requirement added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addRequirementsResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "This Skill is already exist.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
	}
}
