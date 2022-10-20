using DemoEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoRepository
{
    public interface IMachinesRepository
    {
        Task<bool> SaveMachineAsync(string payload);
        Task<IEnumerable<MachinePayLoad>> GetMachineAsync();

       
    }
}
