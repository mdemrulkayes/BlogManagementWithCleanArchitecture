﻿using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.Delete;
public sealed record DeletePostCommand(long PostId) : ICommand<Result<bool>>;
