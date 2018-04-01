using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Forms;
using MyTechnicalTask.Abstractions;
using MyTechnicalTask.Models;

namespace MyTechnicalTask.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        #region fields

        private readonly INotificationService _notificationService;
        private string _selectedInputFoldertxb;
        private string _selectedOutputFiletxb;
        private string _selectedoutputFoldertxb;
        private bool _isSerializeButtonEnable;
        private bool _isDeserializeButtonEnable;
        #endregion

        #region Properties

        public MainWindowViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
            IsSerializeButtonEnabled = false;
            IsDeserializeButtonEnabled = false;
        }

        public bool IsDeserializeButtonEnabled
        {
            get { return _isDeserializeButtonEnable; }
            set { SetProperty(ref _isDeserializeButtonEnable, value); }
        }

        public bool IsSerializeButtonEnabled
        {
            get { return _isSerializeButtonEnable; }
            set { SetProperty(ref _isSerializeButtonEnable, value); }
        }

        public string SelectedInputFoldertxb
        {
            get { return _selectedInputFoldertxb; }
            set { SetProperty(ref _selectedInputFoldertxb, value); }
        }

        public string SelectedOutputFiletxb
        {
            get { return _selectedOutputFiletxb; }
            set { SetProperty(ref _selectedOutputFiletxb, value); }
        }

        public string SelectedOutputFoldertxb
        {
            get { return _selectedoutputFoldertxb; }
            set { SetProperty(ref _selectedoutputFoldertxb, value); }
        }

        #endregion

        #region Commands

        public ICommand InputFolderCommand => new DelegateCommand(OnInputFoler);

        public ICommand SerializeCommand => new DelegateCommand(OnSerialize);

        public ICommand SelectOutputFileCommand => new DelegateCommand(OnOutputFile);

        public ICommand SelectOutputFolderCommand => new DelegateCommand(OnOutputFolder);

        public ICommand DeserializeCommand => new DelegateCommand(OnDeserialize);

        #endregion

        #region Methods

        private void OnInputFoler()
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            browserDialog.ShowNewFolderButton = false;

            DialogResult dialogResult = browserDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(browserDialog.SelectedPath))
            {
                SelectedInputFoldertxb = browserDialog.SelectedPath;
                IsSerializeButtonEnabled = true;
            }
            
        }

        private void OnSerialize()
        {
            string inputPath = SelectedInputFoldertxb;
            string outputPath = "SerializeFile.bin";

            string[] folders = Directory.GetDirectories(inputPath, "*", SearchOption.AllDirectories)
                .Select(Path.GetFullPath).ToArray();

            string[] files = Directory.GetFiles(inputPath, "*", SearchOption.AllDirectories).Select(Path.GetFullPath)
                .ToArray();

            var folder = new BinaryManager(folders, files);
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(outputPath, FileMode.OpenOrCreate))
            {
                try
                {
                    formatter.Serialize(fs, folder);
                    _notificationService.SuccessfulSerialization();
                }
                catch (Exception e)
                {
                    _notificationService.ErrorSerialization();
                }
            }

        }

        private void OnOutputFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.bin)|*.bin";

            DialogResult dialogResult = fileDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fileDialog.FileName))
                SelectedOutputFiletxb = fileDialog.FileName;
             

        }

        private void OnOutputFolder()
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            DialogResult result = browserDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(browserDialog.SelectedPath))
            {
                SelectedOutputFoldertxb = browserDialog.SelectedPath;
                IsDeserializeButtonEnabled = true;
            }

        }

        private void OnDeserialize()
        {
            var formatter = new BinaryFormatter();

            using (var fs = File.Open(SelectedOutputFiletxb, FileMode.Open))
            {
                try
                {
                    BinaryManager binaryManager = (BinaryManager)formatter.Deserialize(fs);
                    binaryManager.Unpack(SelectedOutputFoldertxb);

                    _notificationService.SuccessfulSerialization();

                    SelectedInputFoldertxb = string.Empty;
                    SelectedOutputFiletxb = string.Empty;
                    SelectedOutputFoldertxb = string.Empty;

                }
                catch (Exception e)
                {
                    _notificationService.ErrorDerialization();
                }
            }

           
        }

        #endregion

    }
}