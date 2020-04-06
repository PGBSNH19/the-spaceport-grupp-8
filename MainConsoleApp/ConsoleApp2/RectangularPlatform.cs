using System;
using System.Transactions;

namespace ConsoleApp2
{
    //-----------------------------------------------------------------------------
    // Rectangular Platform                                 .............................
    //      - endast fickparkerings platser, ie rectangle   ..   P    .   🚗   .  P   ..
    //                                                           ^
    //                                                          🚗
    //-----------------------------------------------------------------------------
    public class RectangularPlatform
    {
        readonly double _totalLength;

        public RectangularPlatform(double x)
        {
            _totalLength = x;
        }

      
        public double CalculatePercentage(double shipLength)
        {
            var value = Convert.ToInt32(shipLength / _totalLength * 100);
            return value;
        }

        public double CalculateDockingFee(double shipLength)
        {
            // TODO: make a better calculation here....
            return CalculatePercentage(shipLength);    // How much % of the deck does the ship take? each % = +1kr
        }

        public double CalculateCheckoutFee(StarWarsPerson pilot)
        {
            var timespan = pilot.ExitTime.HasValue ? pilot.ExitTime.Value - pilot.EntryTime : TimeSpan.Zero;
            return (Math.Round((timespan.TotalMinutes/30))*5);
        }

 
        public void DockShip(StarWarsPerson pilot)
        {
            DbUtils.AddNewCustomerDocking(pilot);
            Logger.systemLog($"Docked Ship: {pilot.ShipName}", ConsoleColor.Green);
        }
        
        public void CheckoutShip(StarWarsPerson pilot)
        {
            var existing = DbUtils.CheckOutCustomer(pilot);
            var fee = CalculateCheckoutFee(existing);
            Logger.systemLog($"Ship Left The Parkinglot docking fee is {fee}", ConsoleColor.Green);
        }


        public bool ShipWillFit(double dShipLength)
        {
            var totalAfterAdd = CalculatePercentage(dShipLength) + CalculatePercentage(DbUtils.GetCurrentOccupiedSpace());
            return  totalAfterAdd < 98;
        }


        public void ShowCapacity()
        {
            var occupied = DbUtils.GetCurrentOccupiedSpace();

            var percent = occupied < 1 ? 0 : Convert.ToInt32(occupied / _totalLength * 100);
            Logger.systemLog($"Dock capacity: {100-percent}% left", ConsoleColor.Gray);
        }



    }
}
