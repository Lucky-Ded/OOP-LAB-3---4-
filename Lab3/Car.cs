using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Device.Location;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Threading;

namespace Lab3
{
    class Car : MapObject
    {
        private PointLatLng Point; //координата объекта 
        GMapMarker marker_car;
        public Human human;
        public event EventHandler Arrived;
        public event EventHandler Follow;
      
        public MapRoute route { get; private set; }
        GMapControl gmap = null;
        private Route Route1;
        public Car(string title, PointLatLng Point, GMapControl map) : base(title)
        {
            this.Point = Point;
            gmap = map;
        }

        public override double getDistance(PointLatLng Point)
        {
            // точки в формате System.Device.Location
            GeoCoordinate c1 = new GeoCoordinate(Point.Lat, Point.Lng);
            GeoCoordinate c2 = new GeoCoordinate(this.Point.Lat, this.Point.Lng);
          
            return c1.GetDistanceTo(c2);
        }

        public override PointLatLng getFocus()
        {
            return Point;
        }
        public override GMapMarker getMarker()
        {
            marker_car = new GMapMarker(Point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    ToolTip = getTitle(), // всплывающая подсказка
                    Margin = new System.Windows.Thickness(-16, -16, 0, 0),
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/car.png")) // картинка
                }
            };
            
            
            marker_car.Position = Point;
            return marker_car;
        }
        public GMapMarker MoveTo(PointLatLng endPoint)
        {
            RoutingProvider routingProvider = GMapProviders.OpenStreetMap;
            route = routingProvider.GetRoute(
                Point,
                endPoint,
                false,
                false,
                15);

            List<PointLatLng> routePoints = route.Points;
            this.Route1 = new Route("", routePoints);

            Thread ridingCar = new Thread(MoveByRoute);
            ridingCar.Start();


            return this.Route1.getMarker();
        }
        private void MoveByRoute()
        {

            foreach (var Point in route.Points)
            {
                    Application.Current.Dispatcher.Invoke(delegate
             {
                 if (marker_car != null)
                 {
                     this.Point = Point;
                     marker_car.Position = Point;
                 }
                 if (human != null)
                 {
                     human.marker_human.Position = Point;
                     Follow?.Invoke(this, null);
                 }

             });
                Thread.Sleep(500);
            }
                if (human == null)
                {

                    Arrived?.Invoke(this, null);

               }
                
            
        }

        public void passengerSeated(object sender, EventArgs args)
        {

            human = (Human)sender;

            Application.Current.Dispatcher.Invoke(delegate {
                gmap.Markers.Add(MoveTo(human.destination));
            });
          
            human.Point = Point;

        }
    }
}
