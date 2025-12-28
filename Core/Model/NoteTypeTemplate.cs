using Core.Model.Interface;

namespace Core.Model;

public class NoteTypeTemplate: BaseModel, IPagination<int>
{
    public int Id { get; set; }
    public int NoteTypeId { get; set; }
    public NoteType NoteType { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
}