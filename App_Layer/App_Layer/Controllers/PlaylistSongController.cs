using App_Layer.Auth;
using Business_Layer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business_Layer.DTOs.PlaylistSongDTO;
using static Business_Layer.DTOs.SongDTO;

namespace App_Layer.Controllers
{

    //[Logged]
    [RoutePrefix("api/playlistsong")]
    public class PlaylistSongController : ApiController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(AddSongToPlaylistSongDTO obj)
        {
            try
            {
                var data = PlaylistSongService.Create(obj);
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
                var data = PlaylistSongService.GetWithNav(id);
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
                var data = PlaylistSongService.GetWithNav2(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}/shuffle")]
        public HttpResponseMessage GetShuffled(string id)
        {
            try
            {
                var data = PlaylistSongService.GetShuffledSongs(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{playlistName}/next/{currentSongId}")]
        public HttpResponseMessage GetNextSong(string playlistName, int currentSongId, bool repeatSong = false, bool repeatPlaylist = false)
        {
            try
            {
                var data = PlaylistSongService.GetNextSong(playlistName, currentSongId, repeatSong, repeatPlaylist);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage ViewALL()
        {
            try
            {
                var data = PlaylistSongService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
