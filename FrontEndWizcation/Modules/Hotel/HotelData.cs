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
        DataSet ListAcomodation();
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
                         " \r\n  Case When e.Images IS NULL then 'Images/Hotels/noImage.jpg' else e.Images end as  Images " +
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

        public DataSet ListAcomodation()
        {

            DataSet ds = new DataSet();
            SqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string strSQL = "select * From acomodationtype where IsDelete = 0";
            DataTable dt = DBHelper.List(strSQL, ObjConn);
            dt.TableName = "acomodationtype";
            ds.Tables.Add(dt);
          
            strSQL = "  select * From Amentities where IsDelete = 0;";
            DataTable dt1 = DBHelper.List(strSQL, ObjConn);
            dt1.TableName = "amentities";
            ds.Tables.Add(dt1);

            strSQL = "   select * From Facilities where IsDelete = 0;";
            DataTable dt2 = DBHelper.List(strSQL, ObjConn);
            dt2.TableName = "facilities";
            ds.Tables.Add(dt2);


            ObjConn.Close();
            return ds;
        }


        public DataSet ListHotelTop2(int hotelId)
        {

            DataSet ds = new DataSet();
            SqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string strSQL = "";
            strSQL = " \r\n select Top 2 a.HotelId,a.HotelName,b.Price,c.Content,a.Rate, " +
                     " \r\n  Case When e.Images IS NULL then 'Images/Hotels/noImage.jpg' else e.Images end as  Images " +
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