using AuthByCookiesPOC.Helpers.CommonHelpers;
using AuthByCookiesPOC.Helpers.CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthByCookiesPOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        static List<Users> users = new List<Users>();
        public UserController()
        {
            users = new List<Users> {
                    new Users { Id = 1, UserName = "Tony Stark" },
                    new Users { Id = 2, UserName = "Steav Roggers" },
                    new Users { Id = 3, UserName = "Thor Odinson" },
                    new Users { Id = 4, UserName = "Bruce Banner" },
                    new Users { Id = 5, UserName = "Natasha Rommanof" },
                    new Users { Id = 6, UserName = "Clint Barton" }
                    };
        }

        /// <summary>
        /// Gets a single User's data by Id
        /// </summary>
        /// <returns>200 if success</returns>
        /// <returns>404 if not found</returns>
        /// <returns>500 if failed</returns>
        [HttpGet("GetUserByIdAsync")]
        public CommonResponse GetUserByIdAsync(int userId)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                var user = users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    response.Status = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Message = CommonConstants.Data_Found_Successfully;
                    response.Data = user;
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Message = CommonConstants.Data_Not_Found;
                }
            }
            catch (Exception ex)
            {
                response.Data = ex.ToString();
            }
            return response;
        }

        /// <summary>
        /// Gets all User's data
        /// </summary>
        /// <returns>200 if success</returns>
        /// <returns>404 if not found</returns>
        /// <returns>500 if failed</returns>
        [Authorize(Roles = "Admin")] // only admin can access it
        [HttpGet("GetAllUsersAsync")]
        public CommonResponse GetAllUsersAsync()
        {
            CommonResponse response = new CommonResponse();
            try
            {
                List<Users> users = new List<Users> {
                    new Users { Id = 1, UserName = "Tony Stark" },
                    new Users { Id = 2, UserName = "Steav Roggers" },
                    new Users { Id = 3, UserName = "Thor Odinson" },
                    new Users { Id = 4, UserName = "Bruce Banner" },
                    new Users { Id = 5, UserName = "Natasha Rommanof" },
                    new Users { Id = 6, UserName = "Clint Barton" }
                    };

                if (users.Count > 0)
                {
                    response.Status = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Message = CommonConstants.Data_Found_Successfully;
                    response.Data = users;
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Message = CommonConstants.Data_Not_Found;
                }
            }
            catch (Exception ex)
            {
                response.Data = ex.ToString();
            }
            return response;
        }
    }
}