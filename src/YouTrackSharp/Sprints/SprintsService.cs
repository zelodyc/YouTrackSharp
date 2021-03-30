using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
            string fields = _fieldSyntaxEncoder.Encode(typeof(Sprint), verbose);
            string uri = $"api/agiles/{boardId}/sprints/{sprintId}?fields={fields}";

            return await ExecuteQuery<Sprint>(uri);
        }

        /// <inheritdoc />
        public async Task<List<Sprint>> GetSprints(string boardId, bool verbose = false)
        {
            using HttpClient client = await _connection.GetAuthenticatedHttpClient();

            string fields = _fieldSyntaxEncoder.Encode(typeof(Sprint), verbose);
            
            const int batchSize = 10;
            List<Sprint> sprints = new List<Sprint>();
            List<Sprint> currentBatch;

            do
            {
                string uri = $"api/agiles/{boardId}/sprints?fields={fields}&$top={batchSize}&$skip={sprints.Count}";

                currentBatch = await ExecuteQuery<List<Sprint>>(uri, client);
                sprints.AddRange(currentBatch);
            } while (currentBatch.Count == batchSize);

            return sprints;
        }

        /// <inheritdoc />
        public async Task<Sprint> CreateSprint(string boardId, Sprint sprint, bool verbose = false)
        {
            if (boardId == null)
            {
                throw new ArgumentNullException(nameof(boardId));
            }

            if (sprint == null)
            {
                throw new ArgumentNullException(nameof(sprint));
            }
            
            HttpClient client = await _connection.GetAuthenticatedHttpClient();

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            
            string json = JsonConvert.SerializeObject(sprint, Formatting.None, settings);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            string fields = _fieldSyntaxEncoder.Encode(typeof(Sprint), verbose);
            string uri = $"api/agiles/{boardId}/sprints?fields={fields}";

            HttpResponseMessage message = await client.PostAsync(uri, content);
            message.EnsureSuccessStatusCode();

            string result = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Sprint>(result);
        }

        /// <inheritdoc />
        public async Task DeleteSprint(string boardId, string sprintId)
        {
            if (string.IsNullOrEmpty(boardId))
            {
                throw new ArgumentNullException(nameof(boardId));
            }
            
            if (string.IsNullOrEmpty(sprintId))
            {
                throw new ArgumentNullException(nameof(sprintId));
            }

            using HttpClient client = await _connection.GetAuthenticatedHttpClient();
            HttpResponseMessage message = await client.DeleteAsync($"api/agiles/{boardId}/sprints/{sprintId}");
            
            if (message.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }
            
            message.EnsureSuccessStatusCode();
        }

        private async Task<TResult> ExecuteQuery<TResult>(string uri)
        {
            using HttpClient client = await _connection.GetAuthenticatedHttpClient();

            return await ExecuteQuery<TResult>(uri, client);
        }
        
        private async Task<TResult> ExecuteQuery<TResult>(string uri, HttpClient client)
        {
            HttpResponseMessage message = await client.GetAsync(uri);

            message.EnsureSuccessStatusCode();

            string response = await message.Content.ReadAsStringAsync();

            TResult result = JsonConvert.DeserializeObject<TResult>(response);

            return result;
        } 
    }
}