using IOCServiceCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorByInjection
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        /// 
        public static ServiceProvider provider = null;

        [STAThread]
        static void Main()
        {
            ServiceCollection collection = new ServiceCollection();
            collection.AddTransient<Operator, Plus>();
            //collection.AddTransient<Operator>(x =>
            //{
            //    string type = x.GetService<Form1>().GetSelectedOperator();
            //    switch (type)
            //    {
            //        case "Plus":
            //            return new Plus();
            //        case "Minus":
            //            return new Minus();
            //        case "Multi":
            //            return new Multi();
            //        default:
            //            return new Division();
            //    }
            //});
            collection.AddSingleton<Form1>();
            provider = collection.BuildServiceProvider();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form form = provider.GetService<Form1>();


            Application.Run(form);
        }
    }
}
