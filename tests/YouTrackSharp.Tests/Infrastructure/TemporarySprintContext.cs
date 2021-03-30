using System;
using System.Threading.Tasks;
using YouTrackSharp.Sprints;

namespace YouTrackSharp.Tests.Infrastructure
{
    /// <summary>
    /// This context is used to clean up sprints created during tests
    /// </summary>
    public class TemporarySprintContext : IDisposable
    {
        private Connection _connection;
        private string _boardId;
        private Sprint _sprint;

        /// <summary>
        /// Creates a <see cref="TemporarySprintContext"/> instance, with given <see cref="Connection"/>, board id
        /// and <see cref="Sprint"/>.<br/>
        /// The context will call the <see cref="ISprintsService"/>.<see cref="ISprintsService.DeleteSprint"/> method
        /// on dispose, to ensure that the sprint is cleaned up.<br/>
        /// Note that the sprint is not created automatically by the context; for that, <see cref="CreateSprint"/>
        /// should be called explicitely. 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="boardId"></param>
        /// <param name="sprint"></param>
        /// <remarks>
        /// The given sprint is only created in the board when <see cref="CreateSprint"/> is called.<br/>
        /// This is to allow tests to do their own call to <see cref="ISprintsService.CreateSprint"/> as needed.
        /// </remarks>
        public TemporarySprintContext(Connection connection)
        {
            _connection = connection;
            _sprint = null;
            _boardId = null;
        }

        /// <summary>
        /// Creates the <see cref="Sprint"/> in the given YouTrack board.<br/>
        /// The sprint will automatically be removed on dispose.
        /// </summary>
        /// <returns>Newly created <see cref="Sprint"/></returns>
        /// <exception cref="InvalidOperationException">
        /// A sprint has already been created with this <see cref="TemporarySprintContext"/> instance
        /// </exception>
        public async Task<Sprint> CreateSprint(string boardId, Sprint sprint)
        {
            if (_sprint != null)
            {
                throw new InvalidOperationException("A sprint has already been created in this context instance");
            }
                
            ISprintsService service = _connection.CreateSprintService();
            _sprint = await service.CreateSprint(boardId, sprint);
            _boardId = boardId;

            return _sprint;
        }

        /// <summary>
        /// Called on dispose; deletes the given <see cref="Sprint"/> from the Youtrack agile board.
        /// </summary>
        public void Dispose()
        {
            if (string.IsNullOrEmpty(_sprint?.Id))
            {
                return;
            }

            ISprintsService sprintsService = _connection.CreateSprintService();
            sprintsService.DeleteSprint(_boardId, _sprint.Id);
        }
    }
}