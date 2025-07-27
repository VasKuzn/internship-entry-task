namespace internship_entry_task.Models
{
    public class GameConditions
    {
        private ushort _horizontalWinLength = 3;
        private ushort _verticalWinLength = 3;
        private ushort _diagonalWinLength = 3;

        public bool IsHorizontalWin { get; set; } = true;
        public ushort HorizontalWinLength
        {
            get => _horizontalWinLength;
            set => _horizontalWinLength = value >= 3 ? value : throw new ArgumentException("Win length must be at least 3");
        }

        public bool IsVerticalWin { get; set; } = true;
        public ushort VerticalWinLength
        {
            get => _verticalWinLength;
            set => _verticalWinLength = value >= 3 ? value : throw new ArgumentException("Win length must be at least 3");
        }

        public bool IsDiagonalWin { get; set; } = true;
        public ushort DiagonalWinLength
        {
            get => _diagonalWinLength;
            set => _diagonalWinLength = value >= 3 ? value : throw new ArgumentException("Win length must be at least 3");
        }
    }
}