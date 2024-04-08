using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsPassengerManager
    {
        public List<clsPassenger> lstPassengers;


        /// <summary>
        /// Get passengers based on the flight ID to add to the passenger combo box
        /// </summary>
        /// <param name="sFlightID">Selected flight ID</param>
        /// <returns></returns>
        public List<clsPassenger> GetPassengers(string sFlightID)
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();

            int iRet = 0;
            lstPassengers = new List<clsPassenger>();

            ds = db.ExecuteSQLStatement(clsSQL.GetPassengers(sFlightID), ref iRet);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                clsPassenger passenger = new clsPassenger();
                passenger.PassengerID = dr[0].ToString();
                passenger.FirstName = dr[1].ToString();
                passenger.LastName = dr[2].ToString();
                passenger.Seat = dr[3].ToString();
                lstPassengers.Add(passenger);
            }

            return lstPassengers;
        }

        /// <summary>
        /// Used to return the entire list of passenger
        /// </summary>
        /// <returns></returns>
        public List<clsPassenger> GetAllPassengers()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();

            int iRet = 0;
            lstPassengers = new List<clsPassenger>();

            ds = db.ExecuteSQLStatement(clsSQL.GetAllPassengers(), ref iRet);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                clsPassenger passenger = new clsPassenger();
                passenger.PassengerID = dr[0].ToString();
                passenger.FirstName = dr[1].ToString();
                passenger.LastName = dr[2].ToString();
                lstPassengers.Add(passenger);
            }

            return lstPassengers;
        }

        /// <summary>
        /// Updates the passenger to passenger table
        /// </summary>
        /// <param name="sFlightID">Selected flight ID</param>
        /// <param name="seatNum">Selected Seat number</param>
        /// <param name="passengerID">Selected passengers id</param>
        /// <returns></returns>
        public int UpdatePassengers(string sFlightID, string seatNum, string passengerID)
        {
            clsDataAccess db = new clsDataAccess();
            return db.ExecuteNonQuery(clsSQL.UpdatePassengers(sFlightID, seatNum, passengerID));
        }
        /// <summary>
        /// Inserts directly into the passenger table
        /// </summary>
        /// <param name="firstName">INput from wndAddPassenger for first name</param>
        /// <param name="lastName">Input from wndAddPassenger for last name</param>
        /// <returns></returns>
        public int InsertPassengers(string firstName, string lastName)
        {
            clsDataAccess db = new clsDataAccess();
            return db.ExecuteNonQuery(clsSQL.InsertPassengers(firstName, lastName));
        }
        /// <summary>
        /// Update to the passenger link table
        /// </summary>
        /// <param name="sFlightID">Selected flight</param>
        /// <param name="sPassengerID">Selected passengers id</param>
        /// <param name="seatNum">Selected passengers seat number</param>
        /// <returns></returns>
        public int InsertPassengersLink(string sFlightID, string sPassengerID, string seatNum)
        {
            clsDataAccess db = new clsDataAccess();
            return db.ExecuteNonQuery(clsSQL.InsertIntoLink(sFlightID, sPassengerID, seatNum));
        }
        /// <summary>
        /// Deletes passenger and passenger link
        /// </summary>
        /// <param name="sFlightID">flight id of selected flight</param>
        /// <param name="sPassengerID">passenger ID of the selected passenger</param>
        /// <returns></returns>
        public int DeletePassengers(string sFlightID, string sPassengerID)
        {
            clsDataAccess db = new clsDataAccess();
            return db.ExecuteNonQuery(clsSQL.DeleteLink(sFlightID, sPassengerID)) + db.ExecuteNonQuery(clsSQL.DeletePassenger(sPassengerID));
        }


    }
}
