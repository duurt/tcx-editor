using Autofac;
using System;
using System.Windows.Forms;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = new StartUp().CreateIocContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var form = scope.Resolve<IRouteView>() as Form;
                scope.Resolve<Presenter>();
                Application.Run(form);
            }
        }
    }
}
