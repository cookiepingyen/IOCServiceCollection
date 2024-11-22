using IOCServiceCollection.Test;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IOCServiceCollection
{
    public class ServiceProvider : IServiceProvider
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
                        return (T)CreateInstance(descriptor.ImplementationType);
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
                        // HW: 把 ImplementationFactory.Invoke(this) 用一個物件容器存起來，存在ServiceProvider
                        // 後續的取用判斷(44行)，改成用ServiceProvider 的物件容器 判斷
                        descriptor.ImplementationInstance = descriptor.ImplementationFactory.Invoke(this);
                        return (T)descriptor.ImplementationInstance;
                    }
                    else
                    {
                        descriptor.ImplementationInstance = (T)CreateInstance(descriptor.ImplementationType);
                        return (T)descriptor.ImplementationInstance;
                    }
                default:
                    return default(T);

            }

        }


        public object CreateInstance(Type type)
        {
            // 建構元參數最多的排在最前面
            var ctors = type.GetConstructors().OrderByDescending(x => x.GetParameters());
            foreach (var ctor in ctors)
            {
                // 沒有建構元的話就Create 
                if (ctor.GetParameters().Length == 0)
                {
                    return Activator.CreateInstance(type);
                }

                // 取得所有的參數並放入List
                var parms = ctor.GetParameters();

                List<object> parmsInstanceList = new List<object>();

                foreach (var param in parms)
                {
                    MethodInfo getServiceMethod = typeof(ServiceProvider).GetMethod("GetService");
                    getServiceMethod = getServiceMethod.MakeGenericMethod(param.ParameterType);
                    var result = getServiceMethod.Invoke(this, null);
                    parmsInstanceList.Add(result);
                }
                return Activator.CreateInstance(type, parmsInstanceList.ToArray());

            }

            return null;
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
