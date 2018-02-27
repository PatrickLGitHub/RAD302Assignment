using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cgMonoGameServer2015.Controllers
{
    [RoutePrefix("api/aZControl")]
    public class aZControlController : ApiController
    {
        [Route("getDate")]
        public string getDate()
        {
            DateTime d = DateTime.UtcNow;
            int day = d.Day;
            int month = d.Month;
            int year = d.Year;
            int hour = d.Hour;
            int minutes = d.Minute;
            int second = d.Second;
            string returnDate = "Date:" + day.ToString() + "-" + month.ToString() + "-" + year.ToString() + " Time:" + hour.ToString()
                + "-" + minutes.ToString() + "-" + second.ToString();
            return returnDate;
        }
    }
}
