using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using OnlineDietManager.Domain.UnitsOfWork;

namespace OnlineDietManager.WebUI.Infrastructure
{
    public class NinjectDependencyResolver
        : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            addBindings();
        }

        private void addBindings()
        {
            kernel.Bind<IUnitOfWork>()
                  .To<ODMUnitOfWork>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}