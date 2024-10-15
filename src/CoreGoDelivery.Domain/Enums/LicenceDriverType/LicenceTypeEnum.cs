namespace CoreGoDelivery.Domain.Enums.LicenceDriverType
{
    [Flags]
    public enum LicenceTypeEnum
    {
        None = 0,
        A = 1 << 0,
        AB = A | 1 << 1,
        AC = A | 1 << 2,
        AD = A | 1 << 3,
        AE = A | 1 << 4,

        ABC = A | 1 << 1 | 1 << 2,
        ABD = A | 1 << 1 | 1 << 3,
        ABE = A | 1 << 1 | 1 << 4,

        ACD = A | 1 << 2 | 1 << 3,
        ACE = A | 1 << 2 | 1 << 4,

        ADE = A | 1 << 3 | 1 << 4,

        ABCD = A | 1 << 1 | 1 << 2 | 1 << 3,
        ABCE = A | 1 << 1 | 1 << 2 | 1 << 4,
        ACDE = A | 1 << 2 | 1 << 3 | 1 << 4,
        ABCDE = A | 1 << 1 | 1 << 2 | 1 << 3 | 1 << 4
    }
}
