using Memento.Movies.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Memento.Movies.Shared.Models.Identity.Contracts.Roles
{
	/// <summary>
	/// Implements the 'RoleUserForm' contract.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class RoleUserFormContract
	{
		#region [Properties]
		/// <summary>
		/// The User's identifier.
		/// </summary>
		[Required]
		[Display(Name = nameof(SharedResources.USER_ID), ResourceType = typeof(SharedResources))]
		public long? Id { get; set; }
		#endregion
	}
}