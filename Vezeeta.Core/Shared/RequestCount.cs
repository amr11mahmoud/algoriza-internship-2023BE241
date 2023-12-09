using Vezeeta.Core.Domain.Lookup;
using Vezeeta.Core.Enums;

namespace Vezeeta.Core.Shared
{
    public class RequestCount
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class SpecializationRequestCount
    {
        public string FullName { get; set; }
        public int Requests { get; set; }
    }

    public class DoctorRequestCount: SpecializationRequestCount
    {
        public string Image { get; set; }
        public string Specialization { get; set; }
    }
}
