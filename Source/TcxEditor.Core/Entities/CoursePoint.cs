namespace TcxEditor.Core.Entities
{
    public class CoursePoint : TrackPoint
    {
        public string Name { get; set; }
        public PointType Type { get; set; } = PointType.Undefined;
        public string Notes { get; set; } = "";

        public CoursePoint(double lat, double lon)
            : base(lat, lon) { }

        public enum PointType
        {
            Undefined = 0,
            Left,
            Right,
            Straight,
            Food,
            Generic,
            Sprint,
            ClimbCat4,
            ClimbCat3,
            ClimbCat2,
            ClimbCat1,
            ClimbCatHors,
            Summit,
            Valley,
            Water,
            Danger,
            FirstAid,
        }
    }
}
