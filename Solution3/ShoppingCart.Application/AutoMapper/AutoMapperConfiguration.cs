﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {

        //AutoMapper >> Configurations >> Profiles >> Maps

        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(
                cfg => 
                
                {
                    cfg.AddProfile(new DomainToViewModelProfile());
                    cfg.AddProfile(new ViewModelToDomainProfile());
                }
                
                );

        }

    }
}
