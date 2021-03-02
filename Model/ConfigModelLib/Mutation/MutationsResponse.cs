namespace ConfigModelLib.Mutation
{
    //public interface IRepo<T>
    //{
    //    Task<R> ReadAsync<R>(Func<T, R> func);
    //    Task<MutationsResponse> WriteAsync(Action<T> action);
    //}

    //public class Repo<T> : IRepo<T> where T : class, new()
    //{
    //    public Task<R> ReadAsync<R>(Func<T, R> func) =>
    //        Task.Run(() =>
    //        {
    //            var t = new T();
    //            return func(t);
    //        });

    //    public Task<MutationsResponse> WriteAsync(Action<T> action) =>
    //        Task.Run(() =>
    //        {
    //            MutationsResponse repoResponse = new() { OpStatus = RepoOperationStatus.Success, Message = string.Empty };

    //            //...

    //            return repoResponse;
    //        });
    //}

    public enum OperationStatus
    {
        Success = 0,
        Failure
    }

    public class MutationsResponse
    {
        public OperationStatus OpStatus { private get; set; }

        public string Status => $"{OpStatus}";
        public string Message { get; set; }

        public bool IsOK => OpStatus == OperationStatus.Success;
    }

    //public class StorageXml
    //{
    //}

    //public static class TempHelper
    //{
    //    public static Task<object> FuncPalceholder() =>
    //        Task.Run(() => new object());


    //    public static Task<Exception> LoadAsync(this XmlDocument xmlDocument, string path) =>
    //        Task.Run<Exception>(() =>
    //        {
    //            try
    //            {
    //                Task.Run(() => xmlDocument.Load(path));
    //                return null;
    //            }
    //            catch (Exception e)
    //            {
    //                return e;
    //            }
    //        });
    //}
}
