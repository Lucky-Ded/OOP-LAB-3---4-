using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Device.Location;

namespace Lab3
{
    class Location : MapObject
    {
        private PointLatLng Point; //координата объекта 
        public Location(string title, PointLatLng start) : base(title)
        {
            Point = start;
        }

        public override double getDistance(PointLatLng Point)
        {
            // точки в формате System.Device.Location
            GeoCoordinate c1 = new GeoCoordinate(Point.Lat, Point.Lng);
            GeoCoordinate c2 = new GeoCoordinate(this.Point.Lat, this.Point.Lng);
            // вычисление расстояния между точками в метрах


            return c1.GetDistanceTo(c2); ;
        }
        public PointLatLng getPosition()
        {
            return Point;
        }
        public override PointLatLng getFocus()
        {
            return Point;
        }
        public override GMapMarker getMarker()
        {
            GMapMarker marker_locat = new GMapMarker(Point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    ToolTip = getTitle(),
                    Margin = new System.Windows.Thickness(-16, -16, 0, 0),
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/locat.png")) // картинка
                }
            };
            marker_locat.Position = Point;
            return marker_locat;
        }
    }
}
