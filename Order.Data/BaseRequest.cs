using System;

namespace Order.Data
{
    public class BaseRequest
    {
        public bool IsEdit { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
