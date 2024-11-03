using CoreGoDelivery.Application.Services.Internal.Base;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Common;
using CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;

namespace CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.Create;

public class DeliverierCreateMappers
{
    public readonly IBaseInternalServices _baseInternalServices;

    public readonly DeliverierBuildFileName _buildFileName;
    public readonly DeliverierBuildExtensionFile _buildExtensionFile;
    public readonly DeliverierParseLicenseType _parseLicenseType;

    public DeliverierCreateMappers(
        IBaseInternalServices baseInternalServices,
        DeliverierBuildFileName buildFileName,
         DeliverierBuildExtensionFile buildExtensionFile,
        DeliverierParseLicenseType parseLicenseType)
    {
        _baseInternalServices = baseInternalServices;
        _buildFileName = buildFileName;
        _buildExtensionFile = buildExtensionFile;
        _parseLicenseType = parseLicenseType;
    }

    public DeliverierEntity MapCreateToEntity(DeliverierCreateCommand command)
    {
        var (_, _, fileExtension) = _buildExtensionFile.Build(command.LicenseImageBase64);

        var fileName = _buildFileName.Build(command.LicenseNumber, fileExtension);

        var result = new DeliverierEntity()
        {
            Id = _baseInternalServices.IdBuild(command.Id),
            FullName = command.FullName,
            Cnpj = _baseInternalServices.RemoveCharacteres(command.Cnpj),
            BirthDate = command.BirthDate,
            LicenceDriver = new LicenceDriverEntity()
            {
                Id = command.LicenseNumber,
                Type = _parseLicenseType.Parse(command),
                ImageUrlReference = fileName
            }
        };

        return result;
    }
}
