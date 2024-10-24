using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationCommon.Helper
{
    public static class Storeprocedure
    {
        public static class Admin
        {
            public const string NewMovieReg = "AddNewMovies";
            public const string ListofGenre = "Sp_listofGenre";
            public const string AddNewActor = "sp_AddNewActor";
        }
        public static class Common
        {
            public const string ListofRoles = "Sp_listofRoles";
            public const string ListofActors = "Sp_ListOfActors";
            public const string Listofcities = "Sp_CityList";
            public const string LoginSp = "Sp_UserLogin";
            public const string RegistrationSp = "Sp_Registration";
            public const string AddToken = "sp_userActivity";
            public const string Forgotpassword = "sp_forgotpassword";
            public const string Singleuserdata = "Sp_getsingleuserdata";
            public const string Sp_paginationdata = "paginationdata";
        }
        public static class Users
        {
            public const string ListOfMovies = "Sp_Listofmovies";
            public const string Theaterlist = "sp_Movie_Theaterlist";
            public const string ListofSeats = "Sp_ListofmovieSeates";
            public const string TicketBooking = "Sp_addBookings";
            public const string GetTicket = "Sp_GetTicket";
        }
        public static class Theatersowner
        {
            public const string AddtheTheaters = "Sp_AddNewTheaters";
            public const string AddNewScreen = "Sp_AddNewScreen";
            public const string GetTheaterswithScreen = "GetTheaterWithScreens";
            public const string AddShowTimeing = "Sp_AddShowtimeing";
        }

        public static class Email
        {
            public const string AddOtp = "Sp_AddOtp";
        }
        public static class Movies
        {
            public const string getAllReviews = "Sp_GetAllReviews";
            public const string GetReviewByMovieid = "Sp_GetReviewByMovieid";
            public const string AddorUpdateRewview = "Sp_AddorUpdateRewview";
        }

        public static class Eventorganizations
        {
            public const string addorUpdateEventOrganizations = "Sp_addorupdateEventorganizations";
            public const string addOrUpDateEvent = "Sp_addorupdateEvent";
        }
    }
}
