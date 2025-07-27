namespace internship_entry_task.Infrastructure.Interfaces
{
    public interface IGameService
    {
        Task<List<GameModel>> GetAllGamesAsync();
        Task<GameModel> GetGameAsync(Guid gameId);
        Task<GameModel> CreateNewGameAsync(int fieldSize = 3, GameConditions? conditions = null);
        Task<GameModel> GetGameStateAsync(Guid gameId);
        Task<GameModel> MakeMoveAsync(Guid gameId, ushort x, ushort y);
        Task<bool> DeleteGameAsync(Guid gameId);
    }
}