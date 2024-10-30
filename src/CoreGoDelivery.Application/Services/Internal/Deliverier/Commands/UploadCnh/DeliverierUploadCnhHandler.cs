using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Response;
using MediatR;


namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh
{
    public class DeliverierUploadCnhHandler : IRequestHandler<DeliverierUploadCnhCommand, ApiResponse>
    {
        public readonly DeliverierUploadCnhValidator _validator;
        public readonly BuilderCreateImage _builderCreateImage;
        public readonly BuilderUpdateImage _builderUpdateImage;

        public readonly string UPLOAD_FOLDER = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\uploads_cnh"));

        public DeliverierUploadCnhHandler(
            DeliverierUploadCnhValidator validator,
            BuilderCreateImage builderCreateImage,
            BuilderUpdateImage builderUpdateImage)
        {
            _validator = validator;
            _builderCreateImage = builderCreateImage;
            _builderUpdateImage = builderUpdateImage;
        }

        public async Task<ApiResponse> Handle(DeliverierUploadCnhCommand command, CancellationToken cancellationToken)
        {
            var apiReponse = new ApiResponse
            {
                Data = null,
                Message = await _validator.Build(command)
            };

            if (apiReponse.HasError())
            {
                return apiReponse;
            }

            if (command.IsUpdate)
            {
                return await _builderUpdateImage.Build(UPLOAD_FOLDER, command, apiReponse);
            }

            return await _builderCreateImage.Build(UPLOAD_FOLDER, command, apiReponse);
        }
    }
}
