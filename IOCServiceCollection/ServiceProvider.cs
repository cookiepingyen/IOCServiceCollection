using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCServiceCollection
{
    public class ServiceProvider
    {
        private ServiceCollection _services;
        public ServiceProvider(ServiceCollection services)
        {
            _services = services;
        }

        public T GetService<T>()
        {
            // 如果TryGetValue沒有找到東西的話，就回傳 T 型別的預設值(大部分為null)
            if (!_services.dictiontry.TryGetValue(typeof(T), out ServiceDescriptor descriptor))
                return default(T);

            switch (descriptor.Lifetime)
            {
                // 1. Transient: 物件的話就 new 出來，Func的話就執行
                case ServiceLifetime.Transient:

                    if (descriptor.ImplementationFactory != null)
                    {
                        return (T)descriptor.ImplementationFactory.Invoke(this);
                    }
                    else
                    {
                        return (T)Activator.CreateInstance(descriptor.ImplementationType);
                    }

                // 3. Singleton: 
                // 物件 先找有沒有 instance 存在，沒有的話 new 出來並存到 instance
                // Func 先找有沒有 instance 存在，沒有的話 執行並存到 instance              
                case ServiceLifetime.Singleton:
                    if (descriptor.ImplementationInstance != null)
                    {
                        return (T)descriptor.ImplementationInstance;
                    }
                    else if (descriptor.ImplementationFactory != null)
                    {
                        descriptor.ImplementationInstance = descriptor.ImplementationFactory.Invoke(this);
                        return (T)descriptor.ImplementationInstance;
                    }
                    else
                    {
                        descriptor.ImplementationInstance = Activator.CreateInstance(descriptor.ImplementationType);
                        return (T)descriptor.ImplementationInstance;
                    }
                default:
                    return default(T);

            }


        }


    }
}
