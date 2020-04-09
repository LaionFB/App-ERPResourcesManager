using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPResourcesManager.Services
{
    static class HttpService
    {
        private const string API_URL = "http://192.168.25.107:3000";

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
            catch (Exception e)
            {
                throw new Exception("Impossível se conectar ao servidor!");
            }
            throw new Exception(error);
        }
    }
}
