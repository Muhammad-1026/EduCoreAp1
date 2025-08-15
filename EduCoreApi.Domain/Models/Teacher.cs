using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Models;

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