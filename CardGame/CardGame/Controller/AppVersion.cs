using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Controller
{
    public static class AppVersion
    {
        public static async Task<Model.AppStatus> CheckForUpdate()
        {
            HttpClient webclient = new HttpClient();

            string urls = ""; //Model.Credentials.BaseUrl + Model.Constants.VersionNumnber.ToString();
            
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(urls).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Model.StatusMessage deserialized = JsonConvert.DeserializeObject<Model.StatusMessage>(responseContent);
                        if (deserialized.Code == 200)
                        {
                            List<Model.AppStatus> ress = new List<Model.AppStatus>();
                            ress = JsonConvert.DeserializeObject<List<Model.AppStatus>>(deserialized.Result.ToString());
                            return ress.First();
                        }
                        else
                        {
                            throw new Exception(deserialized.Description);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return null;            
        }

        public static async Task<bool> AddSuggestion(string name,string suggestion,string version,string status,string device)
        {
            HttpClient webclient = new HttpClient();

            string urls = $"";//{Model.Credentials.ApiUrl}{Model.Credentials.SuggestionApi}name={name}&suggestion={suggestion}&version={version}&status={status}&device={device}";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(urls).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Model.StatusMessage deserialized = JsonConvert.DeserializeObject<Model.StatusMessage>(responseContent);
                        if (deserialized.Code == 200)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception(deserialized.Description);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return false;
        }
    }
}
