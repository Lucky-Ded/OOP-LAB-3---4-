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
    abstract class MapObject
    {
        
        public string title;
        public DateTime CreationDate;
        public MapObject(string title)
            {
            this.title = title;
            CreationDate = DateTime.Now;
            }
        public string getTitle()
        {
            return title;
        }
        public DateTime GetCreationData()
            {
                return CreationDate;
            }

        public abstract double getDistance(PointLatLng Point);
        public abstract PointLatLng getFocus();
        public abstract GMapMarker getMarker();
    }
}
