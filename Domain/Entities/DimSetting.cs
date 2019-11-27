using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class DimSetting
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SettingsID { get; set; }

        //[ForeignKey("SettingTypeID")]
        //[HiddenInput(DisplayValue = false)]
        public string SettingTypeID { get; set; }
        public string SettingsDesc { get; set; }

        public string SettingsValue { get; set; }

        public virtual DimSettingType DimSettingType { get; set; }
    }
}
