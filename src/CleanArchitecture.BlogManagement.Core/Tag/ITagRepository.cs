﻿using CleanArchitecture.BlogManagement.Core.Base;

namespace CleanArchitecture.BlogManagement.Core.Tag;
public interface ITagRepository : IRepository<Tag>
{
    Task<IEnumerable<Tag>> GetAllTags(CancellationToken cancellationToken = default);
}