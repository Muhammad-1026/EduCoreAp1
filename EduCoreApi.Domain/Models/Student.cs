using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;
using System.Text.RegularExpressions;

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

    public Guid GroupId { get; private set; }
    public Group Group { get; private set; } = default!;
    public ICollection<Grade> Grades { get; private set; } = new List<Grade>();
    public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();

    public Student(string fullName,DateTime birthDate,string phoneNumber,string address,Gender gender,Guid createdBy) : base(createdBy)
    {
        SetFullName(fullName);
        SetBirthDate(birthDate);
        SetPhoneNumber(phoneNumber);
        SetAddress(address);
        SetGender(gender);

        IsActive = false;
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

        if (birthDate > DateTime.UtcNow.AddYears(-10) || birthDate < DateTime.UtcNow.AddYears(-100))
            throw new BussinessLogicException(StudentsErrors.BirthDateIsInvalid);

        BirthDate = birthDate;
    }

    public void SetEmail(string? email)
    {
        if (!string.IsNullOrWhiteSpace(email) && !email.Contains("@"))
            throw new BussinessLogicException(StudentsErrors.EmailIsInvalid);

        Email = email;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new BussinessLogicException(StudentsErrors.PhoneNumberCantBeNull);

        if (!Regex.IsMatch(phoneNumber, @"^\+[1-9]\d{7,14}$"))
            throw new BussinessLogicException(StudentsErrors.PhoneNumberIsInvalid);

        PhoneNumber = phoneNumber;
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new BussinessLogicException(StudentsErrors.AddressCantBeNull);

        Address = address;
    }

    public void SetImageUrl(string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            ImageUrl = null;
            return;
        }

        if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            throw new BussinessLogicException(StudentsErrors.ImageUrlIsInvalid);

        ImageUrl = imageUrl;
    }

    public void SetGender(Gender gender)
    {
        if (!Enum.IsDefined(typeof(Gender), gender) || gender == Gender.Unknown)
            throw new BussinessLogicException(StudentsErrors.GenderIsInvalid);

        Gender = gender;
    }

    public void SetGroupId(Guid groupId)
    {
        if (groupId == Guid.Empty)
            throw new BussinessLogicException(StudentsErrors.GroupIdCantBeNull);

        GroupId = groupId;
    }

    public void Activate()
    {
        if (GroupId == Guid.Empty)
            throw new BussinessLogicException(StudentsErrors.CantActivateWithoutGroup);

        if (IsActive)
            throw new BussinessLogicException(StudentsErrors.AlreadyActive);

        IsActive = true;
    }

    public static class StudentsErrors
    {
        public static readonly Error FullNameCantBeNull = new(
            "Student.FullNameCantBeNull",
            "Full name cannot be null or empty."
        );

        public static readonly Error BirthDateCantBeNull = new(
            "Student.BirthDateCantBeNull",
            "Birth date cannot be null."
        );

        public static readonly Error BirthDateIsInvalid = new(
           "Student.BirthDateIsInvalid",
           "Birth date is invalid. Age must be between 10 and 100 years."
        );

        public static readonly Error EmailIsInvalid = new(
           "Student.EmailIsInvalid",
           "Provided email is not valid."
        );

        public static readonly Error PhoneNumberCantBeNull = new(
            "Student.PhoneNumberCantBeNull",
            "Phone number cannot be null or empty."
        );

        public static readonly Error PhoneNumberIsInvalid = new(
            "Student.PhoneNumberIsInvalid",
            "Phone number format is invalid (must be in international E.164 format)."
        );

        public static readonly Error AddressCantBeNull = new(
            "Student.AddressCantBeNull",
            "Address cannot be null or empty."
        );

        public static readonly Error ImageUrlIsInvalid = new(
            "Student.ImageUrlIsInvalid",
            "Image URL is not valid."
        );

        public static readonly Error GenderIsInvalid = new(
            "Student.GenderIsInvalid",
            "Provided gender is invalid."
        );

        public static readonly Error CantActivateWithoutGroup = new(
            "Student.CantActivateWithoutGroup",
            "Student cannot be activated without being assigned to a group."
        );

        public static readonly Error AlreadyActive = new(
            "Student.AlreadyActive",
            "The student is already active."
        );

        public static readonly Error AlreadyInactive = new(
            "Student.AlreadyInactive",
            "The student is already inactive."
        );

        public static readonly Error GroupIdCantBeNull = new(
            "Student.GroupIdCantBeNull",
            "Group ID cannot be null."
        );
    }
}