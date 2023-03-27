using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritishLibraryDownloader
{
    public static class Helper
    {
        public static string URL_PAGES = "https://www.bl.uk/manuscripts/Viewer.aspx?ref={0}";
        public static string URL_IMAGE_BLOCK = "https://www.bl.uk/manuscripts/Proxy.ashx?view={0}_files/{1}/{2}_{3}.jpg";
        public static string INVALID_BLOCK_MAGIC_SUBSTRING = "An unexpected error occurred";
        public static string URL_PAGE_INFO = "https://www.bl.uk/manuscripts/FullDisplay.aspx?ref={0}";
        public static int MAX_BLOCK_DOWNLOAD_RETRIES = 6;
        public static string USER_AGENT = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
    }

    public enum BlockResults
    {
        BlockAlreadyDownloaded,
        BlockInvalid,
        None,
        BlockMaxRetriesReached
    }


    public class ManuScriptsInfo
    {
        public string[] Pages { get; set; }
        public string Name { get; set; }
    }


    public class ProcessEventArgs : EventArgs
    {
        public bool IsSuccessful { get; set; }
        public DateTime CompletionTime { get; set; }
        public string Message { get; set; }
        public int PageNumber { get; set; }
    }
}
