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
        
    }
}
