using System.Windows.Media;

namespace BlockChain.WPF.Messaging {

    public class Message {

        public Message(string text){
            Text = text;
        }

        public Message(string text, MessageType messageType = MessageType.Normal) {
            Text = text;
            MessageType = messageType;
        }

        public Message(string text, ImageSource imageSource, MessageType messageType = MessageType.Image) {
            Text = text;
            ImageSource = imageSource;
            MessageType = messageType;
        }

        public string Text { get; set; }
        public ImageSource ImageSource { get; set; }

        public MessageType MessageType { get; set; }
    }
}
