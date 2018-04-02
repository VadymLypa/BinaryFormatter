using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Forms;
using Common;
using Common.Models;
using Extensibility;

namespace MyTechnicalTask.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region fields

        private readonly List<string> _listOfFolder;
        private readonly List<string> _listOfFiles;

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

            _listOfFolder = new List<string>();
            _listOfFiles = new List<string>();

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
            browserDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(browserDialog.SelectedPath))
            {
                SerializeFolderPath = browserDialog.SelectedPath;
                IsSerializeButtonEnabled = true;
            }            
        }

        private async void OnSerialize()
        {
            WalkDirectoryTree(SerializeFolderPath);
            _binary = new FileData(_listOfFolder, _listOfFiles);

            await Task.Run(() =>
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream("SerializeFile.bin", FileMode.OpenOrCreate))
                {
                    try
                    {
                        formatter.Serialize(fs, _binary);
                        _notificationService.SuccessfulSerialization();
                    }
                    catch (Exception ex)
                    {
                        _notificationService.ErrorSerialization(ex.Message);
                    }

                    _binFilePath = fs.Name;
                }
            });
        }

        private void WalkDirectoryTree(string dir)
        {
            if (!Directory.Exists(dir))
            {
                _notificationService.DirNotFoundException();
                return;
            }

            foreach (string file in Directory.GetFiles(dir))
                    _listOfFiles.Add(file);

            foreach (string folder in Directory.GetDirectories(dir))
            {
                _listOfFolder.Add(folder);
                try
                {
                    WalkDirectoryTree(folder);
                }

                catch (UnauthorizedAccessException ex)
                {
                    _notificationService.ErrorSerialization(ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    _notificationService.ErrorSerialization(ex.Message);
                }
            }

        }

        private void OnSelectedDeserializeFolder()
        {
            var browserDialog = new FolderBrowserDialog();
            browserDialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(browserDialog.SelectedPath))
            {
                DeserializeFolderPath = browserDialog.SelectedPath;
                IsDeserializeButtonEnabled = true;
            }
        }

        private async void OnDeserialize()
        {
            if (!File.Exists(_binFilePath))
            {
                _notificationService.DirNotFoundException();
                DeserializeFolderPath = string.Empty;
                return;
            }

            await Task.Run(() =>
            {
                var formatter = new BinaryFormatter();
                using (var fs = File.Open(_binFilePath, FileMode.Open))
                {
                    try
                    {
                        var binaryDeserialize = (FileData) formatter.Deserialize(fs);

                        _binaryService.Unpack(DeserializeFolderPath, _binary);

                        _notificationService.SuccessfulDeserialization();
                        ResetData();
                    }
                    catch (Exception ex)
                    {
                        _notificationService.ErrorDeSerialization(ex.Message);
                        ResetData();
                    }
                }
            });
        }

        private void ResetData()
        {
            SerializeFolderPath = string.Empty;
            DeserializeFolderPath = string.Empty;

            IsSerializeButtonEnabled = false;
            IsDeserializeButtonEnabled = false;
        }

        #endregion
    }
}