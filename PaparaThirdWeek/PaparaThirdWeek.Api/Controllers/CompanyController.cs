﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PaparaThirdWeek.Services.Concretes;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;


namespace PaparaThirdWeek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        [HttpGet("Companies")]
        public async Task<IActionResult> Get()
        {
            var result = await _companyService.GetAll();
            return Ok(result);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> Get(int id, ICompanyService _companyService)
        {
            var result = await _companyService.Get(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, CompanyDto company)
        {
            var _company = await _companyService.Get(id);
            if (_company == null)
            {
                return BadRequest();
            }

            var updatedCompany = _mapper.Map<Company>(company);
            updatedCompany.Id = _company.Id;
            updatedCompany.IsDeleted = _company.IsDeleted;
            updatedCompany.CreatedDate = _company.CreatedDate;
            updatedCompany.LastUpdateBy = "Tuğba";
            updatedCompany.LastUpdateAt = DateTime.Now;

            await _companyService.Update(updatedCompany, id);
            return Ok("Success");
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyDto company)
        {
            var newCompany = _mapper.Map<Company>(company);
            newCompany.CreatedBy = "Samet";
            newCompany.CreatedDate = DateTime.Now;
            newCompany.IsDeleted = "false";
            newCompany.LastUpdateAt = DateTime.Now;

            await _companyService.Add(newCompany);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.Delete(id);
            return Ok();
        }
    }
}
