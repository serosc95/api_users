using Microsoft.AspNetCore.Mvc;

namespace api_users.Controllers;

public class UserController : Controller
{
    private readonly HttpClient _httpClient;
    public UserController()
    {
        // var httpClientHandler = new HttpClientHandler();
        // httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        // _httpClient = new HttpClient(httpClientHandler);
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("API URL");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var response =  await _httpClient.GetAsync("/users");
        response.EnsureSuccessStatusCode();

        var users  = await response.Content.ReadFromJsonAsync<List<UserModel>>();
        return View(users);
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel user)
    {
        var response =  await _httpClient.PostAsJsonAsync("/users", user);
        response.EnsureSuccessStatusCode();

        return RedirectToAction("GetAllUsers");
    }
}
