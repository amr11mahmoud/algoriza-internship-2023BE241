namespace Vezeeta.Core.Shared
{
    public sealed class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "Result Value is Null");
        public static class Errors
        {
            public static class General
            {
                public static Error IsRequiredError()
                    => new Error("value.is.required", "value is required");
                public static Error IsRequiredError(string value)
                    => new Error("value.is.required", $"{value} is required", value);
                public static Error LengthError()
                    => new Error("length.is.incorrect", "value length is incorrect");
                public static Error LengthError(string value, byte minLength)
                    => new Error("length.is.incorrect", $"{value} length should be at least ${minLength}");
                public static Error LengthError(string value, byte minLength, byte maxLength)
                    => new Error("length.is.incorrect", $"{value} length should be at least ${minLength} and ${maxLength} at most");
                public static Error RequestIsNull(string request)
                    => new Error("null.request.error", $"{request} is null");
                public static Error InvalidFieldDataType(string field)
                    => new Error("invalid.input.data", $"Input Data Error in field '{field}'", field);
                public static Error InvalidImage()
                    => new Error("invalid.image", $"Invalid image extension", "image");
            }

            public static class Doctors
            {
                public static Error DoctorNotFound()
                    => new Error("doctor.not.found", "doctor not found!");
                public static Error DoctorHasRequests()
                    => new Error("doctor.has.requests", "can't delete doctor because he/she has requests already!");
            }

            public static class Patients
            {
                public static Error PatientNotFound()
                    => new Error("patient.not.found", "patient not found!");
            }

            public static class Bookings
            {
                public static Error BookingNotFound()
                    => new Error("booking.not.found", "booking not found!");
                public static Error BookingStatusIsNotPending()
                    => new Error("invalid.booking.status", "booking status is not in pending status!");
                public static Error InvalidBookingDoctor()
                    => new Error("invalid.booking.doctor", "unauthorized request, invalid booking doctor!");
            }

            public static class Users
            {
                public static Error UserNotFound()
                    => new Error("user.not.found", "user not found!");
                public static Error UserNotFound(string email)
                    => new Error("user.not.found", $"user with email:{email} not found!", "email");
                public static Error UserIsNull()
                    => new Error("user.is.null", "user is null error");
                public static Error EmailError()
                   => new Error("email.error", "email is incorrect");
                public static Error RegisterUserError(string code, string msg) 
                    => new Error(code, msg);
                public static Error InvalidPassword()
                    => new Error("incorrect.password", "password is incorrect", "password");
                public static Error InvalidGenderFormat()
                    => new Error("invalid.gender.input", "gender should be either Male or Female", "gender");
                public static Error InvalidJwtToken()
                    => new Error("invalid.jwt.token", "invalid jwt token!");
                public static Error UserCanNotSignIn()
                    => new Error("user.can.not.login", "user can't login", "email");
                public static Error UserSignInNotAllowed()
                    => new Error("user.login.not.allowed", "user login not allowed", "email");
                public static Error UserLockout()
                   => new Error("user.lockout", "user account is lockout", "email");
                public static Error PasswordNotMatched()
                   => new Error("password.not.matched", "password not matched", "password");
                public static Error RoleNotFound(string role)
                    => new Error("role.not.found", $"role not found [{role}]!");
                public static Error AlreadyInRole(string role)
                    => new Error("user.already.in.role", $"user already in role [{role}]!");
            }

            public static class Appointments
            {
                public static Error AppointmentNotFound()
                   => new Error("appointment.not.found", $"appointment not found!");
                public static Error AppointmentTimeNotFound()
                   => new Error("appointment.time.not.found", $"appointment time not found!");
                public static Error InvalidTimeFormat(string time)
                       => new Error("invalid.time.format", $"{time} is invalid time format it should be hh:mm between 00:00 and 23:59 !", "Time");
                public static Error InvalidDayFormat(string day)
                       => new Error("invalid.day.format", $"{day} is invalid day format it should be day of week value like Saturday", "Day");
                public static Error InvalidAppointmentDoctor()
                    => new Error("invalid.appointment.doctor", "unauthorized request, invalid appointment doctor!");
                public static Error AppointmentIsBooked()
                   => new Error("appointment.is.booked", $"appointment is booked!");
                public static Error AppointmentDayAlreadyExist(string day)
                   => new Error("appointment.day.exist", $"appointment day [{day}] already exist, you can update it instead!");
            }
            public static class Settings
            {
                public static Error CouponCodeAlreadyExist(string code)
                       => new Error("coupon.code.exist", $"coupon with code [{code}] already exist!", "Code");
                public static Error CouponNotFound()
                    => new Error("coupon.not.found", "coupon not found!");
                public static Error CouponNotActive()
                    => new Error("coupon.not.active", "coupon not active!");
                public static Error CouponNotApplicable(int patientRequestsCount, int couponRequestsCount)
                    => new Error("coupon.not.applicable", 
                        $"you are not eligible to apply this coupon as it needs minimum {couponRequestsCount} " +
                        $"number of requests and you have only {patientRequestsCount}!");
                public static Error CouponAlreadyUsed()
                    => new Error("coupon.already.used", "you already used this coupon before, and you can use coupon only once!");
            }

            public static class Exceptions
            {
                public static class Database
                {
                    public static Error DuplicateRecordException(string record)
                      => new Error("duplicate.record.exception", $"a record already exist for [{record}]!", "N/A");
                }
            }
        }

        
        public string Code { get; }
        public string Message { get; }
        public string? InvalidField { get;}

        private const string sep = "||";

        public string Serialize()
        {
            return $"{Code}{sep}{Message}{sep}{InvalidField}";
        }

        public static Error Deserialize(string serialized)
        {
            string[] data = serialized.Split(new[] { sep }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 3)
            {
                throw new Exception($"Invalid Error Deserialization {serialized}");
            }

            return new Error(data[0], data[1], data[2]);
        }

        public Error(string code, string msg):this(code, msg, "N/A")
        {
        }

        public Error(string code, string msg, string invalidField)
        {
            Code = code;
            Message = msg;
            InvalidField = invalidField;
        }

        public static implicit operator string(Error error) => error.Code;

        public static bool operator ==(Error? a, Error? b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Code == b.Code;
        }

        public static bool operator !=(Error? a, Error? b)
        {
            if (a is null && b is null)
            {
                return false;
            }

            if (a is null || b is null)
            {
                return true;
            }

            return a.Code != b.Code;
        }

        public bool Equals(Error? other)
        {
            if (other is null || this is null) return false;

            if (other.GetType() != this.GetType()) return false;

            if (other.GetHashCode() != this.GetHashCode()) return false;

            return other.Code == other.Code;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Error);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
