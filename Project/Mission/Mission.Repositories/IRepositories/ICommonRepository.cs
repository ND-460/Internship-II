using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Entities.Models.CommonModels;

namespace Mission.Repositories.IRepositories
{
    public interface ICommonRepository
    {
        List<DropDownResponseModel> CountryList();

        List<DropDownResponseModel> CityList(int countryId);

        List<DropDownResponseModel> MissionCountryList();

        List<DropDownResponseModel> MissionCityList();

        List<DropDownResponseModel> MissionThemeList();

        List<DropDownResponseModel> MissionSkillList();

        List<DropDownResponseModel> MissionTitleList();
    }
}
