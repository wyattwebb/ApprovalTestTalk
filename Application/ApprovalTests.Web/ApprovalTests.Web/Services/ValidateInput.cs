using ApprovalUtilities.SimpleLogger;

namespace ApprovalTests.Web.Services
{
    public class ValidateInput : IValidateInput
    {
        public string ValidateGet(int? count)
        {
            
            Logger.Event("validation started");
            if (count == null)
            {
                Logger.Message("count is null");
                Logger.Event("validation complete");
                return "count is required";
            }
            if (count > 0)
            {
                Logger.Event("count greater than one");
                if (count > 10)
                {
                    Logger.Message("WAY TOO MANY PIGS");
                    Logger.Event("validation complete");
                    return "WAY TOO MANY PIGS";
                }
                if (count == 3)
                {
                    Logger.Message("Three is the number to which you should count");
                    Logger.Event("validation complete");
                }
                if (count == 5)
                {
                    Logger.Message("count is equal to five");
                    Logger.Event("validation complete");
                    return "FIVE IS OUTRIGHT";
                }
            }

            Logger.Event("validation complete");
            return null;
        }
    }
}