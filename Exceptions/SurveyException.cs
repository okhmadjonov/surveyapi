namespace surveyapi.Exceptions;


public class SurveyException : Exception
{
    public int Code { get; set; }
    public bool? Global { get; set; }

    public SurveyException(int code, string message, bool? global = true) : base(message)
    {
        Code = code;
        Global = global;
    }
}
