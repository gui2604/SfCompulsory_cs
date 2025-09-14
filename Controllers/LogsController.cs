using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SfCompulsory_cs.Services;
using SfCompulsory_cs.Models;

namespace SfCompulsory_cs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🔒 protegido com JWT
    public class LogsController : ControllerBase
    {
        private readonly LogService _logService;

        public LogsController(LogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Log>> GetLogs()
        {
            return _logService.GetAllLogs();
        }
    }
}
