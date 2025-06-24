using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;
namespace api.interfaces
{
    public interface IFaceImageRepository
    {
        Task<FaceImage> AddFaceImageAsync(FaceImage image);
        Task<List<FaceImage>> GetImagesByUserEmailAsync(string email);
    }
}