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
            int pinsIndex = 0;
            IFrameState frameState = new FrameState(0, 0);
            for (int frame = 0; frame < 10; frame++)
            {
                frameState = frameState.Score(_pinsDown, new IsStrike(), new StrikeScore(), new StrikeIndexAdjustment());
                if (new IsStrike().IsType(_pinsDown, pinsIndex))
                {
                    pinsIndex += new StrikeIndexAdjustment().Adjustment();
                    continue;
                }

                frameState = frameState.Score(_pinsDown, new IsSpare(), new SpareScore(), new SpareIndexAdjustment());
                if (new IsSpare().IsType(_pinsDown, pinsIndex))
                {
                    pinsIndex += new SpareIndexAdjustment().Adjustment();
                    continue;
                }

                frameState = frameState.Score(_pinsDown, new IsDefault(), new DefaultScore(), new DefaultIndexAdjustment());
            }
            return frameState.Score();
        }
    }
}