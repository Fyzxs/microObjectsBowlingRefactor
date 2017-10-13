namespace BowlingKataMicroObjectsRefactor
{
    public interface ITypeScore
    {
        int Score(IPinsDown pinsDown, int pinsIndex);
    }

    public class StrikeScore : ITypeScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => 10 + pinsDown.PinsDownAt(pinsIndex + 1) + pinsDown.PinsDownAt(pinsIndex + 2);
    }
    public class SpareScore : ITypeScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => 10 + pinsDown.PinsDownAt(pinsIndex + 2);
    }
    public class DefaultScore : ITypeScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) + pinsDown.PinsDownAt(pinsIndex + 1);
    }
}