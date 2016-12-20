using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace BlockChain.WPF.Messaging {

    public class MessageCollection : ObservableCollection<Message> {

        public void NewLine(){
            Add(Environment.NewLine);
        }
        
        public void Add(string text, MessageType messageType = MessageType.Normal){
            Add(new Message(text, messageType));
        }

        public void Add(string text, ImageSource imageSource) {
            Add(new Message(text, imageSource));
        }
    }
}
