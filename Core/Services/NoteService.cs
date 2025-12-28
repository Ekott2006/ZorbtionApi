using Core.Data;
using Core.Dto.Common;
using Core.Dto.Note;
using Core.Model;
using Core.Model.Helper;
using Core.Services.Helper;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class NoteService(DataContext context, ITemplateService templateService) : BaseService, INoteService
{
    public async Task<PaginationResult<Note>> Get(string creatorId, int deckId, PaginationRequest<int> request)
    {
        IQueryable<Note> query = context.Notes.AsNoTracking()
            .Where(x => x.CreatorId == creatorId && x.DeckId == deckId);
        return await PaginateAsync(query, request);
    }
    
    public async Task<Note?> Get(string creatorId, int id)
    {
        return await context.Notes.AsNoTracking()
            .FirstOrDefaultAsync(x => x.CreatorId == creatorId && x.Id == id);
    }


    public async Task<int> Update(string creatorId, int id, UpdateNoteRequest request)
    {
        Note? note = await context.Notes
            .Include(x => x.NoteType)
            .ThenInclude(noteType => noteType.Templates)
            .FirstOrDefaultAsync(x => x.CreatorId == creatorId && x.Id == id);
        if (note == null) return 0;

        List<string> fields = templateService.GetAllFields(GetField(note.NoteType.Templates));
        note.Data = Cleanup(fields, request.Data);
        note.Tags = request.Tags;
        await context.SaveChangesAsync();
        return 1;
    }

    public async Task<bool> Create(string creatorId, int deckId, int noteTypeId, List<CreateNoteRequest> request)
    {
        bool doesDeckExist = await context.Decks.AnyAsync(x => x.Id == deckId && x.CreatorId == creatorId);
        if (!doesDeckExist) return false;
        var noteType = await context.NoteTypes
            .Include(x => x.Templates)
            .Select(x => new { x.Id, x.CreatorId, x.Templates })
            .FirstOrDefaultAsync(x => x.Id == noteTypeId && (x.CreatorId == creatorId || x.CreatorId == null));
        if (noteType == null) return false;

        List<string> fields = templateService.GetAllFields(GetField(noteType.Templates));
        (int Interval, int Repetitions, double EaseFactor, DateTime DueDate, int StepIndex) flashcardData =
            Card.FlashcardData();
        IEnumerable<Note> notes = request.Select(x => new Note
            {
                DeckId = deckId,
                NoteTypeId = noteTypeId,
                CreatorId = creatorId,
                Data = Cleanup(fields, x.Data),
                Tags = x.Tags,
                Cards = noteType.Templates.Select(template => new Card
                {
                    State = CardState.New,
                    Interval = flashcardData.Interval,
                    Repetitions = flashcardData.Repetitions,
                    EaseFactor = flashcardData.EaseFactor,
                    DueDate = DateTime.UtcNow,
                    StepIndex = flashcardData.StepIndex,
                    NoteTypeTemplateId = template.Id,
                }).ToList(),
            }
        );
        await context.Notes.AddRangeAsync(notes);
        await context.SaveChangesAsync();
        return true;
    }

    private IEnumerable<string> GetField
        (IEnumerable<NoteTypeTemplate> template) => template.Select(x => $"{x.Back}{x.Front}");

    private static Dictionary<string, string> Cleanup(List<string> fields,
        Dictionary<string, string> data)
    {
        return fields.ToDictionary(
            key => key,
            key => data.GetValueOrDefault(key, string.Empty)
        );
    }

    public async Task<int> Delete(int id, string creatorId)
    {
        return await context.Notes
            .Where(x => x.CreatorId == creatorId && x.Id == id)
            .ExecuteDeleteAsync();
    }
}