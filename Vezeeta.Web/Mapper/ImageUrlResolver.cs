using AutoMapper;
using Vezeeta.Web.Helpers;

namespace Vezeeta.Web.Mapper
{
    public class ImageUrlResolver : IMemberValueResolver<object, object, string, string>
    {
        private readonly IImageHelper _imageHelper;
        public ImageUrlResolver(IImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }

        public string Resolve(object source, object destination, string sourceMember, string destMember,
            ResolutionContext context)
        {
            return _imageHelper.GetImagePath(sourceMember);
        }
    }
}
