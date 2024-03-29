﻿using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Tag.Update;

public sealed record UpdateTagCommand(long TagId, string Name, string Description) : ICommand<Result<TagResponse>>;
