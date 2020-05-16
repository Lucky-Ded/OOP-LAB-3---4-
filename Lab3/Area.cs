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
    class Area : MapObject
    {
        public List<PointLatLng> Point;

        public Area(string title, List<PointLatLng> points) : base(title)
        {
            this.Point = new List<PointLatLng>();
            foreach (PointLatLng p in points)
            {
                this.Point.Add(p);
            }
        }

        public override double getDistance(PointLatLng Point)
        {
            GeoCoordinate c1 = new GeoCoordinate(Point.Lat, Point.Lng);
            GeoCoordinate c2 = new GeoCoordinate(this.Point[0].Lat, this.Point[0].Lng);
           
            return c1.GetDistanceTo(c2);
        }

        public override PointLatLng getFocus()
        {
            return Point[0];
        }

        public override GMapMarker getMarker()
        {
            GMapMarker marker = new GMapPolygon(Point)
            {
                Shape = new Path
                {
                    Stroke = Brushes.Black,
                    Fill = Brushes.Violet,
                    Opacity = 0.7
                }
            };

            return marker;
        }
    }
    
}
