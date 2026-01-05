using Core.Model;
using Core.Model.Helper;

namespace Core.Services.Helper.Interface;

public interface IAiServiceFactory
{
    IAiService GetUserService(UserAiProvider provider);
}