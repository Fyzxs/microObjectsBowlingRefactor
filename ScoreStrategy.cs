namespace BowlingKataMicroObjectsRefactor
{
    public interface IScoreStrategy
    {
        IFrameUpdate Select(IPinsDown pinsDown, int pinsIndex);
    }
    public class ScoreStrategy : IScoreStrategy
    {
        private readonly IScoreStrategy _scoreStrategy;

        public ScoreStrategy() : this(new StrikeStrategy(new SpareStrategy(new DefaultStrategy()))) { }

        private ScoreStrategy(IScoreStrategy scoreStrategy) => _scoreStrategy = scoreStrategy;

        public IFrameUpdate Select(IPinsDown pinsDown, int pinsIndex) => _scoreStrategy.Select(pinsDown, pinsIndex);
    }

    public class StrikeStrategy : IScoreStrategy
    {
        private readonly IScoreStrategy _next;
        private readonly IFrameType _frameType;

        public StrikeStrategy(IScoreStrategy next) : this(next, new IsStrike())
        { }

        private StrikeStrategy(IScoreStrategy next, IFrameType frameType)
        {
            _next = next;
            _frameType = frameType;
        }

        public IFrameUpdate Select(IPinsDown pinsDown, int pinsIndex)
        {
            if (!_frameType.IsType(pinsDown, pinsIndex)) return _next.Select(pinsDown, pinsIndex);
            return new FrameUpdate(new StrikeScore(), new StrikeIndexAdjustment());
        }
    }
    public class SpareStrategy : IScoreStrategy
    {
        private readonly IScoreStrategy _next;
        private readonly IFrameType _frameType;

        public SpareStrategy(IScoreStrategy next) : this(next, new IsSpare())
        { }

        private SpareStrategy(IScoreStrategy next, IFrameType frameType)
        {
            _next = next;
            _frameType = frameType;
        }

        public IFrameUpdate Select(IPinsDown pinsDown, int pinsIndex)
        {
            if (!_frameType.IsType(pinsDown, pinsIndex)) return _next.Select(pinsDown, pinsIndex);
            return new FrameUpdate(new SpareScore(), new SpareIndexAdjustment());
        }
    }
    public class DefaultStrategy : IScoreStrategy
    {
        public IFrameUpdate Select(IPinsDown pinsDown, int pinsIndex) => new FrameUpdate(new DefaultScore(), new DefaultIndexAdjustment());
    }
}