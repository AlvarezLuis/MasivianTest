namespace Roulette.Entities.Utils
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Error(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
        public Error()
        {
            this.Code = string.Empty;
            this.Message = string.Empty;
        }
    }
}
