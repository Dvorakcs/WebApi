using System.Collections.Generic;

namespace WebApiMimic.V1.Models.DTO
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> Links { get; set; }
    }
}
