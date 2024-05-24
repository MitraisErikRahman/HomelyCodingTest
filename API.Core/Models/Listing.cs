using System.Linq;
using System.Text;
using API.Core.Custom;
using Newtonsoft.Json;
using static API.Core.Models.Enums;

namespace API.Core.Models
{
    public class Listing
    {
        public int ListingId { get; set; }

        [JsonIgnore]
        public string StreetNumber { get; set; }

        [JsonIgnore]
        public string Street { get; set; }

        [JsonIgnore]
        public string Suburb { get; set; }

        [JsonIgnore]
        public string State { get; set; }

        [JsonIgnore]
        public int Postcode { get; set; }

        public string Address
        {
            get
            {
                var address = new StringBuilder();
                if (!string.IsNullOrEmpty(StreetNumber))
                    address = address.Append(StreetNumber + " ");

                if (!string.IsNullOrEmpty(Street))
                    address = address.Append(Street + ", ");

                if (!string.IsNullOrEmpty(Suburb))
                    address = address.Append(Suburb + " ");

                if (!string.IsNullOrEmpty(State))
                    address = address.Append(State + " ");

                if (Postcode != 0)
                    address = address.Append(Postcode.ToString());

                return address.ToString();
            }
        }

        public CategoryType CategoryType { get; set; }

        public StatusType StatusType { get; set; }

        public string DisplayPrice { get; set; }

        public string ShortPrice
        {
            get
            {
                var result = string.IsNullOrEmpty(DisplayPrice) ? null : Helper.FormatNumber(string.Concat(DisplayPrice.Trim().Where(x => char.IsDigit(x) || char.IsWhiteSpace(x))));
                return string.IsNullOrEmpty(result) ? string.Empty : "$" + result;
            }
        }

        public string Title { get; set; }
    }
}
