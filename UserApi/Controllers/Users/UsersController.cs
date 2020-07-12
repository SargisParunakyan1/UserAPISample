using AutoMapper;
using BLL.Abstract;
using DomainModels.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Users.DTO;

namespace UserApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fields

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UsersController(IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        #endregion

        #region Operations

        [HttpGet]
        [ProducesResponseType(typeof(List<UserDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] string category = null)
        {
            List<UserModel> userModels = await _userService.GetAllAsync(category);

            List<UserDTO> userDtos = _mapper.Map<List<UserDTO>>(userModels);

            return Ok(userDtos);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int userId)
        {
            UserModel userModel = await _userService.GetByIdAsync(userId);

            UserDTO user = _mapper.Map<UserDTO>(userModel);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody, Required] UserDTO userDto)
        {
            UserModel userModel = _mapper.Map<UserModel>(userDto);

            await _userService.InsertAsync(userModel);

            return CreatedAtAction(nameof(Post), new { user = userDto }, userDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody, Required] UserDTO userDto)
        {
            UserModel userModel = _mapper.Map<UserModel>(userDto);

            await _userService.UpdateAsync(userModel);

            return Ok();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int userId)
        {
            await _userService.DeleteAsync(userId);

            return Ok();
        }

        #endregion
    }
}