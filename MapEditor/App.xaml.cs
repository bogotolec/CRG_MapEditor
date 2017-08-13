using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MapEditor
{
    public partial class App : Application
    {
        public static string MapFileName { get; set; }
        public static Map UsedMap;
    }
}
