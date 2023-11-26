using EventTicket.Entities;
using EventTicket.Models;
using EventTicket.Repository.DBContext;
using EventTicket.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EventTicket.Repository.User
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;
		private readonly IUploadService _uploadService;
		private readonly IMailService _mailService;

		public UserRepository(AppDbContext context, IUploadService uploadService, IMailService mailService)
		{
			_context = context;
			_uploadService = uploadService;
			_mailService = mailService;
		}

		public async Task<List<Entities.User>> GetAll()
		{
			var users = await _context.Users.ToListAsync();
			users.ForEach(x => x.Avatar = _uploadService.GetFullPath(x.Avatar));

			return users;
		}

		public async Task<Entities.User> GetById(long id)
		{
			var user = await _context.Users
				.Include(x => x.UserTickets)
				.ThenInclude(x => x.Event)
				.Include(x => x.Events)
				.ThenInclude(x => x.UserTickets)
				.FirstOrDefaultAsync(x => x.Id == id);
			user.Avatar = _uploadService.GetFullPath(user.Avatar);
			//foreach (var i in user.UserTickets)
			//{
			//	var a = _uploadService.GetFullPath(i.Event.Image);
			//	if (!i.Event.Image.Contains("/"))
			//		i.Event.Image = a;
			//}
			return user;
		}

		public async Task<Entities.User> Login(LoginVM request)
		{
			var u = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName || x.Email == request.UserName);
			if (u == null)
				return null;
			if (u.Password == Encrypt(request.Password))
			{
				u.Avatar = _uploadService.GetFullPath(u.Avatar);
				return u;
			}
			return null;
		}

		private static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";

		private static string Encrypt(string text)
		{
			using var md5 = new MD5CryptoServiceProvider();
			using var tdes = new TripleDESCryptoServiceProvider();
			tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
			tdes.Mode = CipherMode.ECB;
			tdes.Padding = PaddingMode.PKCS7;

			using (var transform = tdes.CreateEncryptor())
			{
				byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
				byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
				return Convert.ToBase64String(bytes, 0, bytes.Length);
			}
		}

		public async Task<bool> Register(RegisterVM request)
		{
			var u = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName || x.Email == request.Email);
			if (u != null)
				return false;

			var user = new Entities.User()
			{
				Email = request.Email,
				Name = request.Name,
				UserName = request.UserName,
				Password = Encrypt(request.Password),
				Role = 1,
				Avatar = "default-user.png",
			};

			await _context.Users.AddAsync(user);

			var res = await _context.SaveChangesAsync() > 0;

			_mailService.SendMail(new CreateMailRequest()
			{
				Content = "Bạn đã đăng ký tài khoản thành công với email: " + request.Email,
				Title = "Đăng ký thành công",
				Email = request.Email,
				Name = request.Name,
			});
			return res;
		}

		public async Task<bool> Toggle(long id)
		{
			var u = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

			u.Status = !u.Status;

			return await _context.SaveChangesAsync() > 0;
		}

		public async Task EditInfoUser(UserEditVM vm)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == vm.Id);

			user.Name = vm.Name;
			user.Address = vm.Address;
			user.Phone = vm.Phone;
			if (!string.IsNullOrEmpty(vm.Password))
				user.Password = Encrypt(vm.Password);
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task EditAvatarUser(UserEditAvatar vm)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == vm.Id);
			var avatar = user.Avatar;
			if (vm.Avatar != null)
			{
				user.Avatar = await _uploadService.SaveFile(vm.Avatar);
			}
			_context.Users.Update(user);
			var success = await _context.SaveChangesAsync() > 0;
			if (success && !avatar.Contains("default"))
				await _uploadService.DeleteFile(avatar);
		}

		public async Task RegisterEvent(RegisterEventVM vm)
		{
			var user = await _context.Users.Include(x => x.UserTickets).FirstOrDefaultAsync(x => x.Id == vm.UserId);

			user.UserTickets ??= new List<UserTicket>();

			user.UserTickets.Add(new UserTicket()
			{
				Address = vm.Address,
				Email = vm.Email,
				Phone = vm.Phone,
				Name = vm.Name,
				EventId = vm.EventId,
				CreatedAt = DateTime.Now,
			});

			_context.Users.Update(user);
			await _context.SaveChangesAsync();

			var ev = await _context.Events.FirstOrDefaultAsync(x => x.Id == vm.EventId);

			_mailService.SendMail(new CreateMailRequest()
			{
				Content = "Bạn đã đăng ký thành công sự kiện " + ev.Name + " vào ngày " + DateTime.Now,
				Title = "Đăng ký thành công",
				Email = vm.Email,
				Name = vm.Name,
			});
		}
	}
}