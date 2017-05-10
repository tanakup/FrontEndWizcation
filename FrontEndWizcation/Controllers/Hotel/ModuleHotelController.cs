using FrontEndWizcation.Modules.Hotel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FrontEndWizcation.Controllers.Hotel
{
    public class ModuleHotelController : ApiController
    {
        static readonly IHotelData repository = new HotelData();

        //  api/ModuleHotel/ListHotel/:id
        [HttpGet]
        [ActionName("ListHotel")]
        public DataSet GetListHotel(int id)
        {
            return repository.ListHotel(id);
        }
    }
}
