using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Objets100cLib;
using System.Collections;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sage100_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BijouController : ControllerBase
    {
        private Objets100cLib.BSCPTAApplication100c cpta;
        public BijouController()
        {
            cpta = new Objets100cLib.BSCPTAApplication100c();
            cpta.CompanyServer = @"SRV-CTA-ABBAYE\SAGE100";
            cpta.CompanyDatabaseName = "BIJOU";
            
        }

        //public SageController(string server, string db, string user, string pw)
        //{
        //    cpta = new Objets100cLib.BSCPTAApplication100c();
        //    cpta.Loggable.UserPwd= pw;
        //    cpta.Loggable.UserName= user;
        //    cpta.CompanyServer = server;
        //    cpta.CompanyDatabaseName = db;

        //}
        // GET: api/<ValuesController>
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            cpta.Open();
            var clientList = cpta.FactoryClient.List;
            List<string> CT_intitule = new List<string>();
            foreach(IBOTiers3 item in clientList)
            {
                CT_intitule.Add(item.CT_Intitule);
            }
            cpta.Close();
            return CT_intitule;
        }
    }
}
