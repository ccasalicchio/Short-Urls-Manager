using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.Entity;

namespace Shorten_Urls.Models
{
    public class UrlRepository : IRepository<Url>
    {
        private XDocument _urls;
        private List<Url> urls;
        ApplicationDbContext _context;
        private readonly string DbProvider = ConfigurationManager.AppSettings["DbProvider"];
        private const string SQL = "Sql";
        private const string XML = "Xml";
        public bool needLastId { get { return DbProvider.Equals(XML); } }

        public List<Url> Urls { get { return urls; } }
        public int LastId
        { get { return urls.Last().Id; } }

        public UrlRepository()
        {
            urls = new List<Url>();

            if (DbProvider == XML)
            {
                _urls = MvcApplication.IO.Forwards;

                IEnumerable<XElement> rootDescendants = _urls.Descendants("url");
                foreach (XElement e in rootDescendants)
                    urls.Add(MapXElement(e));
            }
            else if (DbProvider == SQL)
            {
                _context = new ApplicationDbContext();
                urls = _context.Forwarders.ToList();
            }
        }

        public bool Exists(string src)
        {
            var yes = from y in urls where y.Src == src select y;
            if (yes.Count() > 0) return true;
            else return false;
        }

        public IEnumerable<Url> GetAll()
        {
            return urls;
        }

        public void Add(Url url)
        {
            urls.Add(url);
            if (DbProvider == XML)
                MvcApplication.IO.AddForward(url);
            else if (DbProvider == SQL)
            {
                _context.Forwarders.Add(url);
                _context.SaveChanges();
            }
        }
        public Url GetById(int Id)
        {
            Url e = (from url in urls where url.Id == Id select url).Single();
            if (DbProvider == SQL)
            {
                PopulateUsername(e);
            }
            return e;
        }
        public List<Url> GetByUserId(string userId)
        {
            List<Url> list = (from urlList in urls where urlList.UserId == userId select urlList).ToList();
            if (DbProvider == SQL)
            {
                foreach (Url url in list) PopulateUsername(url);
            }
            return list;
        }
        public Url GetBySrc(string src)
        {
            var e = (from url in urls where url.Src.Equals(src) select url);
            if (e.Count() == 0) return null;
            else {
                if (DbProvider == SQL)
                {
                    PopulateUsername(e.Single());
                }
                return e.Single();
            }
        }

        public void Remove(int id)
        {
            Url remove = (from url in urls where url.Id == id select url).Single();
            urls.Remove(remove);
            Remove(remove);
        }

        public void Remove(Url url)
        {
            urls.Remove(url);
            if (DbProvider == XML)
                MvcApplication.IO.RemoveForward(url);
            else if (DbProvider == SQL)
            {
                _context.Forwarders.Remove(url);
                _context.SaveChanges();
            }
        }

        public void Remove(string src)
        {
            Url remove = (from url in urls where url.Src == src select url).Single();
            urls.Remove(remove);
            Remove(remove);
        }

        public void RemoveAll()
        {
            urls.Clear();
        }

        public void RemoveByUserId(string userId)
        {
            Url remove = (from url in urls where url.UserId == userId select url).Single();
            urls.Remove(remove);
            Remove(remove);
        }

        public void Update(Url url)
        {
            Url old = (from urlE in urls where urlE.Id == url.Id select urlE).Single();
            urls.Remove(old);
            urls.Add(url);
            if (DbProvider == XML)
            {

                MvcApplication.IO.RemoveForward(old);
                MvcApplication.IO.AddForward(url);
            }
            else if (DbProvider == SQL)
            {
                _context.Entry(url).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }


        private Url PopulateUsername(int id)
        {
            Url url = (from u in urls where u.Id == id select u).Single();
            string userId = url.UserId == null ? "" : url.UserId;
            UserRepository userRepo = new UserRepository();
            url.Username = userRepo.GetUsername(userId);
            return url;
        }
        private Url PopulateUsername(Url url)
        {
            UserRepository userRepo = new UserRepository();
            string userId = url.UserId == null ? "" : url.UserId;
            url.Username = userRepo.GetUsername(userId);
            return url;
        }
        private Url MapXElement(XElement element)
        {
            DateTime expires = element.Attribute("expires").Value == "" ? new DateTime(2999, 12, 31) : DateTime.Parse(element.Attribute("expires").Value);
            DateTime createdOn = element.Attribute("created").Value == "" ? DateTime.Now : DateTime.Parse(element.Attribute("created").Value);
            string userId = element.Attribute("userid").Value;
            int id = int.Parse(element.Attribute("id").Value);
            Url url = new Url
            {
                Id = id,
                CreatedOn = createdOn,
                Expires = expires,
                UserId = userId,
                Redirect = element.Value.Trim(),
                Src = element.Attribute("src").Value
            };
            return PopulateUsername(url);
        }
    }
}
