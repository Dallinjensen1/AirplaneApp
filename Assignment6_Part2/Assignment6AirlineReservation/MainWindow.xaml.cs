using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        clsDataAccess clsData;
        wndAddPassenger wndAddPass;
        clsFlightManager clsFlight;
        clsPassengerManager clsPassenger;
        Label selectedSeat;
        Brush selectedSeatColor;
        List<clsPassenger> passengers;
        
        //string to hold flight ID
        string flightID;
        //Boolean to determine if add passenger is true
        bool selectFlightMode = false;
        string flightModeName = "";
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                
                
                clsFlight = new clsFlightManager();
                clsPassenger = new clsPassengerManager();
                List<clsFlight> flights = new List<clsFlight>();
                flights = clsFlight.GetFlights();
                
               

                cbChooseFlight.ItemsSource = flights.Select(i => i.sFlightID).Distinct().ToList();





            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// When a selection is changed on the flight combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                c767_Seats.IsEnabled = true;
                gPassengerCommands.IsEnabled = false;

                flightID = cbChooseFlight.SelectedItem.ToString();

                //Enable the correct fligth based on the combo box
                if (flightID == "1")
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                }

                setSeats();


            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Button to add a passenger, send you to new window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();


                //if saved is != null
                if(wndAddPass.saved)
                {
                    //Insert passenger passing in the input from the add passenger window
                    clsPassenger.InsertPassengers(wndAddPass.txtFirstName.Text, wndAddPass.txtLastName.Text);
                    
                    //disable UI
                    gPassengerCommands.IsEnabled = false;
                    gbPassengerInformation.IsEnabled = false;
                    
                    //set UI to to flight mode
                    selectFlightMode = true;
                    flightModeName = wndAddPass.txtFirstName.Text;
                }
                          
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Handles errors for the try catch
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        /// <summary>
        /// Everytime a label is clicked SeatClick is called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeatClick(object sender, MouseButtonEventArgs e)
        {
            try
            {


                if (!selectFlightMode)

                {
                    if (selectedSeat != null)
                    {
                        selectedSeat.Background = selectedSeatColor;
                    }

                    selectedSeat = (Label)sender;

                    selectedSeatColor = (Brush)selectedSeat.Background;
                    var converter = new BrushConverter();
                    selectedSeat.Background = (Brush)converter.ConvertFromString("#FF00FD00");

                    //Hold the passenger object from list 
                    var selectedPass = passengers.Where(p => p.Seat == (string)selectedSeat.Content).FirstOrDefault();


                    //Null check
                    if (selectedPass != null)
                    {
                        cbChoosePassenger.SelectedIndex = 0;
                        cbChoosePassenger.SelectedValue = selectedPass.FirstName;
                        lblPassengersSeatNumber.Content = selectedPass.Seat;
                    }
                }
                else
                {
                    selectedSeat = (Label)sender;

                    if (selectedSeat.Background != Brushes.Red)
                    {
                        passengers = new List<clsPassenger>();
                        passengers = clsPassenger.GetAllPassengers();


                        var selectedPass = passengers.Where(p => p.FirstName == flightModeName).FirstOrDefault();
                        
                        //Insert into passenger table
                        clsPassenger.InsertPassengersLink(flightID, selectedPass.PassengerID, selectedSeat.Content.ToString());
                        clsPassenger.UpdatePassengers(flightID, selectedSeat.Content.ToString(), selectedPass.PassengerID);
                        selectFlightMode = false;
                        gPassengerCommands.IsEnabled = true;
                        gbPassengerInformation.IsEnabled = true;
                        setSeats();
                    }

                  }
                }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Passenger Combo box is changed this will set selected index to green       /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cbChoosePassenger.SelectedValue == null)
                {
                    cbChoosePassenger.SelectedIndex = 0;
                }

                var selectedPass = passengers.Where(p => p.FirstName == cbChoosePassenger.SelectedValue.ToString()).FirstOrDefault();

                lblPassengersSeatNumber.Content = selectedPass.Seat;

                gPassengerCommands.IsEnabled = true;

                var converter = new BrushConverter();


                //This whole section is to change the background color of the selected combo box to green
                if (selectedSeat != null)
                {
                    selectedSeat.Background = selectedSeatColor;
                }

                if (flightID == "1")
                {
                    foreach (Label child in c767_Seats.Children)

                    {

                        if ((string)child.Content == selectedPass.Seat)
                        {
                            child.Background = (Brush)converter.ConvertFromString("#FF00FD00");
                            selectedSeat = child;
                            selectedSeatColor = Brushes.Red;
                        }

                    }
                }
                else if (flightID == "2")
                {
                    foreach (Label child in cA380_Seats.Children)
                    {

                        if ((string)child.Content == selectedPass.Seat)
                        {
                            child.Background = (Brush)converter.ConvertFromString("#FF00FD00");
                            selectedSeat = child;
                            selectedSeatColor = Brushes.Red;
                        }

                    }

                }
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }


        }
        /// <summary>
        /// Button to change seat 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                //Var to hold passenger object from list
                var selectedPass = passengers.Where(p => p.FirstName == cbChoosePassenger.SelectedValue.ToString()).FirstOrDefault();
                
                //Send that info into the UpdatePassengers SQL and execute
                clsPassenger.UpdatePassengers(flightID, selectedSeat.Content.ToString(), selectedPass.PassengerID);
                
                //Change their seat to red
                selectedSeatColor = Brushes.Red;
                
                //reload board
                setSeats();
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }




        }
        /// <summary>
        /// Button to delete passengers from the list and set their seat to available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Null check
                if (cbChoosePassenger.SelectedValue == null)
                {

                }
                else
                {
                    //Holds Passenger 
                    var selectedPass = passengers.Where(p => p.FirstName == cbChoosePassenger.SelectedValue.ToString()).FirstOrDefault();
                    //Deletes passenger and passenger link in one line!
                    clsPassenger.DeletePassengers(flightID, selectedPass.PassengerID);
                    //Refresh board
                    setSeats();
                }
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }



        }
        /// <summary>
        /// Sets the seats of the entire canvas. Loops through each passenger and label to compare and set the correct background color.
        /// </summary>
        private void setSeats()
        {

            try
            {
                passengers = new List<clsPassenger>();
                passengers = clsPassenger.GetPassengers(flightID);

                cbChoosePassenger.IsEnabled = true;
                cbChoosePassenger.ItemsSource = passengers.Select(i => i.FirstName).Distinct().ToList();
                if (flightID == "1")
                {
                    //Loop through children
                    foreach (Label child in c767_Seats.Children)

                    {
                        //set background to blue by default in case of refresh or swapping flights
                        child.Background = Brushes.Blue;
                        //loop through passengers
                        foreach (clsPassenger p in passengers)
                        {
                            if ((string)child.Content == p.Seat)
                            {
                                child.Background = Brushes.Red;
                            }

                        }

                    }
                    //loop through children
                    foreach (Label child in cA380_Seats.Children)
                    {
                        child.Background = Brushes.Blue;
                    }
                }
                else if (flightID == "2")
                {
                    //loop through children
                    foreach (Label child in cA380_Seats.Children)
                    {

                        child.Background = Brushes.Blue;
                        //loop through passengers
                        foreach (clsPassenger p in passengers)
                        {
                            if ((string)child.Content == p.Seat)
                            {
                                child.Background = Brushes.Red;
                            }
                        }

                    }
                    //loop through children
                    foreach (Label child in c767_Seats.Children)
                    {
                        child.Background = Brushes.Blue;
                    }

                }
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }

        }
    }
    }

