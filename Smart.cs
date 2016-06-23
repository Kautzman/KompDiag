namespace sysinfo
{
  public class Smart
  {
    public bool HasData
    {
      get
      {
        return this.Current != 0 || this.Worst != 0 || (this.Threshold != 0 || this.Data != 0);
      }
    }

    public string Attribute { get; set; }

    public int Current { get; set; }

    public int Worst { get; set; }

    public int Threshold { get; set; }

    public int Data { get; set; }

    public bool IsOK { get; set; }

    public Smart()
    {
    }

    public Smart(string attributeName)
    {
      this.Attribute = attributeName;
    }
  }
}
