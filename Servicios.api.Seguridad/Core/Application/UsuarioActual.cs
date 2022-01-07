﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Servicios.api.Seguridad.Core.Dto;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.JwtLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    public class UsuarioActual
    {
        public class UsuarioActualCommand : IRequest<UsuarioDto> {
           
        }


        public class UsuarioActualHandler : IRequestHandler<UsuarioActualCommand, UsuarioDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IMapper _mapper;

            public UsuarioActualHandler(UserManager<Usuario> userManager, IUsuarioSesion usuarioSesion, IJwtGenerator jwtGenerator, IMapper mapper) {
                _userManager = userManager;
                _usuarioSesion = usuarioSesion;
                _jwtGenerator = jwtGenerator;
                _mapper = mapper;
            }
            
            public async Task<UsuarioDto> Handle(UsuarioActualCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.GetUsuarioSesion());

                if (usuario != null) {
                    var usuarioDTO = _mapper.Map<Usuario, UsuarioDto>(usuario);
                    usuarioDTO.Token = _jwtGenerator.CreateToken(usuario);
                    return usuarioDTO;
                }

                throw new Exception("No se encontro el usuario");

            }
        }

    }
}
