using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingKataMicroObjectsRefactor
{
    [TestClass]
    public class BowlingGameTest
    {
        private Game _g;

        [TestInitialize]
        public void Initialize()
        {
            _g = new Game();
        }
        [TestMethod]
        public void GutterGame()
        {
            RollMany(20, 0);

            Assert.AreEqual(0, _g.Score());
        }

        [TestMethod]
        public void AllSinglePin()
        {
            RollMany(20, 1);
            Assert.AreEqual(20, _g.Score());
        }

        [TestMethod]
        public void SingleSpare()
        {
            RollSpare();
            _g.Roll(4);
            RollMany(17, 0);

            Assert.AreEqual(18, _g.Score());
        }

        [TestMethod]
        public void SingleStrike()
        {
            _g.Roll(10);
            _g.Roll(3);
            _g.Roll(4);
            RollMany(16, 0);

            Assert.AreEqual(24, _g.Score());
        }

        [TestMethod]
        public void PerfectGame()
        {
            RollMany(21, 10);

            Assert.AreEqual(300, _g.Score());
        }

        private void RollSpare()
        {
            _g.Roll(5);
            _g.Roll(5);
        }

        private void RollMany(int rolls, int pins)
        {
            for (int i = 0; i < rolls; i++)
            {
                _g.Roll(pins);
            }
        }
    }
}









/*
namespace BowlingKataMicroObjectsRefactor
{
    public class Game
    {
        private readonly int[] _rolls = new int[21];
        private int _ptr;

        public void Roll(int pins) => _rolls[_ptr++] = pins;

        public int Score()
        {
            int score = 0;
            int pinsIndex = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(pinsIndex))
                {
                    score += StrikeScore(pinsIndex);
                    pinsIndex += 1;
                    continue;
                }
                if (IsSpare(pinsIndex))
                {
                    score += SpareScore(pinsIndex);
                }
                else
                {
                    score += RegularScore(pinsIndex);
                }
                pinsIndex += 2;
            }
            return score;
        }

        private bool IsStrike(int pinsIndex) => _rolls[pinsIndex] == 10;
        private bool IsSpare(int pinsIndex) => _rolls[pinsIndex] + _rolls[pinsIndex + 1] == 10;
        private int RegularScore(int pinsIndex) => _rolls[pinsIndex] + _rolls[pinsIndex + 1];
        private int SpareScore(int pinsIndex) => 10 + _rolls[pinsIndex + 2];
        private int StrikeScore(int pinsIndex) => 10 + _rolls[pinsIndex + 1] + _rolls[pinsIndex + 2];
    }
}
*/
