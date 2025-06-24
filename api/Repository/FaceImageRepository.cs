using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Data;

namespace api.Repository
{
    public class FaceImageRepository : IFaceImageRepository
    {
        private readonly AppDbContext _context;

    public FaceImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FaceImage> AddFaceImageAsync(FaceImage image)
    {
        await _context.FaceImages.AddAsync(image);
        await _context.SaveChangesAsync();
        return image;
    }

    public async Task<List<FaceImage>> GetImagesByUserEmailAsync(string email)
    {
        return await _context.FaceImages
            .Where(i => i.UserEmail == email)
            .ToListAsync();
    }
    }
}