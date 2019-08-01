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
using Duurt.TcxParser.Xsd.Generated;
using GMap.NET.WindowsForms.Markers;

namespace TcxEditor.UI
{
    public partial class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.SetZoomToFitRect(
                new RectLatLng(54, 6, 1, 1));
            gMapControl1.Overlays.Add(new GMapOverlay("route"));
            gMapControl1.Overlays.Add(new GMapOverlay("points"));

            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 10;

            Stream tcxStream = File.OpenRead(@"C:\Users\User\Downloads\Hunedbedroute.tcx");
            var serializer = new XmlSerializer(typeof(TrainingCenterDatabase_t));
            var parsedRoute = serializer.Deserialize(tcxStream) as TrainingCenterDatabase_t;

            var route = new GMapRoute(
                parsedRoute.Courses[0].Track.Select(
                    t => new PointLatLng(
                        t.Position.LatitudeDegrees,
                        t.Position.LongitudeDegrees)),
                "someName");
            
            route.Stroke = new Pen(Color.Red, 3);
            gMapControl1.Overlays.First(o => o.Id.Equals("route")).Routes.Add(route);


            GMapOverlay markerOverlay = gMapControl1.Overlays.First(o => o.Id.Equals("points"));
            foreach(var point in parsedRoute.Courses[0].CoursePoint.Select(c => c.Position))
            {
                GMarkerGoogle marker = new GMarkerGoogle(
                    new PointLatLng(point.LatitudeDegrees, point.LongitudeDegrees),
                    GMarkerGoogleType.lightblue_pushpin);
                markerOverlay.Markers.Add(marker);

                marker.ToolTip = new GMapToolTip(marker);
            }
        }
    }
}
