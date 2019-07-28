using System;
using Windows.UI.Xaml.Media;

namespace ProjectSparky.Model
{
    public class BackgroundImage
    {
        public BackgroundImage(ImageSource _Image, DateTime _ImageDate)
        {
            Image = _Image;
            ImageDate = _ImageDate;
        }

        public ImageSource Image { get; set; }
        public DateTime ImageDate { get; set; }
    }
}
