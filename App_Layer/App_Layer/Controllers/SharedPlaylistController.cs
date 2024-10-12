using App_Layer.Auth;
using Business_Layer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business_Layer.DTOs.PlaylistSongDTO;
using static Business_Layer.DTOs.SharedPlaylistDTO;

namespace App_Layer.Controllers
{
    //[Logged]
    [RoutePrefix("api/sharedplaylist")]
    public class SharedPlaylistController : ApiController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AddSharePlaylistDTO obj)
        {
            try
            {
                var data = SharedPlaylistService.Create(obj);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("show/{id}")]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var data = SharedPlaylistService.GetWithNav(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage View(string id)
        {
            try
            {
                var data = SharedPlaylistService.GetWithNav2(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("showall")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = SharedPlaylistService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
