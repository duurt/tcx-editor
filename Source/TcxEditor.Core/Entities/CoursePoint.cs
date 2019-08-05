namespace TcxEditor.Core.Entities
{
    public class CoursePoint : TrackPoint
    {
        public PointType Type { get; set; } = PointType.Undefined;
        public string Notes { get; set; } = "";

        public CoursePoint(double lat, double lon)
            : base(lat, lon) { }

        public enum PointType
        {
            Undefined = 0,
            Generic,
            Summit,
            Valley,
            Water,
            Food,
            Danger,
            Left,
            Right,
            Straight,
            FirstAid,
            ClimbCat4,
            ClimbCat3,
            ClimbCat2,
            ClimbCat1,
            ClimbCatHors,
            Sprint,
        }
    }
}
