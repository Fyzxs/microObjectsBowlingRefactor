namespace BowlingKataMicroObjectsRefactor
{
    public interface IFrameState
    {
        IFrameState Score(IPinsDown pinsDown, IScoreType scoreType, ITypeScore typeScore, IIndexAdjustment indexAdjustment);
        int Score();
    }

    public class FrameState : IFrameState
    {
        private readonly int _score;
        private readonly int _pinsIndex;

        public FrameState(int score, int pinsIndex)
        {
            _score = score;
            _pinsIndex = pinsIndex;
        }

        public IFrameState Score(IPinsDown pinsDown, IScoreType scoreType, ITypeScore typeScore, IIndexAdjustment indexAdjustment)
        {
            if (scoreType.IsType(pinsDown, _pinsIndex)) return new FrameState(Score(pinsDown, typeScore), PinsIndex(indexAdjustment));
            return this;
        }

        public int Score() => _score;

        private int PinsIndex(IIndexAdjustment indexAdjustment) => _pinsIndex + indexAdjustment.Adjustment();
        private int Score(IPinsDown pinsDown, ITypeScore typeScore) => _score + typeScore.Score(pinsDown, _pinsIndex);
    }
}