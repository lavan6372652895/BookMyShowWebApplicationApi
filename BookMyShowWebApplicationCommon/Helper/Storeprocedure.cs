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
       public class common()
        {
            public const string ListofRoles = "Sp_listofRoles";
            public const string ListofActors = "Sp_ListOfActors";
        }
        public class users()
        {
            public const string ListOfMovies = "Sp_Listofmovies";
        }


    }
}
