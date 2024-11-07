using CoreGoDelivery.Application.Extensions;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Response;
using MediatR;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh;

public class DeliverierUploadCnhHandler : IRequestHandler<LicenseImageCommand, ActionResult>
{
    public readonly DeliverierUploadCnhValidator _validator;
    public readonly DeliverierBuilderCreateImage _builderCreateImage;
    public readonly DeliverierBuilderUpdateImage _builderUpdateImage;

    public readonly string UPLOAD_FOLDER = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\uploads_cnh"));

    public DeliverierUploadCnhHandler(
        DeliverierUploadCnhValidator validator,
        DeliverierBuilderCreateImage builderCreateImage,
        DeliverierBuilderUpdateImage builderUpdateImage)
    {
        _validator = validator;
        _builderCreateImage = builderCreateImage;
        _builderUpdateImage = builderUpdateImage;
    }

    public async Task<ActionResult> Handle(LicenseImageCommand command, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        var message = await _validator.Build(command);

        apiReponse.SetMessage(message?.AppendError());

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
