﻿using Microsoft.Extensions.DependencyInjection;
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
            provider._services.dictiontry.TryGetValue(typeof(TPresenter), out List<ServiceDescriptor> descriptors);
            return (TPresenter)Activator.CreateInstance(descriptors.Last().ImplementationType, view);
        }


        public TPresenter Create<TPresenter, TView>(TView view, Func<ServiceProvider, TPresenter> implementationFactory)
        {
            return implementationFactory.Invoke(provider);
        }
    }
}
