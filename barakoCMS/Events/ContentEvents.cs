using System.Text.Json.Serialization;

namespace barakoCMS.Events;

[method: JsonConstructor]
public record ContentCreated(
    Guid Id,
    string ContentType,
    Dictionary<string, object> Data,
    Models.ContentStatus Status,
    Guid CreatedBy,
    string? SearchText,
    Models.SensitivityLevel Sensitivity)
{
    [Obsolete("Use the seven-value constructor. Removal planned for the next major version.")]
    public ContentCreated(
        Guid id,
        string contentType,
        Dictionary<string, object> data,
        Models.ContentStatus status,
        Guid createdBy,
        string? searchText)
        : this(id, contentType, data, status, createdBy, searchText, Models.SensitivityLevel.Public)
    {
    }

    [Obsolete("Use the six-value constructor. Removal planned for the next major version.")]
    public ContentCreated(
        Guid id,
        string contentType,
        Dictionary<string, object> data,
        Models.ContentStatus status,
        Guid createdBy)
        : this(id, contentType, data, status, createdBy, null, Models.SensitivityLevel.Public)
    {
    }

    [Obsolete("Use the six-value Deconstruct overload. Removal planned for the next major version.")]
    public void Deconstruct(
        out Guid id,
        out string contentType,
        out Dictionary<string, object> data,
        out Models.ContentStatus status,
        out Guid createdBy) =>
        (id, contentType, data, status, createdBy) =
            (Id, ContentType, Data, Status, CreatedBy);
}

[method: JsonConstructor]
public record ContentUpdated(
    Guid Id,
    Dictionary<string, object> Data,
    Guid UpdatedBy,
    string? SearchText)
{
    [Obsolete("Use the four-value constructor. Removal planned for the next major version.")]
    public ContentUpdated(
        Guid id,
        Dictionary<string, object> data,
        Guid updatedBy)
        : this(id, data, updatedBy, null)
    {
    }

    [Obsolete("Use the four-value Deconstruct overload. Removal planned for the next major version.")]
    public void Deconstruct(
        out Guid id,
        out Dictionary<string, object> data,
        out Guid updatedBy) =>
        (id, data, updatedBy) =
            (Id, Data, UpdatedBy);
}
public record ContentStatusChanged(Guid Id, Models.ContentStatus NewStatus, Guid UpdatedBy);

/// <summary>
/// Publication scheduling changed for a content item.
/// </summary>
/// <remarks>
/// Scheduling used to be written straight to the document with no event, so the audit trail said
/// nothing about who scheduled what, and anything reconstructing state from the stream would lose
/// both dates.
/// </remarks>
[method: JsonConstructor]
public record ContentScheduled(
    Guid Id,
    DateTime? ScheduledPublishAt,
    DateTime? ScheduledUnpublishAt,
    Guid UpdatedBy);

/// <summary>
/// Document-level sensitivity changed for a content item.
/// </summary>
/// <remarks>
/// Sensitivity drives field-level redaction, so state rebuilt without it produces a record that
/// looks correct and is readable by roles that should not see it. That is why it is carried
/// explicitly rather than inferred.
/// </remarks>
[method: JsonConstructor]
public record ContentSensitivityChanged(
    Guid Id,
    Models.SensitivityLevel Sensitivity,
    Guid UpdatedBy);
