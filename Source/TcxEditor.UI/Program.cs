using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
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
