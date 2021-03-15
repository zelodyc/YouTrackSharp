using System.Linq;
using System.Threading.Tasks;
using Xunit;
using YouTrackSharp.Agiles;
using YouTrackSharp.Tests.Infrastructure;

namespace YouTrackSharp.Tests.Integration.Agiles
{
    public partial class AgileServiceTest
    {
        public class GetAgile
        {
            [Fact]
            public async Task Valid_Connection_Returns_Existing_Agile()
            {   
                // Arrange
                IAgileService agileService = Connections.Demo1Token.CreateAgileService();

                // Act
                Agile demoBoard = await agileService.GetAgileBoard(DemoBoardId);

                // Assert
                Assert.NotNull(demoBoard);
                Assert.Equal(DemoBoardId, demoBoard.Id);
                Assert.Equal(DemoBoardNamePrefix, demoBoard.Name);
            }
            
            [Fact]
            public async Task Mock_Connection_Returns_Full_Agile_01()
            {
                // Arrange
                ConnectionStub connection = new ConnectionStub(new JsonHandler(FullAgile01));
                IAgileService agileService = connection.CreateAgileService();

                // Act
                Agile result = await agileService.GetAgileBoard("109-1");

                // Assert
                Assert.NotNull(result);
                Assert.Equal("109-1", result.Id);
                Assert.Equal("Full Board 01", result.Name);

                Assert.NotNull(result.ColumnSettings);
                Assert.NotNull(result.Projects);
                Assert.NotNull(result.Sprints);
                Assert.NotNull(result.Projects);
                Assert.NotNull(result.Sprints);
                Assert.NotNull(result.Status);
                Assert.NotNull(result.ColumnSettings);
                Assert.NotNull(result.CurrentSprint);
                Assert.NotNull(result.EstimationField);
                Assert.NotNull(result.SprintsSettings);
                Assert.NotNull(result.SwimlaneSettings);
                Assert.NotNull(result.ColorCoding);
                Assert.NotNull(result.UpdateableBy);
                Assert.NotNull(result.VisibleFor);
                Assert.NotNull(result.OriginalEstimationField);

                Sprint sprint = result.Sprints.FirstOrDefault();
                Assert.NotNull(sprint);
                Assert.Equal(DemoSprintId, sprint.Id);
                Assert.Equal(DemoSprintName, sprint.Name);
                
                Assert.IsType<FieldBasedColorCoding>(result.ColorCoding);
                Assert.IsType<IssueBasedSwimlaneSettings>(result.SwimlaneSettings);
                Assert.IsType<CustomFilterField>(((IssueBasedSwimlaneSettings)result.SwimlaneSettings).Field);
            }
            
            [Fact]
            public async Task Mock_Connection_Returns_Full_Agile_02()
            {
                // Arrange
                ConnectionStub connection = new ConnectionStub(new JsonHandler(FullAgile02));
                IAgileService agileService = connection.CreateAgileService();

                // Act
                Agile result = await agileService.GetAgileBoard("109-2");

                // Assert
                Assert.NotNull(result);
                Assert.Equal("109-2", result.Id);
                Assert.Equal("Full Board 02", result.Name);

                Assert.NotNull(result.ColumnSettings);
                Assert.NotNull(result.Projects);
                Assert.NotNull(result.Sprints);
                Assert.NotNull(result.Projects);
                Assert.NotNull(result.Sprints);
                Assert.NotNull(result.Status);
                Assert.NotNull(result.ColumnSettings);
                Assert.NotNull(result.CurrentSprint);
                Assert.NotNull(result.EstimationField);
                Assert.NotNull(result.SprintsSettings);
                Assert.NotNull(result.SwimlaneSettings);
                Assert.NotNull(result.ColorCoding);
                Assert.NotNull(result.UpdateableBy);
                Assert.NotNull(result.VisibleFor);
                Assert.NotNull(result.OriginalEstimationField);

                Sprint sprint = result.Sprints.FirstOrDefault();
                Assert.NotNull(sprint);
                Assert.Equal(DemoSprintId, sprint.Id);
                Assert.Equal(DemoSprintName, sprint.Name);

                Assert.IsType<ProjectBasedColorCoding>(result.ColorCoding);
                Assert.IsType<AttributeBasedSwimlaneSettings>(result.SwimlaneSettings);
                Assert.IsType<PredefinedFilterField>(((AttributeBasedSwimlaneSettings)result.SwimlaneSettings).Field);
            }
        }
    }
}