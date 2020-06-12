using System.IO;

namespace WebSiteManager.Services
{
    public interface ISnapshotStore
    {
        ServiceResult Save(MemoryStream snapshotStream, string path);
    }
}
