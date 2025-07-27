using internship_entry_task.Infrastructure.Interfaces;
using internship_entry_task.Interfaces.Repositories;

namespace internship_entry_task.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameModel>> GetAllGamesAsync()
        {
            return await _gameRepository.GetAllGamesAsync();
        }

        public async Task<GameModel> GetGameAsync(Guid gameId)
        {
            return await _gameRepository.GetGameByIdAsync(gameId);
        }

        public async Task<GameModel> GetGameStateAsync(Guid gameId)
        {
            return await _gameRepository.GetGameByIdAsync(gameId);
        }

        public async Task<GameModel> CreateNewGameAsync(int fieldSize = 3, GameConditions? conditions = null)
        {
            if (fieldSize < 3)
                throw new ArgumentException("Field size must be at least 3x3");

            conditions ??= new GameConditions
            {
                IsHorizontalWin = true,
                HorizontalWinLength = 3,
                IsVerticalWin = true,
                VerticalWinLength = 3,
                IsDiagonalWin = true,
                DiagonalWinLength = 3
            };

            var gameField = new List<List<char>>();
            for (int i = 0; i < fieldSize; i++)
            {
                gameField.Add(Enumerable.Repeat(' ', fieldSize).ToList());
            }

            var newGame = new GameModel
            {
                gameId = Guid.NewGuid(),
                currentPlayer = 'X',
                moveNumber = 1,
                gameField = gameField,
                gameState = "InProgress",
                conditions = conditions
            };

            return await _gameRepository.AddGameAsync(newGame);
        }

        public async Task<bool> DeleteGameAsync(Guid gameId)
        {
            return await _gameRepository.DeleteGameAsync(gameId);
        }

        public async Task<GameModel> MakeMoveAsync(Guid gameId, ushort x, ushort y)
        {
            var game = await _gameRepository.GetGameByIdAsync(gameId);

            ValidateMove(game, x, y);

            game.gameField[x][y] = game.currentPlayer;
            game.moveNumber++;

            if (CheckWinCondition(game, x, y))
            {
                // добавить логику обработки победы
            }
            else
            {
                game.currentPlayer = game.currentPlayer == 'X' ? 'O' : 'X';
            }

            return await _gameRepository.MakeMoveAsync(game);
        }
        private void ValidateMove(GameModel game, int x, int y)
        {
            if (x < 0 || x >= game.gameField.Count || y < 0 || y >= game.gameField[0].Count)
                throw new ArgumentException("Invalid coordinates");

            if (game.gameField[x][y] != ' ')
                throw new InvalidOperationException("Cell is already occupied");
        }

        private bool CheckWinCondition(GameModel game, int lastMoveX, int lastMoveY)
        {
            var symbol = game.gameField[lastMoveX][lastMoveY];

            if (game.gameField[lastMoveX].All(c => c == symbol))
                return true;

            if (game.gameField.All(row => row[lastMoveY] == symbol))
                return true;

            // для поля 3x3
            if (game.gameField.Count == 3)
            {
                if (game.gameField[0][0] == symbol &&
                    game.gameField[1][1] == symbol &&
                    game.gameField[2][2] == symbol)
                    return true;

                if (game.gameField[0][2] == symbol &&
                    game.gameField[1][1] == symbol &&
                    game.gameField[2][0] == symbol)
                    return true;
            }

            return false;
        }
    }
}