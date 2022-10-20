using DemoEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoRepository
{
    public class MachinesRepository:IMachinesRepository
    {        
        private readonly ILogger<MachinesRepository> logger;
        private DemoDbContext demoDbContext;
        public MachinesRepository(ILogger<MachinesRepository> logger, DemoDbContext context)
        {
          this.logger = logger;
          demoDbContext = context;
        }

        public async Task<bool> SaveMachineAsync(string payload)
        {
            var machinePayLoad = new MachinePayLoad
            {
                Payload = payload
            };
            await demoDbContext.Machines.AddAsync(machinePayLoad);
            await demoDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<MachinePayLoad>> GetMachineAsync()
        {           
            var result = await demoDbContext.Machines.ToListAsync();
            return result;
        }
    }
}
