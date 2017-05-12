namespace HeeP.Models.Application
{
    public enum ValidationError
    {
        Missing,
        Empty,
        NotEmpty,
        LargerThanExpected,
        ShorterThanExpected,
        InvalidFormat,
        InvalidCharacters,
        RangeInverted,
        RangeWithoutBoundaries,
        RangeInvalid,
        DataTypeDoesNotSupportValueRange,
        ChangeNotAllowed,
        Duplicated,
        NotFoundInRepository,
        NotAllowed,
        UnexpectedValue
    }
}
