﻿using IcantHumor.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IcantHumor.Models
{
    public class ReactedUserViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdReactedUser { get; set; }
        public React ChosenReact { get; set; }
    }
}
