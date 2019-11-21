using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudyRouteLibrary.Entities;

namespace StudyRouteClient.Models
{
    public class StudyRouteHttpClient
    {
        private HttpClient client;

        private string collegesBaseAddress = "api/colleges/";

        public StudyRouteHttpClient(HttpClient client)
        {
            this.client = client;
            // TODO: Add the base address url
            client.BaseAddress = new Uri("https://localhost:44369/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<College>> GetColleges()
        {
            // TODO: Add the get colleges end point
            var response = await client.GetAsync(collegesBaseAddress);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<College>>(json);
        }

        public async Task<College> GetCollege(int id)
        {
            // TODO: Add the get college end point
            try
            {
                var response = await client.GetAsync(collegesBaseAddress + id);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<College>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PostCollege(College college)
        {
            var body = new StringContent(JsonConvert.SerializeObject(college), Encoding.UTF8, "application/json");
            // TODO: Add the post college end point
            var response = await client.PostAsync(collegesBaseAddress, body);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutCollege(College college)
        {
            var body = new StringContent(JsonConvert.SerializeObject(college), Encoding.UTF8, "application/json");
            // TODO: Add the put college end point
            var response = await client.PutAsync(collegesBaseAddress + college.Id, body);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCollege(int id)
        {
            // TODO: Add the delete college end point
            var response = await client.DeleteAsync(collegesBaseAddress + id);
            return response.IsSuccessStatusCode;
        }

    }
}
