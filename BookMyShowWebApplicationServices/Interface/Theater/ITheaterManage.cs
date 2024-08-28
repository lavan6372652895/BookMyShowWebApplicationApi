using BookMyShowWebApplicationModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Interface.Theater
{
    public interface ITheaterManage
    {
        Task<List<TheatersDto>> AddNewTheater(TheatersDto Theater);
        Task<List<ScreenDto>> AddNewScren(ScreenDto screen);
        Task<List<TheatersDto>> GetTheaterWithScreens();
        Task<List<Showtime>> AddShow(Showtime show);
    }
}
