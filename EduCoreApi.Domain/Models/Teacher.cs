using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;
using static EduCoreApi.Domain.Models.Student;

namespace EduCoreApi.Domain.Models;

public class Teacher : Entity
{
    public string FullName { get; private set; } = default!;

    public DateTime BirthDate { get; private set; }

    public string? Email { get; private set; }

    public string PhoneNumber { get; private set; } = default!;

    public string Address { get; private set; } = default!;

    public string? ImageUrl { get; private set; }

    public Gender Gender { get; private set; } = Gender.Unknown;

    public bool IsActive { get; private set; }

    public Teacher(string fullName, DateTime birthDate, string phoneNumber, string address, Gender gender, bool isActive)
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
            throw new BussinessLogicException(TeacherErrors.FullNameCantBeNull);

        FullName = fullName;
    }

    public void SetBirthDate(DateTime birthDate)
    {
        if (birthDate == default)
            throw new BussinessLogicException(TeacherErrors.BirthDateCantBeNull);

        BirthDate = birthDate;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new BussinessLogicException(TeacherErrors.PhoneNumberCantBeNull);

        PhoneNumber = phoneNumber;
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new BussinessLogicException(TeacherErrors.AddressCantBeNull);

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
        if (!IsActive)
            throw new BussinessLogicException(TeacherErrors.FullNameCantBeNull);

        IsActive = isActive;
    }

    public class TeacherErrors
    {
        public static readonly Error FullNameCantBeNull = new(
            "Teacher.FullNameCantBeNull",
            "Full name cannot be null or empty."
        );

        public static readonly Error BirthDateCantBeNull = new(
            "Teacher.BirthDateCantBeNull",
            "Birth date cannot be default."
        );

        public static readonly Error PhoneNumberCantBeNull = new(
            "Teacher.PhoneNumberCantBeNull",
            "Phone number cannot be null or empty."
        );

        public static readonly Error AddressCantBeNull = new(
            "Teacher.AddressCantBeNull",
            "Address cannot be null or empty."
        );

        public static readonly Error GenderIsInvalid = new(
           "Student.GenderIsInvalid",
           "Provided gender is invalid."
       );
    }
}