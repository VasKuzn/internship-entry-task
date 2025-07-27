namespace internship_entry_task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet("/games")]
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
        [HttpGet("/games/{gameId}")]
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
        [HttpPost("/games")]
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
        [HttpDelete("/games/{gameId}")]
        public async Task<IActionResult> DeleteGame(Guid gameId)
        {
            try
            {
                var result = await _gameService.DeleteGameAsync(gameId);
                if (!result)
                {
                    return NotFound($"Game with id {gameId} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("/moves/{gameId}")]
        public async Task<ActionResult<GameModel>> MakeAMove(Guid gameId, ushort x, ushort y)
        {
            try
            {
                var moveInGame = await _gameService.MakeMoveAsync(gameId, x, y);
                return CreatedAtAction(
                    nameof(GetGameById),
                    new { gameId = moveInGame.gameId },
                    moveInGame
                );
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