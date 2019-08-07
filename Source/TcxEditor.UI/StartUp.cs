using Autofac;
using TcxEditor.Core;
using TcxEditor.Core.Interfaces;
using TcxEditor.Infrastructure;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI
{
    public class StartUp
    {
        public IContainer CreateIocContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OpenRouteCommand>().As<IOpenRouteCommand>();
            builder.RegisterType<FileStreamCreator>().As<IStreamCreator>();
            builder.RegisterType<TcxParserAdapter>().As<ITcxParser>();

            builder.RegisterType<MainForm>().As<IRouteView>().SingleInstance();
            builder.RegisterType<Presenter>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
