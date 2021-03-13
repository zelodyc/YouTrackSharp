using System;
using Xunit;
using YouTrackSharp.Sprints;
using YouTrackSharp.Tests.Infrastructure;

namespace YouTrackSharp.Tests.Integration.Sprints
{
    public partial class SprintsServiceTests
    { 
        public class GetSprint
        {
            [Fact]
            public async void Mock_Connection_Returns_Full_Sprint()
            {
                string json = FullSprint;
                ConnectionStub connection = new ConnectionStub(new JsonHandler(json));

                ISprintsService agileService = connection.CreateSprintService();
                Sprint sprint = await agileService.GetSprint("", "", true);
                
                Assert.Equal("109-2", sprint.Id);
                Assert.NotNull(sprint.Issues);
                Assert.Equal(3, sprint.Issues.Count);
                Assert.Equal("109-10", sprint.Issues[0].Id);
                Assert.Equal("109-11", sprint.Issues[1].Id);
                Assert.Equal("109-12", sprint.Issues[2].Id);
                Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1615359691762), sprint.Start);
                Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1615359720288), sprint.Finish);
                Assert.True(sprint.IsDefault);
                Assert.False(sprint.Archived);
                Assert.Equal("Goal", sprint.Goal);
                Assert.Equal(3, sprint.UnresolvedIssuesCount);
                Assert.Null(sprint.PreviousSprint);
                Assert.Equal("First sprint", sprint.Name);
            }
            
            [Fact]
            public async void Valid_Connection_Returns_Existing_Sprint()
            {
                Connection connection = Connections.Demo1Token;
                
                ISprintsService agileService = connection.CreateSprintService();
                Sprint sprint = await agileService.GetSprint("108-2", "109-2", true);
                
                Assert.Equal("109-2", sprint.Id);
            }
        }
    }
}