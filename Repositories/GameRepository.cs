using internship_entry_task.Interfaces.Repositories;
using internship_entry_task.Repositories.Data.DataBaseContext;

namespace internship_entry_task.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DataBaseContext _dbContext;

        public GameRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<GameModel>> GetAllGamesAsync()
        {
            return await _dbContext.Games.ToListAsync();
        }

        public async Task<GameModel> GetGameByIdAsync(Guid gameId)
        {
            return await _dbContext.Games.FirstOrDefaultAsync(a => a.gameId == gameId)
                   ?? throw new ArgumentException("Game is not found");
        }

        public async Task<GameModel> AddGameAsync(GameModel game)
        {
            await _dbContext.AddAsync(game);
            await _dbContext.SaveChangesAsync();
            return game;
        }

        public async Task<bool> DeleteGameAsync(Guid gameId)
        {
            var deleted = await _dbContext.Games
                .Where(a => a.gameId == gameId)
                .ExecuteDeleteAsync();
            return deleted > 0;
        }

        public async Task<GameModel> MakeMoveAsync(GameModel game)
        {
            var existingGame = await _dbContext.Games.FindAsync(game.gameId);
            if (existingGame == null)
            {
                throw new ArgumentException("Game not found in database");
            }

            _dbContext.Entry(existingGame).CurrentValues.SetValues(game);

            existingGame.gameField = game.gameField;
            existingGame.currentPlayer = game.currentPlayer;
            existingGame.moveNumber = game.moveNumber;

            await _dbContext.SaveChangesAsync();

            return existingGame;
        }
    }
}