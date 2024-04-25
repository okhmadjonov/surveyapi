using System.ComponentModel;

namespace surveyapi.Entities.enums;

public enum QuestionType
{
    [Description("Text")]
    Text,
    [Description("SingleChoice")]
    SingleChoice,
    [Description("MultipleChoice")]
    MultipleChoice,

}