namespace SimRegisPortal.Core.Enums;

public enum UserProjectPermissionType
{
    ProjectRead = 1,
    ProjectWrite = 2,

    OwnActivityReadWrite = 100,

    AllActivityRead = 110,
    AllActivityWrite = 111,
    AllActivityApprove = 112
}
