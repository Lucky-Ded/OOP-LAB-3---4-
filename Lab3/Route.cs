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
    class Route : MapObject
    {
        private List<PointLatLng> points = new List<PointLatLng>();
        public Route(string title, List<PointLatLng> points) : base(title)
        {
            
           
            foreach (PointLatLng p in points)
            {
                this.points.Add(p);
            }
        }

        public override double getDistance(PointLatLng point)
        {
            GeoCoordinate c1 = new GeoCoordinate(point.Lat, point.Lng);
            GeoCoordinate c2 = new GeoCoordinate(this.points[0].Lat, this.points[0].Lng);
            double distance = c1.GetDistanceTo(c2);
            return c1.GetDistanceTo(c2);
        }

        

        public override GMapMarker getMarker()
        {
            GMapMarker marker = new GMapRoute(points)
            {
                Shape = new Path()
                {
                    Stroke = Brushes.DarkBlue, // цвет обводки
                    Fill = Brushes.DarkBlue, // цвет заливки
                    StrokeThickness = 4 // толщина обводки
                }
            };
            return marker;
        }
        public override PointLatLng getFocus()
        {
            return points[0];
        }
    }
}
