﻿using SharedKernel;

namespace CleanArchitecture.BlogManagement.Application.Post.DeleteComment;
public sealed record DeleteCommentCommand(long PostId, long CommentId) : ICommand<Result<long>>;
