﻿namespace CoreGoDelivery.Domain.Enums.LicenceDriverType
{
    [Flags]
    public enum LicenceTypeEnum
    {
        None = 0,
        A = 1 << 0,
        AB = A | 1 << 1
    }
}
