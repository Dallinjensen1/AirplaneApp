using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsPassenger
    {
        /// <summary>
        /// String to hold passenger ID
        /// </summary>
        public string PassengerID { get; set; }
        
        /// <summary>
        /// String to hold First Name
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// String to hold last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// String to hold passenger seat number
        /// </summary>
        public string Seat { get; set; }
        /// <summary>
        /// Stright to hold passenger flight
        /// </summary>
        public string Flight { get; set; }
        /// <summary>
        /// Overide to concatenate First and Last name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
