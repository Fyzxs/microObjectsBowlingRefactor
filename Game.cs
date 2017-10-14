namespace BowlingKataMicroObjectsRefactor
{
    public class StrikeFrame : IFrame
    {
        private readonly IFrameType _frameType;
        private readonly ITypeScore _typeScore;
        private readonly IIndexAdjustment _indexAdjustment;

        public StrikeFrame() : this(new IsStrike(), new StrikeScore(), new StrikeIndexAdjustment()) { }

        private StrikeFrame(IFrameType frameType, ITypeScore typeScore, IIndexAdjustment indexAdjustment)
        {
            _frameType = frameType;
            _typeScore = typeScore;
            _indexAdjustment = indexAdjustment;
        }
        public bool ShouldScore(IPinsDown pinsDown, int pinsIndex) => _frameType.IsType(pinsDown, pinsIndex);
        public int Score(IPinsDown pinsDown, int pinsIndex) => _typeScore.Score(pinsDown, pinsIndex);
        public int Adjustment() => _indexAdjustment.Adjustment();
    }

    public interface IFrame
    {
        bool ShouldScore(IPinsDown pinsDown, int pinsIndex);
        int Score(IPinsDown pinsDown, int pinsIndex);
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
            int pinsIndex = 0;
            IFrameState frameState = new FrameState(_pinsDown, 0, 0);
            for (int frame = 0; frame < 10; frame++)
            {
                frameState = frameState.Score(new StrikeFrame());
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