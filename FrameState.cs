using System.Linq;

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
        private readonly IFrame[] _frames;

        public FrameState(IPinsDown pinsDown, int score, int pinsIndex)
            : this(pinsDown, score, pinsIndex, new StrikeFrame(), new SpareFrame(), new DefaultFrame())
        { }

        private FrameState(IPinsDown pinsDown, int score, int pinsIndex, params IFrame[] frames)
        {
            _pinsDown = pinsDown;
            _score = score;
            _pinsIndex = pinsIndex;
            _frames = frames;
        }

        public IFrameState ScoreFrame()
        {
            for (int idx = 0; idx < _frames.Length - 1; idx++)
            {
                if (_frames[idx].ShouldScore(_pinsDown, _pinsIndex)) return UpdatedFrameSate(_frames[idx]);
            }
            return UpdatedFrameSate(_frames.Last());
        }

        public int Score() => _score;

        private FrameState UpdatedFrameSate(IFrame frame) => new FrameState(_pinsDown, UpdatedScore(frame), UpdatedIndex(frame));
        private int UpdatedIndex(IFrame frameType) => _pinsIndex + frameType.Adjustment();
        private int UpdatedScore(IFrame frameType) => _score + frameType.Score(_pinsDown, _pinsIndex);
    }
}