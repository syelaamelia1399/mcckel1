using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Handlers;
using API_SystemSekolah.Context;
using API_SystemSekolah.Models;
using API_SystemSekolah.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API_SystemSekolah.Repositories.Data
{
	public class GuruRepository
	{
		private MyContext myContext;

		public IConfiguration _configuration;

		public GuruRepository(MyContext context, IConfiguration config)
		{
			myContext = context;
			_configuration = config;
		}

		public IEnumerable<Guru>Get()
		{
			return myContext.Gurus.ToList();
		}

		public Guru GetById(int id)
		{
			return myContext.Gurus.Find(id);
		}

		public int Create(Guru guru, int Roles, string retypePassword)
		{
			var dataEmail = myContext.Gurus.SingleOrDefault(x => x.Email.Equals(guru.Email) || x.NIP.Equals(guru.NIP));
			if(dataEmail == null)
			{
				if(guru.Password == retypePassword)
				{
					Guru data_guru = new Guru()
					{
						Name = guru.Name,
						Gender = guru.Gender,
						Address = guru.Address,
						Date_Of_Birth = guru.Date_Of_Birth,
						Place_Of_Birth = guru.Place_Of_Birth,
						NoTelp = guru.NoTelp,
						Email = guru.Email,
						NIP = guru.NIP,
						Password = Hashing.HashPassword(guru.Password)
					};

					myContext.Gurus.Add(data_guru);
					var result = myContext.SaveChanges();
					if(result > 0)
					{
						var id = myContext.Gurus.SingleOrDefault(x => x.Email.Equals(guru.Email)).Id;
						GuruRoles guruRoles = new GuruRoles()
						{
							Id_Guru = id,
							Id_Roles = Roles,
						};

						myContext.GuruRoless.Add(guruRoles);
						var resultGuru = myContext.SaveChanges();
						if (resultGuru > 0)
							return 2;
					}
					return 2;
				}
				return 1;
			}
			
			return 0;
		}

		public int Update(UpdateGuruVM updateGuru)
		{
			var data = myContext.Gurus.Find(updateGuru.Id);
			if(data != null)
			{
				data.Name = updateGuru.Name;
				data.Gender = updateGuru.Gender;
				data.Place_Of_Birth = updateGuru.Place_Of_Birth;
				data.Date_Of_Birth = updateGuru.Date_Of_Birth;
				data.Address = updateGuru.Address;
				data.NoTelp = updateGuru.NoTelp;
				data.Email = updateGuru.Email;
				data.NIP = updateGuru.NIP;

                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                return result;

            }
			return 0;
			
		}

		public int Delete(int id)
		{
			var data = GetById(id);
			myContext.Set<Guru>().Remove(data);
			var result = myContext.SaveChanges();
			return result;
		}

		public string Login(string email, string password)
		{
			var data = myContext.GuruRoless.Include(x => x.Gurus).Include(x => x.Roless).SingleOrDefault(x => x.Gurus.Email.Equals(email));
			if (data != null)
			{
				if(Hashing.ValidatePassword(password, data.Gurus.Password))
				{
					var claims = new[]
					{
						new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
						new Claim("guruId", data.Gurus.Id.ToString()),
						new Claim("name", data.Gurus.Name),
						new Claim("email", data.Gurus.Email),
						new Claim("role", data.Roless.Name)
					};

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
					var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var result = new JwtSecurityTokenHandler().WriteToken(token);

                    return result;
                }
				return null;
			}
			return null;
		}

		public int CheckResetPassword(string name, string email, string noTelp, string DateofBirth )
		{
			var data = myContext.Gurus.SingleOrDefault(x => x.Name.Equals(name) && x.Email.Equals(email) && x.NoTelp.Equals(noTelp) && x.Date_Of_Birth.Equals(DateofBirth));

			if( data != null)
			{
				var result = data.Id;

				return result;
			}

			return 0;
		}

		public int NewPassword(int id, string password, string retypePassword)
		{
			try
			{
				var data = myContext.Gurus.Find(id);

				if(data != null )
				{
					if(password == retypePassword)
					{
						data.Password = Hashing.HashPassword(password);
						myContext.Entry(data).State = EntityState.Modified;
						var result = myContext.SaveChanges();
						if (result > 0)
							return 2;
					}
					return 0;
				}
				return 1;
			} catch
			{
				return 1;
			}
		}

		public int ChangePassword(int id, string oldPassword, string retypaPassword, string password)
		{
			var data = myContext.Gurus.SingleOrDefault(x => x.Id.Equals(id));

			if( data != null)
			{
				if(password == retypaPassword)
				{
					if(Hashing.ValidatePassword(oldPassword, data.Password))
					{
						data.Password = Hashing.HashPassword(password);
						myContext.Entry(data).State = EntityState.Modified;
						var result = myContext.SaveChanges();
						if(result > 0)
						
							return 2;
						
						
					}
					return 0;
				}
				return 1;
			}
			return 0;
		}

	}
}

