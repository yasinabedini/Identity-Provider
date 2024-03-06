using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Project.Api.Models;

namespace Project.Client.Pages.Shared.User
{     
    public class IndexModel : PageModel
    {
        private readonly HttpClient _userService;

        public List<Project.Api.Models.User> Users { get; set; }

        public IndexModel(IHttpClientFactory factory)
        {
            _userService = factory.CreateClient("UserService");
        }

        public async Task OnGet()
        {   
            Users = JsonConvert.DeserializeObject<List<Project.Api.Models.User>>(await _userService.GetStringAsync("api/user/GetAll"));
        }
    }
}
