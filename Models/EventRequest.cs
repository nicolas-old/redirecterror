using System;

namespace RedirectError.Models
{
    public class EventRequest
    {
        public EventRequest()
        {
            this.DataConsulta = DateTime.Now;
        }
        public string EvtID { get; set; }
        public string EvtSrc { get; set; }
        public string ProdName { get; set; }
        public string ProdVer { get; set; }
        public string LCID { get; set; }
        public DateTime DataConsulta { get; set; }

    }

}
