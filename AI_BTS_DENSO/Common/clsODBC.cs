using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;
using AI_BTS_DENSO.Common;

namespace AI_BTS_DENSO.Common
{
    class clsODBC
    {
        public OdbcConnection odbcConnection;
        clsCommon common = new clsCommon();
        public clsODBC(string pstrConnectionString)
        {
            try
            {
                //pstrConnectionString = "DSN=GRN_SIGMA;Uid=dbuser;Pwd=Dbtest@123;";// "DSN=KL_New2;Uid=pckitest;pwd=password";
                common.WriteLog("Connection DSN String: " + pstrConnectionString);
                string lstrUserName = "";
                string lstrPwd = "";
                odbcConnection = new OdbcConnection(pstrConnectionString);
                odbcConnection.Open();
            }
            catch(Exception ex)
            {
                ClsMessage.ShowError("There is some issue with connecting to Cigma. Please contact your application administrator.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        public DataSet ExecOdbcQry(string pstrQry,OdbcConnection pOdbcCon)
        {
            DataSet lds = null;
            lds = new DataSet();

            try
            {
                OdbcDataAdapter da = new OdbcDataAdapter(pstrQry, pOdbcCon);
                da.Fill(lds);
                if (lds != null)
                {
                    if (lds.Tables[0].Rows.Count == 0)
                        lds = null;
                }
                else
                    lds = null;

            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));

            }
            finally
            {
                DisconnectODBC();
            }
            return lds;
        }

        public void DisconnectODBC()
        {
            try
            {
                if (odbcConnection.State == ConnectionState.Open)
                    odbcConnection.Close();
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));

            }
        }
    }
}
