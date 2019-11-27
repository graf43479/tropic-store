using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DimSettingType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SettingTypeID { get; set; }
        public string SettingTypeDesc { get; set; }

        public virtual ICollection<DimSetting> DimSettings { get; set; }
    }
}
