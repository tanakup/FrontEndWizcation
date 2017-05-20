using FrontEndWizcation.Modules.ConnectDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FrontEndWizcation.Modules.ConfigSetting
{

    public interface ISettingData
    {

        DataTable ListLanguageData();
    }
    public class SettingData: ISettingData
    {
        string errMsg = "";
        public DataTable ListLanguageData()
        {

            DataSet ds = new DataSet();
            SqlConnection ObjConn = DBHelper.ConnectDb(ref errMsg);
            string strSQL = "";
            strSQL = " \r\n select * From Languages where IsDelete=0";
            DataTable dt = DBHelper.List(strSQL, ObjConn);
         
            return dt;
        }
    }
}