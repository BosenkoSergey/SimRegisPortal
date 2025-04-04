﻿using SimRegisPortal.Domain.Entities.Base;
using SimRegisPortal.Domain.Enums;

namespace SimRegisPortal.Domain.Entities
{
    public class UserProjectPermission : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public UserProjectPermissionType PermissionType { get; set; }

        public User User { get; set; } = default!;
        public Project Project { get; set; } = default!;
    }
}
