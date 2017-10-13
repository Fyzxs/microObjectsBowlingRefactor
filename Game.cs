namespace BowlingKataMicroObjectsRefactor
{
    public class Game
    {
        private readonly int[] _rolls = new int[21];
        private int _ptr;

        public void Roll(int pins) => _rolls[_ptr++] = pins;

        public int Score()
        {
            int score = 0;
            int pinsIndex = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(pinsIndex))
                {
                    score += StrikeScore(pinsIndex);
                    pinsIndex += 1;
                    continue;
                }
                if (IsSpare(pinsIndex))
                {
                    score += SpareScore(pinsIndex);
                }
                else
                {
                    score += RegularScore(pinsIndex);
                }
                pinsIndex += 2;
            }
            return score;
        }

        private bool IsStrike(int pinsIndex) => _rolls[pinsIndex] == 10;
        private bool IsSpare(int pinsIndex) => _rolls[pinsIndex] + _rolls[pinsIndex + 1] == 10;
        private int RegularScore(int pinsIndex) => _rolls[pinsIndex] + _rolls[pinsIndex + 1];
        private int SpareScore(int pinsIndex) => 10 + _rolls[pinsIndex + 2];
        private int StrikeScore(int pinsIndex) => 10 + _rolls[pinsIndex + 1] + _rolls[pinsIndex + 2];
    }
}