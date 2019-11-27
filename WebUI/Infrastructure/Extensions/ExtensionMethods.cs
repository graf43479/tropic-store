using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Calabonga.Xml.Exports;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Infrastructure.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static Product ToDomainProduct(this ProductEditViewModel viewModel)
        {
            Product prod = new Product()
            {
                ProductID = viewModel.ProductID,
                Name = viewModel.Name,
                CategoryID = Convert.ToInt32(viewModel.SelectedCategoryID),
                Price = viewModel.Price,
                ArticleNumber = viewModel.ArticleNumber,
                Quantity = viewModel.Quantity,
                Description = viewModel.Description,
                ShortName = viewModel.ShortName,
                StartDate = viewModel.StartDate,
                UpdateDate = viewModel.UpdateDate,
                Sequence = viewModel.Sequence,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                LastPriceChangeDate = viewModel.LastPriceChangeDate,
                Keywords = viewModel.Keywords,
                Snippet = viewModel.Snippet,
                OldPrice = viewModel.OldPrice
            };
            return prod;
        }

        //------------------!



        public static MvcHtmlString MenuItem(this HtmlHelper helper,
           string linkText, string actionName, string controllerName)
        {
            string currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            string currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            var builder = new TagBuilder("li");
            if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase)
                && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
                builder.AddCssClass("selected");
            builder.InnerHtml = helper.ActionLink(linkText, actionName, controllerName).ToHtmlString();
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionQueryLink(this HtmlHelper htmlHelper,
            string linkText, string action, object routeValues)
        {
            var newRoute = routeValues == null
                ? htmlHelper.ViewContext.RouteData.Values
                : new RouteValueDictionary(routeValues);

            newRoute = htmlHelper.ViewContext.HttpContext.Request.QueryString
                .ToRouteDic(newRoute);

            return HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, null,
                action, null, newRoute, null).ToMvcHtml();
        }

        public static MvcHtmlString ToMvcHtml(this string content)
        {
            return MvcHtmlString.Create(content);
        }

        public static RouteValueDictionary ToRouteDic(this NameValueCollection collection)
        {
            return collection.ToRouteDic(new RouteValueDictionary());
        }

        public static RouteValueDictionary ToRouteDic(this NameValueCollection collection,
            RouteValueDictionary routeDic)
        {
            foreach (string key in collection.Keys)
            {
                if (!routeDic.ContainsKey(key))
                    routeDic.Add(key, collection[key]);
            }
            return routeDic;
        }
        //------------------

    }

    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                

                TagBuilder tag = new TagBuilder("A");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml =  i.ToString();


                TagBuilder tagLi = new TagBuilder("li");
                if (i == pagingInfo.CurrentPage)
                {
                    tagLi.AddCssClass("active");
                }
                
                tagLi.InnerHtml = tag.ToString();

                /*if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");*/
                
                result.Append(tagLi.ToString());

                /*TagBuilder tag = new TagBuilder("A");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml =  i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");

                result.Append(tag.ToString());*/

            }
            return MvcHtmlString.Create(result.ToString());
        }
        /*
        public static MvcHtmlString PageLinks(this AjaxHelper helper, PagingInfo pagingInfo, Func<int, string> pageUrl, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {


                TagBuilder tag = new TagBuilder("A");
             //   tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();


                TagBuilder tagLi = new TagBuilder("li");
                if (i == pagingInfo.CurrentPage)
                {
                    tagLi.AddCssClass("active");
                }

                tagLi.InnerHtml = tag.ToString();

                result.Append(tagLi.ToString());

                /*TagBuilder tag = new TagBuilder("A");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml =  i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");

                result.Append(tag.ToString());

            }
            return MvcHtmlString.Create(result.ToString());
        }*/

    }

    
    /*Public Function LinkButton(html As HtmlHelper, linkText As String, actionName As String) As MvcHtmlString
        Return html.ActionLink(linkText, actionName, Nothing, New With {.class = "linkButton"})
    End Function*/


    public static class ActionLinksHelper
    {
        public static IHtmlString SpanActionLink(this AjaxHelper helper, string linkText,
  string classText, /*int num,*/ string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions,
        object htmlAttributes)
        {
            var builder = new TagBuilder("span");
            builder.MergeAttribute("class", classText);
            /*builder.InnerHtml = num.ToString();*/
            var link = helper.ActionLink(linkText + "temp", actionName, routeValues, ajaxOptions,
                                   htmlAttributes).ToHtmlString();
            return new MvcHtmlString(link.Replace("temp", builder.ToString()));
        }

    }



    /*
     Цена", "List", "Product", new { sortOption = "priceDesc", category = Model.CurrentCategory, searcher = searcher }, 
     */
    //public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes);
    /*
       public static MvcHtmlString ActionQueryLink(this HtmlHelper htmlHelper,
            string linkText, string action, object routeValues)
        {
            var newRoute = routeValues == null
                ? htmlHelper.ViewContext.RouteData.Values
                : new RouteValueDictionary(routeValues);

            newRoute = htmlHelper.ViewContext.HttpContext.Request.QueryString
                .ToRouteDic(newRoute);

            return HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, null,
                action, null, newRoute, null).ToMvcHtml();
        }
     */

    public static class MyExtensionMethods
    {
        // Изменение размеров картинки
        public static Image Resize(this Image img, int iMaxHeight, int iMaxWidth)
        {
            int iDestHeight = 0;
            int iDestWidth = 0;

            // Определяем новые размеры картинки.
            // Если она меньше максимального размера, то оставляем её без изменения.
            // Если хотя бы по одному измерению больше максимального размера,
            // то уменьшаем её пропорционально до максимального размера.
            if ((iMaxHeight == 0 || iMaxHeight >= img.Height) && (iMaxWidth == 0 || iMaxWidth >= img.Width)) return img;
            else
            {
                if (iMaxHeight == 0 && iMaxWidth > 0)
                {
                    iDestWidth = iMaxWidth;
                    iDestHeight = img.Height*iMaxWidth/img.Width;
                }

                if (iMaxHeight > 0 && iMaxWidth == 0)
                {
                    iDestHeight = iMaxHeight;
                    iDestWidth = img.Width*iMaxHeight/img.Height;
                }

                if (iMaxHeight > 0 && iMaxWidth > 0)
                {
                    iDestWidth = iMaxWidth;
                    iDestHeight = img.Height*iMaxWidth/img.Width;

                    if (iDestHeight > iMaxHeight)
                    {
                        iDestHeight = iMaxHeight;
                        iDestWidth = img.Width*iMaxHeight/img.Height;
                    }
                }

                return new Bitmap(img, new Size(iDestWidth, iDestHeight));
            }
        }


        // Сохранение картинки на диск одновременно с изменением размеров
        public static void ResizeAndSave(this HttpPostedFileBase imagefile, int iMaxHeight, int iMaxWidth,
                                         string strSavePath)
        {
            if (imagefile != null)
            {
                ImageFormat format = ImageFormat.Bmp;
                string strExtension = Path.GetExtension(strSavePath);

                switch (strExtension.ToLower())
                {
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;

                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;

                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;

                    case ".png":
                        format = ImageFormat.Png;
                        break;
                }

                Image.FromStream(imagefile.InputStream).Resize(iMaxHeight, iMaxWidth).Save(strSavePath, format);
            }
        }
    }

    public static class QueryExtention
    {
        public static SelectList ToSelectList<T>(this IQueryable<T> query, string dataValueField, string dataTextField, object selectedValue)
        {
            return new SelectList(query, dataValueField, dataTextField, selectedValue ?? -1);
        }

    }
    /*
    public static class AjaxExtension
    {

    public static IHtmlString ImageActionLink(this AjaxHelper helper, string actionName, bool inFavourite, AjaxOptions ajaxOptions)
        {

        }
    }
    */
    /*
    public static class GridExtension
    {
        public static IGridColumn<T> Action<T>(this IGridColumn<T> column, Func<T, string> viewAction, Func<T, string> editAction, Func<T, bool> editMode)
        {
            column.CustomItemRenderer = (context, item) => context.Writer.Write("<td>" + (editMode(item) ? editAction(item) : viewAction(item)) + "</td>");
            return column;
        }
    }
     * */



    /// <summary>
    /// Выдает пользователю для загрузки файл Excel.
    /// </summary>
    
    public class ExcelResult : ActionResult
      {
        /// <summary>
        /// Создает экземпляр класса, которые выдает файл Excel
        /// </summary>
        /// <param name="fileName">наименование файла для экспорта</param>
        /// <param name="report">готовый набор данные для экпорта</param>
    
        public ExcelResult(string fileName, string report)
            {
            this.Filename = fileName;
            this.Report = report;
            }

 public string Report { get; private set; }
   public string Filename { get; private set; }
    public override void ExecuteResult(ControllerContext context)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.BufferOutput = true;
            HttpContext.Current.Response.AddHeader("content-disposition", 
                                                string.Format("attachment; filename={0}", Filename));
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(Report);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }


    internal class WordResult: ActionResult
    {
        /// <summary>
        /// Наименование файла для сохранения документа на стороне клиента.
        /// </summary>
        public string FileName { get; private set; }
 
        /// <summary>
        /// MVC url представления которой надо сохранить в MS Word
        /// </summary>
        public string Content { get; private set; }
 
        /// <summary>
        /// Заголовок HTML
        /// </summary>
        public string Title { get; private set; }
 
        /// <summary>
        /// Создает экземпляр WordResult
        /// </summary>
        /// <param name="filename">имя файла</param>
        /// <param name="content">mvc url представления</param>
        /// <param name="title">заголовок документа</param>
        public WordResult(string filename, string title, string content)
        {
            this.Title= title;
            this.FileName = filename;
            this.Content = content;
        }
 
public override void ExecuteResult(ControllerContext context)
{
 
    string wordstring = WordDocument.CreateFromHtml(this.Content, "Document");
 
    HttpContext ctx = HttpContext.Current;
    ctx.Response.AppendHeader("Content-Type", "application/msword");
    ctx.Response.Clear();
    ctx.Response.Charset = "Windows-1251";
    //ctx.Response.ContentEncoding = Encoding.UTF8;
    ctx.Response.AddHeader("content-disposition", "attachment;filename=" + this.FileName);
    ctx.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    ctx.Response.ContentType = "application/ms-word";
    ctx.Response.Write(wordstring);
    ctx.Response.End();
    ctx.Response.Flush();
    ctx.Response.Clear();    
}
        
/*        
////  Хелпер для получения значений из конфигурационного файла.
private string GetConfigValue(string configValue, string defaultValue)
{
    if (string.IsNullOrEmpty(configValue))
        return defaultValue;
    return configValue;
} */



}


    /*
    public class PlaceHolderAttribute : Attribute, IMetadataAware
    {
        private readonly string _placeholder;
        public PlaceHolderAttribute(string placeholder)
        {
            _placeholder = placeholder;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["placeholder"] = _placeholder;
        }
    }*/

} 


