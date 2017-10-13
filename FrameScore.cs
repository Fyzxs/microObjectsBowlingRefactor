namespace BowlingKataMicroObjectsRefactor
{
    public interface IFrameScore
    {
        int Score(IPinsDown pinsDown, int pinsIndex);
    }

    public class StrikeScore : IFrameScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => 10 + pinsDown.PinsDownAt(pinsIndex + 1) + pinsDown.PinsDownAt(pinsIndex + 2);
    }
    public class SpareScore : IFrameScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => 10 + pinsDown.PinsDownAt(pinsIndex + 2);
    }
    public class DefaultScore : IFrameScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) + pinsDown.PinsDownAt(pinsIndex + 1);
    }
}