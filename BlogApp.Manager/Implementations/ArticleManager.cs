using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace BlogApp.Manager.Implementations
{
    public class ArticleManager : IArticleManager
    {

        private readonly IArticleDataAccess articleDataAccess;
        private readonly ICommentDataAccess commentDataAccess;

        public ArticleManager(IArticleDataAccess articleDataAccess, ICommentDataAccess commentDataAccess)
        {
            this.articleDataAccess = articleDataAccess;
            this.commentDataAccess = commentDataAccess;
        }

        public List<Article> GetLatest(int count)
        {
            return articleDataAccess.GetLatest(count);
        }

        //AGREGAR EL USUARIO CUANDO SE EMPIECE A AUTENTICAR
        public int ImportArticles(XmlDocument xml)
        {
            int errors = 0;
            List<Article> listaArticulos = new List<Article>();
            XmlNodeList articulos = xml.SelectNodes("//Articulos/Articulo");

            foreach (XmlNode nodo in articulos)
            {
                try
                {
                    Article art = new Article
                                        {
                                            Name = nodo.SelectSingleNode("Nombre").InnerText,
                                            Text = nodo.SelectSingleNode("Texto").InnerText
                                        };
                    string upper = nodo.SelectSingleNode("Tipo").InnerText;
                    upper = upper.ToUpper();
                    switch (upper)
                    {
                        case "PUBLICO":
                            art.Type = ArticleType.Public;
                            break;
                        case "PRIVADO":
                            art.Type = ArticleType.Private;
                            break;
                        default: throw new Exception();
                            break;
                    }
                    upper = nodo.SelectSingleNode("Plantilla").InnerText;
                    upper = upper.ToUpper();
                    switch (upper)
                    {
                        case "INFERIOR":
                            art.Layout = ArticleLayout.Bottom;
                            break;
                        case "IZQUIERDA":
                            art.Layout = ArticleLayout.Left;
                            break;
                        case "":
                            art.Layout = ArticleLayout.NoPicture;
                            break;
                        case "SUPERIOR":
                            art.Layout = ArticleLayout.Top;
                            break;
                        default: throw new Exception();
                            break;
                    }
                    art.CreationDate = DateTime.Now;
                    art.ModificationdDate = DateTime.Now;
                    art.AuthorId = 1;
                    byte[] imageByte = Convert.FromBase64String(nodo.SelectSingleNode("Foto").InnerText);
                    art.PicturePath = BytesImage(imageByte, art);
                    articleDataAccess.AddArticle(art);
                } 
                catch (Exception)
                {
                    errors++;
                }
            }
            return errors;
        }

        public string BytesImage(Byte[] ImgBytes, Article article)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "ArticlePictures/" + article.Id +".jpg";
            File.WriteAllBytes(path, ImgBytes);
            return path;
        }

        public List<Tuple<string, string>> ValidateArticle(Article article)
        {
            List<Tuple<string, string>> errors = new List<Tuple<string, string>>();
            if (article.Name == null || article.Name.Equals(String.Empty))
            {
                errors.Add(new Tuple<string,string>("Name", "The Name field is required"));
            }

            if (article.Text == null || article.Text.Equals(String.Empty))
            {
                errors.Add(new Tuple<string, string>("Text", "The Text field is required"));
            }

            return errors;
        }

        public void AddArticle(Article article)
        {
            articleDataAccess.AddArticle(article);
        }

        public void UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public void DeleteArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public Article GetArticleById(int id)
        {
            var article = articleDataAccess.GetArticleById(id);
            var comments = commentDataAccess.GetArticleComments(id);
            article.Comments = new List<Comment>();
            foreach (var comment in comments)
            {
                if(!comment.ParentId.HasValue)
                    article.Comments.Add(commentDataAccess.RetriveComments(comment));  
            }
            return article;
        }
    }
}
