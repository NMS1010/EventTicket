﻿using EventTicket.Entities;
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

		public UserRepository(AppDbContext context, IUploadService uploadService)
		{
			_context = context;
			_uploadService = uploadService;
		}

		public async Task<List<Entities.User>> GetAll()
		{
			var users = await _context.Users.ToListAsync();
			users.ForEach(x => x.Avatar = _uploadService.GetFullPath(x.Avatar));

			return users;
		}

		public async Task<Entities.User> GetById(long id)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
			user.Avatar = _uploadService.GetFullPath(user.Avatar);

			return user;
		}

		public async Task<Entities.User> Login(LoginVM request)
		{
			var u = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName || x.Email == request.UserName);
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
				Role = 1
			};

			await _context.Users.AddAsync(user);

			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> Toggle(long id)
		{
			var u = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

			u.Status = !u.Status;

			return await _context.SaveChangesAsync() > 0;
		}
	}
}