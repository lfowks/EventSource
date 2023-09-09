using EventSource;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Text;
using System.Threading.Tasks;

namespace QuartzSample
{
    public class NumberGeneratorJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using var client = new HttpClient();

            var user = new User();

            user.Id = RandomNumber(1, 1000);
            user.Name = "Name1";
            user.Email = "Email1";
            user.Password = "";


            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:7185/api/notification/";

            try
            {
                var response = await client.PostAsync(url, data);
            }
            catch (Exception ex)
            {

                throw;
            }

            await Task.CompletedTask;
        }

        private int RandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}