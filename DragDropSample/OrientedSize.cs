namespace DragDropSample
{
    using System.Runtime.InteropServices;

    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The OrientedSize structure is used to abstract the growth direction from
    /// the layout algorithms of WrapPanel.  When the growth direction is
    /// oriented horizontally (ex: the next element is arranged on the side of
    /// the previous element), then the Width grows directly with the placement
    /// of elements and Height grows indirectly with the size of the largest
    /// element in the row.  When the orientation is reversed, so is the
    /// directional growth with respect to Width and Height.
    /// </summary>
    /// <QualityBand>Mature</QualityBand>
    [StructLayout(LayoutKind.Sequential)]
    internal struct OrientedSize
    {
        /// <summary>
        /// Gets the orientation of the structure.
        /// </summary>
        public Orientation Orientation { get; }

        /// <summary>
        /// Gets or sets the size dimension that grows directly with layout
        /// placement.
        /// </summary>
        public double Direct { get; set; }

        /// <summary>
        /// Gets or sets the size dimension that grows indirectly with the
        /// maximum value of the layout row or column.
        /// </summary>
        public double Indirect { get; set; }

        /// <summary>
        /// Gets or sets the width of the size.
        /// </summary>
        public double Width
        {
            get
            {
                return (this.Orientation == Orientation.Horizontal) ?
                    this.Direct :
                    this.Indirect;
            }
            set
            {
                if (Orientation == Orientation.Horizontal)
                {
                    this.Direct = value;
                }
                else
                {
                    this.Indirect = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the size.
        /// </summary>
        public double Height
        {
            get
            {
                return (this.Orientation != Orientation.Horizontal) ?
                    this.Direct :
                    this.Indirect;
            }
            set
            {
                if (this.Orientation != Orientation.Horizontal)
                {
                    this.Direct = value;
                }
                else
                {
                    this.Indirect = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new OrientedSize structure.
        /// </summary>
        /// <param name="orientation">Orientation of the structure.</param>
        public OrientedSize(Orientation orientation) :
            this(orientation, 0.0, 0.0)
        {
        }

        /// <summary>
        /// Initializes a new OrientedSize structure.
        /// </summary>
        /// <param name="orientation">Orientation of the structure.</param>
        /// <param name="width">Un-oriented width of the structure.</param>
        /// <param name="height">Un-oriented height of the structure.</param>
        public OrientedSize(Orientation orientation, double width, double height)
        {
            this.Orientation = orientation;

            // All fields must be initialized before we access the this pointer
            this.Direct = 0.0;
            this.Indirect = 0.0;

            this.Width = width;
            this.Height = height;
        }
    }
}