﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_SystemSekolah.Models
{
	public class JadwalMatpel
	{
        [Key]
        public int Id { get; set; }

        public string Hours { get; set; }
        
        public string Day { get; set; }

        public int Id_Matpel { get; set; }

        [ForeignKey("Id_Matpel")]
        [JsonIgnore]
        public virtual Matpel? Matpels { get; set; }
    }
}

