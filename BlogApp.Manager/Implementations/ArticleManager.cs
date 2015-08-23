using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BlogApp.Manager.Implementations
{
    public class ArticleManager : IArticleManager
    {

        private readonly IArticleDataAccess articleDataAccess;
        public ArticleManager(IArticleDataAccess articleDataAccess)
        {
            this.articleDataAccess = articleDataAccess;
        }

        public List<Article> GetLatest(int count)
        {
            return articleDataAccess.GetLatest(count);
        }

        public List<Article> Cargar(XmlDocument xml)
        {
            List<Article> listaArticulos = new List<Article>();
            XmlNodeList articulos = xml.SelectNodes("//Articulos/Articulo");
            foreach (XmlNode nodo in articulos)
            {
                Article art = new Article 
                                    {
                                        Name = nodo.SelectSingleNode("Nombre").InnerText,
                                        Text = nodo.SelectSingleNode("Texto").InnerText,
                                        //Type = nodo.SelectSingleNode("Tipo").InnerText,
                                        PicturePath = nodo.SelectSingleNode("Foto").InnerText,
                                        //Layout = nodo.SelectSingleNode("Plantilla").InnerText
                                    };
                listaArticulos.Add(art);
            }
            return listaArticulos;
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
    }
}
