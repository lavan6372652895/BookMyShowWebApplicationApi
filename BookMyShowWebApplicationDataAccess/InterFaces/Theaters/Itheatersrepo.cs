﻿using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.InterFaces.Theaters
{
    public interface Itheatersrepo
    {
        Task<List<TheatersDto>> AddNewTheater(TheatersDto Theater);
        Task<List<ScreenDto>> AddNewScren(ScreenDto screen);
        Task<List<TheatersDto>> GetTheaterWithScreens();
        Task<List<Showtime>> AddShow(Showtime show);
        Task<List<ListofMovieTheaterscs>> moviesListOfTheaterList(int movieid, int cityid);
    }
}
