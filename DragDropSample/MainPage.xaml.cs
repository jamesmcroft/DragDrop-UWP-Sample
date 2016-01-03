namespace DragDropSample
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Windows.ApplicationModel.DataTransfer;
    using Windows.Storage;
    using Windows.UI.Xaml;

    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Files = new ObservableCollection<AppFile>();
        }

        public ObservableCollection<AppFile> Files { get; }

        private void OnFileDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;

            if (e.DragUIOverride != null)
            {
                e.DragUIOverride.Caption = "Add file";
                e.DragUIOverride.IsContentVisible = true;
            }

            this.AddFilePanel.Visibility = Visibility.Visible;
        }

        private void OnFileDragLeave(object sender, DragEventArgs e)
        {
            this.AddFilePanel.Visibility = Visibility.Collapsed;
        }

        private async void OnFileDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    foreach (var appFile in items.OfType<StorageFile>().Select(storageFile => new AppFile { Name = storageFile.Name, File = storageFile }))
                    {
                        this.Files.Add(appFile);
                    }
                }
            }

            this.AddFilePanel.Visibility = Visibility.Collapsed;
        }
    }
}
