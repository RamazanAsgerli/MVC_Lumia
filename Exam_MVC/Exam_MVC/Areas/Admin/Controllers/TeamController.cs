using Business.CustomExceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Mvc;

namespace Exam_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamservice;

        public TeamController(ITeamService teamservice)
        {
            _teamservice = teamservice;
        }

        public IActionResult Index()
        {
            List<Team> teams = _teamservice.GetAllTeams();
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _teamservice.AddTeam(team);
            }
            catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.V,ex.Message);
                return View();
            }
            catch(FileContentException ex)
            {
                ModelState.AddModelError(ex.V,ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)  return View(); 
            try
            {
                _teamservice.DeleteTeam(id);
            }
            catch (EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var team = _teamservice.GetTeam(x => x.Id == id);
            if(team == null)
            {
                ModelState.AddModelError("", "ss");
                return RedirectToAction("Index");
            }
            return View(team);
        }

        [HttpPost]
        public IActionResult Update(Team team)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _teamservice.UpdateTeam(team.Id, team);
            }
            catch (EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
