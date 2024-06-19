
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IAppSettingsServices
{
    Task<AppsettingsDto> GetMyAppSetting();
    Task<(AppsettingsDto? appsetting, string? error)> Update(Guid id, AppsettingsUpdate appsettingUpdate, string language);
}

public class AppSettingsServices : IAppSettingsServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public AppSettingsServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   


public async Task<AppsettingsDto> GetMyAppSetting()
{
    // Try to retrieve the AppSetting from the database
    var appsetting1 = await _repositoryWrapper.Appsettings.GetAll();
    var appsetting = appsetting1.data.FirstOrDefault();


    // If it doesn't exist, create a default one
    if (appsetting == null)
    {
        var defaultAppSetting = new Appsettings
        {
            // Set default values here
        };

        appsetting = await _repositoryWrapper.Appsettings.Add(defaultAppSetting);
    }

    // Map it to AppSettingDto and return
    return _mapper.Map<AppsettingsDto>(appsetting);
}

public async Task<(AppsettingsDto? appsetting, string? error)> Update(Guid id, AppsettingsUpdate appsettingUpdate, string language)
{
    var appsetting = await _repositoryWrapper.Appsettings.GetById(id);
    if (appsetting == null) return (null, ErrorResponseException.GenerateLocalizedResponse("AppSetting not found", "لم يتم العثور على الإعدادات", language));
    _mapper.Map(appsettingUpdate, appsetting);
    appsetting = await _repositoryWrapper.Appsettings.Update(appsetting, id);
    return (_mapper.Map<AppsettingsDto>(appsetting), null);
}




public async Task<(Appsettings? appsettings, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
