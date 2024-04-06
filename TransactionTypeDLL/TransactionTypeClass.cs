/* Title:           Transaction Type Class
 * Date:            5-22-16
 * Author:          Terry Holmes
 *
 * Description:     This will populate the table for Transaction Types */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEventLogDLL;

namespace TransactionTypeDLL
{
    public class TransactionTypeClass
    {
        //setting the classes
        EventLogClass TheEventLogClass = new EventLogClass();

        //setting up the data
        TransactionTypeDataSet aTransactionTypeDataSet;
        TransactionTypeDataSet TheTransactionTypeDataSet;
        TransactionTypeDataSetTableAdapters.transactiontypeTableAdapter aTransactionTypeTableAdapter;

        TransactionTypeIDDataSet aTransactionTypeIDDataSet;
        TransactionTypeIDDataSetTableAdapters.transactiontypeidTableAdapter aTransactionTypeIDTableAdapter;

        public bool CreateTransactionTypeEntry(string strTransactionType)
        {
            //setting local variables
            bool blnFatalError = false;

            try
            {
                //filling the data set
                TheTransactionTypeDataSet = GetTransactionTypeInfo();

                //creating new row
                TransactionTypeDataSet.transactiontypeRow NewTableRow = TheTransactionTypeDataSet.transactiontype.NewtransactiontypeRow();

                //setting the fields
                NewTableRow.TransactionID = CreateTransactionTypeID();
                NewTableRow.TransactionType = strTransactionType;

                //updating the data source
                TheTransactionTypeDataSet.transactiontype.Rows.Add(NewTableRow);
                UpdateTransactionTypeDB(TheTransactionTypeDataSet);
            }
            catch (Exception Ex)
            {
                //event log entry
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Transation Type DLL " + Ex.Message);

                //setting the data
                blnFatalError = true;
            }

            //return value
            return blnFatalError;
        }
        public TransactionTypeDataSet GetTransactionTypeInfo()
        {
            //try catch for exception
            try
            {
                //loading the data set
                aTransactionTypeDataSet = new TransactionTypeDataSet();
                aTransactionTypeTableAdapter = new TransactionTypeDataSetTableAdapters.transactiontypeTableAdapter();
                aTransactionTypeTableAdapter.Fill(aTransactionTypeDataSet.transactiontype);
            }
            catch (Exception Ex)
            {
                //event log entry
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Transation Type DLL " + Ex.Message);
            }

            //returning value
            return aTransactionTypeDataSet;
        }
        public void UpdateTransactionTypeDB(TransactionTypeDataSet aTransactionTypeDataSet)
        {
            //try catch for exception
            try
            {
                //loading the data set
                aTransactionTypeTableAdapter = new TransactionTypeDataSetTableAdapters.transactiontypeTableAdapter();
                aTransactionTypeTableAdapter.Update(aTransactionTypeDataSet.transactiontype);
            }
            catch (Exception Ex)
            {
                //event log entry
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Transation Type DLL " + Ex.Message);
            }
        }
        //method to create ID
        public int CreateTransactionTypeID()
        {
            //creating local variable
            int intTransaction = 0;

            //try catch
            try
            {
                //loading the data set
                aTransactionTypeIDDataSet = new TransactionTypeIDDataSet();
                aTransactionTypeIDTableAdapter = new TransactionTypeIDDataSetTableAdapters.transactiontypeidTableAdapter();
                aTransactionTypeIDTableAdapter.Fill(aTransactionTypeIDDataSet.transactiontypeid);

                //creating the id
                intTransaction = Convert.ToInt32(aTransactionTypeIDDataSet.transactiontypeid.Rows[0][1]);
                intTransaction++;

                //updating the data
                aTransactionTypeIDDataSet.transactiontypeid.Rows[0][1] = intTransaction;
                aTransactionTypeIDTableAdapter.Update(aTransactionTypeIDDataSet.transactiontypeid);
            }
            catch (Exception Ex)
            {
                //event log entry
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Transation Type DLL " + Ex.Message);
            }

            //returning value
            return intTransaction;
        }
    }
}
