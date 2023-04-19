using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper.Models;

namespace ServiceLayer.Interface
{
	public interface IDropDown
	{
		#region Gender

		public CommonResponse GetAllGender();
		public CommonResponse GetGenderById(GetGenderByIdReqDTO getGenderByIdReqDTO);
		public CommonResponse AddGenders(AddGenderReqDTO addGenderReqDTO);

		#endregion

		#region EmployementType

		public CommonResponse GetAllEmployementType();

		public CommonResponse GetEmployementTypeById(GetEmployementTypeByIdReqDTO getEmployementTypeByIdReqDTO);

		public CommonResponse AddEmployementType(AddEmployementTypeReqDTO addEmployementTypeReqDTO);

		#endregion

		#region Company

		public CommonResponse GetAllCompanyName();

		public CommonResponse GetCompanyById(GetCompanyByIdReqDTO getCompanyByIdReqDTO);

		public CommonResponse AddCompany(AddCompanyReqDTO addCompanyReqDTO);

		#endregion

		#region Role

		public CommonResponse GetAllRoles();

		public CommonResponse GetRoleById(GetAllRolesReqDTO getAllRolesReqDTO);

		public CommonResponse AddRole(AddRoleReqDTO addRoleReqDTO);

		#endregion

		#region Reporting Manager

		public CommonResponse GetAllReportingManager();

		public CommonResponse GetReportingManagerById(GetReportingManagerReqDTO getReportingManagerReqDTO);

		public CommonResponse AddReportingManager(AddReportingManagerReqDTO addReportingManagerReqDTO);

		#endregion

		#region Designation 

		public CommonResponse GetAllDesignation();

		public CommonResponse GetDesignationById(GetDesignationByIdReqDTO getDesignationByIdReqDTO);

		public CommonResponse AddDesignation(AddDesignationReqDTO addDesignationReqDTO);

		#endregion
	}
}
