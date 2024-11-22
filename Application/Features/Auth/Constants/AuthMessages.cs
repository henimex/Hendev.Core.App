namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string EmailAuthenticatorDoesNotExist = "Email authenticator does not exist.";
    public const string OtpAuthenticatorDoesNotExist = "OTP authenticator does not exist.";
    public const string AlreadyVerifiedOtpAuthenticatorExists = "An already verified OTP authenticator exists.";
    public const string EmailActivationKeyDoesNotExist = "Email activation key does not exist.";
    public const string UserDoesNotExist = "User does not exist.";
    public const string UserAlreadyHasAnAuthenticator = "User already has an authenticator.";
    public const string RefreshDoesNotExist = "Refresh does not exist.";
    public const string InvalidRefreshToken = "Invalid refresh token.";
    public const string UserEmailAlreadyExists = "User email already exists.";
    public const string PasswordsDoNotMatch = "Passwords do not match.";
}