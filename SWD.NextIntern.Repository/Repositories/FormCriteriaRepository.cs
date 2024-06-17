﻿using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository.Repositories
{
    public class FormCriteriaRepository : RepositoryBase<FormCriterion, FormCriterion, AppDbContext>, IFormCriteriaRepository
    {
        public FormCriteriaRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}