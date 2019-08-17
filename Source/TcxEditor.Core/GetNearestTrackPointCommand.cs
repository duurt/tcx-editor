using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;

namespace TcxEditor.Core
{
    public class GetNearestTrackPointCommand
    {
        public GetNearestTrackPointResponse Execute(GetNearestTrackPointInput input)
        {
            if(input == null 
                || input.Route == null 
                || input.ReferencePoint == null)
                    throw new ArgumentNullException(nameof(input));

            if(!input.Route.TrackPoints.Any())
                throw new TcxCoreException("Fout!!");


            TrackPoint nearestPoint = GetNearest(input.Route.TrackPoints, input.ReferencePoint);

            return
                new GetNearestTrackPointResponse
                {
                    Nearest = nearestPoint
                };
        }

        private static TrackPoint GetNearest(List<TrackPoint> trackPoints, Position referencePoint)
        {
            var nearestPoint = trackPoints.First();
            double nearestDistance = double.MaxValue;

            foreach (var thisPoint in trackPoints)
            {
                double dLat = thisPoint.Lattitude - referencePoint.Lattitude;
                double dLon = thisPoint.Longitude - referencePoint.Longitude;
                double thisDistance = Math.Sqrt(dLat * dLat + dLon * dLon);

                if (thisDistance < nearestDistance)
                {
                    nearestDistance = thisDistance;
                    nearestPoint = thisPoint;
                }
            }

            return nearestPoint;
        }
    }
}
