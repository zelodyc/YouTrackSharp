using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YouTrackSharp.Internal;

namespace YouTrackSharp.Sprints
{
    /// <summary>
    /// Implementation of <see cref="ISprintsService"/>
    /// </summary>
    public class SprintsService : ISprintsService
    {
        private Connection _connection;
        private FieldSyntaxEncoder _fieldSyntaxEncoder;

        /// <summary>
        /// Creates an instance of <see cref="SprintsService"/> class with given <see cref="Connection"/>
        /// and <see cref="FieldSyntaxEncoder"/> (used to generate the 'fields' parameter
        /// in the YouTrack REST calls).
        /// </summary>
        /// <param name="connection"><see cref="Connection"/> used to query the server.</param>
        /// <param name="fieldSyntaxEncoder">
        /// An <see cref="FieldSyntaxEncoder"/> instance that allows to encode types into Youtrack request URL format
        /// for fields. 
        /// </param>
        public SprintsService(Connection connection, FieldSyntaxEncoder fieldSyntaxEncoder)
        {
            _connection = connection;
            _fieldSyntaxEncoder = fieldSyntaxEncoder;
        }

        /// <inheritdoc />
        public async Task<Sprint> GetSprint(string boardId, string sprintId, bool verbose = false)
        {
            using HttpClient client = await _connection.GetAuthenticatedHttpClient();

            string fields = _fieldSyntaxEncoder.Encode(typeof(Sprint), verbose);

            HttpResponseMessage message = await client.GetAsync($"api/agiles/{boardId}/sprints/{sprintId}?fields={fields}");

            string response = await message.Content.ReadAsStringAsync();

            Sprint sprint = JsonConvert.DeserializeObject<Sprint>(response);

            return sprint;
        }
    }
}