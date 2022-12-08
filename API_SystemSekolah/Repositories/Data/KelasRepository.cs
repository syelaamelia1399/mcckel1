using System;
using API_SystemSekolah.Context;
using API_SystemSekolah.Models;
using Microsoft.EntityFrameworkCore;

namespace API_SystemSekolah.Repositories.Data
{
	public class KelasRepository
	{
        private readonly MyContext _context;

        public KelasRepository(MyContext context)
        {
            _context = context;
        }

        // GET All
        public IEnumerable<Kelas> Get()
        {
            return _context.Kelass.ToList();
        }

        // GET By Id
        public Kelas GetById(int id)
        {
            return _context.Kelass.Find(id);
        }

        // CREATE
        public int Create(Kelas kelas)
        {
            _context.Kelass.Add(kelas);
            var result = _context.SaveChanges();
            return result;
        }

        // UPDATE
        public int Update(Kelas kelas)
        {
            _context.Entry(kelas).State = EntityState.Modified;
            var result = _context.SaveChanges();
            return result;
        }

        // DELETE
        public int Delete(int id)
        {
            var data = _context.Kelass.Find(id);
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

