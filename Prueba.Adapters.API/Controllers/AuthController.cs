using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prueba.Domain.Entities.Dtos;
using Prueba.Domain.Entities.Request;
using Prueba.Domain.Entities.Response;
using Prueba.Domain.Interfaces;

namespace Prueba.Adapters.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public ActionResult Create([FromBody] CreateUserRequest payload)
        {
            try
            {
                var user = _authService.Create(payload);
                UserDto userDto = _mapper.Map<UserDto>(user);
                return StatusCode(StatusCodes.Status201Created, userDto);
            }
            catch (InvalidDataException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Error = ex.Message });
            }
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] AuthRequest payload)
        {
            try
            {
                LoginResponse loginResponse = new();
                loginResponse.Token = _authService.Auth(payload);
                return StatusCode(StatusCodes.Status200OK, loginResponse);
            }
            catch (InvalidDataException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Error = ex.Message });
            }
        }
    }
}
