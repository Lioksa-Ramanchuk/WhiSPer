namespace Lab3.Domain;

public class LabException(ErrorCode code) : Exception()
{
    public ErrorCode Code { get; } = code;
}
