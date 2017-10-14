namespace BowlingKataMicroObjectsRefactor
{
    public interface IFrameType
    {
        bool IsType(IPinsDown pinsDown, int pinsIndex);
    }
    public class IsStrike : IFrameType
    {
        public bool IsType(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) == 10;
    }
    public class IsSpare : IFrameType
    {
        public bool IsType(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) + pinsDown.PinsDownAt(pinsIndex + 1) == 10;
    }
    public class IsDefault : IFrameType
    {
        public bool IsType(IPinsDown pinsDown, int pinsIndex) => pinsDown.PinsDownAt(pinsIndex) + pinsDown.PinsDownAt(pinsIndex + 1) < 10;
    }
}