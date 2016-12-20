namespace BlockChain.WPF.Messaging {

    public class Message {

        public Message(string text){
            Text = text;
        }

        public Message(string text, MessageType messageType = MessageType.Normal) {
            Text = text;
            MessageType = messageType;
        }

        public string Text { get; set; }
        public MessageType MessageType { get; set; }
    }
}
