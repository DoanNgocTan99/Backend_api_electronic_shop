using System;

namespace WebsiteApi.Model.Dtos
{
    public class PaymentDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public bool Allow { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Del { get; set; }
    }
}
