using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using QuickKartDataAccessLayer;

namespace QuickKartWebService.Repository
{
    public class QuickKartMapper<Source, Destination>
        where Source : class
        where Destination : class
    {
        public QuickKartMapper()
        {
            //From entity to model
            Mapper.CreateMap<Product, Models.Product>();

            //From model to entity
            Mapper.CreateMap<Models.Product, Product>();
        }

        public Destination Translate(Source obj)
        {
            return Mapper.Map<Destination>(obj);
        }
    }
}