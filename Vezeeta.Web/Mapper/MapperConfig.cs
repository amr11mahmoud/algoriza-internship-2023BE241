using AutoMapper;
using Vezeeta.Core.Domain.Appointments;
using Vezeeta.Core.Domain.Coupons;
using Vezeeta.Core.Domain.Lookup;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Shared;
using Vezeeta.Service.Dtos.Request.Appointments;
using Vezeeta.Service.Dtos.Request.Coupons;
using Vezeeta.Service.Dtos.Request.Doctors;
using Vezeeta.Service.Dtos.Request.Patients;
using Vezeeta.Service.Dtos.Response.Admin;
using Vezeeta.Service.Dtos.Response.Appointments;
using Vezeeta.Service.Dtos.Response.Coupons;
using Vezeeta.Service.Dtos.Response.Doctors;
using Vezeeta.Service.Dtos.Response.Patients;

namespace Vezeeta.Web.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, GetDoctorDto>()
            .ForMember(dest => dest.Gender, opt => opt
            .MapFrom<TranslateResolver, string>(src => src.Gender.ToString()))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver, string>(src => src.ImageUrl));

            CreateMap<Specialization, SpecializationDto>()
                .ForMember(dest => dest.Name, opt => opt
                .MapFrom<TranslateResolver, string>(src => src.Name));

            CreateMap<User, GetPatientDto>()
            .ForMember(dest => dest.Gender, opt => opt
            .MapFrom<TranslateResolver, string>(src => src.Gender.ToString()))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver, string>(src => src.ImageUrl));

            CreateMap<Appointment, GetAppointmentDto>()
            .ForMember(dest => dest.Day, opt => opt
            .MapFrom<TranslateResolver, string>(src => src.Day.ToString()));

            CreateMap<SpecializationRequestCount, TopSpecializationResponseDto>()
            .ForMember(dest => dest.FullName, opt => opt
            .MapFrom<TranslateResolver, string>(src => src.FullName));

            CreateMap<DoctorRequestCount, TopDoctorsResponseDto>()
            .ForMember(dest => dest.Specialization, opt => opt
            .MapFrom<TranslateResolver, string>(src => src.Specialization));

            CreateMap<User, GetDoctorWithAppointmentsDto>()
            .ForMember(dest => dest.Gender, opt => opt
            .MapFrom<TranslateResolver, string>(src => src.Gender.ToString()))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver, string>(src => src.ImageUrl));

            CreateMap<User, AddDoctorDto>().ReverseMap();
            CreateMap<User, EditDoctorDto>().ReverseMap();
            CreateMap<User, RegisterPatientDto>().ReverseMap();
            CreateMap<AppointmentTime, GetAppointmentTimeDto>().ReverseMap();
            CreateMap<Appointment, AddAppointmentDto>().ReverseMap();
            CreateMap<AppointmentTime, AddAppointmentTimeDto>().ReverseMap();
            CreateMap<Coupon, AddCouponDto>().ReverseMap();
            CreateMap<Coupon, EditCouponDto>().ReverseMap();
            CreateMap<Coupon, GetCouponDto>().ReverseMap();
        }
    }
}