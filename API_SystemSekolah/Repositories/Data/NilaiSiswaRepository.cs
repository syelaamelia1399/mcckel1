using API_SystemSekolah.Context;
using System.Collections;

namespace API_SystemSekolah.Repositories.Data
{
    public class NilaiSiswaRepository
    {
        private readonly MyContext _context;

        public NilaiSiswaRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable GetNilaiSiswa(int nis)
        {
            var queryNilaiSiswa = (from mp in _context.Matpels
                                   join kls in _context.Kelass on mp.Id_Kelas equals kls.Id
                                   join score in _context.Nilais on mp.Id equals score.Id_Matpel
                                   join sis in _context.Siswas on score.NIS_Siswa equals sis.NIS
                                   where sis.NIS == nis
                                   select new
                                   {
                                       mataPelajaran = mp.Name,
                                       kelas = kls.Name,
                                       nilaiHarian = score.Nilai_Harian,
                                       nilaiUTS = score.Nilai_UTS,
                                       nilaiUAS = score.Nilai_UAS,
                                       nilaiRata = score.Nilai_rata_rata
                                   }).ToList();
            return queryNilaiSiswa;
        }
    }
}
