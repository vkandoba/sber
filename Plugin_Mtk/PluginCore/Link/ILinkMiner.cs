using System.Security.Cryptography.X509Certificates;

namespace PluginCore.Link
{
    public interface ILinkMiner
    {
        string[] Extract(string content);
    }
}