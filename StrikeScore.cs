namespace BowlingKataMicroObjectsRefactor
{
    internal interface IStrikeScore
    {
        int Score(IPinsDown pinsDown, int pinsIndex);
    }

    public class StrikeScore : IStrikeScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => 10 + pinsDown.PinsDownAt(pinsIndex + 1) + pinsDown.PinsDownAt(pinsIndex + 2);
    }
    public class SpareScore : IStrikeScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => 10 + pinsDown.PinsDownAt(pinsIndex + 2);
    }
    public class DefaultScore : IStrikeScore
    {
        public int Score(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) + pinsDown.PinsDownAt(pinsIndex + 1);
    }
}