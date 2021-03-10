using System.Collections.Generic;
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
        }
    }
}