using Microsoft.AspNetCore.Mvc;
using Candidates.Data;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Controllers
{
    public class CandidateController : Controller
    {
        private readonly CandidateFactoryContext _candidateFactoryContext;
        
        public CandidateController(CandidateFactoryContext candidateFactoryContext)
        {
            _candidateFactoryContext = candidateFactoryContext;
        }

        public async Task<IActionResult> List()
        {
            List<Candidate> list = await _candidateFactoryContext.Candidates.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult NewCandidate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewCandidate(Candidate candidate)
        {
            candidate.InsertDate = DateTime.Now;
            await _candidateFactoryContext.Candidates.AddAsync(candidate);
            await _candidateFactoryContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> EditCandidate(int idCandidate)
        {
            Candidate candidate = await _candidateFactoryContext.Candidates.FirstAsync(x => x.IdCandidate == idCandidate);
            return View(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> EditCandidate(Candidate candidate)
        {
            candidate.ModifyDate = DateTime.Now;
            _candidateFactoryContext.Candidates.Update(candidate);
            await _candidateFactoryContext.SaveChangesAsync();            
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCandidate(int idCandidate)
        {
            Candidate candidate = await _candidateFactoryContext.Candidates.FirstAsync(x => x.IdCandidate == idCandidate);
            List<CandidateEsperiences> listCandidateExperiences = await _candidateFactoryContext.CandidateEsperiences.Where(x => x.IdCandidate == idCandidate).ToListAsync();

            foreach (var item in listCandidateExperiences)
            {
                _candidateFactoryContext.CandidateEsperiences.Remove(item);
            }

            _candidateFactoryContext.Candidates.Remove(candidate);
            await _candidateFactoryContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
