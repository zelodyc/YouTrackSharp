using System.Threading.Tasks;

namespace YouTrackSharp.Sprints
{
    /// <summary>
    /// Service used to access sprints.
    /// Sprints are accessed as part of an agile board (see <see cref="YouTrackSharp.Agiles.IAgileService"/>
    /// for more information on how to access boards.
    /// </summary>
    public interface ISprintsService
    {
        /// <summary>
        /// Retrieves a sprint by id, from a given agile board.
        /// </summary>
        /// <param name="boardId">Id of agile board</param>
        /// <param name="sprintId">Sprint id</param>
        /// <param name="verbose">
        /// If the full representation of the sprint should be returned.
        /// If this parameter is <c>false</c>, all the fields (and sub-fields) marked with the
        /// <see cref="YouTrackSharp.SerializationAttributes.VerboseAttribute"/> are omitted (for more information, see
        /// <see cref="Sprint"/> and related classes).
        /// </param>
        /// <returns><see cref="Sprint"/> with given id if it was found, <c>null</c> otherwise</returns>
        /// <exception cref="T:System.Net.HttpRequestException">When the call to the remote YouTrack server instance
        /// failed.</exception>
        public Task<Sprint> GetSprint(string boardId, string sprintId, bool verbose = false);
    }
}