using System;
using System.Globalization;
using Xunit;
using YouTrackSharp.Sprints;
using YouTrackSharp.Tests.Infrastructure;

namespace YouTrackSharp.Tests.Integration.Sprints
{
    public partial class SprintsServiceTests
    {
        public class DeleteSprint
        {
            [Fact]
            public async void Valid_Connection_Deletes_Existing_Sprint()
            {
                // Arrange
                Connection connection = Connections.Demo4Token;
                string boardId = "120-0";
                
                Sprint template = new Sprint();
                template.Name = "Demo Sprint";
                template.Archived = false;
                template.Start = DateTime.ParseExact("03/15/2021 00:00:01", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                template.Finish = DateTime.ParseExact("03/30/2021 23:59:59", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                template.Goal = "Demo Sprint Goal";
                template.IsDefault = false;

                // Act
                using TemporarySprintContext tempContext = new TemporarySprintContext(connection);
                Sprint sprint = await tempContext.CreateSprint(boardId, template);

                // Assert (does not throw)
                ISprintsService service = connection.CreateSprintService();
                await service.DeleteSprint(boardId, sprint.Id);
            }
        }
    }
}