using GalaSoft.MvvmLight;
using System;

namespace Example_1_Server.ViewModel
{
    public class StatusInfoVM : ViewModelBase
    {
        private string id;
        private string state;
        private DateTime timeStamp;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }


        public string State
        {
            get { return state; }
            set { state = value; }
        }


        public string Id
        {
            get { return id; }
            set { id = value; }
        }


        public StatusInfoVM(string id, string state, DateTime timestamp)
        {
            this.Id = id;
            this.State = state;
            this.TimeStamp = timestamp;
        }

        public StatusInfoVM(string id)
        {
            this.Id = id;
        }

        public StatusInfoVM(StatusInfoVM item)
        {
            this.Id = item.Id;
            this.State = item.State;
            this.TimeStamp = item.TimeStamp;
        }
    }
}