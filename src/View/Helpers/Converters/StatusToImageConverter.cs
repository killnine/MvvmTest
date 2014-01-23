using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Model;

namespace View.Helpers
{
    [ValueConversion(typeof(Status), typeof(bool))]
    public class StatusToImageConverter : IValueConverter
    {
        public static StatusToImageConverter Instance = new StatusToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PathGeometry pathData = new PathGeometry();

            if ((Status)value == Status.Connected)
            {
                pathData.AddGeometry(Geometry.Parse(""));
            }
            else if ((Status)value == Status.Disconnected)
            {
                pathData.AddGeometry(Geometry.Parse("F1 M 58.5832,55.4172L 17.4169,55.4171C 15.5619,53.5621 15.5619,50.5546 17.4168,48.6996L 35.201,15.8402C 37.056,13.9852 40.0635,13.9852 41.9185,15.8402L 58.5832,48.6997C 60.4382,50.5546 60.4382,53.5622 58.5832,55.4172 Z M 34.0417,25.7292L 36.0208,41.9584L 39.9791,41.9583L 41.9583,25.7292L 34.0417,25.7292 Z M 38,44.3333C 36.2511,44.3333 34.8333,45.7511 34.8333,47.5C 34.8333,49.2489 36.2511,50.6667 38,50.6667C 39.7489,50.6667 41.1666,49.2489 41.1666,47.5C 41.1666,45.7511 39.7489,44.3333 38,44.3333 Z"));
            }
            else
            {
                pathData.AddGeometry(Geometry.Parse("F1 M 37.2083,49.0833C 39.3945,49.0833 41.1667,50.8555 41.1667,53.0417C 41.1667,55.2278 39.3945,57 37.2083,57C 35.0222,57 33.25,55.2278 33.25,53.0417C 33.25,50.8555 35.0222,49.0833 37.2083,49.0833 Z M 38,17.4167C 44.9956,17.4167 50.6666,21.9416 50.6666,28.5C 50.6666,30.875 49.0833,34.8333 45.9167,36.4167C 42.75,38 41.1667,40.1267 41.1667,42.75L 41.1667,45.9167L 33.25,45.9167L 33.25,43.5417C 33.25,38.1571 38,34.8333 39.5833,33.25C 42.75,30.0833 42.75,29.644 42.75,28.5C 42.75,25.8767 40.6233,23.75 38,23.75C 35.3766,23.75 33.25,25.8767 33.25,28.5L 33.25,30.875L 25.3333,30.875L 25.3333,29.2917C 25.3333,22.7333 31.0044,17.4167 38,17.4167 Z"));
            }

            return pathData;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Cannot convert back from image to ConnectionStatus!");
        }
    }
}
