using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using Example_1_Server.TheServer;
using Example_1.TheClient;
using System.Windows.Input;

namespace Example_1_Server.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private Server server;
        private Client client;

        private const int port = 10100;
        private const string ip = "127.0.0.1";

        private bool isConnected = false;
        private bool isClient = false;
        private bool toggle = false;

        public bool IsClient
        {
            get { return isClient; }
            set { isClient = value; }
        }


        private int number_1 = 0;
        private int number_2 = 0;
        private int number_3 = 0;
        private int number_4 = 0;

        public RelayCommand ToggleBtnClicked_1 { get; set; }
        public RelayCommand ToggleBtnClicked_2 { get; set; }
        public RelayCommand ToggleBtnClicked_3 { get; set; }
        public RelayCommand ToggleBtnClicked_4 { get; set; }

        public RelayCommand ListenBtnClicked { get; set; }
        public RelayCommand ConnectBtnClicked { get; set; }
        public ObservableCollection<StatusInfoVM> HistoryCollection { get; set; }

        public int Number_1   
        {
            get { return number_1; }
            set { number_1 = value; RaisePropertyChanged(); }
        }

        public int Number_2
        {
            get { return number_2; }
            set { number_2 = value; RaisePropertyChanged(); }
        }

        public int Number_3
        {
            get { return number_3; }
            set { number_3 = value; RaisePropertyChanged(); }
        }

        public int Number_4
        {
            get { return number_4; }
            set { number_4 = value; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
            HistoryCollection = new ObservableCollection<StatusInfoVM>();
            
            #region Buttons

            ToggleBtnClicked_1 = new RelayCommand(()=> 
            {
                string color = GetColor(Number_1);
                string button = "button 1";
                DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                StatusInfoVM item = new StatusInfoVM(button, color, time);
                //HistoryCollection.Add(item);
                //ChangeNumber_1();

                //client.Send(button + "," + color + "," + time.ToString());
                server.NewMessageReceived(button + "," + color + "," + time.ToString());
                
            },()=> { return !toggle; });

            ToggleBtnClicked_2 = new RelayCommand(() =>
            {
                string color = GetColor(Number_2);
                string button = "button 2";
                DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                StatusInfoVM item = new StatusInfoVM(button, color, time);
                //HistoryCollection.Add(item);
                //ChangeNumber_2();

                //client.Send(button + "," + color + "," + time.ToString());
                server.NewMessageReceived(button + "," + color + "," + time.ToString());
            }, () => { return !toggle; });

            ToggleBtnClicked_3 = new RelayCommand(() =>
            {
                string color = GetColor(Number_3);
                string button = "button 3";
                DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                StatusInfoVM item = new StatusInfoVM(button, color, time);
                //HistoryCollection.Add(item);
                //ChangeNumber_3();

                //client.Send(button + "," + color + "," + time.ToString());
                server.NewMessageReceived(button + "," + color + "," + time.ToString());
            }, () => { return !toggle; });

            ToggleBtnClicked_4 = new RelayCommand(() =>
            {
                string color = GetColor(Number_4);
                string button = "button 4";
                DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                StatusInfoVM item = new StatusInfoVM(button, color, time);
                //HistoryCollection.Add(item);
                //ChangeNumber_4();

                //client.Send(button + "," + color + "," + time.ToString());
                server.NewMessageReceived(button + "," + color + "," + time.ToString());
            }, () => { return !toggle; });

            ListenBtnClicked = new RelayCommand(()=> 
                {
                    ActAsServer();
                },
            ()=> { return !isConnected; }
            );

            ConnectBtnClicked = new RelayCommand(()=> { ActAsClient(); },
            ()=> { return !IsClient; });

            #endregion

        }

        private void ActAsServer()
        {
            
            server = new Server(ip, port, UpdateGuiWithNewMessage);
            //client = new Client(ip, port, new Action<string>(NewMessageReceived), ClientDisconnected);
            server.StartAccepting();
            isConnected = true;
            IsClient = true;
        }

        private void ActAsClient()
        {
            //server = new Server(ip, port, UpdateGuiWithNewMessage);
            //server.StartAccepting();
            client = new Client(ip, port, UpdateGuiWithNewMessage, ClientDisconnected);
            IsClient = true;
            isConnected = true;
            toggle = true;
        }

        private void ClientDisconnected()
        {
            IsClient = false;
            CommandManager.InvalidateRequerySuggested();
        }

        private void UpdateGuiWithNewMessage(string message)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                string[] messageToAdd = new string[3];
                messageToAdd = message.Split(',');
                StatusInfoVM item = new StatusInfoVM(messageToAdd[0], messageToAdd[1], Convert.ToDateTime(messageToAdd[2]));
                HistoryCollection.Add(item);

                switch (messageToAdd[0])
                {
                    case "button 1":
                        ChangeNumber_1();
                        break;
                    case "button 2":
                        ChangeNumber_2();
                        break;
                    case "button 3":
                        ChangeNumber_3();
                        break;
                    case "button 4":
                        ChangeNumber_4();
                        break;
                    default:
                        break;
                }
            });
        }

        private string GetColor(int number_1)
        {
            if (number_1 == 0)
            {
                return "Green";
            }

            else
            {
                return "Red";
            }
        }

        private void ChangeNumber_1()
        {
            if (Number_1 == 1)
            {
                Number_1 = 0;
            }
            else
            {
                Number_1 = 1;
            }
            
        }
        private void ChangeNumber_2()
        {
            if (Number_2 == 1)
            {
                Number_2 = 0;
            }
            else
            {
                Number_2 = 1;
            }

        }
        private void ChangeNumber_3()
        {
            if (Number_3 == 1)
            {
                Number_3 = 0;
            }
            else
            {
                Number_3 = 1;
            }

        }
        private void ChangeNumber_4()
        {
            if (Number_4 == 1)
            {
                Number_4 = 0;
            }
            else
            {
                Number_4 = 1;
            }

        }
    }
}