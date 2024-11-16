using CrazyMusicians.Models;
using CrazyMusicians.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly static List<Musician> _musicians = [
            new Musician  { Name ="Ahmet Çalgı", Job = "Ahmet Çalgı Çalar", FunFeature = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
            new Musician  { Name ="Zeynep Melodi", Job = "Popüler Melodi Yazarı", FunFeature = "Şarkıları yanlış anlaşılır ama çok popüler" },
            new Musician  { Name ="Cemil Akor", Job = "Çılgın Akorist", FunFeature = "Akorları sık değiştirir ama şaşırtıcı derecede yetenekli" },
            new Musician  { Name ="Fatma Nota", Job = "Süpriz Nota Üreticisi", FunFeature = "Nota üretirken sürekli süprizler hazırlar" },
            new Musician  { Name ="Hasan Ritim", Job = "Ritim Canavarı", FunFeature = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
            new Musician  { Name ="Elif Armoni", Job = "Armoni Ustası", FunFeature = "Armonilerini bazen yanlış çalar ama çok yaratıcı" },
            new Musician  { Name ="Ali Perde", Job = "Perde Uygulayıcı", FunFeature = "Her perdeyi farklı şekilde çalar, her zaman süprizlidir" },
            new Musician  { Name ="Ayşe Rezonans", Job = "Rezonans Uzmanı", FunFeature = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır" },
            new Musician  { Name ="Murat Ton", Job = "Tonlama Meraklısı", FunFeature = "Tonlamalardaki farklılıklar bazen komik, ama oldukça ilginç" },
            new Musician  { Name ="Akor Sihirbazı", Job = "Akor Sihirbazı", FunFeature = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır" }
        ];

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_musicians);
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var musicians = _musicians.FirstOrDefault(x => x.Id == id);
            if (musicians == null)return NotFound();


            return Ok(musicians);
        }

        [HttpPost]
        public IActionResult Post([FromBody] MusicianDTO musicianDTO)
        {
            var musician = new Musician()
            {
                FunFeature = musicianDTO.FunFeature,
                IsDeleted = musicianDTO.IsDeleted.Value,
                Name = musicianDTO.Name,
                Job = musicianDTO.Job,
            };

            _musicians.Add(musician);

            return CreatedAtAction(nameof(Get), new { id = musician.Id }, musician);
        }

        [HttpPatch("{id}")]
        public IActionResult Put(int id, [FromBody] MusicianDTO newMusician)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (musician == null) return NotFound();

            musician.Name = newMusician.Name;
            musician.Job = newMusician.Name;
            musician.FunFeature = newMusician.Name;
            musician.IsDeleted = newMusician.IsDeleted == null ? false : newMusician.IsDeleted.Value;

            return Ok(musician);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            var musicians = _musicians.Where(x => x.FunFeature.Contains(keyword) || x.Job.Contains(keyword) || x.Name.Contains(keyword)).ToList();

            if (musicians.Count == 0) return NotFound();

            return Ok(musicians);
        }

        [HttpPut]
        public IActionResult Patch(int id, [FromBody] MusicianDTO request)
        {
            if (request == null) return BadRequest();

            var musician = _musicians.FirstOrDefault(x => x.Id == id);


            if (musician == null) return NotFound();

            musician.Name = string.IsNullOrWhiteSpace(request.Name) ? musician.Name : request.Name;
            musician.Job = string.IsNullOrWhiteSpace(request.Name) ? musician.Name : request.Name;
            musician.FunFeature = string.IsNullOrWhiteSpace(request.Name) ? musician.Name : request.Name;
            musician.IsDeleted = request.IsDeleted == null ? musician.IsDeleted : request.IsDeleted.Value;

            return Ok(musician);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician == null) return NotFound();

            musician.IsDeleted = true;

            return Ok(musician);
        }
    }
}
