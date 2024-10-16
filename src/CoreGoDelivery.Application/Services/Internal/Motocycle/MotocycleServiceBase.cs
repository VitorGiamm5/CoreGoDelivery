﻿using CoreGoDelivery.Domain.DTO.Motocycle;
using CoreGoDelivery.Domain.DTO.Response;
using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Application.Services.Internal.Motocycle
{
    public class MotocycleServiceBase
    {
        private const string MESSAGE_INVALID_DATA = "Dados inválidos";

        public static MotocycleEntity CreateToEntity(MotocycleDto data)
        {
            return new MotocycleEntity()
            {
                Id = SelectId(data),
                YearManufacture = data.YearManufacture,
                ModelMotocycleId = data.ModelName,
                PlateIdNormalized = RemoveCharacteres(data.PlateId)
            };
        }

        public static string RemoveCharacteres(string plateId)
        {
            var result = Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper();

            return result;
        }

        public static string SelectId(MotocycleDto data)
        {
            var result = string.IsNullOrEmpty(data.Id) ? Ulid.NewUlid().ToString() : data.Id;

            return result;
        }

        public static bool ValidatePlate(string plateId)
        {
            var plate = Regex.Replace(plateId, @"[\s\-\.\,]", "").ToUpper();

            if (Regex.IsMatch(plate, @"^[A-Z]{3}\d{4}$") ||    // old format
                Regex.IsMatch(plate, @"^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$")) // new format
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string? FinalMessageBuild(bool resultCreate, ApiResponse apiReponse)
        {
            if (!string.IsNullOrEmpty(apiReponse.Message))
            {
                return null;
            }

            return resultCreate
                ? null
                : MESSAGE_INVALID_DATA;
        }
    }
}