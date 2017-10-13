﻿namespace BowlingKataMicroObjectsRefactor
{
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

    internal interface IStrikeScore { }

    public class Game
    {
        private readonly IPinsDown _pinsDown;

        public Game() : this(new PinsDown()) { }
        public Game(IPinsDown pinsDown) => _pinsDown = pinsDown;

        public void Roll(int pins) => _pinsDown.Roll(pins);

        public int Score()
        {
            int score = 0;
            int pinsIndex = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(pinsIndex))
                {
                    score += new StrikeScore().Score(_pinsDown, pinsIndex);
                    pinsIndex += new StrikeIndexAdjustment().Adjustment();
                    continue;
                }
                if (IsSpare(pinsIndex))
                {
                    score += new SpareScore().Score(_pinsDown, pinsIndex);
                    pinsIndex += new SpareIndexAdjustment().Adjustment();
                }
                else
                {
                    score += new DefaultScore().Score(_pinsDown, pinsIndex);
                    pinsIndex += new DefaultIndexAdjustment().Adjustment();
                }
            }
            return score;
        }

        private bool IsStrike(int pinsIndex) => _pinsDown.PinsDownAt(pinsIndex) == 10;
        private bool IsSpare(int pinsIndex) => _pinsDown.PinsDownAt(pinsIndex) + _pinsDown.PinsDownAt(pinsIndex + 1) == 10;
        private int RegularScore(int pinsIndex) => _pinsDown.PinsDownAt(pinsIndex) + _pinsDown.PinsDownAt(pinsIndex + 1);
        private int SpareScore(int pinsIndex) => 10 + _pinsDown.PinsDownAt(pinsIndex + 2);
        private int StrikeScore(int pinsIndex) => 10 + _pinsDown.PinsDownAt(pinsIndex + 1) + _pinsDown.PinsDownAt(pinsIndex + 2);
    }
}