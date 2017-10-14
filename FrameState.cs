﻿namespace BowlingKataMicroObjectsRefactor
{
    public interface IFrameState
    {
        IFrameState Score(IPinsDown pinsDown, IFrameType frameType, ITypeScore typeScore, IIndexAdjustment indexAdjustment);
        int Score();
        IFrameState Score(IPinsDown pinsDown, IFrame frameType);
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

        public IFrameState Score(IPinsDown pinsDown, IFrameType frameType, ITypeScore typeScore, IIndexAdjustment indexAdjustment)
        {
            if (frameType.IsType(pinsDown, _pinsIndex)) return new FrameState(Score(pinsDown, typeScore), PinsIndex(indexAdjustment));
            return this;
        }

        public int Score() => _score;
        public IFrameState Score(IPinsDown pinsDown, IFrame frameType)
        {
            if (frameType.IsType(pinsDown, _pinsIndex)) return new FrameState(UpdatedScore(pinsDown, frameType), UpdatedIndex(frameType));
            return this;
        }

        private int PinsIndex(IIndexAdjustment indexAdjustment) => _pinsIndex + indexAdjustment.Adjustment();
        private int Score(IPinsDown pinsDown, ITypeScore typeScore) => _score + typeScore.Score(pinsDown, _pinsIndex);
        private int UpdatedIndex(IFrame frameType) => _pinsIndex + frameType.Adjustment();
        private int UpdatedScore(IPinsDown pinsDown, IFrame frameType) => _score + frameType.Score(pinsDown, _pinsIndex);
    }
}