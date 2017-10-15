namespace BowlingKataMicroObjectsRefactor
{


    public class Frame : IFrame
    {
        private readonly ITypeScore _typeScore;
        private readonly IIndexAdjustment _indexAdjustment;

        public Frame(ITypeScore typeScore, IIndexAdjustment indexAdjustment)
        {
            _typeScore = typeScore;
            _indexAdjustment = indexAdjustment;
        }
        public int Score(IPinsDown pinsDown, int pinsIndex) => _typeScore.Score(pinsDown, pinsIndex);
        public int Adjustment() => _indexAdjustment.Adjustment();
    }

    public interface IFrame
    {
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
            IFrameState frameState = new FrameState(_pinsDown, 0, 0);
            for (int frame = 0; frame < 10; frame++)
            {
                frameState = frameState.ScoreFrame();
            }
            return frameState.Score();
        }
    }
}