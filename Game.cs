namespace BowlingKataMicroObjectsRefactor
{
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