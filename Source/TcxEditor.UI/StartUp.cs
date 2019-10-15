using Autofac;
using System.Linq;
using System.Reflection;
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

            builder.RegisterType<CommandRunner>().As<ICommandRunner>();

            builder.RegisterAssemblyTypes(typeof(ITcxEditorCommand).Assembly)
                .Where(x => x.IsAssignableTo<ITcxEditorCommand>())
                .AsImplementedInterfaces();

            builder.RegisterType<FileStreamCreator>().As<IStreamCreator>();
            builder.RegisterType<TcxParserAdapter>().As<ITcxParser>();
            builder.RegisterType<RouteSaver>().As<IRouteSaver>();

            builder.RegisterType<MainForm>()
                    .As<IRouteView>()
                    .As<IErrorView>()
                    .As<IGuiStateSetter>()
                .SingleInstance();

            builder.RegisterType<Presenter>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
