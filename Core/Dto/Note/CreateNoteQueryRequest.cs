namespace Core.Dto.Note;

public class CreateNoteQueryRequest
{
    public int DeckId { get; set; }
    public int NoteTypeId { get; set; }
}