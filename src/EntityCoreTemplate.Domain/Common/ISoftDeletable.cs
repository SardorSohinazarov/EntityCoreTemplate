﻿namespace EntityCoreTemplate.Domain.Common
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
