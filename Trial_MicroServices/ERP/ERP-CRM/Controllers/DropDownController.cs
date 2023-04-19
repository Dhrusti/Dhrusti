using DTO.ReqDTO;
using DTO.ResDTO;
using ERP_CRM.ViewModels.ReqViewModel;
using ERP_CRM.ViewModels.ResViewModel;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace ERP_CRM.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DropDownController : ControllerBase
	{
		private readonly IDropDown _iDropDown;
		public DropDownController(IDropDown iDropDown)
		{
			_iDropDown= iDropDown;
		}

		#region Gender

		[HttpPost("GetAllGender")]
		public CommonResponse GetAllGender()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.GetAllGender();
				List<GetGenderByIdResDTO> getGenderByIdResDTO = commonResponse.Data;
				commonResponse.Data = getGenderByIdResDTO.Adapt<List<GetGenderByIdResViewModel>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetGenderById")]
		public CommonResponse GetGenderById(GetGenderByIdReqViewModel getGenderByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.GetGenderById(getGenderByIdReqViewModel.Adapt<GetGenderByIdReqDTO>());
				GetGenderByIdResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<GetGenderByIdResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddGenders")]
		public CommonResponse AddGenders(AddGenderReqViewModel addGenderReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.AddGenders(addGenderReqViewModel.Adapt<AddGenderReqDTO>());
				AddGenderResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<AddGenderResDTO>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region EmployementType

		[HttpPost("GetAllEmployementType")]
		public CommonResponse GetAllEmployementType()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.GetAllEmployementType();
				List<GetAllEmployementTypeResDTO> getAllEmployementTypeResDTO = commonResponse.Data;
				commonResponse.Data = getAllEmployementTypeResDTO.Adapt<List<GetAllEmployementTypeResDTO>>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetEmployementTypeById")]
		public CommonResponse GetEmployementTypeById(GetEmployementTypeByIdReqViewModel getEmployementTypeByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.GetEmployementTypeById(getEmployementTypeByIdReqViewModel.Adapt<GetEmployementTypeByIdReqDTO>());
				GetAllEmployementTypeResDTO getAllEmployementTypeResDTO =commonResponse.Data;
				commonResponse.Data = getAllEmployementTypeResDTO.Adapt<GetAllEmployementTypeResViewModel>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddEmployementType")]
		public CommonResponse AddEmployementType(AddEmployementTypeReqViewModel addEmployementTypeReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.AddEmployementType(addEmployementTypeReqViewModel.Adapt<AddEmployementTypeReqDTO>());
				AddEmployementTypeResDTO addEmployementTypeResDTO = commonResponse.Data;
				commonResponse.Data = addEmployementTypeResDTO.Adapt<AddEmployementTypeResDTO>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region Company

		[HttpPost("GetAllCompanyName")]
		public CommonResponse GetAllCompanyName()
		{
			CommonResponse commonResponse = new CommonResponse();	
			try
			{
				commonResponse = _iDropDown.GetAllCompanyName();
				List<GetCompanyResDTO> getCompanyResDTO = commonResponse.Data;
				commonResponse.Data = getCompanyResDTO.Adapt<List<GetCompanyResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetCompanyById")]
		public CommonResponse GetCompanyById(GetCompanyByIdReqViewModel getCompanyByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.GetCompanyById(getCompanyByIdReqViewModel.Adapt<GetCompanyByIdReqDTO>());
				GetCompanyResDTO getCompanyResDTO = commonResponse.Data;
				commonResponse.Data = getCompanyResDTO.Adapt<GetCompanyResViewModel>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddCompany")]
		public CommonResponse AddCompany(AddCompanyReqViewModel addCompanyReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.AddCompany(addCompanyReqViewModel.Adapt<AddCompanyReqDTO>());
				AddCompanyResDTO addCompanyResDTO = commonResponse.Data;
				commonResponse.Data = addCompanyResDTO.Adapt<AddCompanyResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region Role

		[HttpPost("GetAllRoles")]
		public CommonResponse GetAllRoles()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.GetAllRoles();
				List<GetAllRolesResDTO> getAllRolesResDTO = commonResponse.Data;
				commonResponse.Data = getAllRolesResDTO.Adapt<List<GetAllRolesResViewModel>>();

			}
			catch(Exception )
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetRoleById")]
		public CommonResponse GetRoleById(GetAllRolesReqViewModel getAllRolesReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				 commonResponse = _iDropDown.GetRoleById(getAllRolesReqViewModel.Adapt<GetAllRolesReqDTO>());
				GetAllRolesResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<GetAllRolesResViewModel>();

			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddRole")]
		public CommonResponse AddRole(AddRoleReqViewModel addRoleReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse(); 
			try
			{
				commonResponse = _iDropDown.AddRole(addRoleReqViewModel.Adapt<AddRoleReqDTO>());
				AddRoleResDTO addRoleResDTO = commonResponse.Data;
				commonResponse.Data = addRoleResDTO.Adapt<AddRoleResViewModel>();
			}
			catch(Exception ) { throw; }
			return commonResponse;
		}

		#endregion

		#region ReportingManager

		[HttpPost("GetAllReportingManager")]
		public CommonResponse GetAllReportingManager()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.GetAllReportingManager();
				List<GetReportingManagerResDTO> getReportingManagerResDTO = commonResponse.Data;
				commonResponse.Data = getReportingManagerResDTO.Adapt<List<GetReportingManagerResViewModel>>();
			}
			catch(Exception ) { throw; }
			return commonResponse;	
		}

		[HttpPost("GetReportingManagerById")]
		public CommonResponse GetReportingManagerById(GetReportingManagerReqViewModel getReportingManagerReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.GetReportingManagerById(getReportingManagerReqViewModel.Adapt<GetReportingManagerReqDTO>());
				GetReportingManagerResDTO getReportingManagerResDTO = commonResponse.Data;
				commonResponse.Data = getReportingManagerResDTO.Adapt<GetReportingManagerResViewModel>();
			}
			catch(Exception ) { throw; }
			return commonResponse;
		}

		[HttpPost("AddReportingManager")]
		public CommonResponse AddReportingManager(AddReportingManagerReqViewModel addReportingManagerReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.AddReportingManager(addReportingManagerReqViewModel.Adapt<AddReportingManagerReqDTO>());
				AddReportingManagerResDTO addReportingManagerResDTO = commonResponse.Data;
				commonResponse.Data = addReportingManagerResDTO.Adapt<AddReportingManagerResViewModel>();

			}
			catch (Exception ) { throw; }
			return commonResponse;
		}

		#endregion

		#region Designation 

		[HttpPost("GetAllDesignation")]
		public CommonResponse GetAllDesignation()
		{
			CommonResponse commonResponse = new CommonResponse();	
			try
			{
				commonResponse = _iDropDown.GetAllDesignation();
				List<GetDesignationResDTO> getDesignationResDTO = commonResponse.Data;
				commonResponse.Data = getDesignationResDTO.Adapt<List<GetDesignationResViewModel>>();
			}
			catch(Exception ) { throw; }	
			return commonResponse;
		}

		[HttpPost("GetDesignationById")]
		public CommonResponse GetDesignationById(GetDesignationByIdReqViewModel getDesignationByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();	
			try
			{
				commonResponse = _iDropDown.GetDesignationById(getDesignationByIdReqViewModel.Adapt<GetDesignationByIdReqDTO>());
				GetDesignationResDTO getDesignationResDTO = commonResponse.Data;
				commonResponse.Data = getDesignationResDTO.Adapt<GetDesignationResViewModel>();
			}
			catch(Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddDesignation")]
		public CommonResponse AddDesignation(AddDesignationReqViewModel addDesignationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iDropDown.AddDesignation(addDesignationReqViewModel.Adapt<AddDesignationReqDTO>());
				AddDesignationResDTO addDesignationResDTO = commonResponse.Data;
				commonResponse.Data = addDesignationResDTO.Adapt<AddDesignationResViewModel>();

			}
			catch(Exception ) { throw; }
			return commonResponse;
		}

		#endregion

	}
}
