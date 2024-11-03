// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1822:Marcar membros como estáticos", Justification = "Método pode depender de propriedades de instância futuras", Scope = "member", Target = "~M:CoreGoDelivery.Application.Services.Internal.Deliverier.Commands.UploadCnh.Common.DeliverierBuildExtensionFile.Build(System.Byte[])~System.ValueTuple{System.Boolean,System.String,CoreGoDelivery.Domain.Enums.LicenceDriverType.FileExtensionValidEnum}")]
[assembly: SuppressMessage("Style", "IDE0290:Usar construtor primário", Justification = "Compatibilidade com versões anteriores ao C# 12", Scope = "member", Target = "~M:CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Consumer.RabbitMQConsumer.#ctor(RabbitMQ.Client.IConnectionFactory,Microsoft.Extensions.DependencyInjection.IServiceScopeFactory)")]
[assembly: SuppressMessage("Style", "IDE0290:Usar construtor primário", Justification = "Compatibilidade com versões anteriores ao C# 12", Scope = "member", Target = "~M:CoreGoDelivery.Application.RabbitMQ.NotificationMotorcycle.Publisher.RabbitMQPublisher.#ctor(RabbitMQ.Client.IConnection)")]
