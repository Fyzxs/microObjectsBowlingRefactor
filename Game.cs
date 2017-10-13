namespace BowlingKataMicroObjectsRefactor
{
    public class FrameState : IFrameState
    {
        private readonly int _score;
        private readonly int _pinsIndex;

        public FrameState(int score, int pinsIndex)
        {
            _score = score;
            _pinsIndex = pinsIndex;
        }

        public IFrameState Score(IPinsDown pinsDown, IFrameScore frameScore, IIndexAdjustment indexAdjustment)
            => new FrameState(Score(pinsDown, frameScore), PinsIndex(indexAdjustment));

        public int Score() => _score;

        private int PinsIndex(IIndexAdjustment indexAdjustment) => _pinsIndex + indexAdjustment.Adjustment();
        private int Score(IPinsDown pinsDown, IFrameScore frameScore) => _score + frameScore.Score(pinsDown, _pinsIndex);
    }

    public interface IFrameState
    {
        IFrameState Score(IPinsDown pinsDown, IFrameScore frameScore, IIndexAdjustment indexAdjustment);
        int Score();
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
            return score + frameState.Score();
        }
    }
}