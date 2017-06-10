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

        //  api/ModuleHotel/ListHotel/:id
        [HttpGet]
        [ActionName("ViewDetailHotel")]
        public DataSet GetViewDetailHotel(int id,string Lang)
        {
            return repository.ViewDetailHotel(id,Lang);
        }
        //  api/ModuleHotel/ListHotelTop2/:id
        [HttpGet]
        [ActionName("ListHotelTop2")]
        public DataSet GetListHotelTop2(int id)
        {
            return repository.ListHotelTop2(id);
        }

        //  api/ModuleHotel/ListAcomodation
        [HttpGet]
        [ActionName("ListAcomodation")]
        public DataSet GetListAcomodation()
        {
            return repository.ListAcomodation();
        }
        
    }
}
