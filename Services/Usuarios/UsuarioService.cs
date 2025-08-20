using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRpgEtec.Models;

namespace AppRpgEtec.Services.Usuarios
{
    internal class UsuarioService : Request 
    {
        private readonly Request _request;
        private const string _apiUrlBase = "isabelinha-rpg.azurewebsites.net";

        public UsuarioService()
        {
            _request = new Request();
        }

        public async Task<Usuario> PostResgistraUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Resgistrar";
            u.Id = await _request.PostReturnIntAsync(_apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Autenticar";
            u = await _request.PostAsync(_apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }




    }
}
