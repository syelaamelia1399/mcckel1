using API_SystemSekolah.Context;
using API_SystemSekolah.Models;
using System.Collections;

namespace API_SystemSekolah.Repositories.Data
{
    public class MatpelSiswaRepository
    {
        private readonly MyContext _context;

        public MatpelSiswaRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable GetMatpelSiswa(int nis)
        {
            var queryMatpelSiswa = (from mp in _context.Matpels
                                    join kls in _context.Kelass on mp.Id_Kelas equals kls.Id
                                    join score in _context.Nilais on mp.Id equals score.Id_Matpel
                                    join sis in _context.Siswas on score.NIS_Siswa equals sis.NIS
                                    where sis.NIS == nis
                                    select new
                                    {
                                        mataPelajaran = mp.Name,
                                        kelas = kls.Name
                                    }).ToList();
            return queryMatpelSiswa;
        }
    }
}
