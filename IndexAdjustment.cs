namespace BowlingKataMicroObjectsRefactor
{
    public interface IIndexAdjustment
    {
        int Adjustment();
    }

    public class StrikeIndexAdjustment : IIndexAdjustment
    {
        public int Adjustment() => 1;
    }
    public class SpareIndexAdjustment : IIndexAdjustment
    {
        public int Adjustment() => 2;
    }
    public class DefaultIndexAdjustment : IIndexAdjustment
    {
        public int Adjustment() => 2;
    }

}