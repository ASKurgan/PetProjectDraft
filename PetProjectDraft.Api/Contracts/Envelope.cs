using PetProjectDraft.Domain.Common;

namespace PetProjectDraft.Api.Contracts
{
    public class Envelope
    {
        public object? Result { get; }
        public List<ErrorInfo>? ErrorInfo { get; }
        public DateTime TimeGenerated { get; }

        private Envelope(object? result, IEnumerable<ErrorInfo>? errors)
        {
            Result = result;
            ErrorInfo = errors?.ToList();
            TimeGenerated = DateTime.UtcNow;
        }

        public static Envelope Ok(object? result = null)
        {
            return new(result, null);
        }

        public static Envelope Error(params ErrorInfo[] errors) // params означает, что параметры-аргументы будут представлены в
                                                                // виде массива. Сколько бы мы не передали значений от клиента
                                                                // через запятую, они здесь, в параметрах, будут собраны в массив 
        {
            return new(null, errors);
        }
    }

    public class ErrorInfo
    {
        public string? ErrorCode { get; }
        public string? ErrorMessage { get; }
        public string? InvalidField { get; }

        public ErrorInfo(Error? error, string? invalidField = null)
        {
            ErrorCode = error?.Code;
            ErrorMessage = error?.Message;
            InvalidField = invalidField;
        }
    }
}
