namespace PutridParrot.Probability.Exceptions;

/// <summary>
/// Provides guard methods for parameter validation
/// </summary>
internal static class Guard
{
    /// <summary>
    /// Throws an ArgumentException if the value is negative
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <param name="paramName">The name of the parameter</param>
    /// <param name="message">Optional custom message</param>
    /// <exception cref="ArgumentException">Thrown when value is negative</exception>
    public static void ThrowIfNegative(int value, string paramName, string? message = null)
    {
        if (value < 0)
        {
            throw new ArgumentException(
                message ?? $"{paramName} must be non-negative",
                paramName);
        }
    }

    /// <summary>
    /// Throws an ArgumentException if the value is negative
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <param name="paramName">The name of the parameter</param>
    /// <param name="message">Optional custom message</param>
    /// <exception cref="ArgumentException">Thrown when value is negative</exception>
    public static void ThrowIfNegative(float value, string paramName, string? message = null)
    {
        if (value < 0)
        {
            throw new ArgumentException(
                message ?? $"{paramName} must be non-negative",
                paramName);
        }
    }

    /// <summary>
    /// Throws an ArgumentException if the value is zero or negative
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <param name="paramName">The name of the parameter</param>
    /// <param name="message">Optional custom message</param>
    /// <exception cref="ArgumentException">Thrown when value is zero or negative</exception>
    public static void ThrowIfNotPositive(int value, string paramName, string? message = null)
    {
        if (value <= 0)
        {
            throw new ArgumentException(
                message ?? $"{paramName} must be positive",
                paramName);
        }
    }

    /// <summary>
    /// Throws an ArgumentException if the value is zero or negative
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <param name="paramName">The name of the parameter</param>
    /// <param name="message">Optional custom message</param>
    /// <exception cref="ArgumentException">Thrown when value is zero or negative</exception>
    public static void ThrowIfNotPositive(float value, string paramName, string? message = null)
    {
        if (value <= 0)
        {
            throw new ArgumentException(
                message ?? $"{paramName} must be positive",
                paramName);
        }
    }

    /// <summary>
    /// Throws an ArgumentException if value is greater than max
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <param name="max">The maximum allowed value</param>
    /// <param name="paramName">The name of the parameter</param>
    /// <param name="maxParamName">The name of the max parameter (optional)</param>
    /// <exception cref="ArgumentException">Thrown when value > max</exception>
    public static void ThrowIfGreaterThan(int value, int max, string paramName, string? maxParamName = null)
    {
        if (value > max)
        {
            var message = maxParamName != null
                ? $"{paramName} cannot be greater than {maxParamName}"
                : $"{paramName} cannot be greater than {max}";
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Throws a DivideByZeroException if the value is zero
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <param name="message">Optional custom message</param>
    /// <exception cref="DivideByZeroException">Thrown when value is zero</exception>
    public static void ThrowIfZero(float value, string? message = null)
    {
        if (value == 0)
        {
            throw new DivideByZeroException(
                message ?? "Value cannot be zero");
        }
    }
}
