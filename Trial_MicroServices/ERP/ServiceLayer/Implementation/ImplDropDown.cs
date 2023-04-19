using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class ImplDropDown : IDropDown
	{
		private readonly DropDownBLL _dropDownBLL;
		public ImplDropDown(DropDownBLL dropDownBLL)
		{
			_dropDownBLL = dropDownBLL;
		}
		#region Gender
		public CommonResponse GetAllGender()
		{
			return _dropDownBLL.GetAllGender();
		}

		public CommonResponse GetGenderById(GetGenderByIdReqDTO getGenderByIdReqDTO)
		{
			return _dropDownBLL.GetGenderById(getGenderByIdReqDTO);
		}

		public CommonResponse AddGenders(AddGenderReqDTO addGenderReqDTO)
		{
			return _dropDownBLL.AddGenders(addGenderReqDTO);
		}
		#endregion

		#region EmployementType

		public CommonResponse GetAllEmployementType()
		{
			return _dropDownBLL.GetAllEmployementType();
		}

		public CommonResponse GetEmployementTypeById(GetEmployementTypeByIdReqDTO getEmployementTypeByIdReqDTO)
		{
			return _dropDownBLL.GetEmployementTypeById(getEmployementTypeByIdReqDTO);
		}

		public CommonResponse AddEmployementType(AddEmployementTypeReqDTO addEmployementTypeReqDTO)
		{
			return _dropDownBLL.AddEmployementType(addEmployementTypeReqDTO);
		}

		#endregion

		#region Company

		public CommonResponse GetAllCompanyName()
		{
			return _dropDownBLL.GetAllCompanyName();
		}

		public CommonResponse GetCompanyById(GetCompanyByIdReqDTO getCompanyByIdReqDTO)
		{
			return _dropDownBLL.GetCompanyById(getCompanyByIdReqDTO);
		}

		public CommonResponse AddCompany(AddCompanyReqDTO addCompanyReqDTO)
		{
			return _dropDownBLL.AddCompany(addCompanyReqDTO);
		}

		#endregion

		#region Role

		public CommonResponse GetAllRoles()
		{
			return _dropDownBLL.GetAllRoles();
		}

		public CommonResponse GetRoleById(GetAllRolesReqDTO getAllRolesReqDTO)
		{
			return _dropDownBLL.GetRoleById(getAllRolesReqDTO);
		}

		public CommonResponse AddRole(AddRoleReqDTO addRoleReqDTO)
		{
			return _dropDownBLL.AddRole(addRoleReqDTO);
		}

		#endregion

		#region Reporting Manager

		public CommonResponse GetAllReportingManager()
		{
			return _dropDownBLL.GetAllReportingManager();
		}

		public CommonResponse GetReportingManagerById(GetReportingManagerReqDTO getReportingManagerReqDTO)
		{
			return _dropDownBLL.GetReportingManagerById(getReportingManagerReqDTO);
		}

		public CommonResponse AddReportingManager(AddReportingManagerReqDTO addReportingManagerReqDTO)
		{
			return _dropDownBLL.AddReportingManager(addReportingManagerReqDTO);
		}

		#endregion

		#region Designation 

		public CommonResponse GetAllDesignation()
		{
			return _dropDownBLL.GetAllDesignation();
		}

		public CommonResponse GetDesignationById(GetDesignationByIdReqDTO getDesignationByIdReqDTO)
		{
			return _dropDownBLL.GetDesignationById(getDesignationByIdReqDTO);
		}

		public CommonResponse AddDesignation(AddDesignationReqDTO addDesignationReqDTO)
		{
			return _dropDownBLL.AddDesignation(addDesignationReqDTO);
		}

		#endregion
	}
}
