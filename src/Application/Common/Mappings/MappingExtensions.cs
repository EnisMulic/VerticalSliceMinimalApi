﻿using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
    {
        return queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
    }
}