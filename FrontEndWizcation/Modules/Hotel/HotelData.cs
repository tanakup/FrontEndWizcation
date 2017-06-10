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
    {   DataSet ViewDetailHotel(int id, string Lang);
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
                         " \r\n  Case When e.Images IS NULL then 'Images/Hotels/noImage.jpg' else e.Images end as  Images,a.AcomodationTypeIds " +
                         " \r\n From Hotel a  join( " +
                         " \r\n select HotelId, Min(Price) as Price From CatergoryRooms " +
                         " \r\n group by HotelId " +
                         " \r\n ) b " +
                         " \r\n on a.HotelId = b.HotelId " +
                        " \r\n Left Outer join ( "+
                        " \r\n select HotelId, Image as Images " +
                        " \r\n From Images " +
                        " \r\n where isDefault = 1 and IsHotel=1" +
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

        public DataSet ViewDetailHotel(int id, string Lang) {

            DataSet ds = new DataSet();
            SqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string strSQL = "";

            try
            {
                strSQL = "Select * From Images where IsDelete=0 " +
                    " and HotelId=" + id;
                DataTable dt = DBHelper.List(strSQL, ObjConn);
                dt.TableName = "slidesImage";
                ds.Tables.Add(dt);


                strSQL = " select "+id+" as HotelId,Count(c.CatergoryRoomsName) as countRoom,  " +
                "\r\n c.CatergoryRoomsId, c.CatergoryRoomsName, Price, MaximunAllowingGuest, " +
                "\r\n c.Cancellation, c.Cancellation_Remark, c.PayAt , " +
                "\r\n case when m.Image IS NULL then '' else m.Image end as  Images "+
                "\r\n From CatergoryRooms c " +
                "\r\n Left outer join rooms r on c.CatergoryRoomsId = r.CatergoryRoomsId " +
                "\r\n Left outer join Images m on c.CatergoryRoomsId=m.CatergoryRoomsId  and m.IsDefault=1 "+
                "\r\n where c.HotelId=" +id + "Group By c.CatergoryRoomsId, c.CatergoryRoomsName," +
                "\r\n Price,MaximunAllowingGuest,c.Cancellation, c.Cancellation_Remark, c.PayAt, m.Image   ";
                DataTable dt1 = DBHelper.List(strSQL, ObjConn);
                dt1.TableName = "roomslist";
                ds.Tables.Add(dt1);



                strSQL = " select * From AmentitiesMapping a " +
                         "\r\n left join Amentities b on a.AmentitiesId = b.AmentitiesId " +
                         "\r\n  where a.HotelId =" + id;

                DataTable dt2 = DBHelper.List(strSQL, ObjConn);
                dt2.TableName = "amentities_mapping";
                ds.Tables.Add(dt2);


                strSQL = " select * From HotelDetail a  "+
                         "\r\n   Left join Description b  on a.HotelID = b.HotelId  " +
                         "\r\n   where a.HotelId =" + id; 
                DataTable dt3 = DBHelper.List(strSQL, ObjConn);
                dt3.TableName = "hotel_detail";
                ds.Tables.Add(dt3);

            }
            catch (Exception e)
            {
            }
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
                    " \r\n where isDefault = 1 and IsHotel=1 " +
                    " \r\n group by HotelId, Image " +
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