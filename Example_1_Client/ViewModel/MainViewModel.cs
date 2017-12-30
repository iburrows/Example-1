using Example_1_Client.Communication;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Example_1_Client.ViewModel
{
  
    public class MainViewModel : ViewModelBase
    {
        private Client clientcom;
        private bool isConnected = false;
        

        public ObservableCollection<string> History { get; set; }
        public RelayCommand ConnectBtnClicked { get; set; }
       
        public MainViewModel()
        {
            History = new ObservableCollection<string>();
            ConnectBtnClicked = new RelayCommand(()=> 
            {
                isConnected = true;
                clientcom = new Client("127.0.0.1", 10100, new Action<string>(NewMessageReceived), ClientDisconnected);
            },
            ()=> { return !isConnected; });
        }

        private void NewMessageReceived(string message)
        {
            App.Current.Dispatcher.Invoke(() => { History.Add(message); });
        }

        private void ClientDisconnected()
        {
            isConnected = false;
            CommandManager.InvalidateRequerySuggested();
        }
    }
}