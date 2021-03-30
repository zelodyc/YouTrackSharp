using System.Collections.Generic;
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

        /// <summary>
        /// Retrieves all the sprints of given agile board.
        /// </summary>
        /// <param name="boardId">Agile board id</param>
        /// <param name="verbose">
        /// If the full representation of the sprints should be returned.
        /// If this parameter is <c>false</c>, all the fields (and sub-fields) marked with the
        /// <see cref="YouTrackSharp.SerializationAttributes.VerboseAttribute"/> are omitted (for more information, see
        /// <see cref="Sprint"/> and related classes).
        /// </param>
        /// <returns>All sprints from given agile board</returns>
        /// <exception cref="T:System.Net.HttpRequestException">When the call to the remote YouTrack server instance
        /// failed.</exception>
        Task<List<Sprint>> GetSprints(string boardId, bool verbose = false);

        /// <summary>
        /// Creates a new sprint in the given board.
        /// Returns the newly created sprint (with id).
        /// </summary>
        /// <remarks>
        /// Uses the REST API <a href="https://www.jetbrains.com/help/youtrack/standalone/resource-api-agiles-agileID-sprints.html#create-Sprint-method">
        /// Add a New Sprint</a>.
        /// </remarks>
        /// <param name="boardId">Id of agile board</param>
        /// <param name="sprint">Sprint to create</param>
        /// <param name="verbose">
        /// If the full representation of the sprint should be returned.
        /// If this parameter is <c>false</c>, all the fields (and sub-fields) marked with the
        /// <see cref="YouTrackSharp.SerializationAttributes.VerboseAttribute"/> are omitted (for more information, see
        /// <see cref="Sprint"/> and related classes).
        /// </param>
        /// <returns>Created sprint</returns>
        /// <exception cref="T:System.ArgumentNullException">When the <paramref name="boardId"/> or
        /// <paramref name="sprint"/> is null or empty.</exception>
        /// <exception cref="T:YouTrackErrorException">
        /// When the call to the remote YouTrack server instance failed and YouTrack reported an error message.
        /// </exception>
        /// <exception cref="T:System.Net.HttpRequestException">
        /// When the call to the remote YouTrack server instance failed.
        /// </exception>
        Task<Sprint> CreateSprint(string boardId, Sprint sprint, bool verbose = false);

        /// <summary>
        /// Deletes the sprint with given <see cref="sprintId"/> from the agile board identified by <see cref="boardId"/> 
        /// </summary>
        /// <remarks>
        /// Uses the REST API <a href="https://www.jetbrains.com/help/youtrack/standalone/operations-api-agiles-agileID-sprints.html#delete-Sprint-method">
        /// Delete a Specific Sprint</a>.
        /// </remarks>
        /// <param name="boardId">Board id</param>
        /// <param name="sprintId">Id of sprint to delete</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// When the <paramref name="boardId"/> or <paramref name="sprintId"/> is null or empty.
        /// </exception>
        /// <exception cref="T:System.Net.HttpRequestException">
        /// When the call to the remote YouTrack server instance
        /// failed.
        /// </exception>
        Task DeleteSprint(string boardId, string sprintId);
    }
}