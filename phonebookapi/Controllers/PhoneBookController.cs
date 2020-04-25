using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhonebookApi.Models;
using PhonebookApi.Repository;

namespace PhonebookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookRepository _productRepository;

        public PhoneBookController(IPhoneBookRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneBook>>> Get()
        {
            var products = await _productRepository.Get();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "PhoneBookRoute")]
        public async Task<ActionResult<PhoneBook>> Get(Guid id)
        {
            var product = await _productRepository.Get(id);
            return Ok(product);
        }


        [HttpPost]
        public ActionResult<PhoneBook> Create(PhoneBook phoneBookIn)
        {
            _productRepository.Create(phoneBookIn);

            return CreatedAtRoute("PhoneBookRoute", new { id = phoneBookIn.Id.ToString() }, phoneBookIn);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, PhoneBook phoneBookIn)
        {
            var phoneBook = _productRepository.Get(id);

            if (phoneBook == null)
            {
                return NotFound();
            }

            _productRepository.Update(id, phoneBookIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var phoneBook = _productRepository.Get(id);

            if (phoneBook == null)
            {
                return NotFound();
            }

            _productRepository.Delete(id);

            return Ok();
        }

    }

}
