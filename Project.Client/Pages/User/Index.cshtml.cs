using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Project.Api.Models;

namespace Project.Client.Pages.Shared.User
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<Project.Api.Models.User> Users { get; set; }


        public async Task OnGet()
        {
            HttpClient _userService = _clientFactory.CreateClient("UserService");

            string token = await HttpContext.GetTokenAsync("access_token");
            _userService.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            string userString = await _userService.GetStringAsync("api/user/GetAll");


            Users = JsonConvert.DeserializeObject<List<Project.Api.Models.User>>(userString);
        }
    }
}
