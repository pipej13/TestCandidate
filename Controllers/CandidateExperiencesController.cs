using Microsoft.AspNetCore.Mvc;
using Candidates.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Candidates.Controllers
{
    public class CandidateExperiencesController : Controller
    {
        private readonly CandidateFactoryContext _candidateFactoryContext;

        public CandidateExperiencesController(CandidateFactoryContext candidateFactoryContext)
        {
            _candidateFactoryContext = candidateFactoryContext;
        }
        public async Task<IActionResult> ListExperiences()
        {
            List<CandidateEsperiences> list = await _candidateFactoryContext.CandidateEsperiences.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> ListExperiencesCandidate(int idCandidate)
        {
            List<Candidate> candidate = await _candidateFactoryContext.Candidates.Where(x => x.IdCandidate == idCandidate).ToListAsync();
            if (candidate == null)
            {
                ViewBag.NombreCandidate = "";
            }
            else
            {
                ViewBag.NombreCandidate = candidate.First().Name + " " + candidate.First().Surname;
            }


            List<CandidateEsperiences> listCandidateExperiences = await _candidateFactoryContext.CandidateEsperiences.Where(x => x.IdCandidate == idCandidate).ToListAsync();

            ViewBag.IdCandidate = idCandidate;
            return View(listCandidateExperiences);
        }

        [HttpGet]
        public async Task<IActionResult> NewCandidateExperience(int idCandidate)
        {
            //ViewBag.CandidatesList = await _candidateFactoryContext.Candidates.ToListAsync();
            Candidate candidate = await _candidateFactoryContext.Candidates.FirstAsync(x => x.IdCandidate == idCandidate);
            ViewBag.IdCandidate = candidate.IdCandidate;
            ViewBag.NombreCandidate = candidate.Name + " " + candidate.Surname;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewCandidateExperience(CandidateEsperiences candidateExperience)
        {
            candidateExperience.InsertDate = DateTime.Now;
            await _candidateFactoryContext.CandidateEsperiences.AddAsync(candidateExperience);
            await _candidateFactoryContext.SaveChangesAsync();
            return RedirectToAction("ListExperiencesCandidate", new { idCandidate = candidateExperience.IdCandidate });
        }

        [HttpGet]
        public async Task<IActionResult> EditCandidateExperience(int idExperience)
        {
            CandidateEsperiences candidateExperience = await _candidateFactoryContext.CandidateEsperiences.FirstAsync(x => x.IdCandidateExperience == idExperience);
            ViewBag.IdCandidate = candidateExperience.IdCandidate;

            Candidate candidate = await _candidateFactoryContext.Candidates.FirstAsync(x => x.IdCandidate == candidateExperience.IdCandidate);
            @ViewBag.NombreCandidate = candidate.Name + " " + candidate.Surname;

            return View(candidateExperience);
        }

        [HttpPost]
        public async Task<IActionResult> EditCandidateExperience(CandidateEsperiences candidateExperience)
        {
            candidateExperience.ModifyDate = DateTime.Now;
            _candidateFactoryContext.CandidateEsperiences.Update(candidateExperience);
            await _candidateFactoryContext.SaveChangesAsync();
            return RedirectToAction("ListExperiencesCandidate", new { idCandidate = candidateExperience.IdCandidate });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCandidateExperience(int IdCandidateExperience)
        {
            CandidateEsperiences candidateEsperiences = await _candidateFactoryContext.CandidateEsperiences.FirstAsync(x => x.IdCandidateExperience == IdCandidateExperience);

            _candidateFactoryContext.CandidateEsperiences.Remove(candidateEsperiences);
            await _candidateFactoryContext.SaveChangesAsync();
            return RedirectToAction("ListExperiencesCandidate", new { idCandidate = candidateEsperiences.IdCandidate });
        }
    }
}
