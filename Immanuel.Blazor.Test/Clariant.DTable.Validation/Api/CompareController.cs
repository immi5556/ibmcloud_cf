using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clariant.DTable.Validation.Api
{
    [Route("api/compare")]
    public class CompareController : Controller
    {
        [HttpPost]
        [Route("tables")]
        public dynamic GetTables([FromBody]JsonElement input)
        {
            var mdl = System.Text.Json.JsonSerializer.Deserialize<DtModel>(input.GetRawText());
            DTables.GetTables(mdl);
            return mdl;
        }
        [HttpPost]
        [Route("compare")]
        public dynamic CompareTable([FromBody] JsonElement input)
        {
            var mdl = System.Text.Json.JsonSerializer.Deserialize<DtModel>(input.GetRawText());
            DTables.CompareTable(mdl);
            return mdl;
        }
    }
}
