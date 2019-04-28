using ChinesePoetry.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebApi.OutputCache.V2;

namespace ChinesePoetry.Controllers
{
    [CacheOutput(ClientTimeSpan = 3000)]

    public class PoetryController : ApiController
    {
        private readonly Db db = new Db();
        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>字典数据</returns>
        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;

        }
        /// <summary>
        /// 按照star倒叙分页查找poetry信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [IgnoreCacheOutput]
        [HttpGet]
        public IEnumerable<dynamic> PoetryTopStar(int page, Boolean type = false)
        {
            //List<object> data = new List<object>();
            var tempPoetry = db.Poetry.AsNoTracking().OrderBy(p => Guid.NewGuid()).Skip(10 * (page - 1)).Take(10).Select(p => new { p.poetryId, name = p.name + "\n" + p.dynasty + "·" + p.author, p.poetryContent, p.dynasty, p.author, onlyname = p.name });
            List<dynamic> poetry = new List<dynamic>(tempPoetry);
            List<object> data = new List<object>();
            if (type)
            {
                for (int i = 0; i < poetry.Count(); i++)
                {
                    Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                    string name = ChineseStringUtility.ToTraditional(poetry[i].onlyname) + "\n" + ChineseStringUtility.ToTraditional(poetry[i].dynasty) + "·" + ChineseStringUtility.ToTraditional(poetry[i].author);
                    string poetryContent = ChineseStringUtility.ToTraditional(poetry[i].poetryContent);
                    result.Add("poetryId", poetry[i].poetryId);
                    result.Add("name", name);
                    result.Add("poetryContent", poetryContent);
                    data.Add(result);
                }
                return data;
            }
            else
            {
                return poetry;
            }

        }
        #region username 查 userId
        public long searchUserIdFromUsername()
        {
            string username = HttpContext.Current.Request.Cookies["UserInfo"].Value;
            username = AES.DecryptAes(username, AES.AesKey);
            long userId = db.UserList.Where(p => p.username == username).Select(p => p.id).First();
            return userId;
        }
        #endregion
        /// <summary>
        /// 查找指定poetryId的poetry详细信息
        /// </summary>
        /// <param name="poetryId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult PoetryDetails(long poetryId, int pn, Boolean type = false)
        {
            try
            {
                var poetry = db.Poetry.AsNoTracking().Where(p => p.poetryId == poetryId).Select(p => new { p.name, p.dynasty, p.poetryContent, p.shangxi, p.about, p.author, p.tags, p.poetryForBaidu }).First();
                Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                if (type)
                {
                    string poetryContent = ChineseStringUtility.ToTraditional(poetry.poetryContent);
                    string name = ChineseStringUtility.ToTraditional(poetry.name);
                    string dynasty = ChineseStringUtility.ToTraditional(poetry.dynasty);
                    string shangxi = ChineseStringUtility.ToTraditional(poetry.shangxi);
                    string about = ChineseStringUtility.ToTraditional(poetry.about);
                    string tags = ChineseStringUtility.ToTraditional(poetry.tags);
                    string author = ChineseStringUtility.ToTraditional(poetry.author);
                    string baiduPoetry = ChineseStringUtility.ToTraditional(poetry.poetryForBaidu);
                    result.Add("name", name);
                    result.Add("dynasty", dynasty);
                    result.Add("poetryContent", poetryContent);
                    result.Add("shangxi", shangxi);
                    result.Add("about", about);
                    result.Add("tags", tags);
                    result.Add("author", author);
                    result.Add("baiduPoetry", baiduPoetry);
                }
                else
                {
                    result.Add("name", poetry.name);
                    result.Add("dynasty", poetry.dynasty);
                    result.Add("poetryContent", poetry.poetryContent);
                    result.Add("shangxi", poetry.shangxi);
                    result.Add("about", poetry.about);
                    result.Add("tags", poetry.tags);
                    result.Add("author", poetry.author);
                    result.Add("baiduPoetry", poetry.poetryForBaidu);
                }
                string image = getPoetryImage(poetry.name, pn);
                result.Add("image", image);
                return Json(result);
            }
            catch
            {
                return Json("error");
            }

        }
        [HttpGet]
        public string getPoetryImage(string word, int pn)
        {
            string url = "http://image.baidu.com/search/wisemiddetail?tn=wisemiddetail&ie=utf8&word=" + word + "&fmpage=detail&pn=" + pn + "&gsm=" + pn + "&size=&pos=prev";
            HttpWebRequest httpReq;
            HttpWebResponse httpResp;


            string strBuff = "";
            char[] cbuffer = new char[256];
            int byteRead = 0;
            Uri httpURL = new Uri(url);

            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换 
            httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换

            httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容

            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理 
            Stream respStream = httpResp.GetResponseStream();

            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以

            //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8） 
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);

