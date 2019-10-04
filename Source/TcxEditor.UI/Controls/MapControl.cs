using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TcxEditor.Core.Entities;

namespace TcxEditor.UI
{
    public partial class MapControl : UserControl
    {
        public Route CurrentRoute { get; internal set; }
        public TrackPoint PointToEdit { get; internal set; }

        public event EventHandler<MapClickEventArgs> MapClickEvent;
        public event EventHandler<PointSelectEventArgs> CoursePointSelectEvent;
        
        public MapControl()
        {
            InitializeComponent();
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.SetZoomToFitRect(
                new RectLatLng(53.2, 6.5, 0.2, 0.2));
            gMapControl1.Overlays.Add(new GMapOverlay("route"));
            gMapControl1.Overlays.Add(new GMapOverlay("points"));
            gMapControl1.Overlays.Add(new GMapOverlay("editPoints"));

            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 3;

            gMapControl1.Click += OnMapClick;
            gMapControl1.OnMarkerClick += OnMarkerClick;
        }

        private void OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (!item.Overlay.Id.Equals("points"))
                return;

            CoursePointSelectEvent?.Invoke(
                this,
                new PointSelectEventArgs((DateTime)item.Tag));
        }

        private void OnMapClick(object sender, EventArgs e)
        {
            var args = (MouseEventArgs)e;
            if (args.Button != MouseButtons.Left)
                return;

            var latLon = gMapControl1.FromLocalToLatLng(args.X, args.Y);
            MapClickEvent?.Invoke(
                this,
                new MapClickEventArgs { Lattitude = latLon.Lat, Longitude = latLon.Lng });
        }

        public void SetRoute(Route openedRoute)
        {
            ClearCueMarkers();
            ClearEditMarkers();

            var routeOnMap = new GMapRoute(
                openedRoute.TrackPoints.Select(
                    t => new PointLatLng(
                        t.Lattitude,
                        t.Longitude)),
                "someName");

            routeOnMap.Stroke = new Pen(Color.Red, 3);
            GMapOverlay routeLayer = gMapControl1.Overlays.First(o => o.Id.Equals("route"));
            routeLayer.Routes.Clear();
            routeLayer.Routes.Add(routeOnMap);

            GMapOverlay markerOverlay = gMapControl1.Overlays.First(o => o.Id.Equals("points"));
            foreach (var point in openedRoute.CoursePoints)
            {
                GMarkerGoogle marker = new GMarkerGoogle(
                    new PointLatLng(point.Lattitude, point.Longitude),
                    new Bitmap(new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAMAAAAMCGV4AAAASFBMVEX///9ISEgGBgbBwcH39/dpaWmYmJjR0dH09PSBgYGJiYnV1dV4eHh+fn7FxcVSUlJgYGDl5eWjo6Pc3Nx5eXk4ODhfX18nJycGtpgYAAAAcklEQVQImUWPWxaAIAhER83MxNKe+99pKFTzAdzDAQaA5WKmHB1Egy9j2MN4+aHhtEaOtwfiOnFFM17NBCwFv8qCvLXCWtPSlkGpd87OiUC1s+lc6e0Lp0PnlXle9wvzfrlvWXJf/TWJv89/Ef/63yH/PeORA/kIj+u1AAAAAElFTkSuQmCC"))));

                markerOverlay.Markers.Add(marker);
                marker.ToolTipText = $"{point.Type}\n{point.Notes}";
                marker.Tag = point.TimeStamp;
            }

            CurrentRoute = openedRoute;
        }

        internal void ShowPointToEdit(TrackPoint point)
        {
            ClearEditMarkers();

            PointToEdit = point;

            GMapOverlay editOverlay = gMapControl1.Overlays.First(o => o.Id.Equals("editPoints"));
            editOverlay.Markers.Add(
                new GMarkerGoogle(
                    new PointLatLng(point.Lattitude, point.Longitude),
                    GMarkerGoogleType.blue){ Tag = point.TimeStamp });
        }

        private void ClearCueMarkers()
        {
            GMapOverlay editOverlay = gMapControl1.Overlays.First(o => o.Id.Equals("points"));
            editOverlay.Markers.Clear();
        }

        private void ClearEditMarkers()
        {
            GMapOverlay editOverlay = gMapControl1.Overlays.First(o => o.Id.Equals("editPoints"));
            editOverlay.Markers.Clear();
        }

        internal void SetEditCoursePointMarker(Position position)
        {
            ClearEditMarkers();

            GMapOverlay editOverlay = gMapControl1.Overlays.First(o => o.Id.Equals("editPoints"));
            editOverlay.Markers.Add(
                new GMarkerGoogle(
                    new PointLatLng(position.Lattitude, position.Longitude),
                    GMarkerGoogleType.arrow));
        }
    }

    public class PointSelectEventArgs
    {
        public DateTime _timeStamp { get; }

        public PointSelectEventArgs(DateTime timeStamp)
        {
            _timeStamp = timeStamp;
        }
    }

    public class MapClickEventArgs : EventArgs
    {
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
