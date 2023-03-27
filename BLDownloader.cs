using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BritishLibraryDownloader
{
    public class BritishLibraryDownloader
    {

        public event EventHandler<ProcessEventArgs> ProcessInfo;
        private List<string> imageBlockList = new List<string>();
        private string _manuscripts = "";
        private bool _stop_flag = false;
        private ProcessEventArgs info = new ProcessEventArgs();

        public void StopProcess()
        {
            _stop_flag = true;
        }
        public string ManuScripts
        {
            get { return _manuscripts; }
            set { _manuscripts = value; }
        }
        protected virtual void OnProcessInfo(ProcessEventArgs e)
        {
            ProcessInfo?.Invoke(this, e);
        }


        private string GetPageInfo()
        {
            HtmlAgilityPack.HtmlDocument htmdoc =
                new HtmlAgilityPack.HtmlDocument();
            string url = String.Format(Helper.URL_PAGE_INFO, _manuscripts);
            string title = "";


            try
            {
                info.Message = "Get " + _manuscripts + " Info";
                OnProcessInfo(info);
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(Helper.USER_AGENT);
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            string result = content.ReadAsStringAsync().Result;
                            htmdoc.LoadHtml(result);
                            var TitleTags = htmdoc.DocumentNode.SelectNodes("//div[@class='col-1']");
                            foreach (var val in TitleTags)
                            {
                                if (val.InnerHtml.Contains("Title"))
                                {
                                    title = val.NextSibling.NextSibling.InnerHtml;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                info.Message = ex.Message;
                OnProcessInfo(info);
                return title;
            }

            return title;
        }

        public ManuScriptsInfo GetPages()
        {

            string manuscriptsTitle = GetPageInfo();
            HtmlAgilityPack.HtmlDocument htmdoc =
                new HtmlAgilityPack.HtmlDocument();
            string url = String.Format(Helper.URL_PAGES, _manuscripts);
            string[] strPageList = null;
           


            try
            {
                info.Message = "Get " + _manuscripts + " Info";
                OnProcessInfo(info);
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(Helper.USER_AGENT);
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            string result = content.ReadAsStringAsync().Result;
                            htmdoc.LoadHtml(result);
                            var pages = htmdoc.DocumentNode.SelectNodes("//input[@id='PageList']");
                            if (pages.Count > 0)
                            {
                                info.Message = "Finding Pages";
                                OnProcessInfo(info);
                                var pageList = pages[0].GetAttributes("value").ToList();
                                if (pageList.Count > 0)
                                {
                                    strPageList =
                                        pageList[0].DeEntitizeValue.Replace("##", "")
                                        .Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                                    info.Message = "ManuScript Info is Completed. Start Download.";
                                    OnProcessInfo(info);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                info.Message = ex.Message;
                OnProcessInfo(info);
            }

            return new ManuScriptsInfo {  Name= manuscriptsTitle, Pages = strPageList };
        }


        private void CreateDir(string base_dir)
        {
            string path = base_dir + "\\" + _manuscripts;
            System.IO.Directory.CreateDirectory(path);
            System.IO.Directory.CreateDirectory(path + "\\Pages");
            System.IO.Directory.CreateDirectory(path + "\\Temp");

        }


        public void DownoadPages(string[] pages,int res, string base_dir)
        {
            _stop_flag = false;
            int count = 1;
            info.PageNumber = 0;
            foreach (string page in pages)
            {
                if (_stop_flag)
                {
                    info.Message = "Process Stoped.";
                    OnProcessInfo(info);
                    return;
                }
                DownloadPage(page, res, base_dir);
                info.PageNumber = count;
                info.Message = "Page " + count.ToString() + " has been Completed";
                OnProcessInfo(info);
                count++;
            }

        }

        private void DownloadPage(string page,int res,string base_dir)
        {
            try
            {
                int column = 0, row = 0;
                int max_column = 0, max_row = 0;

                bool _flag = true;
                _stop_flag = false;
                imageBlockList.Clear();
                while (_flag)
                {
                    CreateDir(base_dir);
                    string filename = base_dir + "\\" + _manuscripts + "\\Temp\\" + string.Format("{0}_{1}_{2}_{3}.jpg", page, res, row, column);
                    string url = string.Format(Helper.URL_IMAGE_BLOCK, page, res, column, row);

                    var blockRes = DownloadBlock(url, filename);
                    if (_stop_flag)
                    {
                        info.Message = "Process Stoped.";
                        OnProcessInfo(info);
                        return;
                    }
                    switch (blockRes)
                    {
                        case BlockResults.BlockAlreadyDownloaded:
                            max_row = Math.Max(row, max_row);
                            max_column = Math.Max(column, max_column);
                            column += 1;
                            info.Message = "Block (" + column.ToString() + "_" + row.ToString() + ") has been Downloaded recently.";
                            OnProcessInfo(info);
                            imageBlockList.Add(filename);
                            break;

                        case BlockResults.BlockInvalid:
                            if (column == 0)
                            {
                                info.Message = "End of the page," +
                                    string.Format("Page {0} Downloaded.", page);
                                OnProcessInfo(info);
                                _flag = false;
                            }
                            else
                            {
                                column = 0;
                                row += 1;
                                continue;
                            }
                            break;

                        case BlockResults.BlockMaxRetriesReached:
                            info.Message = "BlockMaxRetriesReached";
                            OnProcessInfo(info);
                            break;

                        case BlockResults.None:
                            max_row = Math.Max(row, max_row);
                            max_column = Math.Max(column, max_column);
                            info.Message = "Block (" + column.ToString() + "_" + row.ToString() + ") Downloaded.";
                            column += 1;
                            imageBlockList.Add(filename);
                            OnProcessInfo(info);
                            break;
                    }
                }

                if (!_stop_flag)
                {
                    info.Message = "Start To Merge Page Block(s)";
                    OnProcessInfo(info);
                    ConcatenatePage(base_dir, page, max_column, max_row, res);
                }
            }
            catch(Exception ex)
            {
                info.Message = ex.Message;
                OnProcessInfo(info);
            }

        }

        private BlockResults DownloadBlock(string url, string filename)
        {

            for (int i = 0; i < Helper.MAX_BLOCK_DOWNLOAD_RETRIES; i++)
            {
                if (IsValidImage(filename))
                {
                    return BlockResults.BlockAlreadyDownloaded;
                }

                byte[] block = GetDataFromUrl(url);
                if (IsValidBlock(block))
                    return BlockResults.BlockInvalid;

                File.WriteAllBytes(filename, block);

                if (!IsValidImage(filename))
                {
                    if (i != Helper.MAX_BLOCK_DOWNLOAD_RETRIES - 1)
                    {
                        Random rnd = new Random();
                        int sleep_duration = rnd.Next(1, (int)Math.Pow(2, i));
                        Thread.Sleep(sleep_duration);
                    }
                }
                else
                {
                    return BlockResults.None;
                }

            }
            return BlockResults.BlockMaxRetriesReached;
        }
        private bool IsValidImage(string filename)
        {
            if (File.Exists(filename))
            {
                return IsValidImageFile(filename);
            }
            return false;
        }

        private bool IsValidBlock(byte[] currentBlock)
        {
            if (currentBlock == null)
                return true;
            var str = System.Text.Encoding.UTF8.GetString(currentBlock);
            if (str.ToLower().Contains(Helper.INVALID_BLOCK_MAGIC_SUBSTRING.ToLower()))
                return true;

            return false;
        }

        private byte[] GetDataFromUrl(string url)
        {
            try
            {
                byte[] result = null;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(Helper.USER_AGENT);
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            result = content.ReadAsByteArrayAsync().Result;
                        }

                    }
                }

                return result;
            }
            catch(Exception ex)
            {
             
                info.Message = ex.Message;
                OnProcessInfo(info);
                return null;
            }
        }

        private void ConcatenatePage(string base_dir, string page, int columns, int rows,int res)
        {
           
            int width = 0;
            int height = 0;
            for (int column = 0; column <= columns; column++)
            {
                string tempfilename = base_dir + "\\" + _manuscripts + "\\Temp\\" + string.Format("{0}_{1}_{2}_{3}.jpg", page, res, 0, column);
                Image bmp = Image.FromFile(tempfilename);
                width += bmp.Width;
            }
            for (int row = 0; row <= rows; row++)
            {
                string tempfilename = base_dir + "\\" + _manuscripts + "\\Temp\\" + string.Format("{0}_{1}_{2}_{3}.jpg", page, res, row, 0);
                Image bmp = Image.FromFile(tempfilename);
                height += bmp.Height;
            }
            Bitmap conBmp = new Bitmap(width,height);
            using (Graphics graph = Graphics.FromImage(conBmp))
            {
                graph.Clear(Color.White);
                int h = 0, w = 0;
                Image tmp = null;
                for (int row = 0; row <= rows; row++)
                {
                    if (_stop_flag)
                    {
                        info.Message = "Process Stoped.";
                        OnProcessInfo(info);
                        return;
                    }
                    for (int column = 0; column <= columns; column++)
                    {
                        string tempfilename = base_dir + "\\" + _manuscripts + "\\Temp\\" +
                            string.Format("{0}_{1}_{2}_{3}.jpg", page, res, row, column);
                        tmp = Image.FromFile(tempfilename);
                        graph.DrawImage(tmp, new Point(w, h));
                        w += tmp.Width;

                        if (_stop_flag)
                        {
                            info.Message = "Process Stoped.";
                            OnProcessInfo(info);
                            return;
                        }

                    }
                    w = 0;
                    h += tmp.Height;
                }
            }
            if (_stop_flag)
                return;
            conBmp.Save(base_dir + "\\" + _manuscripts + "\\Pages\\"  + page + ".jpg");

        }




        /// <summary>
        /// https://stackoverflow.com/a/49683945
        /// </summary>
        private bool IsValidImageFile(string file)
        {
            byte[] buffer = new byte[8];
            byte[] bufferEnd = new byte[2];

            var bmp = new byte[] { 0x42, 0x4D };               // BMP "BM"
            var gif87a = new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 };     // "GIF87a"
            var gif89a = new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 };     // "GIF89a"
            var png = new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };   // PNG "\x89PNG\x0D\0xA\0x1A\0x0A"
            var tiffI = new byte[] { 0x49, 0x49, 0x2A, 0x00 }; // TIFF II "II\x2A\x00"
            var tiffM = new byte[] { 0x4D, 0x4D, 0x00, 0x2A }; // TIFF MM "MM\x00\x2A"
            var jpeg = new byte[] { 0xFF, 0xD8, 0xFF };        // JPEG JFIF (SOI "\xFF\xD8" and half next marker xFF)
            var jpegEnd = new byte[] { 0xFF, 0xD9 };           // JPEG EOI "\xFF\xD9"

            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    if (fs.Length > buffer.Length)
                    {
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Position = (int)fs.Length - bufferEnd.Length;
                        fs.Read(bufferEnd, 0, bufferEnd.Length);
                    }

                    fs.Close();
                }

                if (this.ByteArrayStartsWith(buffer, bmp) ||
                    this.ByteArrayStartsWith(buffer, gif87a) ||
                    this.ByteArrayStartsWith(buffer, gif89a) ||
                    this.ByteArrayStartsWith(buffer, png) ||
                    this.ByteArrayStartsWith(buffer, tiffI) ||
                    this.ByteArrayStartsWith(buffer, tiffM))
                {
                    return true;
                }

                if (this.ByteArrayStartsWith(buffer, jpeg))
                {
                    // Offset 0 (Two Bytes): JPEG SOI marker (FFD8 hex)
                    // Offest 1 (Two Bytes): Application segment (FF?? normally ??=E0)
                    // Trailer (Last Two Bytes): EOI marker FFD9 hex
                    if (this.ByteArrayStartsWith(bufferEnd, jpegEnd))
                    {
                        return true;
                    }
                }
            }
            catch 
            {
                return false;
                
            }
            return false;
        }

        private bool ByteArrayStartsWith(byte[] a, byte[] b)
        {
            if (a.Length < b.Length)
            {
                return false;
            }

            for (int i = 0; i < b.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }


    }

}
