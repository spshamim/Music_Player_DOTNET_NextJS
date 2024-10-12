using Business_Layer.DTOs;
using Business_Layer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business_Layer.DTOs.UserDTO;

namespace App_Layer.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get()
        {
            try
            {
                var data = UserService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = UserService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("sn/{name}")]
        public HttpResponseMessage GetByn(string name)
        {
            try
            {
                var data = UserService.GetByN(name);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(CreateUserDTO obj)
        {
            try
            {
                var data = UserService.Create(obj);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(UpdateUserDTO obj)
        {
            try
            {
                var data = UserService.Update(obj);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var data = UserService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("reset-request")]
        public HttpResponseMessage GenCode(ResetRequestDTO obj)
        {
            try
            {
                var data = UserService.GenerateResetCode(obj.Email);
                return Request.CreateResponse(HttpStatusCode.OK, "If your email present in our system, you will receive link shortly.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "If your email present in our system, you will receive link shortly.");
            }
        }
        [HttpPost]
        [Route("reset-password")]
        public HttpResponseMessage ResPass(ResetPassDTO obj, string token)
        {
            try
            {
                var data = UserService.ResetPasswordEmail(obj.Email,obj.Password, token);
                if (data)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Password Reset Successfull.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid credentials.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
