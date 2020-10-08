using DocumentsSynchronizer.Models;
using DocumentsSynchronizer.Models.RestModels;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentsSynchronizer.Repository
{
    public class GWRequester
    {
        
        private readonly string database;
        private readonly string url;

        private readonly string username;

        private readonly string password;

        private readonly string key;

        public GWRequester(IConfiguration _conf)
        {
            url = _conf.GetSection("GWRestAddress").Value;
            username = _conf.GetSection("GWUser").Value;
            password = _conf.GetSection("GWUserPW").Value;
            key = _conf.GetSection("GWRestApiKey").Value;
            database = _conf.GetSection("GWDB").Value;
        }

        private RestRequest AddAuthorizationToIREstRequest(RestRequest request)
        {
            request.AddHeader("Authorization", "Basic " + createAuthInfo());
            request.AddHeader("X-CAS-PRODUCT-KEY", key);
            request.Method = Method.GET;
            return request;
        }

        private string createAuthInfo()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(database + "/" + username + ":" + password));
        }

        /// <summary>
        /// Gets All Documents
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Documents> GetAllChangedDocuments()
        {
            var rc = new RestClient(url);
            var rr = new RestRequest("/genesisrest.svc/v6.0/type/Document/list");
            rr = AddAuthorizationToIREstRequest(rr);
            var response = rc.ExecuteAsync<List<Documents>>(rr);
            if (response.IsCompletedSuccessfully)
            {
                return response.Result.Data;
            }
            else
            {
                //Fehler behandlung
                throw new Exception("Connection Failed", new Exception(response.Result.Content));
            }
        }


        /// <summary>
        /// Gets all Documents inserted sinde utcTime
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        public IEnumerable<Documents> GetAllChangedDocuments(DateTime utcTime)
        {
            var rc = new RestClient(url);
            var rr = new RestRequest($"/genesisrest.svc/v6.0/type/Document/list?inserted-after={utcTime.ToUniversalTime():o}");
            rr = AddAuthorizationToIREstRequest(rr);
            var response = rc.ExecuteAsync<List<Documents>>(rr);
            if (response.IsCompletedSuccessfully)
            {
                return response.Result.Data;
            }
            else
            {
                //Fehler behandlung
                throw new Exception("Connection Failed", new Exception(response.Result.Content));
            }
        }

        /// <summary>
        /// Get The documentDossier
        /// </summary>
        /// <param name="GUID"></param>
        /// <returns></returns>
        public IEnumerable<DocumentDossier> GetDocumentDossier(string GUID)
        {
            var rc = new RestClient(url);
            var rr = new RestRequest($"/genesisrest.svc/v6.0/type/document/{GUID}/dossier/full");
            rr = AddAuthorizationToIREstRequest(rr);
            var response = rc.ExecuteAsync<List<DocumentDossier>>(rr);
            if (response.IsCompletedSuccessfully)
            {
                return response.Result.Data;
            }
            else
            {
                //Fehler behandlung
                throw new Exception("Connection Failed", new Exception(response.Result.Content));
            }
        }

       

    }
}
