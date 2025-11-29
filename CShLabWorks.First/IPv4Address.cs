namespace CShLabWorks.First;

public readonly struct IPv4Address(uint raw_)
    : IEquatable<IPv4Address>, IComparable<IPv4Address>
{
    public readonly uint Raw = raw_;

    public bool Equals(IPv4Address other) => Raw == other.Raw;

    public override bool Equals(object? o) => o is IPv4Address a && Equals(a);

    public override int GetHashCode() => Raw.GetHashCode();

    public override string ToString()
        => string.Join(".", BitConverter.GetBytes(Raw).Reverse());

    public int CompareTo(IPv4Address other) => Raw.CompareTo(other.Raw);

    public static bool operator== (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) == 0;

    public static bool operator!= (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) != 0;

    public static bool operator>= (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) >= 0;

    public static bool operator<= (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) <= 0;

    public static bool operator> (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) > 0;

    public static bool operator< (IPv4Address o1, IPv4Address o2) => o1.CompareTo(o2) < 0;


    public static IPv4Address FromByte(byte o1, byte o2, byte o3, byte o4)
        => new (BitConverter.ToUInt32([o4, o3, o2, o1]));

    public static IPv4Address FromUInt32(uint raw)
        => new (raw);

    public static IPv4Address Parse(string address)
    {
        ArgumentNullException.ThrowIfNull(address);

        var octParts = address.Split(".");
        if (octParts.Length != 4)
            throw new FormatException();

        var octs = new byte[4];
        for (int i = 0; i<4; i++)
            octs[i] = byte.Parse(octParts[3-i]);

        return new (BitConverter.ToUInt32(octs));
    }


    public static bool TryParse(string address, out IPv4Address value)
    {

        if (string.IsNullOrEmpty(address))
        {
            value = new ();
            return false;
        }

        var octParts = address.Split(".");
        if (octParts.Length != 4)
        {
            value = new ();
            return false;
        }

        var octs = new byte[4];
        for (int i = 0; i<4; i++)
        {
            if (!byte.TryParse(octParts[3-i], out octs[i]))
            {
                value = new();
                return false;
            }
        }

        value = new (BitConverter.ToUInt32(octs));

        return true;
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
