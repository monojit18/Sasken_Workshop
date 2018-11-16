using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using EasyPostApp.Models;
using EasyPostApp.Services;

namespace EasyPostApp.Controllers
{
    public class PostsController : ApiController
    {

        private PostServices _postServices;

        public PostsController()
        {

            _postServices = new PostServices();

        }
       
        [HttpGet]
        public async Task<List<PostModel>> Get()
        {

            var postsList = await _postServices.GetPostsAsync(null);            
            return postsList;

        }
       
        [HttpGet]
        public async Task<List<PostModel>> Get(string id)
        {

            var postsList = await _postServices.GetPostsAsync(id);
            return postsList;
            
        }

            
        [HttpPost]
        public async Task<bool> Post(string id, [FromBody]PostModel postModel)
        {

            var couldUpdate = await _postServices.UpdatePostAsync(id, postModel);
            return couldUpdate;

        }
       
        [HttpPut]
        public async Task Put([FromBody]PostModel postModel)
        {

            await _postServices.InsertPostAsync(postModel);

        }
               
        [HttpDelete]
        public async Task<bool> Delete([FromUri] string id)
        {


            var couldDelete = await _postServices.DeletePostAsync(id);
            return couldDelete;


        }
    }
}
