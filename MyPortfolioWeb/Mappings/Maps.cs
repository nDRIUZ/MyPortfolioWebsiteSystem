using AutoMapper;
using MyPortfolioWeb.Data;
using MyPortfolioWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Setting, SettingVM>().ReverseMap();
            CreateMap<WelcomePage, WelcomePageVM>().ReverseMap();
            CreateMap<WelcomePage, WelcomePageShowVM>().ReverseMap();
            CreateMap<Skill, SkillVM>().ReverseMap();
            CreateMap<Skill, SkillShowVM>().ReverseMap();
            CreateMap<Experience, ExperienceVM>().ReverseMap();
            CreateMap<Testimonial, TestimonialVM>().ReverseMap();
            CreateMap<Testimonial, TestimonialShowVM>().ReverseMap();
            CreateMap<Award, AwardVM>().ReverseMap();
            CreateMap<Award, AwardShowVM>().ReverseMap();
            CreateMap<Portfolio, PortfolioVM>().ReverseMap();
            CreateMap<Portfolio, PortfolioShowVM>().ReverseMap();
        }
    }
}
