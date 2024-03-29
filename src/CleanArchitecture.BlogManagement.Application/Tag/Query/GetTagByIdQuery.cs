﻿using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Tag.Query;

public sealed record GetTagByIdQuery(long TagId) : ICommand<Result<TagResponse>>;
