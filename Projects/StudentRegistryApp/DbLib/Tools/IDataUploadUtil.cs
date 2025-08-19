using CSharpFunctionalExtensions;

namespace UtilsLib.Tools
{
    public interface IDataUploadUtil
    {
        Result UploadData<T>(IEnumerable<T> records);
    }
}
