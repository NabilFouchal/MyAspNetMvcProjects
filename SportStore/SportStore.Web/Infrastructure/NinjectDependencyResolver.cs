using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using Moq;
using Ninject;
using Ninject.Web.Common;
using SportStore.Domain.Database;
using SportStore.Domain.Entities;
using SportStore.Domain.IRepositories;
using SportStore.Domain.Repositories;

namespace SportStore.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel Kernel { get; set; }

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            Kernel = kernelParam;
            AddBinding();
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        private void AddBinding()
        {
            Kernel.Bind<DbContext>().To<SportStoreDbContext>();
            Kernel.Bind<IGenericRepository<Product>>().To<GenericRepositoryEF<Product>>().InRequestScope();

        }
    }
}