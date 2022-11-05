﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectControlAPI.BusinessLogic.Services.Implementations;
using ProjectControlAPI.Common.DTOs.ProjectDTOs;
using ProjectControlAPI.Common.Exceptions;
using ProjectControlAPI.Common.Resource;
using ProjectControlAPI.DataAccess;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.BusinessLogic.Services.Interfaces
{
    public class ProjectService : IProjectService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProjectService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateProjectDTO project)
        {
            var projectToAdd = _mapper.Map<Project>(project);

            GuardIncorrectProjectData(projectToAdd);
            
            await _context.Projects.AddAsync(projectToAdd);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(int projectId)
        {
            var project = await _context.Projects
                 .SingleOrDefaultAsync(x => x.Id == projectId);

            GuardNotFound(project);

            _context.Projects.Remove(project!);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetProjectDTO>> GetAllAsync()
        {
            return await _context.Projects
                .ProjectTo<GetProjectDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(); 
        }

        public async Task<GetProjectDTO> GetByIdAsync(int projectId)
        {
            var project = await _context.Projects
                .SingleOrDefaultAsync(x => x.Id == projectId);

            GuardNotFound(project);

            return _mapper.Map<GetProjectDTO>(project);
        }

        public async Task UpdateAsync(UpdateProjectDTO project)
        {
            GuardNullArgument(project);

            var projectToUpdate = await _context.Projects
                .SingleOrDefaultAsync(x => x.Id == project.Id);

            GuardNotFound(projectToUpdate);

            if (!string.IsNullOrWhiteSpace(project.Name))
            {
                projectToUpdate.Name = project.Name;
            }

            if (!string.IsNullOrWhiteSpace(project.CustomerCompanyName))
            {
                projectToUpdate.CustomerCompanyName = project.CustomerCompanyName;
            }

            if (!string.IsNullOrWhiteSpace(project.ExecutorCompanyName))
            {
                projectToUpdate.ExecutorCompanyName = project.ExecutorCompanyName;
            }

            if (project.StartDate is not null)
            {
                projectToUpdate.StartDate = (DateTime)project.StartDate;
            }

            if (project.EndDate is not null)
            {
                projectToUpdate.EndDate = (DateTime)project.EndDate;
            }

            if (project.Priority is not null)
            {
                projectToUpdate.Priority = (int)project.Priority;
            }

            GuardIncorrectProjectData(projectToUpdate);
            await _context.SaveChangesAsync();
        }

        private void GuardNullArgument<T>(T arg)
        {
            if (arg is null)
            {
                throw new BadRequestException(ProjectMessageResource.NullArgument);
            }
        }

        private void GuardNotFound(Project? project)
        {
            if (project is null)
            {
                throw new NotFoundException(ProjectMessageResource.NotFound);
            }
        }

        private void GuardNullOrEmptyString(string? str, string message)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new BadRequestException(message);
            }
        }

        private void GuardExceedMaxLen(string str, string message, int maxLen)
        {
            if (str.Length > maxLen)
            {
                throw new BadRequestException(message);
            }
        }

        private void GuardIncorrectDate(DateTime date, string message)
        {
            if (date == DateTime.MinValue)
            {
                throw new BadRequestException(message);
            }
        }

        private void GuardIncorrectStartEndDates(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new BadRequestException(ProjectMessageResource.StartMoreEnd);
            }
        }

        private void GuardPriority(int priority)
        {
            if (priority < 0)
            {
                throw new BadRequestException(ProjectMessageResource.IncorrectPriority);
            }
        }

        private void GuardIncorrectProjectData(Project project)
        {
            GuardNullArgument(project);

            GuardNullOrEmptyString(project.Name, ProjectMessageResource.IncorrectName);
            GuardExceedMaxLen(project.Name, ProjectMessageResource.NameProjectExceedsMaxLen, 100);

            GuardNullOrEmptyString(project.CustomerCompanyName, ProjectMessageResource.IncorrectCustomerCompany);
            GuardExceedMaxLen(project.CustomerCompanyName, ProjectMessageResource.NameCustomerCompanyExceedsMaxLen, 100);

            GuardNullOrEmptyString(project.ExecutorCompanyName, ProjectMessageResource.IncorrectExecutorCompany);
            GuardExceedMaxLen(project.ExecutorCompanyName, ProjectMessageResource.NameExecutorCompanyExceedsMaxLen, 100);

            GuardIncorrectDate(project.StartDate, ProjectMessageResource.IncorrectStartdate);
            GuardIncorrectDate(project.EndDate, ProjectMessageResource.IncorrectEndDate);
            GuardIncorrectStartEndDates(project.StartDate, project.EndDate);

            GuardPriority(project.Priority); 
        }
    }
}
