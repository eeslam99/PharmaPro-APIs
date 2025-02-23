﻿using AutoMapper;
using GraduationProjectAPI.BL;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMedicine imedicine;
        private readonly IShelfStatus shelfstatus;

        public MedicinesController(IMapper mapper,IMedicine Imedicine,IShelfStatus shelfstatus)
        {
            this.mapper = mapper;
            imedicine = Imedicine;
            this.shelfstatus = shelfstatus;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomResponse<IEnumerable<MedicineVM>> GetAll()
        {
            var data=imedicine.GetAll();
            if (data.Count() != 0)
            {
                var result = mapper.Map<IEnumerable<MedicineVM>>(data);
                return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 404, Data = null, Message = "Data Not Found" };

            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomResponse<MedicineVM> GetById(int id)
        {
            var data = imedicine.GetByID(id);
            if(data is not null)
            {
                var result = mapper.Map<MedicineVM>(data);
                return new CustomResponse<MedicineVM> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<MedicineVM> { StatusCode = 404, Data = null, Message = "Medicine Not Found" };

            }
        }

        [HttpPost]
        [Route("create")]
        public CustomResponse<MedicineVM> Create(MedicineVM medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Medicine>(medicine);
                    imedicine.Add(data);
                    return new CustomResponse<MedicineVM> { StatusCode = 200, Data = medicine, Message = "Medicine Added Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = "";
                   
                    message=string.Join(",", errors);
                    return new CustomResponse<MedicineVM> { StatusCode = 400, Data = null, Message = message };
                }
            }catch(Exception ex)
            {
                return new CustomResponse<MedicineVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }


        [HttpPut]
        [Route("Update")]
        public CustomResponse<MedicineVM> Update(MedicineVM medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Medicine>(medicine);
                    imedicine.Update(data);
                    return new CustomResponse<MedicineVM> { StatusCode = 200, Data = medicine, Message = "Medicine Updated Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = string.Join(",", errors);
                    return new CustomResponse<MedicineVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<MedicineVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomResponse<MedicineVM> Delete(int id)
        {
            try
            {


                var data = imedicine.GetByID(id);
                var result = mapper.Map<MedicineVM>(data);
                if (data is not null)
                {
                    imedicine.Delete(id);
                    return new CustomResponse<MedicineVM> { StatusCode = 200, Data = result, Message = "Medicine deleted successfully" };

                }
                else
                {
                    return new CustomResponse<MedicineVM> { StatusCode = 404, Data = null, Message = "Medicine Not Found" };

                }
            }catch(Exception ex)
            {
                return new CustomResponse<MedicineVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }
        [HttpGet("GetShelfNumbers")]
        public CustomResponse<IEnumerable<int>> GetShelfNumbers([FromQuery]int[] ids)
        {
            foreach (var item in ids)
            {
                var res=imedicine.GetByID(item);
                if(res is null)
                {
                    return new CustomResponse<IEnumerable<int>> { StatusCode = 400, Data = null, Message = $"id: {item} not found" };

                }
            }
            var data = imedicine.GetShelFNumbers(ids);

            return new CustomResponse<IEnumerable<int>> { StatusCode = 200, Data = data, Message = "ShelfNumbers Retreived Successfully" };
        }

        [HttpGet("GetSoonExpiredAndSoonOutOfStoock")]
        public CustomResponse<IEnumerable<MedicineVM>> GetSoonExpiredAndSoonOutOfStoock()
        {
            var data = imedicine.GetDangerData();
            if (data.Count() == 0)
            {
                var result = mapper.Map<IEnumerable<MedicineVM>>(data);
                return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 404, Data = null, Message = "No Soon Expired And Soon OutOfStoock data found"};

                }
        }

        [HttpGet("GetExpired")]
        public CustomResponse<IEnumerable<MedicineVM>> GetExpired()
        {
            var data = imedicine.GetExpired();
            if (data.Count() == 0)
            {
                var result = mapper.Map<IEnumerable<MedicineVM>>(data);
                return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };
            }
            else
            {
                return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 404, Data = null, Message = "No Data Found" };

            }
        }

        [HttpGet("GetOutOfStock")]
        public CustomResponse<IEnumerable<MedicineVM>> GetOutOfStock()
        {
            var data = imedicine.GetDangerData();
            if (data.Count() == 0)
            {
                var result = mapper.Map<IEnumerable<MedicineVM>>(data);
                return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };
            }
            return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 404, Data = null, Message = "No Data Found" };

        }
        [HttpGet("GetSoonToExpire")]
        public IActionResult GetSoonToExpire()
        {
            var soonToExpire = imedicine.GetexpiredSoon();
            if(soonToExpire.Count() == 0)
            {
                var result = mapper.Map<IEnumerable<MedicineVM>>(soonToExpire);

                return Ok(new CustomResponse<IEnumerable<MedicineVM>>
                {
                    StatusCode = 200,
                    Data = result,
                    Message = "Soon to expire medicines retrieved successfully."
                });
            }
            else
            {
                return Ok(
                    new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 404, Data = null, Message = "No Data Found" }
                );

            }

        }

        [HttpGet("GetSoonOutOfStock")]
        public IActionResult GetSoonOutOfStock()
        {

            var soonToExpire = imedicine.GetOutofStockSoon();
            if (soonToExpire.Count() == 0)
            {
                var result = mapper.Map<IEnumerable<MedicineVM>>(soonToExpire);

                return Ok(new CustomResponse<IEnumerable<MedicineVM>>
                {
                    StatusCode = 200,
                    Data = result,
                    Message = "Soon to expire medicines retrieved successfully."
                });
            }
            else
            {
                return Ok(
                    new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 404, Data = null, Message = "No Data Found" }
                );

            }
        }

        [HttpPost("SendOutStockToEsp")]
        public IActionResult SendOutStockToEsp()
        {
            var data = imedicine.GetOutofStock();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            shelfstatus.InsertForESP(result, "Red");
            return Ok(new { Message = "Data sent to ESP successfully" });
        }

        [HttpPost("SendExpiredToEsp")]
        public IActionResult SendExpiredToEsp()
        {
            var data = imedicine.GetExpired();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            shelfstatus.InsertForESP(result, "Red");
            return Ok(new { Message = "Data sent to ESP successfully" });
        }

        [HttpPost("SendSoonOutOfStockToEsp")]
        public IActionResult SendSoonOutOfStock()
        {
            var data = imedicine.GetOutofStockSoon();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            shelfstatus.InsertForESP(result, "Red");
            return Ok(new { Message = "Data sent to ESP successfully" });
        }

        [HttpPost("SendSoonToExpireToEsp")]
        public IActionResult SendSoonToExpireToEsp()
        {
            var data = imedicine.GetexpiredSoon();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            shelfstatus.InsertForESP(result, "Red");
            return Ok(new { Message = "Data sent to ESP successfully" });
        }

    }
}
