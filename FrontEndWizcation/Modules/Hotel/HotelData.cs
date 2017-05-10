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

    }
    public class HotelData: IHotelData
    {

        string resultText = "";
        string errMsg = "";

       public DataSet ListHotel(int hotelId) {

            DataSet ds = new DataSet();
            SqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string strSQL = "";
                strSQL = "select * From hotel";
                DataTable dt = DBHelper.List(strSQL, ObjConn);
                ObjConn.Close();
                dt.TableName = "hotels";
                ds.Tables.Add(dt);
            return ds;
        }




    }
}