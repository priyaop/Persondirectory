using Persondirectory.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Http;



namespace Persondirectory.Controllers
{
    public class SearchController : ApiController
    {
        [HttpGet]
        public IList<PersonModel> PerSearch()
        {
            var queryvalues = Request.GetQueryNameValuePairs();           
            string pquery = "";
            string rname = "";
            string rphone = "";
            string raddr = "";
            foreach (KeyValuePair<string, string> qv in queryvalues)
            {

                if (qv.Key == "rname")
                {
                    rname = qv.Value;
                }
                else if (qv.Key == "raddr")
                {
                    raddr = qv.Value;
                }
                else if (qv.Key == "rphone")
                {
                    rphone = qv.Value;
                }
                              
            }
            if ((rname!="")&&(raddr != "")&&(rphone != ""))
            {
                pquery = "select Address,id,Name,Phone,Company from Refdirectory.dbo.Phonedir where name like '%" + rname + "%' and address like '%" + raddr + "%' and phone like '%" + rphone + "%';";
            }
            else if ((rname != "") && (raddr != ""))
            {
                pquery = "select Address,id,Name,Phone,Company from Refdirectory.dbo.Phonedir where name like '%" + rname + "%' and address like '%" + raddr + "%';";
            }
            else if ((raddr!="")&&(rphone != ""))
            {
                pquery = "select Address,id,Name,Phone,Company from Refdirectory.dbo.Phonedir where address like '%" + raddr + "%' and phone like '%" + rphone + "%';";
            }
            else if ((rname != "") && (rphone != ""))
            {
                pquery = "select Address,id,Name,Phone,Company from Refdirectory.dbo.Phonedir where name like '%" + rname + "%' and phone like '%" + rphone + "%';";
            }
            else if (rname != "")
            {
                pquery = "select Address,id,Name,Phone,Company from Refdirectory.dbo.Phonedir where name like '%" + rname + "%';";
            }
            else if (raddr != "")
            {
                pquery = "select Address,id,Name,Phone,Company from Refdirectory.dbo.Phonedir where address like '%" + raddr + "%';";
            }
            else if (rphone != "")
            {
                pquery = "select Address,id,Name,Phone,Company from Refdirectory.dbo.Phonedir where phone like '%" + rphone + "%';";
            }
            else
            {
                pquery = "select top 2 Address,id,Name,Phone,Company from Refdirectory.dbo.Phonedir order by name;";
            }

            SqlConnection Sconn = new SqlConnection(ConfigurationManager.ConnectionStrings["mydb"].ToString());    
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
                    PerAddress = rdr[0].ToString(),
                    PerCompany = rdr[4].ToString()
                }
                );
            }
            Sconn.Close();
            return PerList;
        }
    }

}

