using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace EduCoreApi.Domain.Models;

public class Student : Entity
{
    public string FullName { get; private set; } = default!;
    public DateTime BirthDate { get; private set; }
    public string? Email { get; private set; }
    public string PhoneNumber { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public bool IsDormitoryResident { get; private set; }
    public string? ImageUrl { get; private set; }
    public Gender Gender { get; private set; } = Gender.Unknown;
    public bool IsActive { get; private set; }

    public Guid GroupId { get; private set; }
    public Group Group { get; private set; } = default!;

    public Guid SpecialityId { get; private set; }
    public Speciality Speciality { get; private set; } = default!;

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
        if (string.IsNullOrWhiteSpace(email))
        {
            Email = null;
            return;
        }

        try
        {
            var addr = new MailAddress(email);

            if (addr.Address != email)
                throw new BussinessLogicException(StudentsErrors.EmailIsInvalid);
        }
        catch (FormatException)
        {
            throw new BussinessLogicException(StudentsErrors.EmailIsInvalid);
        }

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

    public void SetIsDormitoryResident(bool isDormitoryResident)
    {
        IsDormitoryResident = isDormitoryResident;
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

    public void Deactivate()
    {
        if (!IsActive)
            throw new BussinessLogicException(StudentsErrors.AlreadyInactive);

        IsActive = false;
    }

    public static class StudentsErrors
    {
        public static readonly Error FullNameCantBeNull = new(
            "Student.FullNameCantBeNull",
            "Полное имя обязательно."
        );

        public static readonly Error BirthDateCantBeNull = new(
            "Student.BirthDateCantBeNull",
            "Дата рождения обязательна."
        );

        public static readonly Error BirthDateIsInvalid = new(
           "Student.BirthDateIsInvalid",
           "Недопустимая дата рождения. Возраст должен быть от 10 до 100 лет."
        );

        public static readonly Error EmailIsInvalid = new(
           "Student.EmailIsInvalid",
           "Недопустимый адрес электронной почты."
        );

        public static readonly Error PhoneNumberCantBeNull = new(
            "Student.PhoneNumberCantBeNull",
            "Номер телефона обязателен."
        );

        public static readonly Error PhoneNumberIsInvalid = new(
            "Student.PhoneNumberIsInvalid",
            "Недопустимый формат номера телефона (E.164)."
        );

        public static readonly Error AddressCantBeNull = new(
            "Student.AddressCantBeNull",
            "Адрес обязателен."
        );

        public static readonly Error ImageUrlIsInvalid = new(
            "Student.ImageUrlIsInvalid",
            "Недопустимая ссылка на изображение."
        );

        public static readonly Error GenderIsInvalid = new(
            "Student.GenderIsInvalid",
            "Недопустимое значение пола."
        );

        public static readonly Error CantActivateWithoutGroup = new(
            "Student.CantActivateWithoutGroup",
            "Невозможно активировать без группы."
        );

        public static readonly Error AlreadyActive = new(
            "Student.AlreadyActive",
            "Студент уже активирован."
        );

        public static readonly Error AlreadyInactive = new(
            "Student.AlreadyInactive",
            "Студент уже деактивирован."
        );

        public static readonly Error GroupIdCantBeNull = new(
            "Student.GroupIdCantBeNull",
            "Идентификатор группы обязателен."
        );
    }
}