using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Models
{
    public class RecordingStartResult
    {
        public bool Success { get; set; }
        public string VideoPath { get; set; }
        public string PreviewPath { get; set; }
        public string MetadataPath { get; set; }
        public string ErrorMessage { get; set; }
        public string FileNameWithoutExtension { get; set; }
    }
}
