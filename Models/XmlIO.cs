using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Shorten_Urls.Models
{
    public class XmlIO
    {
        public XDocument Forwards { get; set; }

        private readonly bool isWebApplication = bool.Parse(ConfigurationManager.AppSettings["Website"]);
        private string ForwardsPath = ConfigurationManager.AppSettings["ForwardsFile"];
        public XmlIO()
        {
            if (isWebApplication)
            {

                ForwardsPath = HttpContext.Current.Server.MapPath(ForwardsPath);
            }
            Forwards = XDocument.Load(ForwardsPath);
        }
        public void Reload()
        {
            Forwards = XDocument.Load(ForwardsPath);
        }
        public bool RemoveForward(Url url)
        {
            XElement u = MapUrl(url);

            try
            {
                Forwards.Descendants("url").Where(x => int.Parse(x.Attribute("id").Value) == url.Id).Remove();
                Forwards.Save(ForwardsPath);
                return true;
            }
            catch
            {

                return false;
            }
        }
        public bool AddForward(Url url)
        {

            XElement u = MapUrl(url);

            try
            {
                Forwards.Element("forwards").Add(u);
                Forwards.Save(ForwardsPath);
                return true;
            }
            catch
            {

                return false;
            }
        }
        private XElement MapUrl(Url url)
        {
            DateTime expires;
            if (url.Expires == null) expires = new DateTime(2999, 12, 31);
            else expires = (DateTime)url.Expires;

            XElement u = new XElement("url");
            u.SetAttributeValue("id", url.Id);
            u.SetAttributeValue("src", url.Src);
            u.SetAttributeValue("userid", url.UserId);
            u.SetAttributeValue("created", url.CreatedOn.ToString("o"));
            u.SetAttributeValue("expires", expires.ToString("o"));
            u.SetValue(url.Redirect);

            return u;
        }
    }
}
