using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.models;

namespace api.Mappers
{
public static class FaceImageMapper
{
    public static FaceImage ToModel(this UploadFaceImageDto dto)
    {
        return new FaceImage
        {
            Name = dto.Name,
            Base64Image = dto.Base64Image,
            UserEmail = dto.UserEmail
        };
    }

    public static GetFaceImageDto ToDto(this FaceImage model)
    {
        return new GetFaceImageDto
        {
            Name = model.Name,
            Base64Image = model.Base64Image
        };
    }
}
}