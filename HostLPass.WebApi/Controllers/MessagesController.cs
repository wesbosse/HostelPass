using HostLPass.Core.Domain;
using HostLPass.Core.Infrastructure;
using HostLPass.Core.Repository;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace HostLPass.WebApi.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessagesController(IMessageRepository messageRepository, IUnitOfWork unitOfWork, IHostLUserRepository userRepository) : base(userRepository)
        {
            _unitOfWork = unitOfWork;
            _messageRepository = messageRepository;
        }

        // GET: api/Messages
        [Authorize]
        public IQueryable<Message> GetMessages()
        {
            return _messageRepository.Where(h => h.UserId == CurrentUser.Id);
        }

        // POST: api/Messages
        [Authorize]
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _messageRepository.Add(message);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = message.MessageId }, message);
        }

        // DELETE: api/Messages/5
        [Authorize(Roles="Admin")]
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = _messageRepository.GetById(id);
            if (message == null)
            {
                return NotFound();
            }

            _messageRepository.Delete(message);
            _unitOfWork.Commit();

            return Ok(message);
        }
    }
}