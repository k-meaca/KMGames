using AutoMapper;
using KMGames.Entities.DTOs.Category;
using KMGames.Entities.DTOs.City;
using KMGames.Entities.DTOs.Developer;
using KMGames.Entities.DTOs.Game;
using KMGames.Entities.DTOs.PlayerType;
using KMGames.Entities.DTOs.User;
using KMGames.Entities.Entities;
using KMGames.Web.Models.Cart;
using KMGames.Web.ViewModel.Cart;
using KMGames.Web.ViewModel.Categories;
using KMGames.Web.ViewModel.Cities;
using KMGames.Web.ViewModel.Country;
using KMGames.Web.ViewModel.Developers;
using KMGames.Web.ViewModel.Games;
using KMGames.Web.ViewModel.PlayerType;
using KMGames.Web.ViewModel.Users;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace KMGames.Web.Mapping
{
    public class MappingProfile : Profile
    {
        //----------CONSTRUCTOR----------//

        public MappingProfile()
        {
            LoadCategoriesMap();
            LoadCountriesMap();
            LoadCitiesMap();
            LoadDevelopersMap();
            LoadGamesMap();
            LoadPlayerTypeMap();
            LoadUsersMap();
            LoadItemsCartMap();
        }

        //----------MAPS----------//


        private void LoadCategoriesMap()
        {
            CreateMap<CategoryListDto, CategoryListVm>();
            CreateMap<CategoryEditVm, Category>().ReverseMap();
            CreateMap<Category, CategoryListVm>().ReverseMap();
            CreateMap<CategoryCheckDto, CategoryCheckVm>();

        }

        private void LoadCountriesMap()
        {
            CreateMap<Country, CountryListVm>();
            CreateMap<CountryEditVm, Country>().ReverseMap();
        }

        private void LoadCitiesMap()
        {
            CreateMap<CityListDto, CityListVm>();
            CreateMap<CityEditVm, City>().ReverseMap();
            CreateMap<City, CityListVm>()
                .ForMember(
                    dest => dest.Country,
                    opt => opt.MapFrom(src => src.Country.Name)
                );
        }

        private void LoadDevelopersMap()
        {
            CreateMap<DeveloperListDto, DeveloperListVm>();
            CreateMap<DeveloperEditVm, Developer>().ReverseMap();
            CreateMap<Developer, DeveloperListVm>()
                .ForMember(
                    dest => dest.Country,
                    opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => src.City.Name));
        }

        private void LoadGamesMap()
        {
            CreateMap<GameListDto, GameListVm>();
            CreateMap<GameEditVm, Game>().ReverseMap();
            CreateMap<Game, GameListVm>()
                .ForMember(
                    dest => dest.Developer,
                    opt => opt.MapFrom(src => src.Developer.Name)
                );
        }

        private void LoadPlayerTypeMap()
        {
            CreateMap<PlayerTypeListDto, PlayerTypeListVm>();
            CreateMap<PlayerType, PlayerTypeListVm>();
            CreateMap<PlayerTypeEditVm, PlayerType>().ReverseMap();
            CreateMap<PlayerTypeCheckDto, PlayerTypeCheckVm>();
        }

        private void LoadUsersMap()
        {
            CreateMap<UserListDto, UserListVm>();
            CreateMap<UserEditVm, User>();
            CreateMap<User,UserEditVm>()
                .ForMember(
                    dest => dest.DeprecatedEmail,
                    opt => opt.MapFrom(src => src.Email)
                );
            CreateMap<User, UserListVm>()
                .ForMember(
                    dest => dest.City,
                    opt => opt.MapFrom(src => src.City.Name)
                );
        }

        private void LoadItemsCartMap()
        {
            CreateMap<ItemCart, ItemCartVm>();
        }
    }
}