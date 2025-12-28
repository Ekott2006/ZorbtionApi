using Core.Model.Helper;
using Core.Model.Interface;

namespace Core.Model;

public class Card : BaseModel, IPagination<int>
{
    public int Id { get; set; }

    // TODO: Optimization
    public int NoteId { get; set; }
    public Note Note { get; set; }
    public int NoteTypeTemplateId { get; set; }
    public NoteTypeTemplate Template { get; set; }

    public CardState State { get; set; }
    public int Interval { get; set; } 
    public int Repetitions { get; set; }
    public double EaseFactor { get; set; }
    private const double DefaultEaseFactor = 2.5;
    public DateTime DueDate { get; set; }
    public int StepIndex { get; set; }
    
    public static (int Interval, int Repetitions, double EaseFactor, DateTime DueDate, int StepIndex)  FlashcardData() => (Interval: 0, Repetitions: 0, EaseFactor: DefaultEaseFactor, DueDate: DateTime.UtcNow, StepIndex: 0);
}
