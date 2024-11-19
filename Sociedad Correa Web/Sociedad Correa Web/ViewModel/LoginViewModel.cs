using Microsoft.AspNetCore.Mvc;
using Sociedad_Correa_Web;

using System.ComponentModel.DataAnnotations;

namespace Sociedad_Correa_Web.ViewModel
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "El nombre de usuario es obligatorio")]
		public string NombreUsuario { get; set; }

		[Required(ErrorMessage = "La clave es obligatoria")]
		public string Clave { get; set; }
	}
}

