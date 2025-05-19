using System.ComponentModel;


namespace Tools
{
    public enum ReturnCode
    {
        [Description("000")]
        Success = 0,
        [Description("001")]
        Error = 1,
        [Description("400")]
        Token = 400,
        [Description("500")]
        Exception = 500
    }
}
