using Chatroom.Models;
using Chatroom.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;

namespace Chatroom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatService chatService;

        public HomeController(ILogger<HomeController> logger, IChatService chatService)
        {
            _logger = logger;
            this.chatService = chatService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(ChatMessageViewModel))]
        public async Task<IActionResult> SendMessage(string message, int chatroomId)
        {
            try
            {
                await chatService.SendMessage(new ChatMessageViewModel
                {
                    ChatRoom = new ChatRoomViewModel { Id = chatroomId },
                    Content = message,
                    UserName = User.Identity.Name,
                });

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(List<ChatMessageViewModel>))]
        public async Task<IActionResult> GetMessage(int chatId)
        {
            try
            {
                return Ok(await chatService.GetMessages(chatId, User.Identity.Name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}