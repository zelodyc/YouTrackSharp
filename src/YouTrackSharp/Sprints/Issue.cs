using Newtonsoft.Json;

namespace YouTrackSharp.Sprints {
  /// <summary>
  /// Represents an issue in the context of an agile sprint. The class only provides the issue's id, summary and issue
  /// number (in project). See <see cref="YouTrackSharp.Issues.IIssuesService"/>.<see
  /// cref="YouTrackSharp.Issues.IIssuesService.GetIssue"/> for more info on how to access a YouTrack issue.
  /// </summary>
  public class Issue {
    /// <summary>
    /// Id of the Issue.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// The text that is entered as the issue summary. Can be null.
    /// </summary>
    [JsonProperty("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// The issue number in the project. Read-only.
    /// </summary>
    [JsonProperty("numberInProject")]
    public long NumberInProject { get; set; }

    /// <summary>
    /// Disables serialization of readonly field <see cref="NumberInProject"/>
    /// </summary>
    /// <returns><c>false</c></returns>
    public bool ShouldSerializeNumberInProject()
    {
        return false;
    }
  }
}
