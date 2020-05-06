using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Infrastructure
{
    /// <summary>
    /// 定义可简化服务位置和依赖关系解析的方法
    /// </summary>
    public class NinjectDependcyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependcyResolver(IKernel _kernel)
        {
            kernel = _kernel;
            AddBinding();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBinding()
        {
            kernel.Bind<IValueCalculator>().To<LinqValueCalculatorNew>().InRequestScope();
            kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument("_discounterSize", 50M);
            kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculatorNew>();
        }
    }
}