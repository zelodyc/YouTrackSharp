using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using YouTrackSharp.Agiles;
using YouTrackSharp.Tests.Infrastructure;

namespace YouTrackSharp.Tests.Integration.Agiles
{
    public partial class AgileServiceTest
    {
        public class GetAgilesInMultipleBatches
        {
            /// <summary>
            /// Creates a JSON array from a range of the given json strings, whose size is determined by the "$top" and
            /// "$skip" parameters of the request.<br></br>
            /// This allows to simulate returning a total number of elements, in batches (the size of the batch is
            /// determined by the <see cref="AgileService"/> itself. 
            /// </summary>
            /// <param name="request">REST request, with the $top parameter indicating the max number of results</param>
            /// <param name="jsonObjects">JSON objects from which the JSON array will be created</param>
            /// <returns>
            /// Json array
            /// </returns>
            private HttpResponseMessage GetJsonArray(HttpRequestMessage request, List<string> jsonObjects)
            {
                Range range = GetRequestedRange(request, jsonObjects.Count);

                List<string> agileRange = jsonObjects.GetRange(range.Start.Value, range.End.Value - range.Start.Value);
                string json = $"[{string.Join(",", agileRange)}]";

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(json);

                return response;
            }
 
            /// <summary>
            /// Parses the $skip and $top parameters from a Youtrack REST request URI, and computes the requested range
            /// (capped to given max index)
            /// </summary>
            /// <param name="request">HTTP request</param>
            /// <param name="maxIndex">Max index (range will not go beyond that index, even if $skip + $top is greater</param>
            /// <returns>Range computed from request's $skip and $top</returns>
            private Range GetRequestedRange(HttpRequestMessage request, int maxIndex)
            {
                string requestUri = request.RequestUri.ToString();

                Match match = Regex.Match(requestUri, "(&\\$top=(?<top>[0-9]+))?(&\\$skip=(?<skip>[0-9]+))?");

                int top = maxIndex;
                if (match.Groups.ContainsKey("top") && match.Groups["top"].Success)
                {
                    top = int.Parse(match.Groups["top"].Value);
                }

                int skip = 0;
                if (match.Groups.ContainsKey("skip") && match.Groups["skip"].Success)
                {
                    skip = int.Parse(match.Groups["skip"].Value);
                }

                return new Range(skip, Math.Min(top, maxIndex));
            }

            [Fact]
            public async Task Many_Agiles_Are_Fetched_In_Batches()
            {
                // Arrange
                const int totalAgileCount = 53;
                List<string> jsonStrings = Enumerable.Range(0, totalAgileCount).Select(i => FullAgile01).ToList();

                Connection connection = new ConnectionStub(request => GetJsonArray(request, jsonStrings));
                IAgileService agileService = connection.CreateAgileService();

                // Act
                ICollection<Agile> result = await agileService.GetAgileBoards(true);

                // Assert
                Assert.NotNull(result);
                Assert.NotEmpty(result);
                Assert.Equal(totalAgileCount, result.Count);
            }
        }
    }
}