using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstruct;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.IoC;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.Autofac
{
    //Projeye Özel Injections Lar Burda Yer Alır 
    //Genel Injections lar ise coreda bulunur
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        { 
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
