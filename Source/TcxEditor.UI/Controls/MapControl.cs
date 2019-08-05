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

namespace TcxEditor.UI
{
    // todo: inject opener command
    public partial class MapControl : UserControl
    {
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

            var opener = new OpenRouteCommand(new FileStreamCreator(), new TcxParserAdapter());
            var openedRoute = opener.Execute(
                new OpenRouteInput { Name = @"C:\Users\User\Downloads\Hunedbedroute.tcx" });

            var route = new GMapRoute(
                openedRoute.Route.TrackPoints.Select(
                    t => new PointLatLng(
                        t.Lattitude,
                        t.Longitude)),
                "someName");

            route.Stroke = new Pen(Color.Red, 3);
            gMapControl1.Overlays.First(o => o.Id.Equals("route")).Routes.Add(route);

            GMapOverlay markerOverlay = gMapControl1.Overlays.First(o => o.Id.Equals("points"));
            foreach (var point in openedRoute.Route.CoursePoints)
            {
                GMarkerGoogle marker = new GMarkerGoogle(
                    new PointLatLng(point.Lattitude, point.Longitude),
                    GMarkerGoogleType.lightblue_pushpin);
                markerOverlay.Markers.Add(marker);
                marker.ToolTipText = $"{point.Type}\n{point.Notes}";
            }
        }
    }
}
