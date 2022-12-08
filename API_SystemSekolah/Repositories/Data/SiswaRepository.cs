using System;
using System.Collections;
using API.Handlers;
using API_SystemSekolah.Context;
using API_SystemSekolah.Models;
using API_SystemSekolah.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_SystemSekolah.Repositories.Data
{
	public class SiswaRepository
	{
        private readonly MyContext _context;

        public SiswaRepository(MyContext context)
        {
            _context = context;
        }

        // GET All
        public IEnumerable<Siswa> Get()
        {
            return _context.Siswas.ToList();
        }

        // GET By Id
        public Siswa GetById(int nis)
        {
            return _context.Siswas.Find(nis);
        }

        // CREATE
        public int Create(Siswa siswa)
        {
            var data = _context.Siswas.SingleOrDefault(x => x.Email.Equals(siswa.Email) || x.Name.Equals(siswa.Name));

            if (data == null)
            {
                Siswa data_siswa = new Siswa()
                {
                    Name = siswa.Name,
                    Gender = siswa.Gender,
                    Date_Of_Birth = siswa.Date_Of_Birth,
                    Place_Of_Birth = siswa.Place_Of_Birth,
                    Address = siswa.Address,
                    NoTelp = siswa.NoTelp,
                    Email = siswa.Email,
                    Mother_Name = siswa.Mother_Name,
                    Password = Hashing.HashPassword(siswa.Password)
                };

                _context.Siswas.Add(data_siswa);
                var result = _context.SaveChanges();

                return result;
            }
            return 0;
        }

        // UPDATE
        public int Update(UpdateSiswaVM updateSiswa)
        {
            var data = _context.Siswas.Find(updateSiswa.NIS);
            if (data != null)
            {
                data.Name = updateSiswa.Name;
                data.Gender = updateSiswa.Gender;
                data.Place_Of_Birth = updateSiswa.Place_Of_Birth;
                data.Date_Of_Birth = updateSiswa.Date_Of_Birth;
                data.Address = updateSiswa.Address;
                data.NoTelp = updateSiswa.NoTelp;
                data.Email = updateSiswa.Email;
                data.Mother_Name = updateSiswa.Mother_Name;
                _context.Entry(data).State = EntityState.Modified;
                var result = _context.SaveChanges();

                return result;

            }
            return 0;
        }

        // DELETE
        public int Delete(int nis)
        {
            var data = _context.Siswas.Find(nis);
            if (data != null)
            {
                _context.Remove(data);
                var result = _context.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}

