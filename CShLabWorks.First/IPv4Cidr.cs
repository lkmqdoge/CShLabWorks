namespace CShLabWorks.First;

public readonly struct Ipv4Cidr
{
    public readonly int PrefixLength { get; }

    public readonly IPv4Address Network { get; }

    public readonly IPv4Address Mask { get; }

    public readonly IPv4Address Broadcast
        => new (Network.Raw | ~Mask.Raw);

    public readonly uint HostCount
    {
        get
        {
            if (PrefixLength == 32)
                return 1;

            if (PrefixLength == 31)
                return 2;

            return 1u << (32 - PrefixLength);
        }
    }

    public Ipv4Cidr(IPv4Address addres, int prefixLength)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(prefixLength, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(prefixLength, 32);

        PrefixLength = prefixLength;

        Mask = new IPv4Address(uint.MaxValue << (32 - PrefixLength));
        Network = new IPv4Address(addres.Raw & Mask.Raw);
    }

    public bool Contains(IPv4Address address)
        => (address.Raw & Mask.Raw) == Network.Raw;
}
