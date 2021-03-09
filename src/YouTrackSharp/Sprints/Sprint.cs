using Newtonsoft.Json;
using System.Collections.Generic;

namespace YouTrackSharp.Sprints {
  /// <summary>
  /// Represents a sprint that is associated with an agile board. Each sprint can include issues from one or more
  /// projects.
  /// </summary>
  public class Sprint {
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

    /// <summary>
    /// Goal of the sprint. Can be null.
    /// </summary>
    [JsonProperty("goal")]
    public string Goal { get; set; }

    /// <summary>
    /// The start date of the sprint as a timestamp. Can be null.
    /// </summary>
    [JsonProperty("start")]
    public long? Start { get; set; }

    /// <summary>
    /// The end date of the sprint as a timestamp. Can be null.
    /// </summary>
    [JsonProperty("finish")]
    public long? Finish { get; set; }

    /// <summary>
    /// Indicates whether the sprint is archived.
    /// </summary>
    [JsonProperty("archived")]
    public bool Archived { get; set; }

    /// <summary>
    /// If true, then new issues that match a column on this board are automatically added to this sprint.
    /// </summary>
    [JsonProperty("isDefault")]
    public bool IsDefault { get; set; }

    /// <summary>
    /// Issues that are present on this sprint.
    /// </summary>
    [JsonProperty("issues")]
    public List<Issue> Issues { get; set; }

    /// <summary>
    /// Number of unresolved issues on this sprint. Read-only.
    /// </summary>
    [JsonProperty("unresolvedIssuesCount")]
    public int UnresolvedIssuesCount { get; set; }

    /// <summary>
    /// If you provide this attribute when you create a new sprint, then all unresolved issues from this sprint will be
    /// moved to the newly created sprint.
    /// </summary>
    [JsonProperty("previousSprint")]
    public Sprint PreviousSprint { get; set; }
  }
}
