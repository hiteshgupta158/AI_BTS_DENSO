using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AI_BTS_DENSO.Common
{
  
    class ClsMessage
    {
        private const string NO_RECORD_SELECTED_MESSAGE = "No record is selected to Modify/Delete. Please select atleast one record.";
        private const string DELETE_CONFRIMATION_MESSAGE= "Data has been removed successfully";
        private const string DELETE_OPERATION_ERROR_MESSAGE = "Data could not be deleted due to some error. Kindly contact application administrator.";
        private const string SAVE_CONFRIMATION_MESSAGE = "Data has been saved successfully";
        private const string UPDATE_CONFRIMATION_MESSAGE = "Data has been updated successfully";
        private const string CONFIRM_TO_DELETE_MESSAGE = "Do you really want to delete the selected record";
        private const string CONFIRM_TO_PRINT_BARCODE_MESSAGE = "Do you really want to print the barocde of selected records";
        private const string CONFIRM_TO_EXIT_MESSAGE = "Do you really want to exit from current view. It may result in loss of unsaved data";
        public const string NO_RECORD_TO_SELECT_MESSAGE = " There is not record to select.";
        public const string DATA_NOT_SAVE_ERROR_MESSAGE = "Data could not be saved. Kindly contact your administrator.";
        public const string READ_ACCESS_ERROR_MESSAGE = "You do not have permission to view this module. Kindly connect with your application administrator.";

        public static bool addAccess;
               

        public static DialogResult ShowInfo(string Msg)
        {
            return MessageBox.Show(Msg, "Batch Traceability - Information.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowWarning(string Msg)
        {
            return MessageBox.Show(Msg, "Batch Traceability - Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowError(string Msg)
        {
            return MessageBox.Show(Msg, "Batch Traceability - Error.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowConfimation(string Msg)
        {
            return MessageBox.Show(Msg, "Batch Traceability - Confirmation.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }


        public static void ShowDataSavedConfirmation(bool pblnIsAddOpr=true)
        {
            if (pblnIsAddOpr)
                ShowInfo(SAVE_CONFRIMATION_MESSAGE);
            else
                ShowInfo(UPDATE_CONFRIMATION_MESSAGE);
        }

        public static void ShowDataDeleteConfirmation()
        {
            ShowInfo(DELETE_CONFRIMATION_MESSAGE);
        }

        public static DialogResult ShowConfirmToDetele()
        {
            return ShowConfimation(CONFIRM_TO_DELETE_MESSAGE);
        }

        public static DialogResult ShowConfirmToPrintBarCode()
        {
            return ShowConfimation(CONFIRM_TO_PRINT_BARCODE_MESSAGE);
        }

        public static DialogResult ShowConfirmToExit()
        {
            return ShowConfimation(CONFIRM_TO_EXIT_MESSAGE);
        }

        public static void ShowDeleteOperationErrorMessage()
        {
            ShowError(DELETE_OPERATION_ERROR_MESSAGE);
        }


        public static void ShowNoRecordSelectedErrorMessage()
        {
            ShowError(NO_RECORD_SELECTED_MESSAGE);
        }

        public static void ShowReadAccessErrorMessage()
        {
            ShowError(READ_ACCESS_ERROR_MESSAGE);
        }
    }
}
