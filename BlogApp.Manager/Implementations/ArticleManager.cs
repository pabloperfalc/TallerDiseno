﻿using BlogApp.Manager.RequiredInterfaces;
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
                    }
                    art.CreationDate = DateTime.Now;
                    art.ModificationdDate = DateTime.Now;
                    art.AuthorId = 1;
                    if (!nodo.SelectSingleNode("Foto").InnerText.Equals(String.Empty))
                    {
                        byte[] imageByte = Convert.FromBase64String(nodo.SelectSingleNode("Foto").InnerText);
                        art.PicturePath = BytesImage(imageByte);
                    }
                    else
                    {
                        art.PicturePath = String.Empty;
                    }
                    articleDataAccess.AddArticle(art);
                } 
                catch (Exception)
                {
                    errors++;
                }
            }
            return errors;
        }

        private string BytesImage(Byte[] ImgBytes)
        {
            string name = Guid.NewGuid().ToString() + ".jpg";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "ArticlePictures/" + name;
            File.WriteAllBytes(path, ImgBytes);
            path = "/ArticlePictures/" + name;
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

        public void UpdateArticle(Article article, Byte[] ImageBytes)
        {
            if (ImageBytes != null)
            {
                article.PicturePath = BytesImage(ImageBytes);
            }
            else //SI TRAE EL PATH EL ART NO ES NECESARIO
            {
                Article artAux = articleDataAccess.GetArticleById(article.Id);
                if (artAux.PicturePath.Equals(String.Empty))
                {
                    article.PicturePath = String.Empty;
                }
                else
                {
                    article.PicturePath = artAux.PicturePath;
                }
            }
            articleDataAccess.ModifyArticle(article);
        }

        public Article GetArticleById(int id)
        {
            var article = articleDataAccess.GetArticleById(id);
            if (article != null)
            {
                var comments = commentDataAccess.GetArticleComments(id);
                article.Comments = new List<Comment>();
                foreach (var comment in comments)
                {
                    if (!comment.ParentId.HasValue)
                        article.Comments.Add(commentDataAccess.RetriveComments(comment));
                }
                return article;
            }
            return null;
        }

        public List<int> GetArticlesPerMonth(int year) 
        {
            return articleDataAccess.GetArticlesPerMonth(year);
        }

        public List<Article> GetPublicArticles(int id)
        {
            return articleDataAccess.GetPublicArticles(id);
        }

        public void CreateArticle(Article article, Byte[] ImageBytes)
        {
            if (ImageBytes != null)
            {
                article.PicturePath = BytesImage(ImageBytes);
            }
            else
            {
                article.PicturePath = String.Empty;
            }
            AddArticle(article);
        }

        public List<Article> GetArticles(int userId)
        {
            return articleDataAccess.GetArticles(userId);
        }

        public List<Article> SearchArticles(string searchText)
        {
            return articleDataAccess.SearchArticles(searchText);
        }
    }
}
