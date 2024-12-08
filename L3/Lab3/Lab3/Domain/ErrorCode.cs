namespace Lab3.Domain;

public enum ErrorCode
{
    StudentNotFoundById = 404_01,
    ErrorCodeNotFound,

    Unexpected = 500_00,
    DatabaseError,
    StudentUpdateConcurrencyError,
}
