using Kepware.ClientAce.OpcDaClient;
using System;
using System.Windows;
using System.Windows.Threading;

namespace DPT_WPF
{
    public partial class HMIM270
    {
        int activeServerSubscriptionHandle;
        int activeClientSubscriptionHandle;
        string SubscriptionUpdateRate = "1000";
        string SubscriptionDeadband = "0";
        bool SubscriptionActiveState = true;

        private bool IsSubscriptionUpdateRateValid()
        {
            // Validate value:
            bool isValid = false;
            int updateRate = -1;

            if (int.TryParse(SubscriptionUpdateRate, out updateRate))
            {
                if (updateRate >= 0 && updateRate <= int.MaxValue)
                {
                    isValid = true;
                }
            }

            // Issue error message:
            if (isValid == false)
            {
                MessageBox.Show("Please enter an update rate between 0 and " + int.MaxValue + " MS.");
            }

            // Set return value:
            return isValid;
        }
        private bool IsSubscriptionDeadbandValid()
        {
            // Validate value:
            bool isValid = false;
            int deadband = -1;

            if (int.TryParse(SubscriptionDeadband, out deadband))
            {
                if (deadband >= 0 && deadband <= 100)
                {
                    isValid = true;
                }
            }

            // Issue error message:
            if (isValid == false)
            {
                MessageBox.Show("Please enter an deadband value between 0 and 100.");
            }

            // Set return value:
            return isValid;
        }
        public Boolean fun_connect(string _ip, string _user)
        {
            Boolean chConnect = false;
            String url = _ip;

            int clientHandle = 1;
            ConnectInfo connectInfo = new ConnectInfo();
            connectInfo.LocalId = "en";
            connectInfo.KeepAliveTime = 60000;
            connectInfo.RetryAfterConnectionError = true;
            connectInfo.RetryInitialConnection = false;
            connectInfo.ClientName = _user;
            bool connectFailed = false;

            try
            {
                daServerMgt.Connect(url, clientHandle, ref connectInfo, out connectFailed);

                if (!connectFailed)
                {
                    chConnect = true;
                    //MessageBox.Show("연결에 성공하였습니다.");
                }
                else
                {
                    chConnect = false;
                }
            }
            catch (Exception ex)
            {
                connectFailed = true;
            }

            return chConnect;
        }
        private void funreading()
        {
            int itemIndex;

            if (IsSubscriptionUpdateRateValid() == false)
            {
                return;
            }


            int clientSubscriptionHandle = 1;
            bool active = true;
            int updateRate = System.Convert.ToInt32(SubscriptionUpdateRate);
            Single deadBand = System.Convert.ToSingle(SubscriptionDeadband);
            ItemIdentifier[] itemIdentifiers = new ItemIdentifier[d.ReadCount];

            for (itemIndex = 0; itemIndex <= d.ReadCount - 1; itemIndex++)
            {
                itemIdentifiers[itemIndex] = new ItemIdentifier();
                itemIdentifiers[itemIndex].ItemName = d.OPCItemNameTextBoxes[itemIndex];
                itemIdentifiers[itemIndex].ClientHandle = itemIndex;
                itemIdentifiers[itemIndex].DataType = Type.GetType("System.Int16");
            }
            int revisedUpdateRate;
            try
            {
                daServerMgt.Subscribe(clientSubscriptionHandle, active, updateRate, out revisedUpdateRate, deadBand,
                                        ref itemIdentifiers, out activeServerSubscriptionHandle);
                activeClientSubscriptionHandle = clientSubscriptionHandle;
                SubscriptionUpdateRate = System.Convert.ToString(revisedUpdateRate);
                for (itemIndex = 0; itemIndex <= d.ReadCount - 1; itemIndex++)
                {

                    if (itemIdentifiers[itemIndex].ResultID.Succeeded == false)
                    {
                        MessageBox.Show("Failed to add item " + itemIdentifiers[itemIndex].ItemName + " to subscription");

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Subscribe exception. Reason: " + ex.Message);
            }
        }
        private void SubscribeToOPCDAServerEvents()
        {
            daServerMgt.ReadCompleted += new DaServerMgt.ReadCompletedEventHandler(daServerMgt_ReadCompleted);
            daServerMgt.WriteCompleted += new DaServerMgt.WriteCompletedEventHandler(daServerMgt_WriteCompleted);
            daServerMgt.DataChanged += new DaServerMgt.DataChangedEventHandler(daServerMgt_DataChanged);
            daServerMgt.ServerStateChanged += new DaServerMgt.ServerStateChangedEventHandler(daServerMgt_ServerStateChanged);
        }
        public void daServerMgt_ServerStateChanged(int clientHandle, ServerState state)
        {
            object[] SSCevHndlrArray = new object[2];
            SSCevHndlrArray[0] = clientHandle;
            SSCevHndlrArray[1] = state;
            Dispatcher.BeginInvoke(new DaServerMgt.ServerStateChangedEventHandler(ServerStateChanged), SSCevHndlrArray);
        }
        private void DisconnectOPCServer()
        {
            try
            {
                if (daServerMgt.IsConnected)
                {
                    daServerMgt.Disconnect();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Handled Disconnect exception. Reason: " + e.Message);
            }
        }
        private void ServerStateChanged(int clientHandle, ServerState state)
        {
            try
            {
                switch (state)
                {
                    case ServerState.ERRORSHUTDOWN:
                        DisconnectOPCServer();
                        MessageBox.Show("The server is shutting down. The client has automatically disconnected.");
                        break;

                    case ServerState.ERRORWATCHDOG:
                        // server connection has failed. ClientAce will attempt to reconnect to the server 
                        // because connectInfo.RetryAfterConnectionError was set true when the Connect method was called.
                        MessageBox.Show("Server connection has been lost. Client will keep attempting to reconnect.");
                        break;

                    case ServerState.CONNECTED:
                        funreading();
                        //Debug.WriteLine("ServerStateChanged, connected");
                        break;

                    case ServerState.DISCONNECTED:
                        Console.WriteLine("ServerStateChanged, disconnected");
                        break;

                    default:
                        MessageBox.Show("ServerStateChanged, undefined state found.");
                        break;
                }
            }
            catch (Exception ex)
            {
                //~~ Handle any exception errors here.
                MessageBox.Show("Handled Server State Changed exception. Reason: " + ex.Message);
            }
        }
        public void daServerMgt_DataChanged(int clientSubscription, bool allQualitiesGood, bool noErrors, ItemValueCallback[] itemValues)
        {
            object[] DCevHndlrArray = new object[4];
            DCevHndlrArray[0] = clientSubscription;
            DCevHndlrArray[1] = allQualitiesGood;
            DCevHndlrArray[2] = noErrors;
            DCevHndlrArray[3] = itemValues;
            Dispatcher.BeginInvoke(new DaServerMgt.DataChangedEventHandler(DataChanged), DCevHndlrArray);
        }
        private void DataChanged(int clientSubscription, bool allQualitiesGood, bool noErrors, ItemValueCallback[] itemValues)
        {
            try
            {
                if (activeClientSubscriptionHandle == clientSubscription)
                {
                    foreach (ItemValueCallback itemValue in itemValues)
                    {
                        int itemIndex = (int)itemValue.ClientHandle;

                        if (itemValue.Value == null)
                        {
                            d.OPCItemValueTextBoxes[itemIndex] = "Unknown";
                        }
                        else
                        {
                            d.OPCItemValueTextBoxes[itemIndex] = itemValue.Value.ToString();
                        }

                        d.OPCItemQualityTextBoxes[itemIndex] = itemValue.Quality.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Data Changed exception. Reason: " + ex.Message);
            }
        }
        public void daServerMgt_WriteCompleted(int transaction, bool noErrors, ItemResultCallback[] itemResults)
        {
            object[] WCevHndlrArray = new object[3];
            WCevHndlrArray[0] = transaction;
            WCevHndlrArray[1] = noErrors;
            WCevHndlrArray[2] = itemResults;
        }
        private void WriteCompleted(int transactionHandle, bool noErrors, ItemResultCallback[] itemResults)
        {
            try
            {
                if (!itemResults[0].ResultID.Succeeded)
                {
                    MessageBox.Show("Async Write Complete failed with error: " + System.Convert.ToString(itemResults[0].ResultID.Code) + "\r\n" + "Description: " + itemResults[0].ResultID.Description);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Async Write Complete exception. Reason: " + ex.Message);
            }
        }
        public void daServerMgt_ReadCompleted(int transactionHandle, bool allQualitiesGood, bool noErrors, ItemValueCallback[] itemValues)
        {
            DaServerMgt.ReadCompletedEventHandler RCevHndlr = new DaServerMgt.ReadCompletedEventHandler(ReadCompleted);
            IAsyncResult returnValue;
            object[] RCevHndlrArray = new object[4];
            RCevHndlrArray[0] = transactionHandle;
            RCevHndlrArray[1] = allQualitiesGood;
            RCevHndlrArray[2] = noErrors;
            RCevHndlrArray[3] = itemValues;

            Dispatcher.Invoke(RCevHndlr, RCevHndlrArray);
        }
        private void ReadCompleted(int transactionHandle, bool allQualitiesGood, bool noErrors, ItemValueCallback[] itemValues)
        {
            int itemIndex = (int)itemValues[0].ClientHandle;

            try
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    if (itemValues[0].ResultID.Succeeded)
                    {
                        if (itemValues[0].Value == null)
                        {
                            d.OPCItemValueTextBoxes[itemIndex] = "Unknown";
                        }
                        else
                        {
                            d.OPCItemValueTextBoxes[itemIndex] = itemValues[0].Value.ToString();
                        }
                    }

                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Handled Async Read Complete exception. Reason: " + ex.Message);
                d.OPCItemQualityTextBoxes[itemIndex] = "OPC_QUALITY_BAD";
            }
        }
    }
}
