using FrontEndWizcation.Modules.ConnectDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FrontEndWizcation.Modules.Hotel
{
    public interface IHotelData
    {
        DataSet ListHotel(int hotelId);
        DataSet ListHotelTop2(int hotelId);
    }
    public class HotelData: IHotelData
    {

        string resultText = "";
        string errMsg = "";

       public DataSet ListHotel(int hotelId) {

            DataSet ds = new DataSet();
            SqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string strSQL = "";
                strSQL = " \r\n select a.HotelId,a.HotelName,b.Price,c.Content,a.Rate, "+
                         " \r\n  Case When e.Images IS NULL then 'Images/Hotels/BC25DCD2-0761-408C-8EF1-CAD88D937C47/hotel_room-t1.jpg' else e.Images end as  Images " +
                         " \r\n From Hotel a  join( " +
                         " \r\n select HotelId, Min(Price) as Price From CatergoryRooms " +
                         " \r\n group by HotelId " +
                         " \r\n ) b " +
                         " \r\n on a.HotelId = b.HotelId " +
                        " \r\n Left Outer join ( "+
                        " \r\n select HotelId, Image as Images " +
                        " \r\n From Images " +
                        " \r\n where isDefault = 1 " +
                        " \r\ngroup by HotelId, Image " +
                        " \r\n) e " +
                        " \r\non a.HotelId = e.HotelId " +
                        " \r\n Left join  Description c on a.HotelId = c.HotelId where a.IsDelete = 'false'";

            if (hotelId > 0) {
                strSQL += " and a.HotelId="+ hotelId;
            }
            strSQL += " Order by b.Price ASC ";
            DataTable dt = DBHelper.List(strSQL, ObjConn);
            DataTable dt1 = DBHelper.List(" select * From acomodationtype where IsDelete = 0 ", ObjConn);
            ObjConn.Close();

                dt.TableName = "hotels";
                ds.Tables.Add(dt);
                dt1.TableName = "acomodationtype";
                ds.Tables.Add(dt1);
            return ds;
        }

        public DataTable ListAcomodation()
        {
            
            SqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string strSQL = "select * From acomodationtype where IsDelete = 0";
           
            DataTable dt = DBHelper.List(strSQL, ObjConn);
            ObjConn.Close();
            return dt;
        }


        public DataSet ListHotelTop2(int hotelId)
        {

            DataSet ds = new DataSet();
            SqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string strSQL = "";
            strSQL = " \r\n select Top 2  a.HotelId,a.HotelName,b.Price,c.Content,a.Rate, " +
                     " \r\n  Case When e.Images IS NULL then '/Images/Hotels/BC25DCD2-0761-408C-8EF1-CAD88D937C47/hotel_room-t1.jpg' else e.Images end as  Images " +
                     " \r\n From Hotel a  join( " +
                     " \r\n select HotelId, Min(Price) as Price From CatergoryRooms " +
                     " \r\n group by HotelId " +
                     " \r\n ) b " +
                     " \r\n on a.HotelId = b.HotelId " +
                    " \r\n Left Outer join ( " +
                    " \r\n select HotelId, Image as Images " +
                    " \r\n From Images " +
                    " \r\n where isDefault = 1 " +
                    " \r\ngroup by HotelId, Image " +
                    " \r\n) e " +
                    " \r\non a.HotelId = e.HotelId " +
                    " \r\n Left join  Description c on a.HotelId = c.HotelId where a.IsDelete = 'false'";

            if (hotelId > 0)
            {

                strSQL += " and a.HotelId=" + hotelId;
            }
            strSQL += " Order by b.Price ASC ";
            DataTable dt = DBHelper.List(strSQL, ObjConn);
            ObjConn.Close();
            dt.TableName = "hotels";
            ds.Tables.Add(dt);
            return ds;
        }

        
    }
}