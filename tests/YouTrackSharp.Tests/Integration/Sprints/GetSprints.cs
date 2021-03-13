using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using YouTrackSharp.Sprints;
using YouTrackSharp.Tests.Infrastructure;

namespace YouTrackSharp.Tests.Integration.Sprints
{
    public partial class SprintsServiceTests
    {
        public class GetSprints
        {
            [Fact]
            public async void Valid_Connection_Returns_Existing_Sprints()
            {
                Connection connection = Connections.Demo1Token;
                ISprintsService sprintsService = connection.CreateSprintService();

                List<Sprint> sprints = await sprintsService.GetSprints("108-2", true);

                Assert.NotNull(sprints);
                Assert.Single(sprints);
                Assert.Equal("109-2", sprints[0].Id);
            }

            [Fact]
            public async void Mock_Connection_Returns_Many_Sprints_In_Batches()
            {
                int totalSprintsCount = 53;
                string[] jsonStrings = Enumerable.Range(0, totalSprintsCount).Select(i => FullSprint).ToArray();
                JsonArrayHandler handler = new JsonArrayHandler(jsonStrings);
                ConnectionStub connection = new ConnectionStub(handler);

                ISprintsService sprintsService = connection.CreateSprintService();
                List<Sprint> sprints = await sprintsService.GetSprints("108-2");
                
                Assert.NotNull(sprints);
                Assert.Equal(totalSprintsCount, sprints.Count);
                Assert.True(sprints.All(s => s.Id.Equals("109-2")));
                Assert.Equal(Math.Ceiling(totalSprintsCount / 10.0), handler.RequestsReceived);
            }
        }
    }
}