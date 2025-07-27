using internship_entry_task.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace internship_entry_task.Controllers
{
    [ApiController]
    [Route("/games")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameModel>>> GetAllGames()
        {
            try
            {
                var games = await _gameService.GetAllGamesAsync();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameModel>> GetGameById(Guid gameId)
        {
            try
            {
                var game = await _gameService.GetGameAsync(gameId);
                return Ok(game);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<GameModel>> CreateGame(ushort fieldSize, GameConditions conditions)
        {
            try
            {
                var createdGame = await _gameService.CreateNewGameAsync(fieldSize, conditions);
                return CreatedAtAction(nameof(GetGameById), new { gameid = createdGame.gameId }, createdGame);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}