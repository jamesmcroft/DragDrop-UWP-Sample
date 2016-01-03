using System;

namespace DragDropSample
{
    using Windows.Storage;
    using Windows.Storage.FileProperties;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    using Microsoft.Xaml.Interactivity;
    public class FileThumbnailImageSourceBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty FileProperty = DependencyProperty.Register("File", typeof(StorageFile), typeof(FileThumbnailImageSourceBehavior), new PropertyMetadata(null, OnFileChanged));

        private static async void OnFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                return;
            }

            var behavior = d as FileThumbnailImageSourceBehavior;
            if (behavior?.Parent == null)
            {
                return;
            }

            var thumb =
                await
                behavior.File.GetThumbnailAsync(ThumbnailMode.SingleItem, 32, ThumbnailOptions.ResizeThumbnail);

            if (thumb == null)
            {
                return;
            }

            var bitmapImage = new BitmapImage();
            bitmapImage.SetSource(thumb.CloneStream());

            behavior.Parent.Source = bitmapImage;
        }

        public StorageFile File
        {
            get
            {
                return (StorageFile)GetValue(FileProperty);
            }
            set
            {
                SetValue(FileProperty, value);
            }
        }

        public void Attach(DependencyObject associatedObject)
        {
            if (this.AssociatedObject != null)
            {
                throw new InvalidOperationException("Cannot assign to the behavior twice.");
            }

            this.AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            this.AssociatedObject = null;
        }

        public Image Parent => this.AssociatedObject as Image;

        public DependencyObject AssociatedObject { get; private set; }
    }
}
