﻿using IndustryFour.Shared.Dtos.Document;

namespace IndustryFour.Client.HttpRepository
{
    public interface IDocumentHttpRepository
    {
        Task<List<DocumentResultDto>> GetDocuments();
        Task<DocumentResultDto> GetDocument(int id);


	}
}
