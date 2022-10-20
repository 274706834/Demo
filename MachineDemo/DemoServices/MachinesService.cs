using DemoDTO;
using DemoRepository;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoServices
{
    public class MachinesService :IMachinesService
    {
        private readonly IMachinesRepository machinesRepository;
        private readonly ILogger<MachinesService> logger;

        public MachinesService(IMachinesRepository machinesRepository, ILogger<MachinesService> logger)
        {
            this.machinesRepository = machinesRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<MachineOutput>> GetMachineAsync()
        {
            var entity = await machinesRepository.GetMachineAsync();
            var results = new List<MachineOutput>();
            foreach (var e in entity)
            {
                var result = JsonConvert.DeserializeObject<MachineOutput>(e.Payload);
                results.Add(result);
               
            }
            return results;
        }

        public async Task<bool> SaveMachineAsync(string payload)
        {
            try
            {
                var result = await machinesRepository.SaveMachineAsync(payload);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }



        }
    }
}
