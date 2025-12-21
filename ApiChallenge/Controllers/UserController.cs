using ApiChallenge.Data.Entities;
using ApiChallenge.Data.Entities.Dtos;
using ApiChallenge.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiChallenge.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IValidator<CreateUserWithAddressDto> _createUserWithAddressValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;

    private readonly string CacheKeyPrefix = "user-";

    public UserController(
        IUserService userService,
        IValidator<CreateUserDto> createUserValidator,
        IValidator<CreateUserWithAddressDto> createUserWithAddressValidator,
        IMapper mapper,
        ILogger<UserController> logger)
    {
        _userService = userService;
        _createUserValidator = createUserValidator;
        _createUserWithAddressValidator = createUserWithAddressValidator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("Search")]
    public async Task<ActionResult<IEnumerable<UserWithAddressResponseDto>>> SearchUsers(
    [FromQuery] string? nombre = null,
    [FromQuery] string? provincia = null,
    [FromQuery] string? ciudad = null)
    {
        try
        {
            var users = await _userService.SearchUsersAsync(nombre, provincia, ciudad);
            var usersDto = _mapper.Map<List<UserWithAddressResponseDto>>(users);
            return Ok(usersDto);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al buscar usuarios: {ex.Message}");
        }
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<UserWithAddressResponseDto>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserWithAddressResponseDto>>(users);
            return Ok(userDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetUser(
        int id,
        [FromServices] IDistributedCache cache,
        CancellationToken ct)
    {
        var cacheKey = CacheKeyPrefix + id;

        try
        {
            var user = await cache.GetAsync(cacheKey, async token =>
            {
                _logger.LogInformation("Cache miss for key {CacheKey}; fetching from service", cacheKey);
                var entity = await _userService.GetByIdAsync(id);
                var dto = _mapper.Map<UserResponseDto>(entity);
                return dto;
            },
            CacheOptions.DefaultExpiration,
            ct);

            if (EqualityComparer<UserResponseDto>.Default.Equals(user, default))
            {
                return NotFound($"Usuario con ID {id} no encontrado");
            }

            _logger.LogInformation("Cache hit or populated for key {CacheKey}; returning user id {Id}", cacheKey, id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error accessing cache for key {CacheKey}", cacheKey);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] CreateUserDto user)
    {
        try
        {
            var validationResult = await _createUserValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var userEntity = _mapper.Map<User>(user);
            var createdUser = await _userService.CreateAsync(userEntity);
            var userDto = _mapper.Map<UserResponseDto>(createdUser);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, userDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPost("CreateAlterUserWithAddress")]
    public async Task<ActionResult<UserWithAddressResponseDto>> CreateAlterUserWithAddress([FromBody] CreateUserWithAddressDto user)
    {
        try
        {
            var validationResult = await _createUserWithAddressValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var userEntity = _mapper.Map<User>(user);
            var domicilios = _mapper.Map<IEnumerable<Domicilio>>(user.Domicilios);
            var createdUser = await _userService.CreateAlterUserWithAddressesAsync(userEntity, domicilios);
            var userDto = _mapper.Map<UserWithAddressResponseDto>(createdUser);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, userDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, [FromBody] UpdateUserDto user, [FromServices] IDistributedCache cache)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userEntity = _mapper.Map<User>(user);
            var updatedUser = await _userService.UpdateAsync(id, userEntity);
            if (updatedUser == null)
                return NotFound($"Usuario con ID {id} no encontrado");
            await cache.RemoveAsync(CacheKeyPrefix + id);
            var userDto = _mapper.Map<UserResponseDto>(updatedUser);
            return Ok(userDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id, IDistributedCache cache)
    {
        try
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
                return NotFound($"Usuario con ID {id} no encontrado");
            
            await cache.RemoveAsync(CacheKeyPrefix + id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}
