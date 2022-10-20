using DemoRepository;
using DemoServices;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DemoUT
{
    [TestClass]
    public class MachinesServiceTest
    {
        private readonly Mock<IMachinesRepository> machinesRepositoryMock;
        private readonly Mock<ILogger<MachinesService>> loggerMock;

        public MachinesServiceTest()
        {
            machinesRepositoryMock = new Mock<IMachinesRepository>();
            loggerMock = new Mock<ILogger<MachinesService>>();
        }      

        [Fact]
        public async Task TestSaveMachine_WhenException_ReturnFalse()
        {
            // Arrange
            var payload = "{\"topic\":\"events\",\"ref\":null,\"payload\":{\"timestamp\":\"\"2022-10-20T13:04:08Z\",\"status\":\"running\",\"machine_id\":\"95d9b02f-3347-4c0c-a8a0-6e6e525121d5\",\"id\":\"5828362d-e4ba-47a8-8523-ed73cce461a6\"},\"join_ref\":null,\"event\":\"new\"}";
            var service = new MachinesService(machinesRepositoryMock.Object, loggerMock.Object);

            // Act
            machinesRepositoryMock.Setup(m => m.SaveMachineAsync(It.IsAny<string>())).ReturnsAsync(true);
            var result = await service.SaveMachineAsync(payload);

            // Assert
            Assert.IsTrue(!result);
        }
    }
}
