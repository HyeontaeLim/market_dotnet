using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace market.Validation;

public class ValidationResult
{
    private readonly List<FieldErrorDetail> _fieldErrors;
    private readonly List<ObjectErrorDetail> _globalErrors;

    public ValidationResult(ModelStateDictionary modelState, object model)
    {
        var fieldErrors = new List<FieldErrorDetail>();
        var globalErrors = new List<ObjectErrorDetail>();
        foreach (var ms in modelState)
        {
            if (ms.Value.Errors.Any() && !ms.Key.Equals(model.GetType().Name))
            {
                fieldErrors.Add(new FieldErrorDetail(
                    ms.Key,
                    ms.Value.RawValue,
                    ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
            }
            else if(ms.Value.Errors.Any() && ms.Key.Equals(model.GetType().Name))
            {
                globalErrors.Add(new ObjectErrorDetail(
                    ms.Key,
                    ms.Value.Errors.Select(e=>e.ErrorMessage).ToArray()
                    ));
            }
        }
        _fieldErrors = fieldErrors;
        _globalErrors = globalErrors;
    }

    public List<FieldErrorDetail> FieldErrors => _fieldErrors;

    public List<ObjectErrorDetail> GlobalErrors => _globalErrors;
}

public class FieldErrorDetail
{
    private readonly string _field;
    private readonly object _rejectedValue;
    private readonly string[] _message;

    public FieldErrorDetail(string field, object rejectedValue, string[] message)
    {
        _field = field;
        _rejectedValue = rejectedValue;
        _message = message;
    }

    public string Field => _field;

    public object RejectedValue => _rejectedValue;

    public string[] Message => _message;
}

public class ObjectErrorDetail
{
    private readonly string _objectName;
    private readonly string[] _message;

    public ObjectErrorDetail(string objectName, string[] message)
    {
        _objectName = objectName;
        _message = message;
    }

    public string ObjectName => _objectName;

    public string[] Message => _message;
}