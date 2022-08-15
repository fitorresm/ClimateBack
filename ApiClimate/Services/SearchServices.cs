using ApiClimate.Data;
using ApiClimate.Domain;
using ApiClimate.Models;

namespace ApiClimate.Services
{
    public class SearchServices
    {
        private readonly ClimateDbContext dbClimate;
        private readonly IConfiguration configuration;

        public SearchServices(ClimateDbContext climatecontext, IConfiguration iConfiguration)
        {
            dbClimate = climatecontext;
            configuration = iConfiguration;
        }

        #region methods
        public List<ClimateResponse> HotCitys()
        {
           List<ClimateResponse> returnList = new List<ClimateResponse>();
           List<LocationClimate> ListLocation = new List<LocationClimate>();
           DateTime todayStart = DateTime.Today;
           DateTime todayEnd = DateTime.Today.AddHours(23).AddMinutes(59);

           
            try
            {
                ListLocation = dbClimate.LocationClimate.Where(x => x.Verification_Date >= todayStart && x.Verification_Date <= todayEnd).OrderBy(x => x.Climate_Now).ToList();
                for (int i = 0; i < 3 || i < ListLocation.Count(); i++)
                {

                    ClimateResponse climateResponse = new ClimateResponse
                    {
                        UF = GetUF(ListLocation[i].Id_UF),
                        Region = GetRegion(ListLocation[i].Id_Region),
                        City = GetCity(ListLocation[i].Id_City),
                        ConditionClimate = GetConditions(ListLocation[i].Id_ConditionClimate),
                        Verification_Date = todayStart,
                        Verification_Min = ListLocation[i].Verification_Min,
                        Verification_Max = ListLocation[i].Verification_Max,
                        Climate_Now = ListLocation[i].Climate_Now
                    };

                    returnList.Add(climateResponse);
                }
                return returnList;
            }
            catch
            {
                return null;
            }
        }

        public List<ClimateResponse> ColdCitys()
        {
            List<ClimateResponse> returnList = new List<ClimateResponse>();
            List<LocationClimate> ListLocation = new List<LocationClimate>();
            DateTime todayStart = DateTime.Today;
            DateTime todayEnd = DateTime.Today.AddHours(23).AddMinutes(59);

            
            try
            {
                for (int i = 0; i < 3 || i < ListLocation.Count(); i++)
                {
                    ListLocation = dbClimate.LocationClimate.Where(x => x.Verification_Date >= todayStart && x.Verification_Date <= todayEnd).OrderByDescending(x => x.Climate_Now).ToList();
                    ClimateResponse climateResponse = new ClimateResponse
                    {
                        UF = GetUF(ListLocation[i].Id_UF),
                        Region = GetRegion(ListLocation[i].Id_Region),
                        City = GetCity(ListLocation[i].Id_City),
                        ConditionClimate = GetConditions(ListLocation[i].Id_ConditionClimate),
                        Verification_Date = todayStart,
                        Verification_Min = ListLocation[i].Verification_Min,
                        Verification_Max = ListLocation[i].Verification_Max,
                        Climate_Now = ListLocation[i].Climate_Now
                    };

                    returnList.Add(climateResponse);
                }
                return returnList;
            }
            catch
            {
                return null;
            }
        }

        public List<ClimateResponse> NextConditions(int IdCity)
        {
            List<ClimateResponse> returnList = new List<ClimateResponse>();
            List<LocationClimate> ListLocation = new List<LocationClimate>();

           
            try
            {
                ListLocation = dbClimate.LocationClimate.Where(x => x.Id_City == IdCity && x.Verification_Date >= DateTime.Today).OrderBy(x=> x.Verification_Date).ToList();
                int cont = ListLocation.Count();
                for (int i = 0; i < cont; i++)
                {
                    ClimateResponse climateResponse = new ClimateResponse
                    {
                        UF = GetUF(ListLocation[i].Id_UF),
                        Region = GetRegion(ListLocation[i].Id_Region),
                        City = GetCity(ListLocation[i].Id_City),
                        ConditionClimate = GetConditions(ListLocation[i].Id_ConditionClimate),
                        Verification_Date = ListLocation[i].Verification_Date,
                        Verification_Min = ListLocation[i].Verification_Min,
                        Verification_Max = ListLocation[i].Verification_Max,
                        Climate_Now = ListLocation[i].Climate_Now
                    };

                    returnList.Add(climateResponse);
                    if(i == 6)
                    {
                        return returnList;
                    }
                }
                return returnList;
            }
            catch(Exception ex)
            {
                return null;
            }


        }

