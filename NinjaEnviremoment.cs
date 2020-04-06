#region Using declarations
using NinjaTrader.Cbi;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace NinjaEnviremoment
{
    class NinjaEnviremoment
    {
        #region Variables
        private NinjaTrader.Cbi.Account     account { get; set; }
        private NinjaTrader.Cbi.Instrument  instrument { get; set; }

        private List<NinjaTrader.Cbi.Order> orders;


        #endregion
        public NinjaEnviremoment()
        {
            // Class Constructor
        }
        public void Step(int action)
        {
            /*
             * observation 
             * reward
             * done
             */
        }
        public void Reset()
        {
            // Demo Account Reset
        }
        #region Account
        // Define an account
        public void SetAccount(string AccountName = "Sim101")
        {
            try
            {
                if (AccountName is null)
                {
                    throw new ArgumentNullException(nameof(AccountName));
                }
                account = Account.All.FirstOrDefault(a => a.Name == AccountName);
                NinjaTrader.NinjaScript.NinjaScript.Log("Account Changed to:" + account.DisplayName, LogLevel.Information);
            }
            catch(Exception exception)
            {
                // Submits an entry into the Control Center logs to inform the user of an error				
                NinjaTrader.NinjaScript.NinjaScript.Log("Error: Fail to set account.", NinjaTrader.Cbi.LogLevel.Warning);
                // Prints the caught exception in the Output Window
                NinjaTrader.Code.Output.Process(exception.ToString(), PrintTo.OutputTab1);
            }
        }
        // Reset Simulated Account values
        public void ResetAccount(string AccountName) {
            try
            {
                if (AccountName is null)
                {
                    throw new ArgumentNullException(nameof(AccountName));
                }
            }catch(Exception exception)
            {
                // Submits an entry into the Control Center logs to inform the user of an error				
                NinjaTrader.NinjaScript.NinjaScript.Log("Error: Fail to reset account.", NinjaTrader.Cbi.LogLevel.Warning);
                // Prints the caught exception in the Output Window
                NinjaTrader.Code.Output.Process(exception.ToString(), PrintTo.OutputTab1);
            }
        }
        #endregion
        #region Instrument
        // Define an instrument
        public void SetInstrument(string InstrumentName = "MES 03-20")
        {
            try
            {
                if (InstrumentName is null)
                {
                    throw new System.ArgumentNullException(nameof(InstrumentName));
                }

                instrument = Instrument.GetInstrument(InstrumentName);
                NinjaTrader.NinjaScript.NinjaScript.Log("Instrument Changed to:" + instrument.FullName, LogLevel.Information);
            }
            catch(Exception exception)
            {
                // Submits an entry into the Control Center logs to inform the user of an error				
                NinjaTrader.NinjaScript.NinjaScript.Log("Error: Fail to set instrument.", NinjaTrader.Cbi.LogLevel.Warning);
                // Prints the caught exception in the Output Window
                NinjaTrader.Code.Output.Process(exception.ToString(), PrintTo.OutputTab1);
            }
        }
        #endregion
        #region Market Data
        public void MarketData() { }
        #endregion
        #region Order
        public void CreateOrder(Instrument instrument, OrderAction action, OrderType orderType, OrderEntry orderEntry, TimeInForce timeInForce, int quantity, double limitPrice, double stopPrice, string oco, string name, DateTime gtd, CustomOrder customOrder)
        {
            orders.Add(
                account.CreateOrder(
                    instrument,                 // Order instrument

                    OrderAction.Sell,           // Possible values: 
                                                //  OrderAction.Buy
                                                //  OrderAction.BuyToCover
                                                //  OrderAction.Sell
                                                //  OrderAction.SellShort

                    OrderType.StopMarket,       // Possible values:
                                                //  OrderType.Limit
                                                //  OrderType.Market
                                                //  OrderType.MIT
                                                //  OrderType.StopMarket
                                                //  OrderType.StopLimit

                    OrderEntry.Automated,       // Possible values:
                                                //  OrderEntry.Automated
                                                //  OrderEntry.Manual
                                                // Allows setting the tag for orders submitted manually or via automated trading logic

                    TimeInForce.Day,            // Possible values:
                                                //  TimeInForce.Day
                                                //  TimeInForce.Gtc
                                                //  TimeInForce.Gtd
                                                //  TimeInForce.Ioc
                                                //  TimeInForce.Opg

                    1,                          // Order quantity

                    0,                          // Order limit price. Use "0" should this parameter be irrelevant for the OrderType being submitted.

                    1400,                       // Order stop price.Use "0" should this parameter be irrelevant for the OrderType being submitted.

                   "myOCO",                     // A string representing the OCO ID used to link OCO orders together

                    "stopOrder",                // A string representing the name of the order. Max 50 characters.

                    NinjaTrader.Core.Globals.MaxDate,   // A DateTime value to be used with TimeInForce.Gtd - for all other cases you can pass in Core.Globals.MaxDate

                    null                        // Custom order if it is being used
                )
            );
        }
        public void SendOrder()
        {
            account.Submit(new[] { orders[0] });
        }
        public void ListOrder() { }
        #endregion
    }
}
