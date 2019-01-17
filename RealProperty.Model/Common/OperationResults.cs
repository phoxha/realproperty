namespace RealProperty.Model.Common
{
    public static class OperationResults
    {
        public static OperationStatus InvalidArgument = new OperationStatus(-4, "Invalid arguments");
        public static OperationStatus AccessDenied = new OperationStatus(-3, "Access denied");
        public static OperationStatus NotFound = new OperationStatus(-2, "Not found");
        public static OperationStatus Exception = new OperationStatus(-1, "Internal server error");

        public static OperationStatus Success = new OperationStatus(1, "Success");
    }
}
