namespace Common.Unknown
{
    public enum LoginResultType : byte
    {
        Success = 1,

        InvalidUserNameOrPassword,

        UserIsNotActive,

        UserIsNotVerified,

        UserLockout,

        InvalidSystem,

        UserNotConfirmedException,

        UserLoginAfterAlongTime,

        UserLoginDifferentDevice
    }
}
