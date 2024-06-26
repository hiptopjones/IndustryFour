﻿using IndustryFour.Server.Models;

namespace IndustryFour.Server.Services
{
    public interface IDocumentIndexService
    {
        Task Add(Document document);
        Task Update(Document document);
        Task Remove(Document document);

        Task<IEnumerable<ChunkMatch>> SimilaritySearch(string text, int k);
    }
}
