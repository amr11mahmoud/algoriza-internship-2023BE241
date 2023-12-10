using AutoMapper;
using Microsoft.Extensions.Localization;
using Vezeeta.Web.Helpers;

namespace Vezeeta.Web.Mapper
{
    public class TranslateResolver : IMemberValueResolver<object, object, string, string>
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public TranslateResolver(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string Resolve(object source, object destination, string sourceMember, string destMember,
            ResolutionContext context)
        {
            return _localizer[sourceMember];
        }
    }
}
