using Microsoft.AspNetCore.Mvc;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [NonController]
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        /*private IHubContext<ChatHub> _hubContext;
        public SignalRController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        [HttpPut("ProcessVarDesignCommResponse/{id}")]
        public async Task<IActionResult> ProcessVarDesignCommResponse(int id)
        {
            //call method TaskCompleted in Hub !!!! How?
           
            //_hubContext.Clients.All.onInsertTask(task);
            return new JsonResult(true);
        }*/



    }
}
