using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                string json = $"[{FullAgile01}]";
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(json);
                ConnectionStub connection = new ConnectionStub(_ => response);
                
                IAgileService agileService = connection.CreateAgileService();

                // Act
                ICollection<Agile> result = await agileService.GetAgileBoards(true);

                // Assert
                Assert.NotNull(result);
                Assert.NotEmpty(result);

                Agile demoBoard = result.FirstOrDefault();
                Assert.NotNull(demoBoard);
                Assert.Equal(DemoBoardId, demoBoard.Id);
                Assert.Equal(DemoBoardNamePrefix, demoBoard.Name);

                Assert.NotNull(demoBoard.ColumnSettings);
                Assert.NotNull(demoBoard.Projects);
                Assert.NotNull(demoBoard.Sprints);
                Assert.NotNull(demoBoard.Projects);
                Assert.NotNull(demoBoard.Sprints);
                Assert.NotNull(demoBoard.Status);
                Assert.NotNull(demoBoard.ColumnSettings);
                Assert.NotNull(demoBoard.CurrentSprint);
                Assert.NotNull(demoBoard.EstimationField);
                Assert.NotNull(demoBoard.SprintsSettings);
                Assert.NotNull(demoBoard.SwimlaneSettings);
                Assert.NotNull(demoBoard.ColorCoding);
                Assert.NotNull(demoBoard.UpdateableBy);
                Assert.NotNull(demoBoard.VisibleFor);
                Assert.NotNull(demoBoard.OriginalEstimationField);

                Sprint sprint = demoBoard.Sprints.FirstOrDefault();
                Assert.NotNull(sprint);
                Assert.Equal(DemoSprintId, sprint.Id);
                Assert.Equal(DemoSprintName, sprint.Name);
                
                Assert.IsType<FieldBasedColorCoding>(demoBoard.ColorCoding);
                Assert.IsType<IssueBasedSwimlaneSettings>(demoBoard.SwimlaneSettings);
                Assert.IsType<CustomFilterField>(((IssueBasedSwimlaneSettings)demoBoard.SwimlaneSettings).Field);
            }
            
            [Fact]
            public async Task Mock_Connection_Returns_Full_Agile_02()
            {
                // Arrange
                string json = $"[{FullAgile02}]";
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(json);
                ConnectionStub connection = new ConnectionStub(_ => response);

                IAgileService agileService = connection.CreateAgileService();

                // Act
                ICollection<Agile> result = await agileService.GetAgileBoards(true);

                // Assert
                Assert.NotNull(result);
                Assert.NotEmpty(result);

                Agile demoBoard = result.FirstOrDefault();
                Assert.NotNull(demoBoard);
                Assert.Equal(DemoBoardId, demoBoard.Id);
                Assert.Equal(DemoBoardNamePrefix, demoBoard.Name);

                Assert.NotNull(demoBoard.ColumnSettings);
                Assert.NotNull(demoBoard.Projects);
                Assert.NotNull(demoBoard.Sprints);
                Assert.NotNull(demoBoard.Projects);
                Assert.NotNull(demoBoard.Sprints);
                Assert.NotNull(demoBoard.Status);
                Assert.NotNull(demoBoard.ColumnSettings);
                Assert.NotNull(demoBoard.CurrentSprint);
                Assert.NotNull(demoBoard.EstimationField);
                Assert.NotNull(demoBoard.SprintsSettings);
                Assert.NotNull(demoBoard.SwimlaneSettings);
                Assert.NotNull(demoBoard.ColorCoding);
                Assert.NotNull(demoBoard.UpdateableBy);
                Assert.NotNull(demoBoard.VisibleFor);
                Assert.NotNull(demoBoard.OriginalEstimationField);

                Sprint sprint = demoBoard.Sprints.FirstOrDefault();
                Assert.NotNull(sprint);
                Assert.Equal(DemoSprintId, sprint.Id);
                Assert.Equal(DemoSprintName, sprint.Name);

                Assert.IsType<ProjectBasedColorCoding>(demoBoard.ColorCoding);
                Assert.IsType<AttributeBasedSwimlaneSettings>(demoBoard.SwimlaneSettings);
                Assert.IsType<PredefinedFilterField>(((AttributeBasedSwimlaneSettings)demoBoard.SwimlaneSettings).Field);
            }
        }
    }
}