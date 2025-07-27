namespace internship_entry_task.Interfaces.Repositories
{
    public interface IGameRepository
    {
        public Task<List<GameModel>> GetAllGamesAsync();
        public Task<GameModel> GetGameByIdAsync(Guid gameId);
        public Task<GameModel> AddGameAsync(GameModel game);
        public Task<bool> DeleteGameAsync(Guid gameId);
        public Task<GameModel> MakeMoveAsync(GameModel game);
    }
}