using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs.Account
{
    public class GetFaceImageDto
    {
        public String Name { get; set; }
        public String Base64Image { get; set; }
    }
}
