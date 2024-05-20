using Business.CustomExceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public void AddTeam(Team team)
        {
            if(team == null) throw new EntityNullException("","Team object null exception");
            if (!team.PhotoFile.ContentType.Contains("image/")) throw new FileContentException("PhotoFile", "Content Type error!!!!!");
            string path = "C:\\Users\\ll Novbe\\source\\Exam_MVC\\Exam_MVC\\wwwroot\\Upload\\Team\\" + team.PhotoFile.FileName;
            using(FileStream stream=new FileStream(path,FileMode.Create))
            {
                team.PhotoFile.CopyTo(stream);
            }
            team.ImgUrl = team.PhotoFile.FileName;
            _teamRepository.Add(team);
            _teamRepository.Commit();
        }

        public void DeleteTeam(int id)
        {
            var getTeam=_teamRepository.Get(x=>x.Id == id);
            if (getTeam == null) throw new EntityNullException("","Team object null exception");
            string path = "C:\\Users\\ll Novbe\\source\\Exam_MVC\\Exam_MVC\\wwwroot\\Upload\\Team\\" + getTeam.ImgUrl;
            if (!File.Exists(path)) throw new EntityNullException("", "Team object null exception");
            FileInfo fileInfo = new FileInfo(path);
            fileInfo.Delete();
            _teamRepository.Delete(getTeam);
            _teamRepository.Commit();
        }

        public List<Team> GetAllTeams(Func<Team, bool>? func = null)
        {
            return _teamRepository.GetAll(func);
        }

        public Team GetTeam(Func<Team, bool>? func = null)
        {
            return _teamRepository.Get(func);
        }

        public void UpdateTeam(int id, Team team)
        {
           var oldTeam= _teamRepository.Get(x=> x.Id == id);
           if(oldTeam == null) throw new EntityNullException("","ss");
           if(oldTeam != null)
            {
                string path2= "C:\\Users\\ll Novbe\\source\\Exam_MVC\\Exam_MVC\\wwwroot\\Upload\\Team\\" + oldTeam.ImgUrl;
                FileInfo fileInfo = new FileInfo(path2);
                fileInfo.Delete();
                string path= "C:\\Users\\ll Novbe\\source\\Exam_MVC\\Exam_MVC\\wwwroot\\Upload\\Team\\" + team.PhotoFile.FileName;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    team.PhotoFile.CopyTo(stream);
                }

                oldTeam.ImgUrl = team.PhotoFile.FileName;
            }

              oldTeam.Name = team.Name;
             oldTeam.Description = team.Description;
            oldTeam.Position = team.Position;
            _teamRepository.Commit();
        }
    }
}
