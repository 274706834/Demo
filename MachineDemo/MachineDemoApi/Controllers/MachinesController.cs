using DemoDTO;
using DemoServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MachineDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
       
        private readonly IMachinesService machinesService;
        private readonly ILogger<MachinesController> logger;
        public MachinesController(IMachinesService machinesService, ILogger<MachinesController> logger)
        {
            this.machinesService = machinesService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<MachineOutput>> GetMachinesListAsync()
        {
            var result = await machinesService.GetMachineAsync();
            return result;           
           
        }

    }
}
