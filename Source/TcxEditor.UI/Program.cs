using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcxEditor.Core;
using TcxEditor.Core.Interfaces;
using TcxEditor.Infrastructure;

namespace TcxEditor.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OpenRouteCommand>().As<IOpenRouteCommand>();
            builder.RegisterType<FileStreamCreator>().As<IStreamCreator>();
            builder.RegisterType<TcxParserAdapter>().As<ITcxParser>();

            builder.RegisterType<MainForm>().AsSelf();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(scope.Resolve<MainForm>());
            }
        }
    }
}
