using ApiClimate.Models;
using ApiClimate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiClimate.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SearchController : ControllerBase
    {
        private readonly SearchServices searchServices;

        public SearchController(SearchServices _searchServices)
        {
            searchServices = _searchServices;
        }

        /// <summary>
        /// Retorna o top 3 de Cidades mais quentes no dia de hoje
        /// </summary>
        /// <returns>Lista de ClimateResponse</returns>
        [HttpGet]
        [Authorize]
        public List<ClimateResponse> GetHotCity()
        {
            List<ClimateResponse> returnList = new List<ClimateResponse>();

            returnList = searchServices.HotCitys();

            return returnList;
        }

        /// <summary>
        /// Retorna o top 3 de Cidades mais frias no dia de hoje
        /// </summary>
        /// <returns>Lista de ClimateResponse</returns>
        [HttpGet]
        [Authorize]
        public List<ClimateResponse> GetColdCity()
        {
            List<ClimateResponse> returnList = new List<ClimateResponse>();

            returnList = searchServices.ColdCitys();

            return returnList;
        }

        /// <summary>
        /// Retorna as condições dos próximos dias máximo de 7
        /// </summary>
        /// <returns>Lista de ClimateResponse</returns>
        [HttpGet]
        [Authorize]
        public List<ClimateResponse> GetNextConditions(int cityId)
        {
            List<ClimateResponse> returnList = new List<ClimateResponse>();

            returnList = searchServices.NextConditions(cityId);

            return returnList;
        }

        /// <summary>
        /// Retorna a condição da data atual
        /// </summary>
        /// <returns>Lista de ClimateResponse</returns>
        [HttpGet]
        [Authorize]
        public ClimateResponse GetTodayClimate(int cityId)
        {
            ClimateResponse returnList = new ClimateResponse();

            returnList = searchServices.TodayCondition(cityId);

            return returnList;
        }

        /// <summary>
        /// Retorna Lista de Cidades pelo ID do Estado
        /// </summary>
        /// <param name="ufID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        
        public List<CityResponse> GetCity(int ufID)
        {
            List<CityResponse> returnList = new List<CityResponse>();

            returnList = searchServices.GetCitybyUF(ufID);

            return returnList;
        }

        /// <summary>
        /// Retorna lista de estados 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public List<UFResponse> GetUf()
        {
            List<UFResponse> returnList = new List<UFResponse>();

            returnList = searchServices.GetUFtoList();

            return returnList;
        }

      
    }
}
