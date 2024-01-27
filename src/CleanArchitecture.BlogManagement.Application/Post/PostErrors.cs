﻿using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Application.Post;
internal struct PostErrors
{
    public static Error PostCanNotCreated = Error.Failure("Post.Create", "Post can not created");
    public static Error NotFound = Error.NotFound("Post.GetById", "Post details not found");

    internal struct CommentErrors
    {
        public static Error CommentNotFound = Error.NotFound("Comment.Update", "Comment details not found");
    }
}