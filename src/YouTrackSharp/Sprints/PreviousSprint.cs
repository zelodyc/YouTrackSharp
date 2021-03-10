using Newtonsoft.Json;

namespace YouTrackSharp.Sprints
{
    /// <summary>
    /// Represents a reference to a previous sprint. The class only provides the sprint's id and name, see <see
    /// cref="YouTrackSharp.Sprints.SprintsService.GetSprint"/> to access further info about this sprint.
    /// </summary>
    public class PreviousSprint
    {
        /// <summary>
        /// Id of the Sprint.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the sprint. Can be null.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}