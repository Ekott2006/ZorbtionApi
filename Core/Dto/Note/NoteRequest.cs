namespace Core.Dto.Note;

public class NoteRequest
{
    public Dictionary<string, string> Data { get; set; }

    // TODO: Ensure it is alphabets only
    public List<string> Tags { get; set; }
}