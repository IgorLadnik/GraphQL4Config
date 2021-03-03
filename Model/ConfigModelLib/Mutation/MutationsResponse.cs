namespace ConfigModelLib.Mutation
{
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
}
