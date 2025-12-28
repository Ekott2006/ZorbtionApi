using Bogus;

namespace Core.Model.Faker;

public sealed class NoteTypeTemplateFaker : Faker<NoteTypeTemplate>
{
    public NoteTypeTemplateFaker(int noteTypeId)
    {
        RuleFor(ntt => ntt.Front, f => $"{{{{{f.Lorem.Word()}}}}}");
        RuleFor(ntt => ntt.Back, f => $"{{{{{f.Lorem.Word()}}}}}<br>{{{{{f.Lorem.Word()}}}}}");
        RuleFor(ntt => ntt.NoteTypeId, _ => noteTypeId);
    }
}