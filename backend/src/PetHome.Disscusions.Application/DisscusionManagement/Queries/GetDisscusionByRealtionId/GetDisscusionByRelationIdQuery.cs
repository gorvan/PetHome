using PetHome.Shared.Core.Abstractions;

namespace PetHome.Disscusions.Application.DisscusionManagement.Queries.GetDisscusionByRealtionId;
public record GetDisscusionByRelationIdQuery(Guid RelationId) : IQuery;
