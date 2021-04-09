
using Business.Abstruct;

namespace Test
{
    class Program
    {
        private static  IUserService _userService;
        private static  IUserDetailService _userDetailService;
        private static  ICommunicationService _communicationService;

        public Program(IUserService userService, IUserDetailService userDetailService, ICommunicationService communicationService)
        {
            _userService = userService;
            _userDetailService = userDetailService;
            _communicationService = communicationService;
        }

        static void Main(string[] args)
        {
            int id = 1;

            UserDelete(id);
            UserDetailDelete(id);
            CommunicationDelete(id);
        }


        public static void CommunicationDelete(int id)
        {
            _communicationService.Delete(id);
        }

        public static void UserDelete(int id)
        {
            _userService.Delete(id);
        }

        public static void UserDetailDelete(int id)
        {
            _userDetailService.Delete(id);
        }





    }
}
