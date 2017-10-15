namespace BowlingKataMicroObjectsRefactor
{
    public interface IFrameUpdate
    {
        int Score(IPinsDown pinsDown, int pinsIndex);
        int Adjustment();
    }
    public class FrameUpdate : IFrameUpdate
    {
        private readonly ITypeScore _typeScore;
        private readonly IIndexAdjustment _indexAdjustment;

        public FrameUpdate(ITypeScore typeScore, IIndexAdjustment indexAdjustment)
        {
            _typeScore = typeScore;
            _indexAdjustment = indexAdjustment;
        }
        public int Score(IPinsDown pinsDown, int pinsIndex) => _typeScore.Score(pinsDown, pinsIndex);
        public int Adjustment() => _indexAdjustment.Adjustment();
    }
}