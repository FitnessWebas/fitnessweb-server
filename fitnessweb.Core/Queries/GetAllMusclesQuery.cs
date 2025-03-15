﻿using fitnessweb.Domain.Dtos;

namespace fitnessweb.Core.Queries;

using fitnessweb.Domain.Entities;
using MediatR;

public class GetAllMusclesQuery : IRequest<List<MuscleInfoDto>>;