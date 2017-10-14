namespace BowlingKataMicroObjectsRefactor
{
    public interface IFrameState
    {
        IFrameState Score(IPinsDown pinsDown, IFrameType frameType, ITypeScore typeScore, IIndexAdjustment indexAdjustment);
        int Score();
        IFrameState Score(IFrame frame);
    }

    public class FrameState : IFrameState
    {
        private readonly IPinsDown _pinsDown;
        private readonly int _score;
        private readonly int _pinsIndex;

        public FrameState(IPinsDown pinsDown, int score, int pinsIndex)
        {
            _pinsDown = pinsDown;
            _score = score;
            _pinsIndex = pinsIndex;
        }

        public IFrameState Score(IPinsDown pinsDown, IFrameType frameType, ITypeScore typeScore, IIndexAdjustment indexAdjustment)
        {
            if (frameType.IsType(pinsDown, _pinsIndex)) return new FrameState(_pinsDown, Score(pinsDown, typeScore), PinsIndex(indexAdjustment));
            return this;
        }

        public int Score() => _score;
        public IFrameState Score(IFrame frame) => frame.ShouldScore(_pinsDown, _pinsIndex) ? UpdatedFrameSate(frame) : this;

        private int PinsIndex(IIndexAdjustment indexAdjustment) => _pinsIndex + indexAdjustment.Adjustment();
        private int Score(IPinsDown pinsDown, ITypeScore typeScore) => _score + typeScore.Score(pinsDown, _pinsIndex);

        private FrameState UpdatedFrameSate(IFrame frame) => new FrameState(_pinsDown, UpdatedScore(frame), UpdatedIndex(frame));
        private int UpdatedIndex(IFrame frameType) => _pinsIndex + frameType.Adjustment();
        private int UpdatedScore(IFrame frameType) => _score + frameType.Score(_pinsDown, _pinsIndex);
    }
}