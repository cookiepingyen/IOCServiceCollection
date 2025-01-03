using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCServiceCollection
{
    public class PresenterFactory
    {
        public ServiceProvider provider;

        public PresenterFactory(ServiceProvider serviceProvider)
        {
            this.provider = serviceProvider;
        }

        public TPresenter Create<TPresenter, TView>(TView view)
        {
            provider._services.dictiontry.TryGetValue(typeof(TPresenter), out ServiceDescriptor descriptor);
            return (TPresenter)Activator.CreateInstance(descriptor.ImplementationType, view);
        }
    }
}
