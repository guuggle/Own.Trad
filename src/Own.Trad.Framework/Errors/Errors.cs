using Own.Trad.Framework.OResult;

namespace Own.Trad.Framework.Errors
{
    public static class Errors
    {
        public static class General
        {
            public static OError NotFound => OError.NotFound();
        }
        public static class User
        {
            public static OError EmailIsTaken => OError.Validation(
                code: "User.EmailIsTaken",
                description: "Email is already in use.");
            public static OError UserNotExist => OError.Validation(
                code: "User.UserNotExist",
                description: "User does not exist.");
            public static OError InvalidPassword => OError.Validation(
                code: "User.InvalidPassword",
                description: "Incorrect Password.");
        }
    }
}