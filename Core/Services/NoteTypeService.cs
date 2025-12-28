using Core.Data;
using Core.Dto.Common;
using Core.Dto.NoteType;
using Core.Model;
using Core.Services.Helper;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class NoteTypeService(DataContext context, IHtmlService htmlService, ICssService cssService): BaseService, INoteTypeService
{
    
    public async Task<PaginationResult<NoteType>> Get(string creatorId, PaginationRequest<int> request)
    {
        IQueryable<NoteType> query = context.NoteTypes.AsNoTracking()
            .Where(x => x.CreatorId == creatorId || x.CreatorId == null);

        return await PaginateAsync(query, request);
    }

    public async Task<NoteType?> Get(string creatorId, int id)
    {
        return await context.NoteTypes.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && (x.CreatorId == creatorId || x.CreatorId == null));
    }
    
    public async Task<int> Update(int id, string creatorId, UpdateNoteTypeRequest request)
    {
        NoteTypeRequest cleanupRequest = await Cleanup(request);
        if (await DoesNoteNameExist(cleanupRequest.Name)) return 0;
        
        NoteType? noteType = await context.NoteTypes
            .Include(x => x.Templates)
            .FirstOrDefaultAsync(x => x.Id == id && x.CreatorId == creatorId);

        if (noteType == null) return 0;

        // Scalar updates
        noteType.Name = request.Name;
        noteType.CssStyle = request.CssStyle;

        // UPSERT Logic for the Collection
        foreach (NoteTypeTemplate reqTemplate in request.Templates)
        {
            // If ID > 0, it likely exists
            NoteTypeTemplate? existing = noteType.Templates.FirstOrDefault(t => t.Id == reqTemplate.Id && reqTemplate.Id != 0);

            if (existing != null) 
            {
                // Edit existing
                existing.Front = reqTemplate.Front;
                existing.Back = reqTemplate.Back;
            }
            else 
            {
                // Create new
                noteType.Templates.Add(new NoteTypeTemplate {
                    Front = reqTemplate.Front,
                    Back = reqTemplate.Back
                });
            }
        }

        return await context.SaveChangesAsync();
    }

    public async Task<NoteType?> Create(string creatorId, CreateNoteTypeRequest request)
    {
        NoteTypeRequest cleanupRequest = await Cleanup(request);
        if (await DoesNoteNameExist(cleanupRequest.Name)) return null;

        NoteType deck = new()
        {
            CreatorId = creatorId,
            Name = cleanupRequest.Name,
            Templates = cleanupRequest.Templates,
            CssStyle = cleanupRequest.CssStyle,
        };
        await context.NoteTypes.AddAsync(deck);
        await context.SaveChangesAsync();
        return deck;
    }

    public async Task<int> Delete(int id, string creatorId)
    {
        return await context.NoteTypes
            .Where(x => x.CreatorId == creatorId && x.Id == id)
            .ExecuteDeleteAsync();
    }

    
    private async Task<bool> DoesNoteNameExist(string name) =>
        await context.NoteTypes.AnyAsync(x => x.Name == name && x.CreatorId == null);
    
    private async Task<NoteTypeRequest> Cleanup(NoteTypeRequest request)
    {
        // Parsing all the HTML 
        IEnumerable<Task<(string? frontHtml, string? backHtml)>> tasks = request.Templates.Select(async template =>
            (await htmlService.Parse(template.Back), await htmlService.Parse(template.Front)));
        (string? frontHtml, string? backHtml)[] results = await Task.WhenAll(tasks);
        request.Templates = results
            .Where(r => r is { frontHtml: not null, backHtml: not null })
            .Select(r => new NoteTypeTemplate { Back = r.backHtml!, Front = r.frontHtml! })
            .ToList();
        request.CssStyle = await cssService.Parse(request.CssStyle);
        return request;
    }
}