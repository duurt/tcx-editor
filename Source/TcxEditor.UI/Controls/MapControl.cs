using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Configuration;
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
        private readonly GMapOverlay LAYER_ROUTE = new GMapOverlay("route");
        private readonly GMapOverlay LAYER_POINTS = new GMapOverlay("points");
        private readonly GMapOverlay LAYER_EDIT_POINTS = new GMapOverlay("editPoints");

        public event EventHandler<MapClickEventArgs> MapClickEvent;
        public event EventHandler<PointSelectEventArgs> CoursePointSelectEvent;

        public MapControl()
        {
            InitializeComponent();
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.MapProvider = GoogleMapProvider.Instance;

            gMapControl1.Overlays.Add(LAYER_ROUTE);
            gMapControl1.Overlays.Add(LAYER_POINTS);
            gMapControl1.Overlays.Add(LAYER_EDIT_POINTS);

            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 10;

            gMapControl1.Click += OnMapClick;
            gMapControl1.OnMarkerClick += OnMarkerClick;
        }

        public void SetLocation(string location)
        {
            gMapControl1.MapProvider = OpenCycleLandscapeMapProvider.Instance;
            gMapControl1.SetPositionByKeywords(location);
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
        }

        private void OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (!item.Overlay.Id.Equals(LAYER_POINTS.Id))
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
            LAYER_ROUTE.Routes.Clear();
            LAYER_ROUTE.Routes.Add(routeOnMap);

            foreach (var point in openedRoute.CoursePoints)
            {
                GMarkerGoogle marker = new GMarkerGoogle(
                    new PointLatLng(point.Lattitude, point.Longitude),
                    new Bitmap(new MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAMAAAAMCGV4AAAASFBMVEX///9ISEgGBgbBwcH39/dpaWmYmJjR0dH09PSBgYGJiYnV1dV4eHh+fn7FxcVSUlJgYGDl5eWjo6Pc3Nx5eXk4ODhfX18nJycGtpgYAAAAcklEQVQImUWPWxaAIAhER83MxNKe+99pKFTzAdzDAQaA5WKmHB1Egy9j2MN4+aHhtEaOtwfiOnFFM17NBCwFv8qCvLXCWtPSlkGpd87OiUC1s+lc6e0Lp0PnlXle9wvzfrlvWXJf/TWJv89/Ef/63yH/PeORA/kIj+u1AAAAAElFTkSuQmCC"))));

                LAYER_POINTS.Markers.Add(marker);
                marker.ToolTipText = $"{point.Type}\n{point.Notes}";
                marker.Tag = point.TimeStamp;
            }
        }

        private void ClearCueMarkers()
        {
            LAYER_POINTS.Clear();
        }

        internal void ShowPointToEdit(TrackPoint point)
        {
            ShowSingleMarker(point, GMarkerGoogleType.blue, LAYER_EDIT_POINTS);
        }

        internal void SetEditCoursePointMarker(TrackPoint point)
        {
            ShowSingleMarker(point, GMarkerGoogleType.arrow, LAYER_EDIT_POINTS);
        }

        private void ShowSingleMarker(TrackPoint point, GMarkerGoogleType markerType, GMapOverlay layer)
        {
            ClearEditMarkers();
            layer.Markers.Add(
                new GMarkerGoogle(
                    new PointLatLng(point.Lattitude, point.Longitude),
                    markerType)
                { Tag = point.TimeStamp });

            SetMapCenterPosition(point);
        }

        private void ClearEditMarkers()
        {
            LAYER_EDIT_POINTS.Clear();
        }

        private void SetMapCenterPosition(Position point)
        {
            gMapControl1.Position = new PointLatLng(point.Lattitude, point.Longitude);
        }
    }

    public class PointSelectEventArgs : EventArgs
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
