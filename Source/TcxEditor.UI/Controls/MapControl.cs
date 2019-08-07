using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.IO;
using System.Xml.Serialization;
using GMap.NET.WindowsForms.Markers;
using TcxEditor.Core;
using TcxEditor.Infrastructure;
using TcxEditor.Core.Entities;

namespace TcxEditor.UI
{
    // todo: inject opener command
    public partial class MapControl : UserControl
    {
        public Route CurrentRoute { get; internal set; }

        public MapControl()
        {
            InitializeComponent();
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.SetZoomToFitRect(
                new RectLatLng(53.2, 6.5, 0.2, 0.2));
            gMapControl1.Overlays.Add(new GMapOverlay("route"));
            gMapControl1.Overlays.Add(new GMapOverlay("points"));

            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 3;
        }


        public void SetRoute(Route openedRoute)
        {
            var routeOnMap = new GMapRoute(
                openedRoute.TrackPoints.Select(
                    t => new PointLatLng(
                        t.Lattitude,
                        t.Longitude)),
                "someName");

            routeOnMap.Stroke = new Pen(Color.Red, 3);
            gMapControl1.Overlays.First(o => o.Id.Equals("route")).Routes.Add(routeOnMap);

            GMapOverlay markerOverlay = gMapControl1.Overlays.First(o => o.Id.Equals("points"));
            foreach (var point in openedRoute.CoursePoints)
            {
                GMarkerGoogle marker = new GMarkerGoogle(
                    new PointLatLng(point.Lattitude, point.Longitude),
                    new Bitmap(new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAMAAAAMCGV4AAAASFBMVEX///9ISEgGBgbBwcH39/dpaWmYmJjR0dH09PSBgYGJiYnV1dV4eHh+fn7FxcVSUlJgYGDl5eWjo6Pc3Nx5eXk4ODhfX18nJycGtpgYAAAAcklEQVQImUWPWxaAIAhER83MxNKe+99pKFTzAdzDAQaA5WKmHB1Egy9j2MN4+aHhtEaOtwfiOnFFM17NBCwFv8qCvLXCWtPSlkGpd87OiUC1s+lc6e0Lp0PnlXle9wvzfrlvWXJf/TWJv89/Ef/63yH/PeORA/kIj+u1AAAAAElFTkSuQmCC"))));
                    
                //    GMarkerGoogleType.lightblue_pushpin);
                markerOverlay.Markers.Add(marker);
                marker.ToolTipText = $"{point.Type}\n{point.Notes}";
            }

            CurrentRoute = openedRoute;
        }
    }
}
