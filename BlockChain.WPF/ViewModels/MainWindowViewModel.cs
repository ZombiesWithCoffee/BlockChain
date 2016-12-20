using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using BlockChain.WPF.Dialogs;
using BlockChain.WPF.Messaging;
using BlockChain.WPF.Properties;
using BlockChain.WPF.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;

namespace BlockChain.WPF.ViewModels {

    public class MainWindowViewModel : ViewModelBase {

        public BlockContainer Blocks { get; } = new BlockContainer();

        public MessageCollection Messages { get; } = new MessageCollection();

        public ICommand ClearCommand => new RelayCommand(() => {
            Messages.Clear();
        });

        public ICommand Exit => new RelayCommand(() => {
            Application.Current.Shutdown();
        });

        public ICommand SettingsCommand => new RelayCommand(() => {
            var settingsDialog = new SettingsDialog();
            settingsDialog.ShowDialog();
        });

        public ICommand OpenCommand => new RelayCommand(async() => {

            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = "dat",
                Title = "Select bitcoin block",
                Multiselect = true,
                InitialDirectory = Settings.Default.InputPath
            };

            if (openFileDialog.ShowDialog() == false)
                return;

            try{
                Mouse.OverrideCursor = Cursors.Wait;

                Blocks.Clear();

                foreach (var file in openFileDialog.FileNames){
                    Messages.Add($"Opening file {Path.GetFileNameWithoutExtension(file)} ");
                    await Blocks.Add(file);
                    Blocks.JoinInsAndOuts();
                }
            }
            finally{
                Mouse.OverrideCursor = Cursors.Arrow;
                Messages.Add("Opening file complete");
            }
        });

        public ICommand ReadTransaction => new RelayCommand(() => {

            var openTransactionDialog = new OpenTransactionsDialog();

            if (openTransactionDialog.ShowDialog() == false)
                return;

            try {
                Mouse.OverrideCursor = Cursors.Wait;

                var readTransaction = new ReadTransaction(Blocks, Messages);
                readTransaction.Execute(openTransactionDialog.ViewModel.Transaction);
            }
            catch (Exception ex) {
                Messages.Add($"Exception: {ex.Message}", MessageType.Error);
            }
            finally {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        });

        public ICommand WalkAddresses => new RelayCommand(() => {

            var openTransactionDialog = new OpenTransactionsDialog();

            if (openTransactionDialog.ShowDialog() == false)
                return;

            Messages.NewLine();
            Messages.Add("Walking Transaction");

            try {
                Mouse.OverrideCursor = Cursors.Wait;

                var addresses = Blocks.WalkAddresses(openTransactionDialog.ViewModel.Transaction);

                if (addresses == null) {
                    Messages.Add("Transaction not found");
                    return;
                }

                foreach (var result in addresses){
                    Messages.Add($"{result}");
                }
            }
            catch (Exception ex) {
                Messages.Add($"Exception: {ex.Message}", MessageType.Error);
            }
            finally {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        });

        public ICommand DownloadFile => new RelayCommand(() => {

            var openTransactionDialog = new OpenTransactionsDialog();

            if (openTransactionDialog.ShowDialog() == false)
                return;

            try {
                Mouse.OverrideCursor = Cursors.Wait;

                var downloadFile = new DownloadFile(Blocks);
                var fileName = downloadFile.Download(openTransactionDialog.ViewModel.Transaction.Split('\r', '\n'));

                Messages.Add($"File saved to {fileName}");
            }
            catch (Exception ex) {
                Messages.Add(ex.Message, MessageType.Error);
            }
            finally {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        });

        public ICommand DownloadSatoshiUploadedFile => new RelayCommand(() => {

            var openTransactionDialog = new OpenTransactionsDialog();

            if (openTransactionDialog.ShowDialog() == false)
                return;

            try {
                Mouse.OverrideCursor = Cursors.Wait;

                var downloadSatoshi = new DownloadSatoshi(Blocks);
                var fileName = downloadSatoshi.Download(openTransactionDialog.ViewModel.Transaction);

                Messages.Add($"File saved to {fileName}");
            }
            catch (InvalidDataException ex) {
                Messages.Add(ex.Message, MessageType.Error);
            }
            finally {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        });

        public ICommand SearchForKnownExtensions => new RelayCommand(async() => {

            var openBlocksDialog = new OpenBlocksDialog();

            if (openBlocksDialog.ShowDialog() == false)
                return;

            var knownExtensions = new KnownExtensions(Messages);

            try{
                Mouse.OverrideCursor = Cursors.Wait;
                await knownExtensions.Search(openBlocksDialog.ViewModel.Start, openBlocksDialog.ViewModel.Stop);
            }
            finally{
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        });

        public ICommand SearchForHighFees => new RelayCommand(async () => {

            var openBlocksDialog = new OpenBlocksDialog();

            if (openBlocksDialog.ShowDialog() == false)
                return;

            var knownExtensions = new HighFees(Messages);

            try {
                Mouse.OverrideCursor = Cursors.Wait;
                await knownExtensions.Search(openBlocksDialog.ViewModel.Start, openBlocksDialog.ViewModel.Stop);
            }
            finally {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        });

        public ICommand SearchSatoshiUploads => new RelayCommand(async() => {

            var openBlocksDialog = new OpenBlocksDialog();

            if (openBlocksDialog.ShowDialog() == false)
                return;

            var satoshiUploads = new SatoshiUploads(Messages);

            try {
                Mouse.OverrideCursor = Cursors.Wait;
                await satoshiUploads.Search(openBlocksDialog.ViewModel.Start, openBlocksDialog.ViewModel.Stop);
            }
            finally {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        });

        public ICommand SearchForTransactionId => new RelayCommand(async() => {

            var openTransactionDialog = new OpenTransactionsDialog();

            if (openTransactionDialog.ShowDialog() == false)
                return;

            var searchTransactions = new SearchTransactionId(Messages);

            try {
                Mouse.OverrideCursor = Cursors.Wait;
                await searchTransactions.Search(openTransactionDialog.ViewModel.Transaction, 2, 706);
            }
            finally {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        });


        public ICommand QueryTextMessages => new RelayCommand(() => {

            Messages.NewLine();
            Messages.Add("Searching for text messages");

            foreach (var block in Blocks) {
                foreach (var transaction in block.Transactions){

                    var fileData = Blocks.GetFile(transaction.ToString());

                    if (fileData == null){
                        continue;
                    }

                    if (fileData.Extension == ".txt"){
                        Messages.Add($"{transaction} - {fileData.Text}");
                    }
                }
            }
        });


        public ICommand QuerySeveralTxOutsFile => new RelayCommand(() => {
            Messages.NewLine();
            Messages.Add("Finding Transactions that have a high number of TxOut Transaction");

            foreach (var block in Blocks) {
                foreach (var transaction in block.Transactions) {

                    if (transaction.Outs.Unique < 30){
                        continue;
                    }

                    if (transaction.Outs.SpentCount > 2){
                        continue;
                    }

                    Messages.Add($"{transaction} - {transaction.Outs.Count}");
                }
            }
        });
    }
}
