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
            IFrameState frameState = new FrameState(0, 0);
            for (int frame = 0; frame < 10; frame++)
            {
                if (new IsStrike().IsType(_pinsDown, pinsIndex))
                {
                    frameState = frameState.Score(_pinsDown, new StrikeScore(), new StrikeIndexAdjustment());
                    pinsIndex += new StrikeIndexAdjustment().Adjustment();
                    continue;
                }

                if (new IsSpare().IsType(_pinsDown, pinsIndex))
                {
                    frameState = frameState.Score(_pinsDown, new SpareScore(), new SpareIndexAdjustment());
                    pinsIndex += new SpareIndexAdjustment().Adjustment();
                    continue;
                }

                if (new IsDefault().IsType(_pinsDown, pinsIndex))
                {
                    frameState = frameState.Score(_pinsDown, new DefaultScore(), new DefaultIndexAdjustment());
                    pinsIndex += new DefaultIndexAdjustment().Adjustment();
                }
            }
            return score + frameState.Score();
        }
    }
}