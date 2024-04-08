using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Reflection;

namespace Assignment6AirlineReservation
{
    internal class clsSQL
    {
        /// <summary>
        /// Gets all the information from the flight table in the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFlights()
        {
            try
            {
                string sSQL = $"SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Gets the passengers for the selected flgith
        /// </summary>
        /// <param name="sFlightID">The selected flight</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengers(string sFlightID)
        {
            try
            {
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                "FLIGHT.FLIGHT_ID = " + sFlightID;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the passengers for the selected flgith
        /// </summary>
        /// <param name="sFlightID">The selected flight</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetAllPassengers()
        {
            try
            {
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name " +
                "FROM PASSENGER";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the current passenger
        /// </summary>
        /// <param name="sFlightID">Selected Flight</param>
        /// <param name="seatNum">Selected Seat</param>
        /// <param name="passengerID">passengers ID number</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdatePassengers(string sFlightID,string seatNum, string passengerID)
        {
            try
            {
                string sSQL = $"UPDATE FLIGHT_PASSENGER_LINK " +
                              $"SET Seat_Number = {seatNum} " +
                              $"WHERE FLIGHT_ID = {sFlightID} AND PASSENGER_ID = {passengerID}";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Inserts a passenger into the passenger Table
        /// </summary>
        /// <param name="fName">First Name</param>
        /// <param name="lName">Last Name</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertPassengers(string fName, string lName)
        {
            try
            {
                string sSQL = $"INSERT INTO PASSENGER(First_Name, Last_Name) VALUES ('{fName}', '{lName}')";


                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Inserts data into the link table 
        /// </summary>
        /// <param name="sFlightID">Selected Flight</param>
        /// <param name="sPassengerID">Passengers ID</param>
        /// <param name="seatNum">Seat selected</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertIntoLink(string sFlightID, string sPassengerID, string seatNum)
        {
            try
            {
                string sSQL = $"INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                              $"VALUES( {sFlightID} , {sPassengerID} , {seatNum})";


                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Deletes a passenger from the link table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteLink(string sFlightID, string sPassengerID)
        {
            try
            {
                string sSQL =  "Delete FROM FLIGHT_PASSENGER_LINK " +
                              $"WHERE FLIGHT_ID = {sFlightID} AND " +
                              $"PASSENGER_ID = {sPassengerID}";


                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Delets passenger from the passenger table
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeletePassenger(string sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM PASSENGER " +
                             $"WHERE PASSENGER_ID = {sPassengerID}";


                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
