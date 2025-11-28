namespace CShLabWorks.First;

public readonly struct Ipv4Cidr
{
  public readonly int prefixLength; 

  public readonly IPv4Address Network;

  public readonly IPv4Address Mask { get; }

  public readonly IPv4Address Broadcast { get; }

  public readonly uint HostCount { get; }

  public Ipv4Cidr(IPv4Address addres, int prefixLength)
  {
    throw new NotImplementedException();
  }

  public bool Contains(IPv4Address address)
  {
    throw new NotImplementedException();
  }
}
