using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Student : Entity
{
    public string FullName { get; private set; } = default!;

    public DateTime BirthDate { get; private set; }

    public string? Email { get; private set; }

    public string PhoneNumber { get; private set; } = default!;

    public string Address { get; private set; } = default!;

    public string? ImageUrl { get; private set; }

    public Gender Gender { get; private set; } = Gender.Unknown;

    public bool IsActive { get; private set; }

    public Student(string fullName, DateTime birthDate, string phoneNumber, string address, Gender gender, bool isActive)
    {
        FullName = fullName;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Address = address;
        Gender = gender;
        IsActive = isActive;
    }

    public void SetFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BussinessLogicException(StudentsErrors.FullNameCantBeNull);

        FullName = fullName;
    }

    public void SetBirthDate(DateTime birthDate)
    {
        if (birthDate == default)
            throw new BussinessLogicException(StudentsErrors.BirthDateCantBeNull);

        BirthDate = birthDate;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new BussinessLogicException(StudentsErrors.PhoneNumberCantBeNull);

        PhoneNumber = phoneNumber;
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new BussinessLogicException(StudentsErrors.AddressCantBeNull);

        Address = address;
    }
    public void SetGender(Gender gender)
    {
        if (!Enum.IsDefined(typeof(Gender), gender) || gender == Gender.Unknown)
            throw new BussinessLogicException(StudentsErrors.GenderIsInvalid);

        Gender = gender;
    }

    public void SetIsActive(bool isActive)
    {
        if (!isActive)
            throw new BussinessLogicException(StudentsErrors.IsActiveCantBeNull);

        IsActive = isActive;
    }

    public class StudentsErrors
    {
        public static readonly Error FullNameCantBeNull = new(
            "Student.FullNameCantBeNull",
            "Full name cannot be null or empty."
        );

        public static readonly Error BirthDateCantBeNull = new(
            "Student.BirthDateCantBeNull",
            "Birth date cannot be null."
        );

        public static readonly Error PhoneNumberCantBeNull = new(
            "Student.PhoneNumberCantBeNull",
            "Phone number cannot be null or empty."
        );

        public static readonly Error AddressCantBeNull = new(
            "Student.AddressCantBeNull",
            "Address cannot be null or empty."
        );

        public static readonly Error GenderIsInvalid = new(
            "Student.GenderIsInvalid",
            "Provided gender is invalid."
        );

        public static readonly Error IsActiveCantBeNull = new(
            "Student.IsActiveCantBeNull",
            "IsActive cannot be null."
        );
    }
}