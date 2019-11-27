using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Article
    {
        public int ArticleID { get; set; }

        [Required(ErrorMessage = "Необходимо указать заголовок статьи")]
        public string Header { get; set; }

        [Required(ErrorMessage = "Заполните превью статьи")]
        [MaxLength(1000, ErrorMessage = "Количество символов не должно превышать 1000 ")]
        public string ArticlePreview { get; set; }

        public string ImgPath { get; set; }

        [Required(ErrorMessage = "Поле с текстом статьи должно быть заполнено")]
        public string ArticleText { get; set; }

        public DateTime UpdateDate { get; set; }

        [MaxLength(100, ErrorMessage = "Количество символов не должно превышать 100 ")]
        public string Keywords { get; set; }

        [MaxLength(1000, ErrorMessage = "Количество символов не должно превышать 1000 ")]
        public string Snippet { get; set; }

        public string ShortLink { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста, введите дату публикации статьи")]
        public DateTime ArticleDate { get; set; }
    }
}
