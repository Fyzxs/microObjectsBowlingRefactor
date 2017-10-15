namespace BowlingKataMicroObjectsRefactor
{
    public interface IFrameState
    {
        int Score();
        IFrameState ScoreFrame();
    }

    public class FrameState : IFrameState
    {
        private readonly IPinsDown _pinsDown;
        private readonly int _score;
        private readonly int _pinsIndex;
        private readonly IScoreStrategy _scoreStrategy;

        public FrameState(IPinsDown pinsDown, int score, int pinsIndex)
            : this(pinsDown, score, pinsIndex, new ScoreStrategy())
        { }

        private FrameState(IPinsDown pinsDown, int score, int pinsIndex, IScoreStrategy scoreStrategy)
        {
            _pinsDown = pinsDown;
            _score = score;
            _pinsIndex = pinsIndex;
            _scoreStrategy = scoreStrategy;
        }

        public IFrameState ScoreFrame() => UpdatedFrameSate(_scoreStrategy.Select(_pinsDown, _pinsIndex));

        public int Score() => _score;

        private FrameState UpdatedFrameSate(IFrame frame) => new FrameState(_pinsDown, UpdatedScore(frame), UpdatedIndex(frame));
        private int UpdatedIndex(IFrame frameType) => _pinsIndex + frameType.Adjustment();
        private int UpdatedScore(IFrame frameType) => _score + frameType.Score(_pinsDown, _pinsIndex);
    }
}