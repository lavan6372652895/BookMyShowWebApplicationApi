using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationCommon.Helper
{
    public class Storeprocedure
    {
        public const string LoginSp = "Sp_UserLogin";

       
        public class Admin()
        {
            public const string NewMovieReg = "AddNewMovies";
            public const string ListofGenre = "Sp_listofGenre";
            public const string AddNewActor = "sp_AddNewActor";
        }
       public class Common()
        {
            public const string ListofRoles = "Sp_listofRoles";
            public const string ListofActors = "Sp_ListOfActors";
            public const string Listofcities = "Sp_CityList";
        }
        public class Users()
        {
            public const string ListOfMovies = "Sp_Listofmovies";
            public const string Theaterlist = "sp_Movie_Theaterlist";
            public const string ListofSeats = "Sp_ListofmovieSeates";
            public const string TicketBooking = "Sp_addBookings";
        }


    }
}
