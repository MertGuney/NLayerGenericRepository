using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Services.Helpers
{
    //public class IztekPlatformService : IIztekPlatformService
    //{
    //    private readonly HttpClient _httpClient;
    //    private const string client = "b27a4a3b-8975-444b-b5c1-facfe6c17481";
    //    public IztekPlatformService(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;
    //    }

    //    public async Task<Response<ResponseMessageDTO>> SendMailAsync(SendMailDTO sendMail)
    //    {
    //        sendMail.Client = client;
    //        var response = await _httpClient.PostAsJsonAsync("mail", sendMail);
    //        if (!response.IsSuccessStatusCode)
    //        {
    //            return Response<ResponseMessageDTO>.Fail($"Mail gönderme sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.", 500, true);
    //        }
    //        return Response<ResponseMessageDTO>.Success(new ResponseMessageDTO { Message = "Mail gönderme işlemi başarılı.", IsSuccessful = true }, 200);
    //    }
    //}
}
