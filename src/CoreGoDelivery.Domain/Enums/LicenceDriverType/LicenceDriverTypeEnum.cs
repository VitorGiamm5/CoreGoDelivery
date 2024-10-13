namespace CoreGoDelivery.Domain.Enums.LicenceDriverType
{
    [Flags]
    public enum LicenceDriverTypeEnum
    {
        None = 0,         // Sem habilitação
        A = 1 << 0,       // Habilitação para moto
        AB = A | 1 << 1,  // Moto e carro
        AC = A | 1 << 2,  // Moto e caminhão
        AD = A | 1 << 3,  // Moto e ônibus
        AE = A | 1 << 4,  // Moto e reboque pesado

        ABC = A | 1 << 1 | 1 << 2,  // Moto, carro e caminhão
        ABD = A | 1 << 1 | 1 << 3,  // Moto, carro e ônibus
        ABE = A | 1 << 1 | 1 << 4,  // Moto, carro e reboque pesado

        ACD = A | 1 << 2 | 1 << 3,  // Moto, caminhão e ônibus
        ACE = A | 1 << 2 | 1 << 4,  // Moto, caminhão e reboque pesado

        ADE = A | 1 << 3 | 1 << 4,  // Moto, ônibus e reboque pesado

        ABCD = A | 1 << 1 | 1 << 2 | 1 << 3,  // Moto, carro, caminhão e ônibus
        ABCE = A | 1 << 1 | 1 << 2 | 1 << 4,  // Moto, carro, caminhão e reboque pesado
        ACDE = A | 1 << 2 | 1 << 3 | 1 << 4,  // Moto, caminhão, ônibus e reboque pesado
        ABCDE = A | 1 << 1 | 1 << 2 | 1 << 3 | 1 << 4  // Moto, carro, caminhão, ônibus e reboque pesado
    }
}
