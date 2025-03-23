﻿using EntityCoreTemplate.Domain.Common;

namespace EntityCoreTemplate.Domain.Entities
{
    public class Book : Auditable
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
