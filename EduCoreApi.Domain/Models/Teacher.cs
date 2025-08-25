using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;
using System.Net.Mail;

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

    public Guid DepartmentId { get; private set; }
    public Department Department { get; private set; } = default!;

    public Teacher(string fullName, DateTime birthDate, string phoneNumber, string address, Gender gender, bool isActive, Guid createdBy) : base(createdBy)
    {
        SetFullName(fullName);
        SetBirthDate(birthDate);
        SetPhoneNumber(phoneNumber);
        SetAddress(address);
        SetGender(gender);
        SetIsActive(isActive);
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

    public void SetEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new BussinessLogicException(TeacherErrors.EmailIsInvalid);

        try
        {
            var addr = new MailAddress(email);
            if (addr.Address != email)
                throw new BussinessLogicException(TeacherErrors.EmailIsInvalid);
        }
        catch (FormatException)
        {
            throw new BussinessLogicException(TeacherErrors.EmailIsInvalid);
        }

        Email = email;
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
            throw new BussinessLogicException(TeacherErrors.GenderIsInvalid);

        Gender = gender;
    }

    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }

    public class TeacherErrors
    {
        public static readonly Error FullNameCantBeNull = new(
            "Teacher.FullNameCantBeNull",
            "ФИО преподавателя обязательно для заполнения."
        );

        public static readonly Error BirthDateCantBeNull = new(
            "Teacher.BirthDateCantBeNull",
            "Дата рождения обязательна."
        );

        public static readonly Error EmailIsInvalid = new(
            "Teacher.EmailIsInvalid",
            "Некорректный формат электронной почты."
        );

        public static readonly Error PhoneNumberCantBeNull = new(
            "Teacher.PhoneNumberCantBeNull",
            "Номер телефона обязателен."
        );

        public static readonly Error AddressCantBeNull = new(
            "Teacher.AddressCantBeNull",
            "Адрес обязателен."
        );

        public static readonly Error GenderIsInvalid = new(
           "Teacher.GenderIsInvalid",
           "Указан недопустимый пол."
       );
    }
}