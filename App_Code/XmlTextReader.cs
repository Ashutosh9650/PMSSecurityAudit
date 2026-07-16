using System.IO;

internal class XmlTextReader : System.Xml.XmlTextReader
{
    public XmlTextReader(Stream input) : base(input)
    {
    }
}