[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

# Video Steraming

## Kullanılan Teknolojiler

[![C-Sharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Asp-net](https://img.shields.io/badge/ASP.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![MSSQL](https://img.shields.io/badge/MSSQL-004880?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server/sql-server-2019?rtc=2)
[![Entity-Framework](https://img.shields.io/badge/Entity%20Framework-004880?style=for-the-badge&logo=nuget&logoColor=white)](https://docs.microsoft.com/en-us/ef/)
[![Autofac](https://img.shields.io/badge/Autofac-004880?style=for-the-badge&logo=nuget&logoColor=white)](https://autofac.org/)
[![Fluent-Validation](https://img.shields.io/badge/Fluent%20Validation-004880?style=for-the-badge&logo=nuget&logoColor=white)](https://fluentvalidation.net/)

## Veritabanı Tabloları

<details>
  <summary>Tablolar</summary>

### Users

|     Name     |    Data Type   | Allow Nulls | Default |
| :----------- | :------------- | :---------- | :------ |
| Id           | int            | False       |         |
| FirstName    | varchar(50)    | False       |         | 
| LastName     | varchar(50)    | False       |         |
| Email        | varchar(50)    | False       |         |
| PasswordSalt | varbinary(500) | False       |         |
| PasswordHash | varbinary(500) | False       |         |
| Status       | bit            | False       |         |


### UserDetails

|      Name      |    Data Type   | Allow Nulls | Default           |
| :------------- | :------------- | :---------- | :---------------- |
| Id             | int            | False       |                   |
| UserId         | int            | False       |                   | 
| Gender         | varchar(7)     | False       |                   |
| IdentityNumber | varchar(20)    | False       |                   |
| DateOfBorn     | datetime       | False       |                   |
| RecoveryEmail  | varchar(50)    | False       |                   |
| DateOfJoin     | datetime       | False       | current_Timestamp |
| PhotoPath      | varchar(50)    | False       |                   |

### Communications 

|      Name   |    Data Type   | Allow Nulls | Default |
| :---------- | :------------- | :---------- | :------ |
| Id          | int            | False       |         |
| UserId      | int            | False       |         | 
| Street      | varchar(50)    | False       |         |
| City        | varchar(50)    | False       |         |
| Continent   | varchar(50)    | False       |         |
| Country     | varchar(50)    | False       |         |
| Address1    | varchar(100)   | False       |         |
| Address2    | varchar(100)   | True        |         |
| PhoneNumber | varchar(15)    | False       |         |
| ZipCode     | varchar(20)    | False       |         |

### Operation Claims

|     Name     |    Data Type   | Allow Nulls | Default          |
| :----------- | :------------- | :---------- | :--------------- |
| Id           | int            | False       |                  |
| Name         | varchar(50)    | False       |                  | 
| Date         | datetime       | False       |current_Timestamp |
| ClaimType    | varchar(50)    | False       |Default           |

### User Operation Claims

|     Name         |    Data Type   | Allow Nulls | Default          |
| :--------------- | :------------- | :---------- | :--------------- |
| Id               | int            | False       |                  |
| UserId           | int            | False       |                  |
| OperationClaimId | int            | False       |                  | 
| Date             | datetime       | False       |current_Timestamp |

### Channels

|      Name        |    Data Type   | Allow Nulls | Default           |
| :--------------- | :------------- | :---------- | :---------------- |
| Id               | int            | False       |                   |
| UserId           | int            | False       |                   | 
| ChannelName      | varchar(25)    | False       |                   |
| InstallationDate | datetime       | False       |                   |
| UpdateDate       | datetime       | False       |                   |
| ChannelPhotoPath | text           | False       |                   |
| Description      | text           | False       |                   |

### Videos

|      Name        |    Data Type   | Allow Nulls | Default           |
| :--------------- | :------------- | :---------- | :---------------- |
| Id               | int            | False       |                   |
| UserId           | int            | False       |                   | 
| ChannelId        | int            | False       |                   |
| Description      | text           | False       |                   |
| Views            | int            | False       |                   |
| Duration         | int            | False       |                   |
| VideoPath        | text           | False       |                   |
| ThumbnailPath    | text           | False       |                   |
| Date             | datetime           | False       |                   |
| UpdateDate       | datetime       | False       |                   |

### Subscribers 

|     Name         |    Data Type   | Allow Nulls | Default          |
| :--------------- | :------------- | :---------- | :--------------- |
| Id               | int            | False       |                  |
| UserId           | int            | False       |                  |
| ChannelId        | int            | False       |                  | 
| Date             | datetime       | False       |current_Timestamp |

### Dislikes 

|     Name         |    Data Type   | Allow Nulls | Default          |
| :--------------- | :------------- | :---------- | :--------------- |
| Id               | int            | False       |                  |
| UserId           | int            | False       |                  |
| VideoId          | int            | False       |                  | 

### Likes 

|     Name         |    Data Type   | Allow Nulls | Default          |
| :--------------- | :------------- | :---------- | :--------------- |
| Id               | int            | False       |                  |
| UserId           | int            | False       |                  |
| VideoId          | int            | False       |                  | 

### Comments 

|     Name         |    Data Type   | Allow Nulls | Default          |
| :--------------- | :------------- | :---------- | :--------------- |
| Id               | int            | False       |                  |
| PostedByUserId   | int            | False       |                  |
| VideoId          | int            | False       |                  | 
| ResponseByUserId | int            | False       |                  |
| LikeId           | int            | False       |                  |
| DislikeId        | int            | False       |                  |
| CommentBody      | text           | False       |                   |
| Date             | datetime       | False       |current_Timestamp |

### Jason Web Tokens 

|     Name         |    Data Type   | Allow Nulls | Default          |
| :--------------- | :------------- | :---------- | :--------------- |
| Id               | int            | False       |                  |
| Token            | text           | False       |                  |
| Expiration       | datetime       | False       |                  | 

</details>

## Katmanlar
### Business : İş Katmanı
  * Abstruct
    * Mikroservis yaklaşımıyla yazılmış serviceler.
  * BusinessAspects
    * Authorization işlemi için yazılan aspect.Aspect:.net de çalışacak methodun ne zaman çalışmasını istediğimizde kullanılır.
  * Concrete
    * Manager classlarının bulunduğu klasör.Bu classlar sadece iş kodlarını içerir.
  * Constant
    * Kodlanan manager clasları için mesajların tutulduğu klasör.
  * DependencyResolvers
    * Autofac
      * Dependency Injection yaparken kullanılır."builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance()" dediğimizde IAuthService kullandığımda aslında AuthManageri çağır ve onun tüm projeden erişilebilecek bir instance'ını oluştur demek isteriz.
  * ValidationRules
    * FluentValidation
      * RegisterValidator
        * Beliri kurallar koymak istediğimizde kullanılır.
### Core : Tüm .net projelerinde geçerli kodların bulunduğu katman
  * Aspects
    * Autofac
      * Business katmanındaki aspect ile aynı şekilde sadece burda farklı işlemler var.
  * CrossCuttingConcerns
    * Tüm uygulamada sıkça başvurulacak Caching , Validation gibi olayların
      koddan ayrılması.Daha sonra AOP yardımı ile koda uygulanması.
  * DataAcces
    * Burada tüm programlarda bulunacak ekleme silme güncelleme gibi işlemler yapılmıştır daha sonra koddada göründüğü gibi DataAccess katmanında buradan faydalanılıp daha temiz ve solid'e uygun bir  kod yazılması sağlanmıştır.
  * Entities
    * Burada neredeyse tüm projelerde bulunacak user gibi entity sınıfları mevcuttur.
  * Extensions
    * Extension varolan kodda değişiklip yapıp onu kendimize göre yeniden formatlamaktır.Burdada sistemin kendisinde bulunan System.Security.Claims
    daki Claim gerekli alaların daha kolay eklenmesini sağlamak için konfigrasyonlar yapılmıştır.
  * Utilities 
    * Burada JWT,dosya işlemlerini yapan classlar,Interceptor yani method çalışırken ne zaman ne yapılması gerekiyor gibi Projeye gerekli özellikleri kazandıran classlar mevcuttur.
### DataAccess : Veritabanı ile ilgili kodların yazıldığı katman.
  * Concrute 
    * Burada Entity Fremawork ve hafızada veri yönetimini yapan classlar mevcuttur.
  * Abstruct 
   * Burada Classların ne yapıcağını söyleyen yani fonksiyonun örneğini bulunduran interfaceler mevcuttur.
### Entities : Veritabanındaki nesnelerin kodlara döküldüğü katman.
  * Concrute
    * Daha öncede bahsettiğim gibi veritabanındaki nesnelerin kodda ki örnekleri burada bulunur.
  * Dtos
    * Dto (veri aktarım nesneleri), verileri katmanlar arasında taşımak için kullanılan bir veri kapsayıcısıdır.Bu yüzden Concrute deki classları kullanmak yerine veritabanında olmayan ama katmanlar arasında taşınması gereken bilgileri taşıyan dtolar kullanıldı.
### UIPreparation : Arayüz kodlarını içeren katman.
  * Bu katmanda sadece arayüz kodları bulunur.Örnek olarak CSS,Javascript gibi.
  * Ayrıca bu katman WebAPI katmanı çalıştırıldıktan sonra Debug>Start New Instance diyerek çalıştırılacak.Çaşıştıktan sonra gerekli dosyalar indirilecek ve vscode uygulaması otomatik olarak açılacak ve arayüz kodlaması orada devam edecektir.
### WebAPI : Arayüz ile projeyi birbirine bağlayarak verinin yönetilmesini sağlayan katman
  * wwwroot
    * Bu klasörün altında bulunan klasörlere belirlenen dosyalar yüklenecek.Örnek olarak video ,profil resmi gibi.
  * Controllers
    * Bu klasörün altında arayüz ile projenin haberleşmesini sağlayan kodların yazılı olduğu classlar bulunmaktadır.

# Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

# License

Distributed under the MIT License. See <a href="https://github.com/oguzhan-c/VideoStreaming/blob/master/LICENSE.txt">LICENSE<a/> for more information.

## Contact
### Project Link: <a href="https://github.com/oguzhan-c/VideoStreaming">VideoStream<a/>
### Email:<a href="oguzhan.can.iletisim@gmail.com">oguzhan.can.iletisim@gmail.com<a/>

## Acknowledgements
<a href="https://github.com/engindemirog">engindemirog</a>


[contributors-shield]: https://img.shields.io/github/contributors/oguzhan-c/VideoStreaming.svg?style=for-the-badge
[contributors-url]: https://github.com/oguzhan-c/VideoStreaming/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/oguzhan-c/VideoStreaming.svg?style=for-the-badge
[forks-url]: https://github.com/oguzhan-c/VideoStreaming/network/members
[stars-shield]: https://img.shields.io/github/stars/oguzhan-c/VideoStreaming.svg?style=for-the-badge
[stars-url]: https://github.com/oguzhan-c/VideoStreaming/stargazers
[issues-shield]: https://img.shields.io/github/issues/oguzhan-c/VideoStreaming.svg?style=for-the-badge
[issues-url]: https://github.com/oguzhan-c/VideoStreaming/issues
[license-shield]: https://img.shields.io/github/license/oguzhan-c/VideoStreaming.svg?style=for-the-badge
[license-url]: https://github.com/oguzhan-c/VideoStreaming/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white
[linkedin-url]: https://www.linkedin.com/in/oğuzhan-can-4141a6208/
