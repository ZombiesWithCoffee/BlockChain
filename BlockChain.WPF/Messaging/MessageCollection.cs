using System.Collections.ObjectModel;

namespace BlockChain.WPF.Messaging {

    public class MessageCollection : ObservableCollection<Message> {

        public void Add(string text, MessageType messageType = MessageType.Normal){
            Add(new Message(text, messageType));
        }
    }
}