            byteRead = respStreamReader.Read(cbuffer, 0, 256);

            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                strBuff = strBuff + strResp;
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
            }

            respStream.Close();
            Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""\s]?)(?<href>[^'""\s]*)\1[^>]*?>");
            List<string> image = new List<string>();
            //Regex reg = new Regex(@"(?m)<div class=""wm lh""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            MatchCollection match = reg.Matches(strBuff);
            foreach (Match m in match)
            {
                image.Add(m.Groups["href"].Value); // 序号为6的是大图
            }
            var result = from p in image where p.Contains("http://") select p;
            List<string> img = new List<string>();
            foreach (var item in result)
            {
                img.Add(item);
            }
            return img[0];
        }
        [HttpGet]
        public IHttpActionResult save()
        {
            var data = db.PoetrySentence.Select(p => new { p.id, p.author }).ToList();
            for (int i = 6616; i <= data.Count(); i++)
            {
                string author = data[i].author;
                Regex rg = new Regex("(?<=(《))[.\\s\\S]*?(?=(》))", RegexOptions.Multiline | RegexOptions.Singleline);
                author = rg.Match(author).Value;
                string imageUrl = getPoetryImage(author, 1);
                PoetrySentence poetrySentence = new PoetrySentence();
                poetrySentence.id = data[i].id;
                poetrySentence.image = imageUrl;
                DbEntityEntry<object> entry = db.Entry<object>(poetrySentence);
                entry.State = EntityState.Unchanged;
                entry.Property("image").IsModified = true;
                db.SaveChanges();
            }
            return Json("success");
        }
        [HttpGet]
        public IHttpActionResult SavePoetImage()
        {
            var poetList = db.Poet.Where(p => p.image == null).Select(p => new { p.poetId, p.name }).ToList();
            for (int i = 0; i < poetList.Count(); i++)
            {
                string imageUrl = getPoetryImage(poetList[i].name, 1);
                Poet poet = new Poet();
                poet.poetId = poetList[i].poetId;
                poet.image = imageUrl;
                DbEntityEntry<object> entry = db.Entry<object>(poet);
                entry.State = EntityState.Unchanged;
                entry.Property("image").IsModified = true;
                db.SaveChanges();
            }
            return Json("suc");

        }
        [HttpGet]
        public IHttpActionResult getPoetryBaidu(string word, Boolean type)
        {
            string url = "https://baike.baidu.com/item/" + word;
            HttpWebRequest httpReq;
            HttpWebResponse httpResp;
            string strBuff = "";
            char[] cbuffer = new char[256];
            int byteRead = 0;
            Uri httpURL = new Uri(url);
            httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            httpResp = (HttpWebResponse)httpReq.GetResponse();
            Stream respStream = httpResp.GetResponseStream();
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
            byteRead = respStreamReader.Read(cbuffer, 0, 256);
            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                strBuff = strBuff + strResp;
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
            }
            respStream.Close();
            Regex reg = new Regex(@"(?m)<div class=""para"" [^>]*>(?<div>(?:\w|\W)*?)[^(]*</div[^>]*>");
            List<string> image = new List<string>();
            MatchCollection match = reg.Matches(strBuff);
            foreach (Match m in match)
            {
                image.Add(m.Value); // 序号为6的是大图
            }
            string result = null;
            for (int i = 0; i < image.Count(); i++)
            {
                result = result + image[i];
            }
            if (result == null)
            {
                return Json("未找到数据");
            }
            string pppppp = Regex.Replace(result, @"<table\b[^>]*>([\s\S]*?)</table>", "");
            string ppppppp = Regex.Replace(pppppp, @"<img\b[^>]*>([\s\S]*?)</img>", "");
            string qqqqq = Regex.Replace(ppppppp, @"<dl\b[^>]*>([\s\S]*?)</dl>", "");
            string qqqq = Regex.Replace(qqqqq, @"<dd\b[^>]*>([\s\S]*?)</dd>", "");
            string qaz = Regex.Replace(qqqq, @"<img\b[^>]*>", "");
            string qaz1 = Regex.Replace(qaz, @"<li\b[^>]*>", "");
            string cts = Regex.Replace(qaz1, @"(?m)<div class=""anchor-list""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string cts1 = Regex.Replace(cts, @"(?m)<div class=""hotspotminingContent_rel_tit""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string cts2 = Regex.Replace(cts1, @"(?m)<div class=""hotspotminingContent_web""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string ctsqq = Regex.Replace(cts2, @"(?m)<a class=""edit-icon j-edit-link""[^>]*>(?<div>(?:\w|\W)*?)</a[^>]*>", "");//一定要在过滤a标签之前
            string xx = Regex.Replace(ctsqq, "<a (.*?)>", "", RegexOptions.Compiled);//过滤a标签
            string ct = Regex.Replace(xx, @"(?m)<div class=""lemmaWgt-lemmaCatalog""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string ct1 = Regex.Replace(ct, @"(?m)<div class=""album-wrap""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string ct2 = Regex.Replace(ct1, @"(?m)<div id=""hotspotmining_s""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string a = Regex.Replace(ct2, @"(?m)<div class=""tashuo-bottom""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string p = Regex.Replace(a, @"(?m)<div class=""tashuo-multiple""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string pq = Regex.Replace(p, @"(?m)<div class=""side-content""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string p1 = Regex.Replace(pq, @"(?m)<div class=""lemma-picture text-pic layout-right""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string pa = Regex.Replace(p1, @"(?m)<div class=""lemmaWgt-promotion-vbaike""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string paa = Regex.Replace(pa, @"(?m)<div class=""tashuo-operator""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string paaa = Regex.Replace(paa, @"(?m)<div class=""album-list""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string b = Regex.Replace(paaa, @"(?m)<sup class=""normal""[^>]*>(?<div>(?:\w|\W)*?)</sup[^>]*>", "");
            string aa = Regex.Replace(b, @"(?m)<sup class=""sup--normal""[^>]*>(?<div>(?:\w|\W)*?)</sup[^>]*>", "");
            string aaa = Regex.Replace(aa, @"(?m)<div id=""open-tag""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            string aaaa = Regex.Replace(aaa, @"(?m)<div class=""wgt-footer-main""[^>]*>(?<div>(?:\w|\W)*?)</div[^>]*>", "");
            if (type)
            {
                aaa = ChineseStringUtility.ToTraditional(aaa);
            }
            return Json(aaaa);
        }
        /// <summary>
        /// 按照star倒叙分页查找poet信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [IgnoreCacheOutput]

        [HttpGet]
        public IEnumerable<dynamic> Poet(int page)
        {
            List<object> data = new List<object>();
            var tempPoet = db.Poet.AsNoTracking().OrderBy(p => Guid.NewGuid()).Skip(10 * (page - 1)).Take(20).Select(p => new { p.poetId, p.name, p.image, p.dynasty, p.minIntroduce, p.introduce });
            List<dynamic> poet = new List<dynamic>(tempPoet);
            return poet;
        }
        /// <summary>
        /// 查找指定poetId的poet详细信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult PoetDetails(string name, Boolean type = false)
        {
            name = ChineseStringUtility.ToSimplified(name);// 查询作者不管简体还是繁体都转为简体查询
            List<object> data = new List<object>();
            try
            {
                var poet = db.Poet.AsNoTracking().Where(p => p.name == name).Select(p => new { p.poetId, p.name, p.image, p.dynasty, p.minIntroduce, p.introduce }).First();
                if (type)
                {
                    Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                    string poetName = ChineseStringUtility.ToTraditional(poet.name);
                    string dynasty = ChineseStringUtility.ToTraditional(poet.dynasty);
                    string minIntroduce = ChineseStringUtility.ToTraditional(poet.minIntroduce);
                    string introduce = ChineseStringUtility.ToTraditional(poet.introduce);
                    result.Add("poetId", poet.poetId);
                    result.Add("image", poet.image);
                    result.Add("name", poetName);
                    result.Add("dynasty", dynasty);
                    result.Add("minIntroduce", minIntroduce);
                    result.Add("introduce", introduce);
                    return Json(result);
                }
                else
                {
                    return Json(poet);
                }
            }
            catch
            {
                return Json("NoPoet");
            }
        }
        /// <summary>
        /// 根据指定信息模糊搜索poetry
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<dynamic> SearchPoetry(string key, int page, Boolean type, Boolean bol)
        {
            if (bol) // bol 等于true 的时候根据诗人查古诗
            {
                if(key != null)
                {
                    key = ChineseStringUtility.ToSimplified(key);
                }
                var tempPoetry = db.Poetry.AsNoTracking().Where(p => p.author == key).OrderByDescending(p => p.star).Skip(10 * (page - 1)).Take(20).Select(p => new { p.poetryId, name = p.name + "\n" + p.dynasty + "·" + p.author, p.poetryContent, p.dynasty, p.author, onlyname = p.name }).ToList();
                List<dynamic> poetry = new List<dynamic>(tempPoetry);
                List<object> data = new List<object>();
                if (type)
                {
                    for (int i = 0; i < poetry.Count(); i++)
                    {
                        Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                        string name = ChineseStringUtility.ToTraditional(poetry[i].onlyname) + "\n" + ChineseStringUtility.ToTraditional(poetry[i].dynasty) + "·" + ChineseStringUtility.ToTraditional(poetry[i].author);
                        string poetryContent = ChineseStringUtility.ToTraditional(poetry[i].poetryContent);
                        result.Add("poetryId", poetry[i].poetryId);
                        result.Add("name", name);
                        result.Add("poetryContent", poetryContent);
                        data.Add(result);
                    }
                    return data;
                }
                else
                {
                    return poetry;
                }
            }
            else // bol 等于false 的时候根据诗人,诗名,诗词查古诗
            {
                //先根据关键词查诗名
                key = ChineseStringUtility.ToSimplified(key);
                var tempPoetry = db.Poetry.AsNoTracking().Where(p => p.name.Contains(key) || p.author.Contains(key) || p.poetryContent.Contains(key)).OrderByDescending(p => p.star).Skip(10 * (page - 1)).Take(10).Select(p => new { p.poetryId, name = p.name + "\n" + p.dynasty + "·" + p.author, p.poetryContent, p.star, p.dynasty, p.author, onlyname = p.name }).ToList();
                if (type)
                {
                    List<object> list = new List<object>();
                    for (int i = 0; i < tempPoetry.Count(); i++)
                    {
                        Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                        string name = ChineseStringUtility.ToTraditional(tempPoetry[i].onlyname) + "\n" + ChineseStringUtility.ToTraditional(tempPoetry[i].dynasty) + '·' + ChineseStringUtility.ToTraditional(tempPoetry[i].author);
                        string poetryContent = ChineseStringUtility.ToTraditional(tempPoetry[i].poetryContent);
                        result.Add("name", name);
                        result.Add("poetryContent", poetryContent);
                        result.Add("poetryId", tempPoetry[i].poetryId);
                        list.Add(result);
                    }
                    return list;
                }
                else
                {
                    return tempPoetry;
                }
            }

        }
        [IgnoreCacheOutput]

        [HttpGet]
        public IHttpActionResult SearchPoet(int page, Boolean type)
        {
            var poetList = db.Poet.AsNoTracking().OrderBy(p => Guid.NewGuid()).Skip(10 * (page - 1)).Take(10).Select(p => new { p.name, p.image, p.minIntroduce }).ToList();
            if (type)
            {
                List<object> list = new List<object>();
                for (int i = 0; i < poetList.Count(); i++)
                {
                    Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                    string name = ChineseStringUtility.ToTraditional(poetList[i].name);
                    string minIntroduce = ChineseStringUtility.ToTraditional(poetList[i].minIntroduce);
                    result.Add("image", poetList[i].image);
                    result.Add("name", name);
                    result.Add("minIntroduce", minIntroduce);
                    list.Add(result);
                }
                return Json(list);
            }
            else
            {
                return Json(poetList);
            }
        }
        //[HttpGet]
        //public IHttpActionResult save()
        //{
        //    var data = db.PoetrySentence.AsNoTracking().Where(p=>p.poetryId==0).Select(p => new { p.id, p.poetry }).ToList();
        //    for (int i = 0; i <= data.Count(); i++)
        //    {
        //        string name = data[i].poetry;
        //        long poetryId = 0;
        //        try
        //        {
        //            poetryId = db.Poetry.AsNoTracking().Where(p => p.poetryContent.Contains(name)).Select(p => p.poetryId).First();
        //        }
        //        catch
        //        {
        //        }
        //        PoetrySentence poetrySentence = new PoetrySentence();
        //        poetrySentence.id = data[i].id;
        //        poetrySentence.poetryId = poetryId;
        //        DbEntityEntry<object> entry = db.Entry<object>(poetrySentence);
        //        entry.State = EntityState.Unchanged;
        //        entry.Property("poetryId").IsModified = true;
        //        db.SaveChanges();
        //    }
        //    return Json("success");
        //}
        #region 查找古诗名句 
        [IgnoreCacheOutput]

        [HttpGet]
        public IHttpActionResult SearchPoetrySentence(int page, Boolean type)
        {
            var list = db.PoetrySentence.AsNoTracking().OrderBy(p => Guid.NewGuid()).Skip(10 * (page - 1)).Take(10).Select(p => new { p.poetry, p.author, p.poetryId, p.image }).ToList();
            List<object> result = new List<object>();
            if (type)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                    string poetry = ChineseStringUtility.ToTraditional(list[i].poetry);
                    string author = ChineseStringUtility.ToTraditional(list[i].author);
                    data.Add("poetry", poetry);
                    data.Add("author", author);
                    data.Add("poetryId", list[i].poetryId);
                    data.Add("image", list[i].image);
                    result.Add(data);
                }
                return Json(result);
            }
            else
            {
                return Json(list);
            }
        }
        #endregion

        #region 根据朝代查找古诗
        [HttpGet]
        public IHttpActionResult SearchPoetryForDynasty(string dynasty, int page)
        {
            var tempPoetry = db.Poetry.AsNoTracking().Where(p => p.dynasty == dynasty).OrderByDescending(p => p.star).Skip(10 * (page - 1)).Take(20).Select(p => new { p.name, p.dynasty, p.poetryContent, p.poetryId }).ToList();
            List<dynamic> poetry = new List<dynamic>(tempPoetry);
            return Json(poetry);
        }
        #endregion

        //#region 根据诗人查找古诗
        //[HttpGet]
        //public IEnumerable<dynamic> SearchPoetryForPoet(string poet, int page, Boolean type)
        //{
        //    poet = ChineseStringUtility.ToSimplified(poet);
        //    var tempPoetry = db.Poetry.AsNoTracking().Where(p => p.author == poet).OrderByDescending(p => p.star).Skip(10 * (page - 1)).Take(20).Select(p => new { p.poetryId, name = p.name + "\n" + p.dynasty + "·" + p.author, p.poetryContent, p.dynasty, p.author, onlyname = p.name }).ToList();
        //    List<dynamic> poetry = new List<dynamic>(tempPoetry);
        //    List<object> data = new List<object>();
        //    if (type)
        //    {
        //        for (int i = 0; i < poetry.Count(); i++)
        //        {
        //            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
        //            string name = ChineseStringUtility.ToTraditional(poetry[i].onlyname) + "\n" + ChineseStringUtility.ToTraditional(poetry[i].dynasty) + "·" + ChineseStringUtility.ToTraditional(poetry[i].author);
        //            string poetryContent = ChineseStringUtility.ToTraditional(poetry[i].poetryContent);
        //            result.Add("poetryId", poetry[i].poetryId);
        //            result.Add("name", name);
        //            result.Add("poetryContent", poetryContent);
        //            data.Add(result);
        //        }
        //        return data;
        //    }
        //    else
        //    {
        //        return poetry;
        //    }
        //}
        //#endregion

        #region 根据诗名查找古诗
        [HttpGet]
        public IHttpActionResult SearchPoetryForName(string name, int page)
        {
            var tempPoetry = db.Poetry.AsNoTracking().Where(p => p.name == name).OrderByDescending(p => p.star).Skip(10 * (page - 1)).Take(20).Select(p => new { p.name, p.dynasty, p.poetryContent, p.poetryId }).ToList();
            List<dynamic> poetry = new List<dynamic>(tempPoetry);
            return Json(poetry);
        }
        #endregion

        #region 根据tag查找古诗
        [HttpGet]
        public IHttpActionResult SearchPoetryFromTags(int page, string tags, string dynasty, string state, Boolean type)
        {
            if (dynasty == null)
            {
                dynasty = "";
            }
            if (tags == null)
            {
                tags = "";
            }
            if (state == null)
            {
                state = "";
            }
            tags = ChineseStringUtility.ToSimplified(tags);
            dynasty = ChineseStringUtility.ToSimplified(dynasty);
            state = ChineseStringUtility.ToSimplified(state);
            List<dynamic> poetry = new List<dynamic>();
            if (dynasty == "")
            {
                var tempResult = db.Poetry.Where(p => p.tags.Contains(tags) && p.tags.Contains(state)).OrderByDescending(p => p.star).Skip(10 * (page - 1)).Take(10).Select(p => new { p.poetryId, name = p.name + "\n" + p.dynasty + "·" + p.author, p.poetryContent, p.dynasty, p.author, onlyname = p.name }).ToList();
                poetry.Add(tempResult);
            }
            else if(dynasty == "当代" || dynasty == "明末清初" || dynasty == "近现代" || dynasty == "民国末当代初") // 因为这几个没有star 所有每次随机取
            {
                var tempResult = db.Poetry.Where(p => p.tags.Contains(tags) && p.tags.Contains(state) && p.dynasty == dynasty).OrderBy(p => Guid.NewGuid()).Skip(10 * (page - 1)).Take(10).Select(p => new { p.poetryId, name = p.name + "\n" + p.dynasty + "·" + p.author, p.poetryContent, p.dynasty, p.author, onlyname = p.name }).ToList();
                poetry.Add(tempResult);
            }
            else
            {
                 var tempResult = db.Poetry.Where(p => p.tags.Contains(tags) && p.tags.Contains(state) && p.dynasty == dynasty).OrderByDescending(p => p.star).Skip(10 * (page - 1)).Take(10).Select(p => new { p.poetryId, name = p.name + "\n" + p.dynasty + "·" + p.author, p.poetryContent, p.dynasty, p.author, onlyname = p.name }).ToList();
                poetry.Add(tempResult);

            }
            //var tempResult = db.Poetry.Where(p => p.tags.Contains(tags) && p.tags.Contains(state) && p.dynasty == dynasty).OrderByDescending(p => p.star).Skip(10 * (page - 1)).Take(10).Select(p => new { p.poetryId, name = p.name + "\n" + p.dynasty + "·" + p.author, p.poetryContent, p.dynasty, p.author, onlyname = p.name }).ToList();
            //List<dynamic> poetry = new List<dynamic>(tempResult);
            List<object> data = new List<object>();
            if (type)
            {
                for (int i = 0; i < poetry[0].Count; i++)
                {
                    Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                    string name = ChineseStringUtility.ToTraditional(poetry[0][i].onlyname) + "\n" + ChineseStringUtility.ToTraditional(poetry[0][i].dynasty) + "·" + ChineseStringUtility.ToTraditional(poetry[0][i].author);
                    string poetryContent = ChineseStringUtility.ToTraditional(poetry[0][i].poetryContent);
                    result.Add("poetryId", poetry[0][i].poetryId);
                    result.Add("name", name);
                    result.Add("poetryContent", poetryContent);
                    data.Add(result);
                }
                return Json(data);
            }
            else
            {
                return Json(poetry);
            }
        }
        #endregion

        #region 随机取一条名句
        [IgnoreCacheOutput]

        [HttpGet]
        public IHttpActionResult SearchRandomSentence(Boolean type)
        {
            Random rd = new Random();
            long randomNum = rd.Next(1, 10001);
            var data = db.PoetrySentence.AsNoTracking().Where(p => p.id == randomNum).Select(p => new { p.poetry, p.poetryId }).FirstOrDefault();
            if (type)
            {
                Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                string poetry = ChineseStringUtility.ToTraditional(data.poetry);
                result.Add("poetry", poetry);
                result.Add("poetryId", data.poetryId);
                return Json(result);
            }
            else
            {
                return Json(data);
            }
        }
        #endregion

        #region 注册
        [IgnoreCacheOutput]
        [HttpGet]
        public string Register(string username, string password)
        {
            UserList userList = new UserList();
            userList.username = username;
            userList.password = AES.EncryptAes(password, AES.AesKey); //加密
            userList.time = DateTime.Now;
            //查用户名有没有重复
            if (db.UserList.Where(p => p.username == username).Count() == 0)
            {
                db.UserList.Add(userList);
                db.SaveChanges();
                return "success";
            }
            else
            {
                return "error";
            }
        }
        #endregion

        #region 登录
        [IgnoreCacheOutput]
        [HttpGet]
        public string Login(string username, string password)
        {
            password = AES.EncryptAes(password, AES.AesKey);
            int isCorrect = db.UserList.Where(p => p.username == username && p.password == password).Count();
            if (isCorrect >= 1)
            {
                HttpCookie cookie = new HttpCookie("UserInfo");//创建多值cookie
                cookie.Expires = DateTime.Now.AddDays(10000);//设置cookie的失效时间为一天，如果不设置失效时间，cookie会在浏览器关闭即消失，不会保存本地文件
                cookie.Value = AES.EncryptAes(username, AES.AesKey);
                HttpContext.Current.Response.Cookies.Add(cookie);
                return "success";
            }
            else
            {
                return "error";
            }
        }
        #endregion

        #region 获取收藏列表
        [IgnoreCacheOutput]
        [HttpGet]
        public IHttpActionResult CollectList(int page, Boolean type)
        {

            string username = HttpContext.Current.Request.Cookies["UserInfo"].Value;
            username = AES.DecryptAes(username, AES.AesKey);
            var data = (from a in db.UserList.Where(p => p.username == username) join b in db.Collect on a.id equals b.userId orderby b.time descending select new { b.poetryId }).Skip(10 * (page - 1)).Take(10).ToList();
            List<object> result = new List<object>();
            for (int i = 0; i < data.Count(); i++)
            {
                long poetryId = data[i].poetryId;
                var poetryData = db.Poetry.Where(p => p.poetryId == poetryId).Select(p => new { p.poetryId, p.name, p.dynasty, p.author, p.poetryContent }).First();
                Dictionary<string, dynamic> tempResult = new Dictionary<string, dynamic>();
                if (type)
                {
                    string name = ChineseStringUtility.ToTraditional(poetryData.name);
                    string dynasty = ChineseStringUtility.ToTraditional(poetryData.dynasty);
                    string author = ChineseStringUtility.ToTraditional(poetryData.author);
                    string poetryContent = ChineseStringUtility.ToTraditional(poetryData.poetryContent);
                    tempResult.Add("name", name);
                    tempResult.Add("dynasty", dynasty);
                    tempResult.Add("author", author);
                    tempResult.Add("poetryContent", poetryContent);
                }
                else
                {
                    tempResult.Add("name", poetryData.name);
                    tempResult.Add("dynasty", poetryData.dynasty);
                    tempResult.Add("author", poetryData.author);
                    tempResult.Add("poetryContent", poetryData.poetryContent);
                }
                tempResult.Add("poetryId", data[i].poetryId);
                result.Add(tempResult);
            }
            return Json(result);
        }
        #endregion

        #region 添加收藏
        [HttpPost]
        public string AddCollect(dynamic obj)
        {
            long poetryId = (long)obj.poetryId;
            Collect collect = new Collect();
            collect.poetryId = poetryId;
            long userId = searchUserIdFromUsername();
            collect.userId = userId;
            collect.time = DateTime.Now;
            db.Collect.Add(collect);
            db.SaveChanges();
            return "success";
        }
        #endregion
        #region 添加收藏
        [HttpPost]
        public string DelCollect(dynamic obj)
        {
            long poetryId = (long)obj.poetryId;
            long userId = searchUserIdFromUsername();
            long collectId = db.Collect.Where(p => p.userId == userId && p.poetryId == poetryId).Select(p => p.id).First();
            Collect collect = new Collect() { id = collectId };
            //collect.id = collectId;
            //db.Collect.Remove(collect);

            db.Entry<Collect>(collect).State = EntityState.Deleted;
            db.SaveChanges();
            return "success";
        }
        #endregion

        #region 查找指定poetryId 是否被收藏
        [IgnoreCacheOutput]
        [HttpGet]
        public Boolean IsCollect(long poetryId)
        {
            Boolean isCollect = false;
            long userId = searchUserIdFromUsername();
            if (db.Collect.Where(p => p.poetryId == poetryId && p.userId == userId).Count() != 0) // 这首诗未收藏
            {
                isCollect = true;
            }
            return isCollect;
        }
        #endregion

        [HttpGet]
        public IHttpActionResult tang1()
        {
            for (int j = 1; j <= 73281; j++)
            {
                try
                {
                    string jsonfile = "C:/Users/Administrator/Desktop/poetry/诗鲸/data/poetry/poetry_" + j + ".json";
                    using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            JObject o = (JObject)JToken.ReadFrom(reader);
                            Poetry poetry = new Poetry();
                            poetry.poetryId = Convert.ToInt64(o["id"]);
                            poetry.tags = o["tags"].ToString();
                            db.Poetry.Add(poetry);
                            DbEntityEntry<object> entry = db.Entry<object>(poetry);
                            entry.State = EntityState.Unchanged;
                            entry.Property("tags").IsModified = true;
                            db.SaveChanges();
                        }
                    }
                }
                catch
                {

                }
            }
            return Json("success");
        }
    }
}
