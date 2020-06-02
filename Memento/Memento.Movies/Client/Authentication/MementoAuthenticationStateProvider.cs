using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Memento.Movies.Client.Authentication
{
	public sealed class MementoAuthenticationStateProvider : AuthenticationStateProvider
	{
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var user = new ClaimsIdentity();

			return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(user)));
		}
	}
}