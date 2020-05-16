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
    class Human : MapObject
    {
        public PointLatLng Point { get; set; } //координата объекта 

        public PointLatLng destination { get; set; }
        public GMapMarker marker_human  { get;  set; }
    public event EventHandler seated;
    public Human(string title, PointLatLng Point): base(title)
        {
            this.Point = Point;
        }

        public override double getDistance(PointLatLng Point)
        {
            // точки в формате System.Device.Location
            GeoCoordinate c1 = new GeoCoordinate(Point.Lat, Point.Lng);
            GeoCoordinate c2 = new GeoCoordinate(this.Point.Lat, this.Point.Lng);
            // вычисление расстояния между точками в метрах
           // double distance = c1.GetDistanceTo(c2);

            return c1.GetDistanceTo(c2);
        }
        public PointLatLng getPosition()
        {
            return Point;
        }
        public void moveTo(PointLatLng dest)
        {
            destination = dest;
        }
        public override PointLatLng getFocus()
        {
            return Point;
        }
        public override GMapMarker getMarker()
        {
            marker_human = new GMapMarker(Point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    ToolTip = getTitle(),
                    Margin = new System.Windows.Thickness(-16, -16, 0, 0),
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/human.png")) // картинка
                }            };
            marker_human.Position = Point;
            return marker_human;
        }
        public void CarArrived(object sender, EventArgs args)
        {
            seated?.Invoke(this, EventArgs.Empty);

        }
    }

}
