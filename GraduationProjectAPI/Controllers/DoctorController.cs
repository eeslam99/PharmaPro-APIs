using AutoMapper;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.BL;
using GraduationProjectAPI.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraduationProjectAPI.BL.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GraduationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IDoctor IDoctor;
        private readonly UserManager<IdentityUser> userManager;

        public DoctorController(IMapper mapper, IDoctor IDoctor,UserManager<IdentityUser> userManager)
        {
            this.mapper = mapper;
            this.IDoctor = IDoctor;
            this.userManager = userManager;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomResponse<IEnumerable<DoctorVM>> GetAll()
        {
            var data = IDoctor.GetAll();
            if (data.Count() != 0)
            {
                var result = mapper.Map<IEnumerable<DoctorVM>>(data);
                return new CustomResponse<IEnumerable<DoctorVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<IEnumerable<DoctorVM>> { StatusCode = 200, Data = null, Message = "Data Not Foind" };

            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomResponse<DoctorVM> GetById(int id)
        {
            var data = IDoctor.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<DoctorVM>(data);
                return new CustomResponse<DoctorVM> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<DoctorVM> { StatusCode = 404, Data = null, Message = "Doctor Not Found" };

            }
        }

        [HttpPost]
        [Route("create")]
        public CustomResponse<DoctorVM> Create(DoctorVM Doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Doctor>(Doctor);
                    IDoctor.Add(data);
                    return new CustomResponse<DoctorVM> { StatusCode = 200, Data = Doctor, Message = "Doctor Added Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = "";
                  
                    message = string.Join(",", errors); return new CustomResponse<DoctorVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<DoctorVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }


        [HttpPut]
        [Route("Update")]
        public CustomResponse<DoctorVM> Update(DoctorVM Doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Doctor>(Doctor);
                    IDoctor.Update(data);
                    return new CustomResponse<DoctorVM> { StatusCode = 200, Data = Doctor, Message = "Doctor Updated Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = string.Join(",", errors);
                    return new CustomResponse<DoctorVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<DoctorVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<CustomResponse<DoctorVM>> Delete(int id)
        {
            try
            {


                var data = IDoctor.GetById(id);

                var result = mapper.Map<DoctorVM>(data);
                if (data is not null)
                {
                    var user = await userManager.FindByEmailAsync(data.Email);
                   
                    IDoctor.Delete(id);
                    if (user is not null)
                    {
                        await userManager.DeleteAsync(user);
                    }

                    return new CustomResponse<DoctorVM> { StatusCode = 200, Data = result, Message = "Doctor deleted successfully" };

                }
                else
                {
                    return new CustomResponse<DoctorVM> { StatusCode = 404, Data = null, Message = "Doctor Not Found" };

                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<DoctorVM> { StatusCode = 500, Data = null, Message = ex.Message };
            }

}
    }
}

