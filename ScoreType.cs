namespace BowlingKataMicroObjectsRefactor
{
    public interface IScoreType
    {
        bool IsType(IPinsDown pinsDown, int pinsIndex);
    }
    public class IsStrike : IScoreType
    {
        public bool IsType(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) == 10;
    }
    public class IsSpare : IScoreType
    {
        public bool IsType(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) + pinsDown.PinsDownAt(pinsIndex + 1) == 10;
    }
    public class IsDefault : IScoreType
    {
        public bool IsType(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) + pinsDown.PinsDownAt(pinsIndex + 1) < 10;
    }
}