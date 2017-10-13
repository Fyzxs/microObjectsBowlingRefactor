namespace BowlingKataMicroObjectsRefactor
{
    public interface IPinsDown
    {
        void Roll(int pins);
        int PinsDownAt(int pinsIndex);
    }
    public class PinsDown : IPinsDown
    {
        private readonly int[] _rolls = new int[21];
        private int _ptr;
        public void Roll(int pins) => _rolls[_ptr++] = pins;
        public int PinsDownAt(int pinsIndex) => _rolls[pinsIndex];
    }
}