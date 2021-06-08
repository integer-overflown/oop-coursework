using System.Collections.Generic;
using Avalonia.Controls;

namespace CourseWork.Input
{
    public static class FileExtensionFilters
    {
        public static List<FileDialogFilter> Images => new()
        {
            new FileDialogFilter
            {
                Name = "Images",
                Extensions = {"png", "jpg", "jpeg"}
            }
        };
    }
}