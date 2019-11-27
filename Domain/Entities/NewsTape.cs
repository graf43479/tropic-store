using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class NewsTape
    {
        [Key]
        public int NewsID { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите дату новости")]


        // [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        //[DefaultValue("01.01.2014")]
        public DateTime NewsDate { get; set; }
        [MaxLength(150, ErrorMessage = "Количество символов в заголовке не должно превышать 150 ")]
        public string Header1 { get; set; }
        [MaxLength(150, ErrorMessage = "Количество символов в подзаголовке не должно превышать 150 ")]
        public string Header2 { get; set; }
        public string ImgPath { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите краткое описание новости")]
        [MaxLength(700, ErrorMessage = "Количество символов не должно превышать 700 ")]
        public string NewsPreview { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите текст новости")]
        public string NewsText { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
