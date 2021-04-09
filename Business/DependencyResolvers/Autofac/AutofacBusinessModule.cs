using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstruct;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Helpers.FileHelpers;
using Core.Utilities.Helpers.FileHelpers.FileOnDiskManager;
using Core.Utilities.IoC;
using Core.Utilities.Security.JWT;
using DataAccess.Abstruct;
using DataAccess.Concrete.EntityFremavork;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.Autofac
{
    //Projeye Özel Injections Lar Burda Yer Alır 
    //Genel Injections lar ise coreda bulunur
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Business katmanı için instance 
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<ChannelManager>().As<IChannelService>().SingleInstance();
            builder.RegisterType<ChannelPhotoManager>().As<IChannelPhotoService>().SingleInstance();
            builder.RegisterType<CommentManager>().As<ICommentService>().SingleInstance();
            builder.RegisterType<CommunicationManager>().As<ICommunicationService>().SingleInstance();
            builder.RegisterType<CoverImageManager>().As<ICoverImageService>().SingleInstance();
            builder.RegisterType<DislikeManager>().As<IDislikeService>().SingleInstance();
            builder.RegisterType<LikeManager>().As<ILikeService>().SingleInstance();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();
            builder.RegisterType<ProfilePictureManager>().As<IProfilePictureService>().SingleInstance();
            builder.RegisterType<SubscriberManager>().As<ISubscriberService>().SingleInstance();
            builder.RegisterType<TrendManager>().As<ITrendService>().SingleInstance();
            builder.RegisterType<UserDetailManager>().As<IUserDetailService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
            builder.RegisterType<VideoFileManager>().As<IVideoFileService>().SingleInstance();
            builder.RegisterType<VideoManager>().As<IVideoService>().SingleInstance();
            //EntityFremawork için instance 
            builder.RegisterType<EfChannelDal>().As<IChannelDal>().SingleInstance();
            builder.RegisterType<EfChannelPhotoDal>().As<IChannelPhotoDal>().SingleInstance();
            builder.RegisterType<EfCommentDal>().As<ICommentDal>().SingleInstance();
            builder.RegisterType<EfCommunicationDal>().As<ICommunicationDal>().SingleInstance();
            builder.RegisterType<EfCoverImageDal>().As<ICoverImageDal>().SingleInstance();
            builder.RegisterType<EfDislikeDal>().As<IDislikeDal>().SingleInstance();
            builder.RegisterType<EfLikeDal>().As<ILikeDal>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();
            builder.RegisterType<EfProfilePictureDal>().As<IProfilePictureDal>().SingleInstance();
            builder.RegisterType<EfSubscriberDal>().As<ISubscriberDal>().SingleInstance();
            builder.RegisterType<EfTrendDal>().As<ITrendDal>().SingleInstance();
            builder.RegisterType<EfUserDetailDal>().As<IUserDetailDal>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();
            builder.RegisterType<EfVideoFileDal>().As<IVideoFileDal>().SingleInstance();
            builder.RegisterType<EfVideoDal>().As<IVideoDal>().SingleInstance();
            //JWT için instance
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();
            //File için
            builder.RegisterType<FileOnDiskManager>().As<IFileSystem>().SingleInstance();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
