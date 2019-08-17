using Autofac;
using TcxEditor.Core;
using TcxEditor.Core.Interfaces;
using TcxEditor.Infrastructure;
using TcxEditor.UI.Interfaces;
using TcxParser.Infrastructure;

namespace TcxEditor.UI
{
    public class StartUp
    {
        public IContainer CreateIocContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OpenRouteCommand>().As<IOpenRouteCommand>();
            builder.RegisterType<AddStartFinishCommand>().As<IAddStartFinishCommand>();
            builder.RegisterType<SaveRouteCommand>().As<ISaveRouteCommand>();
            builder.RegisterType<GetNearestTrackPointCommand>().As<IGetNearestTrackPointCommand>();
            builder.RegisterType<AddCoursePointCommand>().As<IAddCoursePointCommand>();

            builder.RegisterType<FileStreamCreator>().As<IStreamCreator>();
            builder.RegisterType<TcxParserAdapter>().As<ITcxParser>();
            builder.RegisterType<RouteSaver>().As<IRouteSaver>();

            builder.RegisterType<MainForm>().As<IRouteView>().SingleInstance();
            builder.RegisterType<Presenter>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
