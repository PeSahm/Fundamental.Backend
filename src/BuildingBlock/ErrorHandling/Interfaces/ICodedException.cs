using Fundamental.ErrorHandling.Enums;

namespace Fundamental.ErrorHandling.Interfaces;

public interface ICodedException
{
    public CommonErrorCode GetCommonErrorCode();
}