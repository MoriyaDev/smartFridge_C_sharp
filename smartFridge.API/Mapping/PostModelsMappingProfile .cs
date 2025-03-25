using AutoMapper;
using SmartFridge.API.Models;
using SmartFridge.Core.Model;


namespace SmartFridge.API.Mapping
{
    public class PostModelsMappingProfile : Profile
    {
        public PostModelsMappingProfile() {

            CreateMap<FridgePostModel, Fridge>();
            CreateMap<ProductPostModel, Product>();
            CreateMap<RecipePostModel, Recipe>();
        }


    }
}
