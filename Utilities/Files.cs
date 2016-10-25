using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Utilities
{
    public class Files
    {
        #region String & File Methods
        public static FileInfo MoveFile(string sourcepath, string targetdirectory, bool overwrite = false)
        {
            return MoveFile(new FileInfo(sourcepath), new DirectoryInfo(targetdirectory), overwrite);
        }
        public static FileInfo MoveFile(FileInfo sourcefileinfo, DirectoryInfo targetdirectoryinfo, bool overwrite = false)
        {
            string separator = targetdirectoryinfo.FullName.EndsWith("\\") ? "" : "\\";
            string target_file = string.Format(@"{0}{1}{2}", targetdirectoryinfo.Parent.FullName, separator, sourcefileinfo.Name);
            if (File.Exists(target_file) && !overwrite)
            {
                throw new Utilities.Exceptions.FileExistsException(string.Format(@"Target file {0} already exists and overwrite flag is set to false", target_file));
            }
            File.Move(sourcefileinfo.FullName, target_file);
            return new FileInfo(target_file);
        }
        public static string GetFileNameWithoutExtension(string filename)
        {
            return GetFileNameWithoutExtension(new FileInfo(filename));
        }
        public static string GetFileNameWithoutExtension(FileInfo filename)
        {
            return filename.Name.Replace(filename.Extension, "");
        }
        public static List<FileInfo> GetFileInfos(string directory, string pattern, SearchOption searchoption, ushort limit = ushort.MinValue)
        {
            return GetFileInfos(new DirectoryInfo(directory), pattern, searchoption, limit);
        }
        public static List<FileInfo> GetFileInfos(DirectoryInfo directory, string pattern, SearchOption searchoption, ushort limit = ushort.MinValue)
        {
            List<FileInfo> files = directory.GetFiles(pattern, searchoption).ToList<System.IO.FileInfo>();
            if (files.Any() && limit > 0)
            {
                return files.TakeWhile(x => x.Exists).ToList<FileInfo>();
            }
            return files;
        }
        public static List<string> ReadAllFileLines(string filename)
        {
            List<string> lines = new List<string>();
            lines.AddRange(File.ReadAllLines(filename));
            return lines;
        }
        #endregion

        #region JSON Serialization
        /*
            * Requires 
            * 
            * Reference to System.Runtime.Serialization
            * using System.Runtime.Serialization;
            * using System.Runtime.Serialization.Json;
        */
        public static object Serialize(object objecttoserialize, string targetfile)
        {
            return Serialize(objecttoserialize, new FileInfo(targetfile));
        }
        public static object Serialize(object objecttoserialize, FileInfo targetfile)
        {
            DataContractJsonSerializer json_serializer = new DataContractJsonSerializer(objecttoserialize.GetType());
            using (FileStream data_stream = new FileStream(targetfile.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                json_serializer.WriteObject(data_stream, objecttoserialize);
            }
            return objecttoserialize;
        }

        public static object DeSerialize(object objecttodeserialize, string targetfile)
        {
            return DeSerialize(objecttodeserialize, new FileInfo(targetfile));
        }

        public static object DeSerialize(object objecttodeserialize, FileInfo targetfile)
        {
            object result = null;
            if (!File.Exists(targetfile.FullName))
            {
                throw new SerializationException(string.Format(@"Cannot deserialize, {0} file does not exist.", targetfile.FullName));
            }
            DataContractJsonSerializer json_serializer = new DataContractJsonSerializer(objecttodeserialize.GetType());
            using (FileStream data_stream = new FileStream(targetfile.FullName, FileMode.Open, FileAccess.Read))
            {
                result = json_serializer.ReadObject(data_stream);
            }
            return result;
        }
        #endregion

    }
}
