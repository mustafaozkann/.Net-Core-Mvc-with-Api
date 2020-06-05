using Autofac;
using NLayerProject.Core.Repositories;
using NLayerProject.Core.Services;
using NLayerProject.Core.UnitOfWorks;
using NLayerProject.Data.Repositories;
using NLayerProject.Data.UnitOfWork;
using NLayerProject.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerProject.Service.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>));
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
