using AutoMapper;
using Avans.Core.Mappers;
using Avans.DAL.Concrete;
using Avans.DTOs;
using Avans.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.BLL.Concrete
{
    public class ProjectService
    {
        ProjectRepository projectRepository;
        private readonly MyMapper<Project, ProjectSelectDTO> _projectMapper;
        public ProjectService(ProjectRepository projectRepository, MyMapper<Project, ProjectSelectDTO> projectMapper)
        {
            this.projectRepository = projectRepository;
            _projectMapper = projectMapper;
        }

        public List<ProjectSelectDTO> GetAll()
        {
            List<ProjectSelectDTO> projectDTOs = new List<ProjectSelectDTO>();
            try
            {
                List<Project> projects = projectRepository.GetAll().ToList();

                projectDTOs = _projectMapper.Map<List<Project>, List<ProjectSelectDTO>>(projects);

                //projectDTOs = _projectMapper.Map<List<Project>, List<ProjectSelectDTO>>(projects); // Entity listesini DTO listesine maple
            }
            catch (Exception ex)
            {
                // Hata yönetimi
            }

            return projectDTOs;
        }
    }
}
