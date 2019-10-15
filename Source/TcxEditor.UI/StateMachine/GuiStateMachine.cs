//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TcxEditor.UI.StateMachine
//{
//    internal class GuiStateMachine
//    {
//        private readonly IGuiControls _guiControls;

//        private IGuiState _currentState;

//        public GuiStateMachine(IGuiControls guiControls)
//        {
//            _guiControls = guiControls;
//            SetState(new NoRouteState());
//        }

//        internal void SetState(IGuiState state)
//        {
//            _currentState = state;
//            _currentState.EnterState(this);
//        }

//        internal void RouteOpened() 
//            => _currentState.RouteOpened(this);

//        internal void CoursePointSelected() 
//            => _currentState.CoursePointSelected(this);

//        internal void TrackPointSelected()
//            => _currentState.TrackPointSelected(this);

//        internal void EnableAddCoursePoint(bool val)
//            => _guiControls.CanAddCoursePoint = val;

//        internal void EnableDeleteCoursePoint(bool val)
//            => _guiControls.CanDeleteCoursePoint = val;
//    }
//}
