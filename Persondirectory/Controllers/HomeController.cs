using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Persondirectory.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Newtonsoft.Json;

namespace Persondirectory.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public IList<PersonModel> GetPerRec()
        {
            SqlConnection Sconn = new SqlConnection(ConfigurationManager.ConnectionStrings["mydb"].ToString());
            string pquery = "";
            pquery = "select Address,id,Name,Phone from Refdirectory.dbo.Phonedir;";
            SqlCommand cmd = new SqlCommand(pquery, Sconn);
            Sconn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            List<PersonModel> PerList = new List<PersonModel>();
            while (rdr.Read())
            {
                PerList.Add(new PersonModel()
                {
                    PerId = rdr[1].ToString(),
                    PerName = rdr[2].ToString(),
                    PerPhone = rdr[3].ToString(),
                    PerAddress = rdr[0].ToString()                    
                }
                );
            }
            Sconn.Close();
            return PerList;
        }

        [HttpPost]
        public object PostPerRec()
        {
            var contentType = Request.Content.Headers.ContentType.MediaType;
            var requestParams = Request.Content.ReadAsStringAsync().Result;
            List<PersonModel> lmodel = JsonConvert.DeserializeObject<List<PersonModel>>(requestParams);
            List<StatusModel> Slist = new List<StatusModel>();
            try
            {
                if (contentType == "application/json")
                {
                    SqlConnection Iconn = new SqlConnection(ConfigurationManager.ConnectionStrings["mydb"].ToString());
                    string Insquery = "";
                    foreach (PersonModel lm in lmodel)
                    {
                        System.Diagnostics.Debug.WriteLine("this is " + lm.PerId);
                        System.Diagnostics.Debug.WriteLine("this is " + lm.PerName);
                        Insquery = "Insert into [Refdirectory].[dbo].[Phonedir] (Id,Name,Address,Phone,Company) VALUES(" + lm.PerId + ",'" + lm.PerName + "','" + lm.PerAddress + "','" + lm.PerPhone + "','" + lm.PerCompany + "');";
                        SqlCommand Icmd = new SqlCommand(Insquery, Iconn);
                        try
                        {
                            Iconn.Open();
                            SqlDataReader Irdr = Icmd.ExecuteReader();
                            Slist.Add(new StatusModel()
                            {
                                Perid = lm.PerId,
                                Status = "SUCCESS",
                                Statusmsg = "No. of rows inserted is " + Irdr.RecordsAffected
                            });
                            Iconn.Close();
                        }
                        catch (Exception ex)
                        {
                            Iconn.Close();
                            Slist.Add(new StatusModel()
                            {
                                Perid = lm.PerId,
                                Status = "FAILED",
                                Statusmsg = ex.Message
                            });
                        }
                    }
                    return Slist;
                }
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            catch (HttpResponseException Hex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Hex.Response.StatusCode.ToString());
            }
        }
    //latest commit
    }
}
