using BookMyShowWebApplicationDataAccess.InterFaces.Admin;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.Admin
{
    public class AdminServices: IAdminServices
    {
        public IAdmin _Services;
        public AdminServices(IAdmin Services)
        {
            _Services = Services;
        }

        public async Task<List<ActorDto>> AddNewActor(ActorDto Act)
        {
            return await _Services.AddNewActor(Act);
        }

        public async Task<List<MoviesDto>> AddNewMovie(MoviesDto movie)
        {
          return await _Services.AddNewMovie(movie);
        }

        public Task<List<GenreDto>> GetListofGenre()
        {
           return _Services.GetListofGenre();
        }
    }
}
