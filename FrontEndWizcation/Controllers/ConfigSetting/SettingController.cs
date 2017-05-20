using FrontEndWizcation.Modules.ConfigSetting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FrontEndWizcation.Controllers.ConfigSetting
{
    public class SettingController : ApiController
    {

        static readonly ISettingData repository = new SettingData();

        //  api/Setting/ListLanguageData
        [HttpGet]
        [ActionName("ListLanguageData")]
        public DataTable GetListHotel()
        {
            return repository.ListLanguageData();
        }
    }
}
