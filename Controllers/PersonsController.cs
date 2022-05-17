using Microsoft.AspNetCore.Mvc;

using Persons.Controllers.DTO;
using Persons.Services.Interfaces;

namespace Persons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonsController(IPersonService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получение человека по идентификатору
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetPersonById([FromRoute] int id)
        {
            
            return Ok(_service.GetById(id));
        }

        /// <summary>
        /// Поиск человека по имени
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public IActionResult GetPersonByName([FromQuery] string searchTerm)
        {
            return Ok(_service.GetByName(searchTerm));
        }

        /// <summary>
        /// Получение списка людей с пагинацией
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPersonsListByPage([FromQuery] int skip, [FromQuery] int take)
        {
            return Ok(_service.GetItemsList(skip, take));
        }

        /// <summary>
        /// Добавление новой персоны в коллекцию
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonDto person)
        {
            return Ok(_service.AddItem(person));
        }


        /// <summary>
        /// Обновление существующей персоны в коллекции
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdatePerson([FromBody] PersonDto person)
        {
            return Ok(_service.UpdateItem(person));
        }

        /// <summary>
        /// Удаление персоны из коллекции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePerson([FromRoute] int id)
        {
            return Ok(_service.DeleteItem(id));
        }
    }
}
