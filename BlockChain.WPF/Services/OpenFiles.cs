using System.IO;
using System.Threading.Tasks;
using BlockChain.WPF.Messaging;

namespace BlockChain.WPF.Services {
    public class OpenFiles{

        public OpenFiles(BlockContainer block, MessageCollection messages){
            _block = block;
            _messages = messages;
        }

        readonly BlockContainer _block;
        readonly MessageCollection _messages;

        public async Task Execute(string[] fileNames) {

            _block.Clear();

            foreach (var file in fileNames) {
                _messages.Add($"Opening file {Path.GetFileNameWithoutExtension(file)} ", MessageType.Heading);

                await _block.Add(file);
                _block.JoinInsAndOuts();
            }

            _messages.Add("Opening file complete", MessageType.Heading);
        }
    }
}
