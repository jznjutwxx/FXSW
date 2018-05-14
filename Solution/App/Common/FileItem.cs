using System;
using System.IO;

namespace App.Common
{
    public enum FileItemType
    {
        FileInfo,
        HttpUrl,
        Content,
    }

    //添加图片所需要的对象
    public class FileItem
    {
        private FileItemType _itemType;
        private string _fileName;
        private string _mimeType;
        private byte[] _content;
        private FileInfo _fileInfo;
        private string _url;

        /// <summary>
        /// 基于本地文件的构造器。
        /// </summary>
        /// <param name="fileInfo">本地文件</param>
        public FileItem(FileInfo fileInfo)
        {
            InitFileInfo(fileInfo);
        }

        private void InitFileInfo(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
            {
                throw new ArgumentException("fileInfo is null or not exists!");
            }
            _fileInfo = fileInfo;
            _itemType = FileItemType.FileInfo;
            _fileName = fileInfo.FullName;
        }

        private void InitUrl(string filePath)
        {
            string s = Path.GetFileName(filePath);
            if (s.IndexOf("?") >= 0)
            {
                s = s.Substring(0, s.IndexOf("?"));
            }
            _fileName = s;
            _url = filePath;
            _itemType = FileItemType.HttpUrl;
        }

        public FileItem(FileItemType fileItemType, string filePath) :
            this(fileItemType, filePath, null)
        {
        }

        /// <summary>
        /// 基于路径或则URL的定义方式，必须是全路径。
        /// </summary>
        /// <param name="filePath">本地文件全路径</param>
        public FileItem(FileItemType fileItemType, string filePath, string fileAlias)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath is null or empty!");
            }

            switch (fileItemType)
            {
                case FileItemType.FileInfo:
                    InitFileInfo(new FileInfo(filePath));
                    break;

                case FileItemType.HttpUrl:
                    InitUrl(filePath);
                    break;

                default:
                    throw new ArgumentException("unsupport FileItemType!");
            }

            if (!string.IsNullOrEmpty(fileAlias))
            {
                _fileName = fileAlias;
            }
        }



        /// <summary>
        /// 基于文件名和字节流的构造器。
        /// </summary>
        /// <param name="fileName">文件名称（服务端持久化字节流到磁盘时的文件名）</param>
        /// <param name="content">文件字节流</param>
        public FileItem(string fileName, byte[] content)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
            if (content == null || content.Length == 0) throw new ArgumentNullException("content");

            _fileName = fileName;
            _content = content;
            _itemType = FileItemType.Content;
        }

        /// <summary>
        /// 基于文件名、字节流和媒体类型的构造器。
        /// </summary>
        /// <param name="fileName">文件名（服务端持久化字节流到磁盘时的文件名）</param>
        /// <param name="content">文件字节流</param>
        /// <param name="mimeType">媒体类型</param>
        public FileItem(String fileName, byte[] content, String mimeType)
            : this(fileName, content)
        {
            if (string.IsNullOrEmpty(mimeType)) throw new ArgumentNullException("mimeType");
            _mimeType = mimeType;
        }

        public string GetFileName()
        {
            return _fileName;
        }

        public string GetMimeType()
        {
            if (_mimeType == null)
            {
                _mimeType = GetMimeType(GetContent());
            }
            return _mimeType;
        }

        /// <summary>
        /// 获取文件的真实媒体类型。目前只支持JPG, GIF, PNG, BMP四种图片文件。
        /// </summary>
        /// <param name="fileData">文件字节流</param>
        /// <returns>媒体类型</returns>
        public static string GetMimeType(byte[] fileData)
        {
            string suffix = GetFileSuffix(fileData);
            string mimeType;

            switch (suffix)
            {
                case "JPG": mimeType = "image/jpeg"; break;
                case "GIF": mimeType = "image/gif"; break;
                case "PNG": mimeType = "image/png"; break;
                case "BMP": mimeType = "image/bmp"; break;
                default: mimeType = "application/octet-stream"; break;
            }

            return mimeType;
        }

        /// <summary>
        /// 获取文件的真实后缀名。目前只支持JPG, GIF, PNG, BMP四种图片文件。
        /// </summary>
        /// <param name="fileData">文件字节流</param>
        /// <returns>JPG, GIF, PNG or null</returns>
        public static string GetFileSuffix(byte[] fileData)
        {
            if (fileData == null || fileData.Length < 10)
            {
                return null;
            }

            if (fileData[0] == 'G' && fileData[1] == 'I' && fileData[2] == 'F')
            {
                return "GIF";
            }
            else if (fileData[1] == 'P' && fileData[2] == 'N' && fileData[3] == 'G')
            {
                return "PNG";
            }
            else if (fileData[6] == 'J' && fileData[7] == 'F' && fileData[8] == 'I' && fileData[9] == 'F')
            {
                return "JPG";
            }
            else if (fileData[0] == 'B' && fileData[1] == 'M')
            {
                return "BMP";
            }
            else
            {
                return null;
            }
        }


        public byte[] GetContent()
        {
            if (_content != null && _content.Length > 0)
            {
                return _content;
            }

            switch (_itemType)
            {
                case FileItemType.FileInfo:
                    if (_fileInfo == null || !_fileInfo.Exists)
                    {
                        throw new Exception("fileInfo is null or not exists!");
                    }
                    using (Stream fileStream = _fileInfo.OpenRead())
                    {
                        _content = new byte[fileStream.Length];
                        fileStream.Read(_content, 0, _content.Length);
                    }
                    break;

                case FileItemType.HttpUrl:

                    if (string.IsNullOrEmpty(_url))
                    {
                        throw new Exception("url is empty!");
                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        WebUtils.Default.GetStream(_url, stream);
                        _content = new byte[stream.Length];
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Read(_content, 0, _content.Length);
                    }

                    break;

                case FileItemType.Content:
                    if (_content == null)
                    {
                        throw new Exception("content is null!");
                    }

                    break;

                default:
                    throw new Exception("unsupport FileItemType!");
            }
            return _content;
        }
    }
}
