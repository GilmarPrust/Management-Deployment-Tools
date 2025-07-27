using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using API.Control.DTOs.Device;
using AutoMapper;
using API.Control.Services.Implementations;
using API.Control.Models;
using API.Control.ValueObjects;
using Microsoft.Extensions.Logging;


public class DeviceServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldAddDevice_WhenValidData()
    {
        // Arrange  
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var deviceModelId = Guid.NewGuid();

        using var context = new AppDbContext(options);
        var mapperMock = new Mock<IMapper>();
        var loggerMock = new Mock<ILogger<DeviceService>>();

        var dto = new DeviceCreateDTO
        {
            ComputerName = "DESKTOP-3434",
            SerialNumber = "12345678",
            DeviceModelId = deviceModelId,
            MacAddress = "10-7C-61-B4-F0-DA"
        };

        var device = new Device
        {
            ComputerName = ComputerName.Create("DESKTOP-3434"),
            SerialNumber = "12345678",
            DeviceModelId = deviceModelId,
            MacAddress = MacAddress.Create("10-7C-61-B4-F0-DA")
        };

        mapperMock.Setup(m => m.Map<Device>(dto)).Returns(device);

        var service = new DeviceService(context, mapperMock.Object, loggerMock.Object);

        // Act  
        var result = await service.CreateAsync(dto);

        // Assert  
        var devices = await context.Devices.ToListAsync();
        devices.Should().HaveCount(1);
        devices.First().SerialNumber.Should().Be("12345678");
        devices.First().ComputerName.Value.Should().Be("DESKTOP-3434");
        devices.First().DeviceModelId.Should().Be(deviceModelId);
        devices.First().MacAddress.Value.Should().Be("10-7C-61-B4-F0-DA");
    }
}
