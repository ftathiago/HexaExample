using HexaEmployee.Api.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HexaEmployee.Api.Models.Requests
{
    [Serializable]
    public class PaginationRequest : QueryFields, IValidatableObject
    {
        internal const int MinOffset = 1;
        internal const int MinRecordsPerPage = 5;
        internal const int MaxRecordsPerPage = 50;

        public PaginationRequest()
        {
        }

        protected PaginationRequest(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [Required]
        [Description("Request page number.")]
        [OpenApiExample("1")]
        public int Offset
        {
            get => int.TryParse(this.GetValueOrDefault("offset"), out var parsedOffset)
                ? parsedOffset
                : MinOffset;
        }

        [Required]
        [Description("Max register count per page.")]
        [OpenApiExample("10")]
        public int Limit
        {
            get => int.TryParse(this.GetValueOrDefault("limit"), out var parsedLimit)
                ? parsedLimit
                : MinRecordsPerPage;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Offset < MinOffset)
            {
                yield return new ValidationResult(
                    "An offset must be greather or equal than 1",
                    new[] { nameof(Offset) });
            }

            if (Limit > MaxRecordsPerPage)
            {
                yield return new ValidationResult(
                    $"A limit must be between {MinRecordsPerPage} and {MaxRecordsPerPage}.",
                    new[] { nameof(Limit) });
            }
        }
    }
}
