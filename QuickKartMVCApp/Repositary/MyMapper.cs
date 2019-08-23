using QuickKartDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace QuickKartMVCApp.Repositary
{
    public class MyMapper<Source,Destination>where Source :class where Destination:class
    {
        public MyMapper()
        {
            Mapper.CreateMap<Source, Destination>();
        }

        public Destination Translate(Source obj)
        {
            return Mapper.Map<Source, Destination>(obj);
        }
    }
}