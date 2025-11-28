namespace CShLabWorks.First;

public readonly struct IPv4Address(uint raw)
    : IEquatable<IPv4Address>, IComparable<IPv4Address>
{
    public readonly uint Raw = raw;

    public bool Equals(IPv4Address other) => Raw == other.Raw;

    public override bool Equals(object? o) => o is IPv4Address a && Equals(a);

    public override int GetHashCode() => Raw.GetHashCode();

    public int CompareTo(IPv4Address other) => Raw.CompareTo(other.Raw);

    public static bool operator== (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) == 0;

    public static bool operator!= (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) != 0;

    public static bool operator>= (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) >= 0;

    public static bool operator<= (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) <= 0;

    public static bool operator> (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) > 0;

    public static bool operator< (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) < 0;


    public static IPv4Address FromByte(byte o1, byte o2, byte o3, byte o4)
        => new (BitConverter.ToUInt32([o1, o2, o3, o4]));

    public static IPv4Address FromUInt32(uint raw)
        => new (raw);

    public static IPv4Address Parse(string address)
    {
        ArgumentNullException.ThrowIfNull(address);
        var oct = address.Split(".");

        if (oct.Length != 4)
          throw new FormatException();

        // byte.Parse()

        return new ();
    }


    public static bool TryParse(string address, out IPv4Address value)
    {
    }

    public byte this[int index]
    {
        get
        {
            if (index >=0 && index < 4)
                return BitConverter.GetBytes(Raw)[index];
            else
                throw new ArgumentOutOfRangeException(index.ToString());
        }
    }
}
