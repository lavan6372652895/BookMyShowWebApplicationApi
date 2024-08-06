﻿using BookMyShowWebApplicationDataAccess.InterFaces.Theaters;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.Theater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.Theater
{
    public class TheaterManage : ITheaterManage
    {
        public Itheatersrepo _theatersrepo;
        public TheaterManage(Itheatersrepo theatersrepo) { 

            _theatersrepo = theatersrepo;
        }

        public async Task<List<ScreenDto>> AddNewScren(ScreenDto screen)
        {
            var data =await _theatersrepo.AddNewScren(screen);
            return data;
        }

        public async Task<List<TheatersDto>> AddNewTheater(TheatersDto Theater)
        {
           var data = await _theatersrepo.AddNewTheater(Theater);
            return data;
        }

        public async Task<List<TheatersDto>> GetTheaterWithScreens()
        {
           var data = await _theatersrepo.GetTheaterWithScreens();
            return data;
        }
    }
}
