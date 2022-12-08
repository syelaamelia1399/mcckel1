using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_SystemSekolah.Models
{
    public class Nilai
    {
        [Key]
        public int Id { get; set; }

        public int Nilai_Harian { get; set; }

        public int Nilai_UTS { get; set; }

        public int Nilai_UAS { get; set; }

        public int Nilai_rata_rata { get; set; }

        public int Id_Matpel { get; set; }

        public int NIS_Siswa { get; set; }

        [ForeignKey("Id_Matpel")]
        [JsonIgnore]
        public Matpel? Matpels { get; set; }

        [ForeignKey("NIS_Siswa")]
        [JsonIgnore]
        public Siswa? Siswas { get; set; }
    }
}
