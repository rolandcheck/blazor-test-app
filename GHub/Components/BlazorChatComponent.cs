using GHub.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace GHub.Components
{
    public class BlazorChatComponent : ComponentBase
    {
        [Inject] private UserManager<AppUser> UserManager { get; set; }

    }
}