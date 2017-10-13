namespace BowlingKataMicroObjectsRefactor
{

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
                if (new IsStrike().IsType(_pinsDown, pinsIndex))
                {
                    score += new StrikeScore().Score(_pinsDown, pinsIndex);
                    pinsIndex += new StrikeIndexAdjustment().Adjustment();
                    continue;
                }

                if (new IsSpare().IsType(_pinsDown, pinsIndex))
                {
                    score += new SpareScore().Score(_pinsDown, pinsIndex);
                    pinsIndex += new SpareIndexAdjustment().Adjustment();
                    continue;
                }

                if (new IsDefault().IsType(_pinsDown, pinsIndex))
                {
                    score += new DefaultScore().Score(_pinsDown, pinsIndex);
                    pinsIndex += new DefaultIndexAdjustment().Adjustment();
                }
            }
            return score;
        }
    }
}