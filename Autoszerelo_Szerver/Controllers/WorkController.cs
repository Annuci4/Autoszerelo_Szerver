using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autoszerelo_Szerver.Models;
using Autoszerelo_Szerver.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoszerelo_Szerver.Controllers
{
    [Route("api/car_mechanic_work")]
    [ApiController]
    public class WorkController : ControllerBase
    {

        //work tömb tagjait adja vissza.
        [HttpGet]
        public ActionResult<IEnumerable<Work>> Get()
        {

            var works = WorkRepository.GetWorks();
            return Ok(works);
        }

        //Egy Bizonyos work-t kérjünk le!
        [HttpGet("{Id}")]
        public ActionResult<Work> Get(long id)
        {
            var works = WorkRepository.GetWorks();

            var work = works.FirstOrDefault(x => x.Id == id);

            if (work != null)
            {
                return Ok(work);
            }
            else
            {
                return NotFound();
            }
        }

        //Create metódus.
        [HttpPost]
        public ActionResult Post(Work work)
        {
            var works = WorkRepository.GetWorks();

            var newId = GetNewId(works);
            work.Id = newId;

            works.Add(work);
            WorkRepository.StoreWorks(works);

            return Ok();
        }
        //Update metódus
        [HttpPut]
        public ActionResult Put(Work work)
        {
            var works = WorkRepository.GetWorks();

            var oldWork = works.FirstOrDefault(x => x.Id == work.Id);

            if (oldWork != null)
            {
                oldWork.LastName = work.LastName;
                oldWork.FirstName = work.FirstName;
                oldWork.Date = work.Date;
                oldWork.TypeOfCar = work.TypeOfCar;
                oldWork.CarLicencePlate = work.CarLicencePlate;
                oldWork.Issues = work.Issues;
                oldWork.StateOfWork = work.StateOfWork;
            }
            else
            {
                var newId = GetNewId(works);
                work.Id = newId;
                works.Add(work);
            }

            WorkRepository.StoreWorks(works);
            return Ok();

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var works = WorkRepository.GetWorks();
            var work = works.FirstOrDefault(x => x.Id == id);

            if (work != null)
            {
                works.Remove(work);
                WorkRepository.StoreWorks(works);
                return Ok();
            }

            return NotFound();
        }

        //Meglevő work tömbhöz kitalál egy olyan id-t ami még nem létezik
        private long GetNewId(IList<Work> works)
        {
            long id = 0;

            foreach (var work in works)
            {
                if (id < work.Id)
                {
                    id = work.Id;
                }
            }
            return id + 1;
        }
    }
}