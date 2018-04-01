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

        private FileData _binary;

        private readonly INotificationService _notificationService;
        private readonly IBinaryService _binaryService;

        private string _serializeFolderPath;
        private string _deserializeFolderPath;

        private bool _isSerializeButtonEnable;
        private bool _isDeserializeButtonEnable;

        private string _binFilePath;

        #endregion

        #region Properties

        public MainWindowViewModel(INotificationService notificationService, IBinaryService binaryService)
        {
            _notificationService = notificationService;
            _binaryService = binaryService;

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


        public string SerializeFolderPath
        {
            get { return _serializeFolderPath; }
            set { SetProperty(ref _serializeFolderPath, value); }
        }
       
        public string DeserializeFolderPath
        {
            get { return _deserializeFolderPath; }
            set { SetProperty(ref _deserializeFolderPath, value); }
        }

        #endregion

        #region Commands

        public ICommand SelectSerializeFolderPathCommand => new DelegateCommand(OnSelectedSerializeFolder);

        public ICommand SerializeCommand => new DelegateCommand(OnSerialize);
        
        public ICommand SelectDeserializeFolderPathCommand => new DelegateCommand(OnSelectedDeserializeFolder);

        public ICommand DeserializeCommand => new DelegateCommand(OnDeserialize);

        #endregion

        #region Methods

        private void OnSelectedSerializeFolder()
        {
            var browserDialog = new FolderBrowserDialog();
            browserDialog.ShowNewFolderButton = false;

            var dialogResult = browserDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(browserDialog.SelectedPath))
            {
                SerializeFolderPath = browserDialog.SelectedPath;
                IsSerializeButtonEnabled = true;
            }            
        }

        private void OnSerialize()
        {
            string inputPath = SerializeFolderPath;
            string outputPath = "SerializeFile.bin";

            string[] folders = Directory.GetDirectories(inputPath, "*", SearchOption.AllDirectories)
                .Select(Path.GetFullPath).ToArray();

            string[] files = Directory.GetFiles(inputPath, "*", SearchOption.AllDirectories).Select(Path.GetFullPath)
                .ToArray();


            _binary = new Models.FileData(folders, files);
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(outputPath, FileMode.OpenOrCreate))
            {
                try
                {
                    formatter.Serialize(fs, _binary);
                    _notificationService.SuccessfulSerialization();
                }
                catch (Exception e)
                {
                    _notificationService.ErrorSerialization();
                }

                _binFilePath = fs.Name;
            }

        }
        
        private void OnSelectedDeserializeFolder()
        {
            var browserDialog = new FolderBrowserDialog();
            var result = browserDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(browserDialog.SelectedPath))
            {
                DeserializeFolderPath = browserDialog.SelectedPath;
                IsDeserializeButtonEnabled = true;
            }
        }

        private void OnDeserialize()
        {
            if (!File.Exists(_binFilePath))
            {
                _notificationService.ErrorFileNotExist();
            }

            var formatter = new BinaryFormatter();
            using (var fs = File.Open(_binFilePath, FileMode.Open))
            {
                try
                {
                    Models.FileData binaryDeserialize = (Models.FileData)formatter.Deserialize(fs);
                    _binaryService.Unpack(DeserializeFolderPath,_binary);
                  
                    _notificationService.SuccessfulSerialization();

                    SerializeFolderPath = string.Empty;
                    DeserializeFolderPath = string.Empty;

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