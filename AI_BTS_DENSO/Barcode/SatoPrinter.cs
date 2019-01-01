using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SATOPrinterAPI;
using System.Windows.Forms;
using AI_BTS_DENSO.Common;

namespace AI_BTS_DENSO
{

    class SatoPrinter
    {
        Printer printer = null;
        clsCommon common = new clsCommon();
        public SatoPrinter(Printer.InterfaceType PrinterInterface,string PrinterIp="192.168.1.1",string PortNo="9100")
        {
            try
            {
                printer = new Printer();
                if (PrinterInterface == Printer.InterfaceType.USB)
                {
                    FillUSBPTR();
                }
                else if (PrinterInterface == Printer.InterfaceType.TCPIP)
                {
                    FillTcpPTR(PrinterIp, PortNo);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Printer Object could not be created.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        public void PrinLocationBarCode(string pstrLocationName)
        {
            string sbpl = "";
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("PRN FILES\\Location.Prn", Encoding.UTF8))
                {
                    sbpl = reader.ReadToEnd();
                    reader.Close();
                }
                sbpl = sbpl.Replace("{VARLOCNAME}", pstrLocationName);
                sbpl = sbpl.Replace("{VARLOCLEN}", pstrLocationName.Length.ToString());
                
                if (sbpl == "")
                {
                    //  throw new InfoException("Printing data can not be empty.");
                }
                byte[] sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl);
                printer.Send(sbplByte);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured during label Printing.\r\nError Description: " + ex.InnerException + "\r\n" +
                    "Error at: " + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        public void PrintMachineLabel(string MachineCode, string Desc)
        {
            string sbpl = "";
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("Output.Prn", Encoding.UTF8))
                {
                    sbpl = reader.ReadToEnd();
                    reader.Close();
                }
                sbpl = sbpl.Replace("{VARMACHINECODE}", MachineCode);
                sbpl = sbpl.Replace("{VARDESC}", Desc);
                if (sbpl == "")
                {
                    //  throw new InfoException("Printing data can not be empty.");
                }
                byte[] sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl);
                printer.Send(sbplByte);
            }
            catch (Exception ex)
            {
                throw new Exception("Printing Error : " + ex.Message);
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void FillUSBPTR()
        {
            var USBPorts = printer.GetUSBList();
            if (USBPorts.Count > 0)
            {
                printer.Interface = Printer.InterfaceType.USB;
                printer.USBPortID = USBPorts[0].PortID;
            }
        }
        
        public string QueryStatus()
        {
            byte[] qry = SATOPrinterAPI.Utils.StringToByteArray("");
            byte[] result=printer.Query(qry);
            string status = SATOPrinterAPI.Utils.ByteArrayToString(result);
            return status;
        }
        public string GetprinterStatus()
        {
            string status = "Not Connected";
            try
            {
                
                printer.Connect();
                SATOPrinterAPI.Printer.Status printerStatus = printer.GetPrinterStatus();
                status = printerStatus.Code;
                status = GetStatusMessages(status);
                if (status.StartsWith("ONLINE"))
                {
                    return "OK_" + status;
                }
                else
                {
                    return "NOT_OK_" + status;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                ClsMessage.ShowError("Error in connnecting Printer.");
                throw new Exception("Error Connecting with Printer in GetPrinterStatus");
            }
            finally
            {
                printer.Disconnect();
            }
            return status;
        }
        public void printGRNLabel(string partNo, string partName, string invoiceNo, string ANoticeNo, string quantity, string labelNo,DateTime GRNDatetime,string location, string serialNo)
        {
            string sbpl = "";
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("PRN Files\\GRN.Prn", Encoding.UTF8))
                {
                    sbpl = reader.ReadToEnd();
                    reader.Close();
                }
                sbpl = sbpl.Replace("{VARPARTNO}", partNo);
                sbpl = sbpl.Replace("{VARPARTNAME}", partName);
                sbpl = sbpl.Replace("{VARINVOICENO}", invoiceNo);
                sbpl = sbpl.Replace("{VARANOTICENO}", ANoticeNo);
                sbpl = sbpl.Replace("{VARQTY}", quantity);
                sbpl = sbpl.Replace("{VARLABELNO}", labelNo);
                sbpl = sbpl.Replace("{VARDATE}", GRNDatetime.ToString("dd-MM-yy"));
                sbpl = sbpl.Replace("{VARTIME}", GRNDatetime.ToString("HH:mm"));
                sbpl = sbpl.Replace("{VARLOCATION}", location);
                string barcode = string.Format("{0}|{1}|{2}|{3}|{4}", partNo, ANoticeNo, quantity, location,  serialNo);
                sbpl = sbpl.Replace("{VARBARCODELEN}", barcode.Length.ToString());
                sbpl = sbpl.Replace("{VARBARCODE}", barcode);

                sbpl = sbpl.Replace("{VARBARCODEDATA1}", string.Format("{0}", serialNo));
                //sbpl = sbpl.Replace("{VARBARCODEDATA2}", string.Format("{0}|{1}", location,serialNo));
                if (sbpl == "")
                {
                    //  throw new InfoException("Printing data can not be empty.");
                }
                byte[] sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl);
                printer.Send(sbplByte);

            }
            catch (Exception ex)
            {
                ClsMessage.ShowError(ex.Message);
                throw new Exception("Printing Error : " + ex.Message + System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
        private void FillTcpPTR(string printerIP,string portNo)
        {
            try
            {
                printer.Interface = Printer.InterfaceType.TCPIP;
                printer.TCPIPAddress = printerIP;
                printer.TCPIPPort = portNo;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string GetPrinterStatusBeforePrinting()
        {
            try
            {
                //MessageBox.Show("before status");
                Printer.Status st = printer.GetPrinterStatus();
                string status = GetStatusMessages(st.Code);
                if (status.StartsWith("ONLINE"))
                {
                    return "OK_" + status;
                }
                else
                {
                    return "NOT_OK_" + status;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                throw ex;
            }
        }
        private static string GetStatusMessages(string data)
        {
            switch (Convert.ToChar(data))//HexToInt(data)))
            {
                case '0':
                    return ("OFFLINE_STATE" + " : " + "STATUS_NO_ERROR");

                case '1':
                    return ("OFFLINE_STATE" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case '2':
                    return ("OFFLINE_STATE" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case '3':
                    return ("OFFLINE_STATE" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case '4':
                    return ("OFFLINE_STATE" + " : " + "STATUS_PRINTER_PAUSE");

                case 'A':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_NO_ERROR");

                case 'B':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case 'C':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case 'D':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case 'E':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_PRINTER_PAUSE");

                case 'G':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING");

                case 'H':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case 'I':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case 'J':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case 'K':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING" + " : " + "STATUS_PRINTER_PAUSE");

                case 'M':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY");

                case 'N':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case 'O':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case 'P':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case 'Q':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY" + " : " + "STATUS_PRINTER_PAUSE");

                case 'S':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING");

                case 'T':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case 'U':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case 'V':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case 'W':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING" + " : " + "STATUS_PRINTER_PAUSE");

                case 'b':
                    return ("ERROR_DETECTION" + " : " + "STATUS_HEAD_OPEN");

                case 'c':
                    return ("ERROR_DETECTION" + " : " + "STATUS_PAPER_END");

                case 'd':
                    return ("ERROR_DETECTION" + " : " + "STATUS_RIBBON_END");

                case 'e':
                    return ("ERROR_DETECTION" + " : " + "STATUS_MEDIA_ERROR");

                case 'f':
                    return ("ERROR_DETECTION" + " : " + "STATUS_SENSOR_ERROR");

                case 'g':
                    return ("ERROR_DETECTION" + " : " + "STATUS_HEAD_ERROR");

                case 'h':
                    return ("ERROR_DETECTION" + " : " + "STATUS_CUTTER_OPEN_ERROR");

                case 'i':
                    return ("ERROR_DETECTION" + " : " + "STATUS_CARD_ERROR");

                case 'j':
                    return ("ERROR_DETECTION" + " : " + "STATUS_CUTTER_ERROR");

                case 'k':
                    return ("ERROR_DETECTION" + " : " + "STATUS_OTHER_ERRORS");

                case 'o':
                    return ("ERROR_DETECTION" + " : " + "STATUS_OTHER_IC_TAG_ERROR");

                case 'q':
                    return ("ERROR_DETECTION" + " : " + "STATUS_BATTER_ERROR");
            }
            return "UNEXPECTED_VALUE";
        }
    }
}

