namespace RealProperty.Model.Common
{
    public class OperationStatus
    {
        public int Status { get; set; }
        public string Description { get; set; }

        public OperationStatus(int status, string description = null)
        {
            Status = status;
            Description = description;
        }
    }
}
