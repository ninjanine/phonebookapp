﻿using System;
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
            if (products == null)
            {
                return NotFound();

            }
            return Ok(products);
        }

        [HttpGet("{id}", Name = "PhoneBookRoute")]
        public async Task<ActionResult<PhoneBook>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var product = await _productRepository.Get(id);
            return Ok(product);
        }


        [HttpPost]
        public ActionResult<PhoneBook> Create(PhoneBook phoneBook)
        {
            if (phoneBook == null)
            {
                return BadRequest();
            }
            _productRepository.Create(phoneBook);

            return CreatedAtRoute("PhoneBookRoute", new { id = phoneBook.Id.ToString() }, phoneBook);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, PhoneBook phoneBookIn)
        {
            if (id == Guid.Empty) {
                return NotFound();
            }

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
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var phoneBook = _productRepository.Get(id);

            if (phoneBook == null)
            {
                return NotFound();
            }

            _productRepository.Delete(id);

            return Ok();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<PhoneBook>>> Search(string name) {
            if (String.IsNullOrEmpty(name))
            {
                return NoContent();
            }

            var searchResult = await _productRepository.Search(name);

            return Ok(searchResult);
        }

    }

}
