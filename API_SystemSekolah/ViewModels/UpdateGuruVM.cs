using System;
namespace API_SystemSekolah.ViewModels
{
	public class UpdateGuruVM
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Place_Of_Birth { get; set; }

        public string Date_Of_Birth { get; set; }

        public string Address { get; set; }

        public string NoTelp { get; set; }

        public string Email { get; set; }

        public string NIP { get; set; }

        public int Id_Matpel { get; set; }
    }
}

