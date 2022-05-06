using System.IO.Compression;

namespace NotionHashRemoverUI.Helpers
{
    internal static class ZipHelpers
    {
        /// <summary>
        /// Unpacks the Notion ZIP
        /// </summary>
        /// <param name="filePath">filepath to Zip</param>
        /// <returns>success if unpacking was successful and if Directory contains any Files</returns>
        internal static bool ExtractZipDirectory(string filePath)
        {
            try
            {
                using var archive = new ZipArchive(File.Open(filePath, FileMode.Open, FileAccess.ReadWrite), ZipArchiveMode.Update);
                archive.ExtractToDirectory(Constants.EXPORT_DIRECTORY, true);

                if (Directory.GetFiles(Constants.EXPORT_DIRECTORY).Any())
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}
