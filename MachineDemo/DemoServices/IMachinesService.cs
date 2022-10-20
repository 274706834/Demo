using DemoDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoServices
{
    public interface IMachinesService
    {
        Task<bool> SaveMachineAsync(string payload);

        Task<IEnumerable<MachineOutput>> GetMachineAsync();
    }
}
