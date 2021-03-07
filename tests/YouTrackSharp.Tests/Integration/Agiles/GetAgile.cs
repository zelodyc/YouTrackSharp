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
        }
    }
}