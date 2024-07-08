using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.InterFaces.Admin
{
    public interface IAdmin
    {
        Task<List<MoviesDto>> AddNewMovie(MoviesDto movie);
        Task<List<GenreDto>> GetListofGenre();
        Task<List<ActorDto>> AddNewActor(ActorDto Act);
    }
}
