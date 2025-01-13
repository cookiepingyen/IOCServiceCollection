using IOCServiceCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
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
        public static IOCServiceCollection.ServiceProvider provider = null;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var config = new ConfigurationBuilder().Build();



            IOCServiceCollection.ServiceCollection collection = new IOCServiceCollection.ServiceCollection();
            collection.AddTransient<Operator>(x =>
            {
                Form1 f = (Form1)x.GetService<Form>();
                string type = f.GetSelectedOperator();
                switch (type)
                {
                    case "Plus":
                        return new Plus();
                    case "Minus":
                        return new Minus();
                    case "Multi":
                        return new Multi();
                    default:
                        return new Division();
                }
            });
            collection.AddSingleton<Form, Form1>();
            collection.AddSingleton<Form, Form2>();
            collection.AddSingleton<Form, Form3>();
            collection.AddLogging(loggingBuilder =>
                       {
                           loggingBuilder.AddNLog(config);
                       });

            provider = collection.BuildServiceProvider();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form form = provider.GetService<Form>();

            Application.Run(form);
        }
    }
}
