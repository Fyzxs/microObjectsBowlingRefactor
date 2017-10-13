namespace BowlingKataMicroObjectsRefactor
{
    public class StrikeIndexAdjustment : IIndexAdjustment
    {
        public int Adjustment() => 1;
    }
    public class SpareIndexAdjustment : IIndexAdjustment
    {
        public int Adjustment() => 2;
    }
    public class DefaultIndexAdjustment : IIndexAdjustment
    {
        public int Adjustment() => 2;
    }

    public interface IIndexAdjustment
    {
        int Adjustment();
    }

    public class Game
    {
        private readonly IPinsDown _pinsDown;

        public Game() : this(new PinsDown()) { }
        public Game(IPinsDown pinsDown) => _pinsDown = pinsDown;

        public void Roll(int pins) => _pinsDown.Roll(pins);

        public int Score()
        {
            int score = 0;
            int pinsIndex = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(pinsIndex))
                {
                    score += StrikeScore(pinsIndex);
                    pinsIndex += new StrikeIndexAdjustment().Adjustment();
                    continue;
                }
                if (IsSpare(pinsIndex))
                {
                    score += SpareScore(pinsIndex);
                    pinsIndex += new SpareIndexAdjustment().Adjustment();
                }
                else
                {
                    score += RegularScore(pinsIndex);
                    pinsIndex += new DefaultIndexAdjustment().Adjustment();
                }
            }
            return score;
        }

        private bool IsStrike(int pinsIndex) => _pinsDown.PinsDownAt(pinsIndex) == 10;
        private bool IsSpare(int pinsIndex) => _pinsDown.PinsDownAt(pinsIndex) + _pinsDown.PinsDownAt(pinsIndex + 1) == 10;
        private int RegularScore(int pinsIndex) => _pinsDown.PinsDownAt(pinsIndex) + _pinsDown.PinsDownAt(pinsIndex + 1);
        private int SpareScore(int pinsIndex) => 10 + _pinsDown.PinsDownAt(pinsIndex + 2);
        private int StrikeScore(int pinsIndex) => 10 + _pinsDown.PinsDownAt(pinsIndex + 1) + _pinsDown.PinsDownAt(pinsIndex + 2);
    }
}