﻿using System.Threading.Tasks;

namespace jwtauth.Application.Services
{
    public interface ITokenService
    {
        Task<string> Gerar(string nome, string funcao);
    }
}