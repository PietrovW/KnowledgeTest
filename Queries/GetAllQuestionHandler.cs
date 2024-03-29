﻿using KnowledgeTest.Models;
using KnowledgeTest.Repositorys;

namespace KnowledgeTest.Queries;

public class GetAllQuestionHandler
{
    private readonly IQuestionRepository _questionRepository;

    public GetAllQuestionHandler(IQuestionRepository questionRepository)
    {
        this._questionRepository = questionRepository;
    }

    public async Task<IEnumerable<Question>> Handle(GetAllQuestion command, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _questionRepository.GetAll(cancellationToken: cancellationToken);
    }
}
