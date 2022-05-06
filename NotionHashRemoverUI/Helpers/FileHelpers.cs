namespace NotionHashRemoverUI.Helpers
{
    internal static class FileHelpers
    {
        /// <summary>
        /// Renames all Files found in Every Directory to a Name without MD5
        /// </summary>
        private static void RenameFiles()
        {
            foreach (var file in Directory.EnumerateFiles(Constants.EXPORT_DIRECTORY, "*.*", SearchOption.AllDirectories))
            {
                var newFileName = HashHelpers.RemoveMD5InFiles(file);
                File.Move(file, newFileName, true);
                string newFile = HashHelpers.RemoveMD5InFiles(File.ReadAllText(newFileName));
                File.WriteAllText(newFileName, newFile);
            }
        }

        /// <summary>
        /// Recursively renames all Directories found to a Name without MD5
        /// </summary>
        private static void RenameDirectoryRecursively(DirectoryInfo currentFolder)
        {
            if (currentFolder.Exists)
            {
                // Rename subfolders
                DirectoryInfo[] subFolders = currentFolder.GetDirectories();
                if (subFolders != null && subFolders.Length > 0)
                {
                    foreach (DirectoryInfo subFolder in subFolders)
                    {
                        try
                        {
                            Directory.Move(subFolder.FullName, GetNewFolderName(currentFolder, subFolder));
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    // Recursive
                    subFolders = currentFolder.GetDirectories();
                    foreach (DirectoryInfo subFolder in subFolders)
                    {
                        // Renamed subfolder become current folder.
                        RenameDirectoryRecursively(subFolder);
                    }
                }
            }
        }

        private static string GetNewFolderName(DirectoryInfo parent, DirectoryInfo targetFolder)
        {
            try
            {
                return Path.Combine(parent.FullName, HashHelpers.RemoveMD5InDirectories(targetFolder.Name));
            }
            catch
            {
                return targetFolder.FullName;
            }
        }

        internal static void Rename()
        {
            RenameDirectoryRecursively(new DirectoryInfo(Constants.EXPORT_DIRECTORY));
            RenameFiles();
        }
    }
}
