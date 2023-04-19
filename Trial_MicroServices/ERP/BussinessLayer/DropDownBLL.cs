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
	public class DropDownBLL
	{
		private readonly CommonRepo _commonRepo;
		private readonly ErpDbContext _dbContext;
		public DropDownBLL(CommonRepo commonRepo, ErpDbContext dbContext)
		{
			_commonRepo = commonRepo;
			_dbContext = dbContext;
		}

		#region Gender

		public CommonResponse GetAllGender()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var genderList = _commonRepo.gender().ToList();
				if (genderList.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = genderList.Adapt<List<GetGenderByIdResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetGenderById(GetGenderByIdReqDTO getGenderByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var type = _commonRepo.gender().FirstOrDefault(x => x.GenderId == getGenderByIdReqDTO.GenderId);
				if (type != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}

				commonResponse.Data = type.Adapt<GetGenderByIdResDTO>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse AddGenders(AddGenderReqDTO addGenderReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddGenderResDTO addGenderResDTO = new AddGenderResDTO();
			try
			{
				var addGender = _commonRepo.gender().Where(x => x.Gender == addGenderReqDTO.Gender.ToLower()).FirstOrDefault();
				if (addGender == null)
				{
					GenderMst genderMst = new GenderMst();
					genderMst.Gender = addGenderReqDTO.Gender;
					genderMst.IsActive = true;
					genderMst.IsDeleted = false;
					genderMst.CreatedDate = DateTime.Now;
					genderMst.UpdatedDate = DateTime.Now;

					_dbContext.GenderMsts.Add(genderMst);
					_dbContext.SaveChanges();

					addGenderResDTO.GenderId = genderMst.GenderId;
					addGenderResDTO.Gender = genderMst.Gender;

					commonResponse.Message = "Gender added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addGenderResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Gender already exist.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region EmployementType

		public CommonResponse GetAllEmployementType()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var employementType = _commonRepo.employementTypeList().ToList();
				if (employementType.Count > 0)
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
				commonResponse.Data = employementType.Adapt<List<GetAllEmployementTypeResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetEmployementTypeById(GetEmployementTypeByIdReqDTO getEmployementTypeByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var employementType = _commonRepo.employementTypeList().FirstOrDefault(x => x.EmployementTypeId == getEmployementTypeByIdReqDTO.EmployementTypeId);
				if (employementType != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}

				commonResponse.Data = employementType.Adapt<GetAllEmployementTypeResDTO>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse AddEmployementType(AddEmployementTypeReqDTO addEmployementTypeReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddEmployementTypeResDTO addEmployementTypeResDTO = new AddEmployementTypeResDTO();
			try
			{
				var employeeType = _commonRepo.employementTypeList().Where(x => x.EmployementType == addEmployementTypeReqDTO.EmployementType.ToLower()).FirstOrDefault();
				if (employeeType == null)
				{
					EmployementTypeMst employementTypeMst = new EmployementTypeMst();
					employementTypeMst.EmployementType = addEmployementTypeReqDTO.EmployementType;
					employementTypeMst.IsActive = true;
					employementTypeMst.IsDeleted = false;
					employementTypeMst.CreatedDate = DateTime.Now;
					employementTypeMst.UpdatedDate = DateTime.Now;

					_dbContext.EmployementTypeMsts.Add(employementTypeMst);
					_dbContext.SaveChanges();

					addEmployementTypeResDTO.EmployementTypeId = employementTypeMst.EmployementTypeId;
					addEmployementTypeResDTO.EmployementType = employementTypeMst.EmployementType;

					commonResponse.Message = "EmployementType added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addEmployementTypeResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "EmployementType already exist.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region Company

		public CommonResponse GetAllCompanyName()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var companyList = _commonRepo.companyList().ToList();
				if (companyList.Count > 0)
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
				commonResponse.Data = companyList.Adapt<List<GetCompanyResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetCompanyById(GetCompanyByIdReqDTO getCompanyByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var company = _commonRepo.companyList().Where(x => x.CompanyId == getCompanyByIdReqDTO.CompanyId).FirstOrDefault();
				if (company != null)
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
				commonResponse.Data = company.Adapt<GetCompanyResDTO>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse AddCompany(AddCompanyReqDTO addCompanyReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddCompanyResDTO addCompanyResDTO = new AddCompanyResDTO();
			try
			{
				var company = _commonRepo.companyList().Where(x => x.CompanyName.ToLower() == addCompanyReqDTO.CompanyName.ToLower()).FirstOrDefault();
				if (company == null)
				{
					CompanyMst companyMst = new CompanyMst();
					companyMst.CompanyName = addCompanyReqDTO.CompanyName;
					companyMst.IsActive = true;
					companyMst.IsDeleted = false;
					companyMst.CreatedDate = DateTime.Now;
					companyMst.UpdatedDate = DateTime.Now;

					_dbContext.CompanyMsts.Add(companyMst);
					_dbContext.SaveChanges();

					addCompanyResDTO.CompanyId = companyMst.CompanyId;
					addCompanyResDTO.CompanyName = companyMst.CompanyName;

					commonResponse.Message = "Company added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addCompanyResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Company already exist.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region Role

		public CommonResponse GetAllRoles()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var roles = _commonRepo.roleList().ToList();
				if (roles.Count > 0)
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
				commonResponse.Data = roles.Adapt<List<GetAllRolesResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetRoleById(GetAllRolesReqDTO getAllRolesReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var role = _commonRepo.roleList().Where(x => x.RoleId == getAllRolesReqDTO.RoleId).FirstOrDefault();
				if (role != null)
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
				commonResponse.Data = role.Adapt<GetAllRolesResDTO>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse AddRole(AddRoleReqDTO addRoleReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddRoleResDTO addRoleResDTO = new AddRoleResDTO();
			try
			{
				var role = _commonRepo.roleList().Where(x => x.Role.ToLower() == addRoleReqDTO.Role.ToLower()).FirstOrDefault();
				if (role == null)
				{
					RoleMst roleMst = new RoleMst();
					roleMst.Role = addRoleReqDTO.Role;
					roleMst.IsActive = true;
					roleMst.IsDeleted = false;
					roleMst.CreatedDate = DateTime.Now;
					roleMst.UpdatedDate = DateTime.Now;

					_dbContext.RoleMsts.Add(roleMst);
					_dbContext.SaveChanges();

					addRoleResDTO.RoleId = roleMst.RoleId;
					addRoleResDTO.Role = roleMst.Role;

					commonResponse.Message = "Role added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addRoleResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Role already exist.";
				}
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region Reporting Manager

		public CommonResponse GetAllReportingManager()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var report = _commonRepo.reportingManagerList().ToList();
				if (report.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Data not fopund.";
				}
				commonResponse.Data = report.Adapt<List<GetReportingManagerResDTO>>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetReportingManagerById(GetReportingManagerReqDTO getReportingManagerReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var manager = _commonRepo.reportingManagerList().Where(x => x.ReportingManagerId == getReportingManagerReqDTO.ReportingManagerId).FirstOrDefault();
				if (manager != null)
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
				commonResponse.Data = manager.Adapt<GetReportingManagerResDTO>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse AddReportingManager(AddReportingManagerReqDTO addReportingManagerReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddReportingManagerResDTO addReportingManagerResDTO = new AddReportingManagerResDTO();
			try
			{
				var manager = _commonRepo.reportingManagerList().Where(x => x.ReportingManagerName.ToLower() == addReportingManagerReqDTO.ReportingManagerName.ToLower()).FirstOrDefault();
				if (manager == null)
				{
					ReportingManagerMst reportingManagerMst = new ReportingManagerMst();
					reportingManagerMst.ReportingManagerName = addReportingManagerReqDTO.ReportingManagerName;
					reportingManagerMst.IsActive = true;
					reportingManagerMst.IsDeleted = false;
					reportingManagerMst.CreatedDate = DateTime.Now;
					reportingManagerMst.UpdatedDate = DateTime.Now;

					_dbContext.ReportingManagerMsts.Add(reportingManagerMst);
					_dbContext.SaveChanges();

					addReportingManagerResDTO.ReportingManagerId = reportingManagerMst.ReportingManagerId;
					addReportingManagerResDTO.ReportingManagerName = reportingManagerMst.ReportingManagerName;

					commonResponse.Message = "Manager Name added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addReportingManagerResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Manager Name already exist.";
				}
			}
			catch(Exception) { throw; }
			return commonResponse;
		}

		#endregion

		#region Designation

		public CommonResponse GetAllDesignation()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var designation = _commonRepo.designationList().ToList();
				if (designation.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode= HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Data not found.";
				}
				commonResponse.Data = designation.Adapt<List<GetDesignationResDTO>>();
			}
			catch(Exception ) { throw; }
			return commonResponse;
		}

		public CommonResponse GetDesignationById(GetDesignationByIdReqDTO getDesignationByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var designation = _commonRepo.designationList().Where(x => x.DesignationId == getDesignationByIdReqDTO.DesignationId).FirstOrDefault();
				if (designation != null)
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
				commonResponse.Data = designation.Adapt<GetDesignationResDTO>();
			}
			catch(Exception ) { throw; }	
			return commonResponse;
		}

		public CommonResponse AddDesignation(AddDesignationReqDTO addDesignationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddDesignationResDTO addDesignationResDTO = new AddDesignationResDTO();
			try
			{
				var designation = _commonRepo.designationList().Where(x => x.Designation.ToLower() == addDesignationReqDTO.Designation.ToLower()).FirstOrDefault();
				if (designation == null)
				{
					DesignationMst designationMst = new DesignationMst();
					designationMst.Designation = addDesignationReqDTO.Designation;
					designationMst.IsActive = true;
					designationMst.IsDeleted = false;
					designationMst.CreatedDate = DateTime.Now;
					designationMst.UpdatedDate = DateTime.Now;

					_dbContext.DesignationMsts.Add(designationMst);
					_dbContext.SaveChanges();

					addDesignationResDTO.DesignationId = designationMst.DesignationId;
					addDesignationResDTO.Designation = designationMst.Designation;

					commonResponse.Message = "Designation added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addDesignationResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Designation already exist.";
				}
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

	}
}
