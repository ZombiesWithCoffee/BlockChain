using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace BlockChain.WPF.Messaging {

    public class MessageCollection : ObservableCollection<Message> {

        public void NewLine(){
            Add(string.Empty);
        }
        
        public void Add(string text, MessageType messageType = MessageType.Normal){
            Add(new Message(text, messageType));
        }

        public void Add(string text, ImageSource imageSource) {
            Add(new Message(text, imageSource));
        }

        public bool Cancel { get; set; }

        public void AddHeading(string text){
            NewLine();
            Add("Walking down Transactions", MessageType.Heading);
            Cancel = false;
        }

        public void AddCompletion(){
            if (Cancel){
                Add("Search Canceled", MessageType.Error);
                Cancel = false;
            }
            else{
                Add("Search Complete", MessageType.Heading);
            }
        }
    }
}