        public ClimateResponse TodayCondition(int IdCity)
        {
            ClimateResponse returnConditionNow = new ClimateResponse();
            LocationClimate ConditionNow = new LocationClimate();


            try
            {
                ConditionNow = dbClimate.LocationClimate.Where(x => x.Id_City == IdCity && x.Verification_Date >= DateTime.Today && x.Verification_Date <= DateTime.Today.AddHours(23).AddMinutes(59)).FirstOrDefault();
                if(ConditionNow != null)
                {
                    returnConditionNow.UF = GetUF(ConditionNow.Id_UF);
                    returnConditionNow.Region = GetRegion(ConditionNow.Id_Region);
                    returnConditionNow.City = GetCity(ConditionNow.Id_City);
                    returnConditionNow.ConditionClimate = GetConditions(ConditionNow.Id_ConditionClimate);
                    returnConditionNow.Verification_Date = ConditionNow.Verification_Date;
                    returnConditionNow.Verification_Min = ConditionNow.Verification_Min;
                    returnConditionNow.Verification_Max = ConditionNow.Verification_Max;
                    returnConditionNow.Climate_Now = ConditionNow.Climate_Now;
                }

               
                return returnConditionNow;
            }
            catch(Exception ex)
            {
                return null;
            }


        }
        public List<CityResponse> GetCitybyUF(int ufID)
        {
            List<CityResponse> cityResponse = new List<CityResponse>();
            List<City> city = new List<City>();
            try
            {
                city = dbClimate.City.Where(x => x.Id_UF == ufID).ToList();
                if(city.Count > 0)
                {
                    foreach (var item in city)
                    {
                        CityResponse member = new CityResponse()
                        {
                            Nome = item.Nome,
                            Id = item.Id,
                            SiglaUf = GetUF(ufID)
                        };
                        cityResponse.Add(member);
                    }
                }
              

                return cityResponse;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<UFResponse> GetUFtoList()
        {
            try
            {
                List<UFResponse> uFResponses = new List<UFResponse>();
                List<UF> ufList = dbClimate.UF.Where(x => x.Id > 0).ToList();
                if (ufList.Count > 0)
                {
                    foreach (var item in ufList)
                    {
                        UFResponse member = new UFResponse()
                        {
                            Sigla = item.Sigla,
                            Id = item.Id
                        };
                        uFResponses.Add(member);
                    }
                }


                return uFResponses;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        #endregion

        #region privates

        private string GetUF(int Id)
        {
            UF uF = dbClimate.UF.Where(c => c.Id == Id).FirstOrDefault();

            if (uF == null)
            {
                return null;
            }
            else
            {
                return uF.Sigla;
            }
        }

        private string GetCity(int Id)
        {
            City city = dbClimate.City.Where(c => c.Id == Id).FirstOrDefault();

            if (city == null)
            {
                return null;
            }
            else
            {
                return city.Nome;
            }
        }

        private string GetRegion(int Id)
        {
            Region region = dbClimate.Region.Where(c => c.Id == Id).FirstOrDefault();

            if (region == null)
            {
                return null;
            }
            else
            {
                return region.Nome;
            }
        }
        private string GetConditions(int Id)
        {
            ConditionClimate conditions = dbClimate.ConditionClimate.Where(c => c.Id == Id).FirstOrDefault();

            if (conditions == null)
            {
                return null;
            }
            else
            {
                return conditions.Condition;
            }
        }

        #endregion
    }
}
