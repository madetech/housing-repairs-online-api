﻿using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Gateways;
using HousingRepairsOnlineApi.Helpers;
using HousingRepairsOnlineApi.UseCases;
using Moq;
using Xunit;

namespace HousingRepairsOnlineApi.Tests.UseCasesTests;

public class SaveRepairRequestUseCaseTests
{
    private readonly Mock<IBlobStorageGateway> mockAzureStorageGateway;
    private readonly Mock<IRepairStorageGateway> mockRepairStorageGateway;
    private readonly Mock<ISoREngine> mockSorEngine;
    private readonly SaveRepairRequestUseCase systemUnderTest;

    public SaveRepairRequestUseCaseTests()
    {
        mockSorEngine = new Mock<ISoREngine>();
        mockRepairStorageGateway = new Mock<IRepairStorageGateway>();
        mockAzureStorageGateway = new Mock<IBlobStorageGateway>();
        systemUnderTest = new SaveRepairRequestUseCase(
            mockRepairStorageGateway.Object,
            mockAzureStorageGateway.Object,
            mockSorEngine.Object
        );
    }

    [Fact]
    public async void GivenARepairRequestWithImageARepairIsSaved()
    {
        const string Location = "kitchen";
        const string Problem = "cupboards";
        const string Issue = "doorHangingOff";
        const string RepairCode = "N373049";
        const string Base64Img =
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAABhWlDQ1BJQ0MgcHJvZmlsZQAAKJF9kT1Iw1AUhU9TpaJVB4uIOGSoThZERRy1CkWoEGqFVh1MXvojNGlIUlwcBdeCgz+LVQcXZ10dXAVB8AfE0clJ0UVKvC8ptIjxwuN9nHfP4b37AKFWYprVNgZoum2mEnExk10RQ68IIIQe9KNLZpYxK0lJ+NbXPXVT3cV4ln/fn9Wt5iwGBETiGWaYNvE68dSmbXDeJ46woqwSnxOPmnRB4keuKx6/cS64LPDMiJlOzRFHiMVCCystzIqmRjxJHFU1nfKFjMcq5y3OWqnCGvfkLwzn9OUlrtMaQgILWIQEEQoq2EAJNmK066RYSNF53Mc/6Polcink2gAjxzzK0CC7fvA/+D1bKz8x7iWF40D7i+N8DAOhXaBedZzvY8epnwDBZ+BKb/rLNWD6k/RqU4seAb3bwMV1U1P2gMsdYODJkE3ZlYK0hHweeD+jb8oCfbdA56o3t8Y5Th+ANM0qeQMcHAIjBcpe83l3R+vc/u1pzO8H+I9yds6VEEcAAAAJcEhZcwAALiMAAC4jAXilP3YAAAAHdElNRQfmAQcOFjXsyx/IAAAAGXRFWHRDb21tZW50AENyZWF0ZWQgd2l0aCBHSU1QV4EOFwAAAAxJREFUCNdj0HiTBAACtgF3wqeo5gAAAABJRU5ErkJggg==";
        const string FileExtension = "png";
        const string ImgUrl = "http://img.png";

        var repairRequest = new RepairRequest
        {
            Location = new RepairLocation { Value = Location },
            Problem = new RepairProblem { Value = Problem },
            Issue = new RepairIssue { Value = Issue },
            Description = new RepairDescriptionRequest { Base64Img = Base64Img, FileExtension = FileExtension }
        };

        mockSorEngine.Setup(x => x.MapSorCode(Location, Problem, Issue))
            .Returns(RepairCode);

        mockAzureStorageGateway.Setup(x => x.UploadBlob(Base64Img, FileExtension))
            .ReturnsAsync(ImgUrl);

        mockRepairStorageGateway.Setup(x => x.AddRepair(It.IsAny<Repair>()))
            .ReturnsAsync((Repair r) => r);

        var _ = await systemUnderTest.Execute(repairRequest);

        mockAzureStorageGateway.Verify(x => x.UploadBlob(Base64Img, FileExtension), Times.Once);
        mockSorEngine.Verify(x => x.MapSorCode(Location, Problem, Issue), Times.Once);
        mockRepairStorageGateway.Verify(
            x => x.AddRepair(It.Is<Repair>(p => p.SOR == RepairCode && p.Description.PhotoUrl == ImgUrl)), Times.Once);
    }

    [Fact]
    public async void GivenARepairRequestWithoutAnImageARepairIsSaved()
    {
        const string Location = "kitchen";
        const string Problem = "cupboards";
        const string Issue = "doorHangingOff";
        const string RepairCode = "N373049";

        var repairRequest = new RepairRequest
        {
            Location = new RepairLocation { Value = Location },
            Problem = new RepairProblem { Value = Problem },
            Issue = new RepairIssue { Value = Issue },
            Description = new RepairDescriptionRequest { Text = "Lorem ipsum" }
        };

        mockSorEngine.Setup(x => x.MapSorCode(Location, Problem, Issue))
            .Returns(RepairCode);

        mockRepairStorageGateway.Setup(x => x.AddRepair(It.IsAny<Repair>()))
            .ReturnsAsync((Repair r) => r);

        var _ = await systemUnderTest.Execute(repairRequest);

        mockSorEngine.Verify(x => x.MapSorCode(Location, Problem, Issue), Times.Once);
        mockRepairStorageGateway.Verify(
            x => x.AddRepair(It.Is<Repair>(p => p.SOR == RepairCode && p.Description.PhotoUrl == null)), Times.Once);
        mockAzureStorageGateway.Verify(x => x.UploadBlob(null, null), Times.Never());
    }
}
