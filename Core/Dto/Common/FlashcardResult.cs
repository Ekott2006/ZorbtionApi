using Core.Model.Helper;

namespace Core.Dto.Common;

public class FlashcardResult
{
    public int Interval { get; set; }
    public int Repetitions { get; set; }
    public double EaseFactor { get; set; }
    public CardState State { get; set; }
    public int LearningStepIndex { get; set; }
}