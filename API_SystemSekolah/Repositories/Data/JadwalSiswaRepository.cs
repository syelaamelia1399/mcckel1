using API_SystemSekolah.Context;
using API_SystemSekolah.Models;
using System.Collections;

namespace API_SystemSekolah.Repositories.Data
{
    public class JadwalSiswaRepository
    {
        private readonly MyContext _context;

        public JadwalSiswaRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable GetJadwalSiswa(int nis)
        {
            var queryJadwalSiswa = (from jm in _context.JadwalMatpels
                                    join mp in _context.Matpels on jm.Id_Matpel equals mp.Id
                                    join kls in _context.Kelass on mp.Id_Kelas equals kls.Id
                                    join score in _context.Nilais on mp.Id equals score.Id_Matpel
                                    join sis in _context.Siswas on score.NIS_Siswa equals sis.NIS
                                    where sis.NIS == nis
                                    select new
                                    {
                                        hari = jm.Day,
                                        jam = jm.Hours,
                                        mataPelajaran = mp.Name,
                                        kelas = kls.Name
                                    }).ToList();
            return queryJadwalSiswa;
        }
    }
}
