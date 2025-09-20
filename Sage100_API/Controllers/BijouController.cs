using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Objets100cLib;
using Sage100_API.Models;
using System.Collections;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sage100_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BijouController : ControllerBase
    {
        private readonly SageOptions _sageoptions;
        private readonly string _dbName = "BIJOU";
        private Objets100cLib.BSCPTAApplication100c cpta;

        public BijouController(IOptions<SageOptions> options)
        {
            _sageoptions = options.Value;

            cpta = new Objets100cLib.BSCPTAApplication100c();
            cpta.CompanyServer = _sageoptions.Server;
            
            cpta.Loggable.UserName = _sageoptions.SageUser;
            cpta.Loggable.UserPwd = _sageoptions.SagePassword;

            cpta.CompanyDatabaseName = _dbName;
        }

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
