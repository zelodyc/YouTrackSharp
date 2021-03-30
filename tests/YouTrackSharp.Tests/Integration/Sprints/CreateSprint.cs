using System;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;
using YouTrackSharp.Sprints;
using YouTrackSharp.Tests.Infrastructure;

namespace YouTrackSharp.Tests.Integration.Sprints
{
    public partial class SprintsServiceTests
    {
        public class CreateSprint
        {
            [Fact]
            public async Task Valid_Connection_Creates_Sprint()
            {
                // Arrange
                Connection connection = Connections.Demo4Token;
                string boardId = "120-0";
                
                Sprint sprint = new Sprint();
                sprint.Name = "Demo Sprint";
                sprint.Archived = false;
                sprint.Start = DateTime.ParseExact("03/15/2021 00:00:01", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                sprint.Finish = DateTime.ParseExact("03/30/2021 23:59:59", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                sprint.Goal = "Demo Sprint Goal";
                sprint.IsDefault = false;

                // Act (this context will create the sprint using the given connection, and clean it up on dispose)
                using TemporarySprintContext tempContext = new TemporarySprintContext(connection);
                Sprint result = await tempContext.CreateSprint(boardId, sprint);
                
                // Assert
                Assert.False(string.IsNullOrEmpty(result?.Id));
                Assert.Equal(sprint.Name, result.Name);
                Assert.Equal(sprint.Archived, result.Archived);
                Assert.Equal(sprint.Start, result.Start);
                Assert.Equal(sprint.Finish, result.Finish);
                Assert.Equal(sprint.Goal, result.Goal);
            }
        }    
    }
    
}