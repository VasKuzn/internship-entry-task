namespace internship_entry_task.Models
{
    public class GameModel
    {
        private List<List<char>> _gameField;

        public required Guid gameId { get; set; }

        public required char currentPlayer { get; set; } // первый игрок - false, второй true

        public required ushort moveNumber { get; set; }

        public required List<List<char>> gameField
        {
            get => _gameField;
            set
            {
                if (value == null || value.Count < 3 || value.Any(row => row?.Count < 3))
                    throw new ArgumentException("Game field must be at least 3x3");
                _gameField = value;
            }
        }

        public GameConditions? conditions { get; set; } // задаем длину победной комбинации и включение/выключение линий для победы
    }
}