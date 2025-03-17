using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Domain.ValueObjects;

public record Comment
{
    private Comment() { }
    private Comment(string comment)
    {
        Value = comment;
    }

    public string Value { get; } = default!;

    public static Result<Comment> Create(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
        {
            return Errors.General.ValueIsRequeired("Comment");
        }

        return new Comment(comment);
    }
}