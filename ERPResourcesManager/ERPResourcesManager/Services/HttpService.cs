using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPResourcesManager.Services
{
    static class HttpService
    {
        private const string API_URL = "http://192.168.25.108:3000";

        public static string Login(string username, string password)
        {
            string error;
            try
            {
                var restClient = new RestClient(API_URL + "/login");
                var request = new RestRequest(Method.POST);

                request.AddJsonBody(new { username = username, password = password });

                var response = restClient.Execute<string>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return response.Content;

                else
                    error = response.Content;
            }
            catch (Exception)
            {
                throw new Exception("Impossível se conectar ao servidor!");
            }
            throw new Exception(error);
        }

        public static async System.Threading.Tasks.Task<List<dynamic>> SearchAsync(string cod, string name, string position)
        {
            string error;
            try
            {
                var restClient = new RestClient(API_URL + "/search");
                var request = new RestRequest(Method.GET);

                request.AddParameter("cod", cod);
                request.AddParameter("name", name);
                request.AddParameter("position", position);

                var token = await getAuthTokenAsync();
                request.AddHeader("Authorization", "Bearer " + token);

                var response = restClient.Execute<List<string>>(request);

                List<Object> list = new List<Object>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    foreach (var item in response.Data)
                        list.Add(JsonConvert.DeserializeObject(item));
                }
                else
                    error = response.Content;
                return list;
            }
            catch (Exception)
            {
                throw new Exception("Impossível se conectar ao servidor!");
            }
            throw new Exception(error);
        }

        public static async System.Threading.Tasks.Task<dynamic> GetByIdAsync(int id)
        {
            string error;
            try
            {
                var restClient = new RestClient(API_URL + "/getById");
                var request = new RestRequest(Method.GET);

                request.AddParameter("id", id);

                var token = await getAuthTokenAsync();
                request.AddHeader("Authorization", "Bearer " + token);

                var response = restClient.Execute<dynamic>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return response.Data;
                else
                    error = response.Content;
            }
            catch (Exception)
            {
                throw new Exception("Impossível se conectar ao servidor!");
            }
            throw new Exception(error);
        }

        public static async System.Threading.Tasks.Task<dynamic> GetByCodeAsync(string code)
        {
            string error;
            try
            {
                var restClient = new RestClient(API_URL + "/getByCode");
                var request = new RestRequest(Method.GET);

                request.AddParameter("code", code);

                var token = await getAuthTokenAsync();
                request.AddHeader("Authorization", "Bearer " + token);

                var response = restClient.Execute<dynamic>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return response.Data;
                else
                    error = response.Content;
            }
            catch (Exception)
            {
                throw new Exception("Impossível se conectar ao servidor!");
            }
            throw new Exception(error);
        }

        public static async System.Threading.Tasks.Task<bool> Save(dynamic product)
        {
            string error;
            try
            {
                var restClient = new RestClient(API_URL + "/save");
                var request = new RestRequest(Method.POST);

                request.AddJsonBody(new
                {
                    id = (int)product["id"],
                    name = product["name"].ToString(),
                    desc = product["desc"].ToString(),
                    position = product["position"].ToString(),
                    cod = product["cod"].ToString(),
                    qtd = (int)product["qtd"]
                });

                var token = await getAuthTokenAsync();
                request.AddHeader("Authorization", "Bearer " + token);

                var response = restClient.Execute<bool>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return response.Data;
                else
                    error = response.Content;
            }
            catch (Exception)
            {
                throw new Exception("Impossível se conectar ao servidor!");
            }
            throw new Exception(error);
        }

        private static async System.Threading.Tasks.Task<string> getAuthTokenAsync()
        {
            List<Notes.Models.AuthToken> tokens = await App.Database.GetAuthTokensAsync();
            if (tokens.Count > 0)
                return tokens[0].Token;
            return "";
        }
    }
}
