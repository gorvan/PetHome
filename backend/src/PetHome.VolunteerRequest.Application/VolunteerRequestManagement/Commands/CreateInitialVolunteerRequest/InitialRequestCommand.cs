﻿using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Commands.CreateInitialVolunteerRequest;
public record InitialRequestCommand(
    Guid RequestId,
    Guid UserId,
    FullNameDto FullName,
    string Email,
    string Description,
    string Phone,
    RequestStatus Status,
    DateTime CreateAt) : ICommand;

