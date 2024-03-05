using Newtonsoft.Json;
using RegistrationAPI.Models;
using RestSharp;
using UserAuthentication.Models;
using UserAuthentication.Repository.Interface;


namespace UserAuthentication.Repository.ExternalAPI
{
    public class ExternalCall : IExternalCall
    {
        private readonly string? baseUrl;
        public ExternalCall(IConfiguration config)
        {
            baseUrl = config.GetSection("Enternal")["baseURL"];
        }

        public async Task<ApiResponse<ExternalResponse>> Fetch()
        {
            try
            {
                var options = new RestClientOptions(baseUrl)
                {
                    MaxTimeout = int.MaxValue,
                };
                var client = new RestClient(options);
                var request = new RestRequest("api/", Method.Get);
                request.AddHeader("Content-Type", "application/json");

                RestResponse response = await client.ExecuteAsync(request);

                var result = JsonConvert.DeserializeObject<RootObject>(response.Content);
                var person = new ExternalResponse()
                {
                    firstname = result.Results[0].Name.First,
                    lastname = result.Results[0].Name.Last,
                    email = result.Results[0].Email,
                    country = result.Results[0].Location.Country,
                    gender = result.Results[0].Gender
                };

                if (result == null)
                {
                    return new ApiResponse<ExternalResponse>
                    {
                        Success = false,
                        Message = "Failed to retrieve user details",
                        Data = null
                    };
                }
                var final = new ApiResponse<ExternalResponse>
                {
                    Success = true,
                    Message = "User details",
                    Data = person
                };
                return await Task.FromResult(final)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
