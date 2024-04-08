using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsFlightManager
    {
        public List<clsFlight> lstFlights;
      

        /// <summary>
        /// Gets all the flights from the database and returns them in a list
        /// </summary>
        /// <returns></returns>
        public List<clsFlight> GetFlights()
        {
            clsDataAccess db = new clsDataAccess();
            DataSet ds = new DataSet();

            int iRet = 0;
            lstFlights = new List<clsFlight>();

            ds = db.ExecuteSQLStatement(clsSQL.GetFlights(), ref iRet);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                clsFlight Flight = new clsFlight();
                Flight.sFlightID = dr[0].ToString();
                Flight.FlightNumber = dr[1].ToString();
                Flight.AircraftType = dr[2].ToString();
                lstFlights.Add(Flight); 
            }

            return lstFlights;
        }
    }
}
