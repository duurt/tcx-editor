//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TcxEditor.UI.StateMachine
//{
//    internal abstract class IGuiState
//    {
//        protected bool _canAddCoursePoint = false;
//        protected bool _canDeleteCoursePoint = false;

//        public void EnterState(GuiStateMachine m)
//        {
//            m.EnableAddCoursePoint(_canAddCoursePoint);
//            m.EnableDeleteCoursePoint(_canDeleteCoursePoint);
//        }

//        public void CoursePointSelected(GuiStateMachine m)
//           => m.SetState(new CoursePointSelectedState());

//        public void RouteOpened(GuiStateMachine m)
//            => m.SetState(new RouteLoadedState());

//        public void TrackPointSelected(GuiStateMachine m)
//            => m.SetState(new TrackPointSelectedState());
//    }

//    internal class NoRouteState : IGuiState
//    {
//        public NoRouteState()
//        {
//            _canAddCoursePoint = false;
//            _canDeleteCoursePoint = false;
//        }       
//    }

//    internal class RouteLoadedState : IGuiState
//    {
//        public RouteLoadedState()
//        {
//            _canAddCoursePoint = false;
//            _canDeleteCoursePoint = false;
//        }
//    }

//    internal class CoursePointSelectedState : IGuiState
//    {
//        public CoursePointSelectedState()
//        {
//            _canAddCoursePoint = false;
//            _canDeleteCoursePoint = true;
//        }
//    }

//    internal class TrackPointSelectedState : IGuiState
//    {
//        public TrackPointSelectedState()
//        {
//            _canAddCoursePoint = true;
//            _canDeleteCoursePoint = false;
//        }
//    }
//}
