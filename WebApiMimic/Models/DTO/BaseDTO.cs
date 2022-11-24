using System.Collections.Generic;

namespace WebApiMimic.Models.DTO
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> Links { get; set; }
    }
}
